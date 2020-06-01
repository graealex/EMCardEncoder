using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace EMCardEncoder
{
	public class EMCardEncoder : IDisposable
    {
		public const byte StartByte = 0x02;
		public const byte StopByte = 0x03;

		public enum SendCommand : ushort
		{
			None = 0x0000,
			HELLO = 0x0100,
			READ = 0x01a4,
			WRITE = 0x01a5
		}

		public enum ReceiveCommand : ushort
		{
			None = 0x0000,
			ACK = 0x0100

		}

		public enum WriteProtect : byte
		{
			READ_WRITE = 0x00,
			READ_ONLY = 0xFF
		}

		private SerialPort interfacePort;
		private Stream interfaceStream;
		private bool disposed = false;

		public EMCardEncoder()
		{
		}

		public EMCardEncoder(string port)
		{
			Open(port);
		}

		public void Open(string port)
		{
			if (port == null)
				throw new ArgumentNullException("Port");

			if (interfacePort != null)
			{
				if (interfacePort.IsOpen)
				{
					Close();
				}
			}

			interfacePort = new SerialPort(port, 4800, Parity.None, 8, StopBits.One);
			interfacePort.ReadTimeout = 100;
			interfacePort.WriteTimeout = 100;
			interfacePort.Open();

			interfaceStream = interfacePort.BaseStream;
		}

		public void Close()
		{
			if (interfacePort != null)
			{
				if (interfacePort.IsOpen)
				{
					interfacePort.Close();
				}

				interfacePort = null;
			}

			if (interfaceStream != null)
			{
				interfaceStream.Close();
				interfaceStream = null;
			}
		}

		public void SendHello()
		{
			SendMessage(SendCommand.HELLO, null);
		}

		public void SendRead()
		{
			SendMessage(SendCommand.READ, null);
		}

		public void SendWrite(byte[] data, bool writeProtect)
		{
			byte[] payload = new byte[11];

			payload[5] = writeProtect ? 
				(byte)WriteProtect.READ_ONLY :
				(byte)WriteProtect.READ_WRITE;

			payload[6] = data[0];
			payload[7] = data[1];
			payload[8] = data[2];
			payload[9] = data[3];
			payload[10] = data[4];

			SendMessage(SendCommand.WRITE, payload);
		}

		public void SendMessage(SendCommand command, byte[] payload)
		{
			// Length = Start byte + command + checksum + stop byte + payload length + payload
			byte[] sendBuffer = new byte[6 + (payload?.Length ?? 0)];

			// Start byte 0x02
			sendBuffer[0] = StartByte;

			// Command bytes 0xff ff
			sendBuffer[1] = (byte)(((ushort)command >> 8) & 0xff);
			sendBuffer[2] = (byte)((ushort)command & 0xff);

			// Payload length 0xff
			sendBuffer[3] = (byte)(payload?.Length ?? 0);

			for (byte n = 0; n < (payload?.Length ?? 0); n++)
			{
				sendBuffer[4 + n] ^= payload[n];
			}

			// Calculate checksum 0xff
			sendBuffer[4 + (payload?.Length ?? 0)] = 0x00;
			for (byte i = 0; i < 4 + (payload?.Length ?? 0); i++)
			{
				sendBuffer[4 + (payload?.Length ?? 0)] ^= sendBuffer[i];
			}

			// Stop byte 0x03
			sendBuffer[5 + (payload?.Length ?? 0)] = StopByte;

			// Send the buffered data
			interfacePort.Write(sendBuffer, 0, sendBuffer.Length);
		}

		public byte[] ReadMessage()
		{
			ReceiveCommand message = ReceiveCommand.None;
			int readData1 = 0;
			int readData2 = 0;

			try
			{
				// Expected byte 0x02 (Start byte)
				readData1 = interfacePort.ReadByte();
				if (readData1 != StartByte)
					throw new InvalidDataException(String.Format("Unexpected data {0}", readData1));

				// Expected bytes 0x01 ff (Command bytes)
				readData1 = interfacePort.ReadByte();
				readData2 = interfacePort.ReadByte();
				message = (ReceiveCommand)((readData1 << 8) | (readData2));

				// Payload if applicable
				readData1 = interfacePort.ReadByte();
				byte[] payload = new byte[readData1];
				for (byte i = 0; i < readData1; i++)
				{
					payload[i] = (byte)interfacePort.ReadByte();
				}

				// Checksum
				readData1 = interfacePort.ReadByte();

				// Check values
				readData1 ^= StartByte;
				readData1 ^= (ushort)message >> 8;
				readData1 ^= (ushort)message & 0xff;
				readData1 ^= payload.Length;

				for (byte n = 0; n < payload.Length; n++)
				{
					readData1 ^= payload[n];
				}

				if (readData1 != 0x00)
					throw new InvalidDataException("Unexpected checksum");

				// Expected byte 0x03 (Stop byte)
				readData1 = interfacePort.ReadByte();
				if (readData1 != StopByte)
					throw new InvalidDataException(String.Format("Unexpected data {0}", readData1));

				return payload;
			}
			catch (TimeoutException)
			{
				return null;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				Close();
			}

			disposed = true;
		}
	}
}
