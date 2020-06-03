using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.CommandLine;
using System.CommandLine.Invocation;
using EMCardEncoder;

namespace EMCardEncoder.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var rootCommand = new RootCommand("EMCardEncoder");

			var hello = new Command("hello") { new Argument<string>("port") };
			hello.Handler = CommandHandler.Create((string port) => { Hello(port); });
			rootCommand.AddCommand(hello);

			var read = new Command("read") {
				new Argument<string>("port"),
				new Option<bool>("--wait")
			};
			read.Handler = CommandHandler.Create((string port, bool wait) => { Read(port, wait); });
			rootCommand.AddCommand(read);

			var poll = new Command("poll") {
				new Argument<string>("port")
			};
			poll.Handler = CommandHandler.Create((string port) => { Poll(port); });
			rootCommand.AddCommand(poll);

			var write = new Command("write") {
				new Argument<string>("port"),
				new Argument<string>("data"),
				new Option<bool>("--writeProtect"),
				new Option<bool>("--wait")
			};

			write.Handler = CommandHandler.Create((string port, string data, bool wait, bool writeProtect) =>
			{ Write(port, data, wait, writeProtect); });

			rootCommand.AddCommand(write);
			rootCommand.InvokeAsync(args).Wait();
		}

		
		public static void Hello(string port)
		{
			using (var emCardEncoder = new EMCardEncoder())
			{
				emCardEncoder.Open(port);
				emCardEncoder.SendHello();
				Thread.Sleep(50);
				byte[] ack = emCardEncoder.ReadMessage();
				if ((ack != null) && (ack.Length > 0))
					System.Console.WriteLine(EMCardEncoder.FormatBytes(ack));
				else if ((ack != null) && (ack.Length == 0))
					System.Console.WriteLine("ACK");
			}
		}

		public static void Read(string port, bool wait)
		{
			using (var emCardEncoder = new EMCardEncoder())
			{
				emCardEncoder.Open(port);

				byte[] ack = null;
				do
				{
					emCardEncoder.SendRead();
					Thread.Sleep(50);
					ack = emCardEncoder.ReadMessage();
				}
				while ((ack == null) && wait);

				if ((ack != null) && (ack.Length > 0))
					System.Console.WriteLine(EMCardEncoder.FormatBytes(ack));
				else if ((ack != null) && (ack.Length == 0))
					System.Console.WriteLine("ACK");
			}
		}

		public static void Poll(string port)
		{
			using (var emCardEncoder = new EMCardEncoder())
			{
				emCardEncoder.Open(port);
				while (true)
				{
					emCardEncoder.SendRead();
					Thread.Sleep(50);
					byte[] ack = emCardEncoder.ReadMessage();
					if ((ack != null) && (ack.Length > 0))
						System.Console.WriteLine(EMCardEncoder.FormatBytes(ack));
				}
			}
		}

		public static void Write(string port, string data, bool wait, bool writeProtect)
		{
			byte[] dataBytes = EMCardEncoder.ReadBytes(data);
			if (dataBytes == null)
				throw new ArgumentNullException("data");
			if (dataBytes.Length == 0)
				throw new ArgumentException("Data must be provided", "data");
			if (dataBytes.Length != 5)
				throw new ArgumentException("Provided data must be 5 bytes", "data");

			using (var emCardEncoder = new EMCardEncoder())
			{
				emCardEncoder.Open(port);

				byte[] ack = null;
				do
				{
					emCardEncoder.SendWrite(dataBytes, writeProtect);
					Thread.Sleep(250);
					ack = emCardEncoder.ReadMessage();
					Thread.Sleep(50);
					emCardEncoder.SendRead();
					Thread.Sleep(50);
					ack = emCardEncoder.ReadMessage();
				}
				while ((!ByteArrayCompare(ack, dataBytes)) && wait);

				if ((ack != null) && (ack.Length > 0))
					System.Console.WriteLine(EMCardEncoder.FormatBytes(ack));
				else if ((ack != null) && (ack.Length == 0))
					System.Console.WriteLine("ACK");
			}
		}

		public static bool ByteArrayCompare(byte[] a1, byte[] a2)
		{
			if ((a1 == null) && (a2 == null)) return true;
			if (a1 == null) return false;
			if (a2 == null) return false;

			return a1.SequenceEqual(a2);
		}
	}
}
