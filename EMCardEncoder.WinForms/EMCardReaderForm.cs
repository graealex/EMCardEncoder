using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMCardEncoder;
using System.IO.Ports;

namespace EMCardEncoder.WinForms
{
	public partial class EMCardReaderForm : Form
	{
		public EMCardReaderForm()
		{
			InitializeComponent();
			TogglePortCorrect();
		}

		private void TogglePortCorrect()
		{
			buttonRead.Enabled = (port != null);
			checkBoxPoll.Checked = false;
			checkBoxPoll.Enabled = (port != null);
			buttonWrite.Enabled = (port != null);
		}

		private string port = null;
		private bool wait = false;
		private bool writeProtect = false;
		private EMCardEncoder emCardEncoder;

		private void checkBoxWriteProtect_CheckedChanged(object sender, EventArgs e)
		{
			writeProtect = checkBoxWriteProtect.Checked;
		}

		private void buttonRead_Click(object sender, EventArgs e)
		{
			if (wait)
			{
				timerRead.Enabled = true;
				return;
			}

			if (emCardEncoder != null)
			{
				byte[] ack = null;
				do
				{
					emCardEncoder.SendRead();
					Thread.Sleep(50);
					ack = emCardEncoder.ReadMessage();
				}
				while ((ack == null) && wait);

				if ((ack != null) && (ack.Length > 0))
					maskedTextBoxRead.Text = (EMCardEncoder.FormatBytes(ack));
			}
		}

		private void checkBoxPoll_CheckedChanged(object sender, EventArgs e)
		{
			timerPoll.Enabled = checkBoxPoll.Checked;
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			maskedTextBoxWrite.Text = maskedTextBoxRead.Text;
		}

		private void buttonWrite_Click(object sender, EventArgs e)
		{
			byte[] dataBytes = EMCardEncoder.ReadBytes(maskedTextBoxWrite.Text);
			if (dataBytes == null)
				throw new ArgumentNullException("data");
			if (dataBytes.Length == 0)
				throw new ArgumentException("Data must be provided", "data");
			if (dataBytes.Length != 5)
				throw new ArgumentException("Provided data must be 5 bytes", "data");

			if (emCardEncoder != null)
			{
				byte[] ack = null;
				do
				{
					emCardEncoder.SendWrite(dataBytes, writeProtect);
					Thread.Sleep(250);
					ack = emCardEncoder.ReadMessage();

					emCardEncoder.SendRead();
					Thread.Sleep(50);
					ack = emCardEncoder.ReadMessage();
				}
				while ((!ByteArrayCompare(ack, dataBytes)) && wait);


				if ((ack != null) && (ack.Length > 0))
					maskedTextBoxRead.Text = (EMCardEncoder.FormatBytes(ack));
			}
		}

		private void EMCardReaderForm_Load(object sender, EventArgs e)
		{
			string[] portNames = SerialPort.GetPortNames();
			comboBoxPortNames.Items.Clear();

			foreach (var portName in portNames)
			{
				comboBoxPortNames.Items.Add(portName);
			}
		}

		private void comboBoxPortNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			port = null;
			if (emCardEncoder != null)
			{
				emCardEncoder.Close();
				emCardEncoder.Dispose();
				emCardEncoder = null;
			}
			string selectedPort = comboBoxPortNames.SelectedItem as string;

			try
			{
				emCardEncoder = new EMCardEncoder();

				emCardEncoder.Open(selectedPort);
				emCardEncoder.SendHello();
				Thread.Sleep(50);
				byte[] ack = emCardEncoder.ReadMessage();
				if ((ack != null) && (ack.Length == 0))
				{
					port = selectedPort;
				}
				else
				{
					port = null;
					emCardEncoder.Close();
					emCardEncoder.Dispose();
					emCardEncoder = null;
				}
			}
			catch (Exception)
			{

			}

			TogglePortCorrect();
		}

		private void checkBoxWait_CheckedChanged(object sender, EventArgs e)
		{
			wait = checkBoxWait.Checked;

			if (!wait)
			{
				timerRead.Enabled = false;
				timerWrite.Enabled = false;
			}
		}

		private static bool ByteArrayCompare(byte[] a1, byte[] a2)
		{
			if ((a1 == null) && (a2 == null)) return true;
			if (a1 == null) return false;
			if (a2 == null) return false;

			return a1.SequenceEqual(a2);
		}

		private void timerRead_Tick(object sender, EventArgs e)
		{
			// Check if there is a message pending
			byte[] ack = emCardEncoder.ReadMessage();
			if ((ack != null) && (ack.Length > 0))
			{
				maskedTextBoxRead.Text = (EMCardEncoder.FormatBytes(ack));
				timerRead.Enabled = false;
			}
			else
			{
				emCardEncoder.SendRead();
			}
		}

		private void timerPoll_Tick(object sender, EventArgs e)
		{
			// Check if there is a message pending
			byte[] ack = emCardEncoder.ReadMessage();
			if ((ack != null) && (ack.Length > 0))
			{
				maskedTextBoxRead.Text = (EMCardEncoder.FormatBytes(ack));
			}
			else
			{
				emCardEncoder.SendRead();
			}
		}

		private void timerWrite_Tick(object sender, EventArgs e)
		{
			byte[] dataBytes = EMCardEncoder.ReadBytes(maskedTextBoxWrite.Text);
			if (dataBytes == null)
				throw new ArgumentNullException("data");
			if (dataBytes.Length == 0)
				throw new ArgumentException("Data must be provided", "data");
			if (dataBytes.Length != 5)
				throw new ArgumentException("Provided data must be 5 bytes", "data");

			// Check if there is a message pending
			byte[] ack = emCardEncoder.ReadMessage();
			if (ByteArrayCompare(ack, dataBytes))
			{
				maskedTextBoxRead.Text = (EMCardEncoder.FormatBytes(ack));
				timerRead.Enabled = false;
			}
			else
			{
				emCardEncoder.SendWrite(dataBytes, writeProtect);
				Thread.Sleep(250);
				ack = emCardEncoder.ReadMessage();

				emCardEncoder.SendRead();
				Thread.Sleep(50);
				ack = emCardEncoder.ReadMessage();
			}
		}
	}
}
