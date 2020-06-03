namespace EMCardEncoder.WinForms
{
	partial class EMCardReaderForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMCardReaderForm));
			this.maskedTextBoxRead = new System.Windows.Forms.MaskedTextBox();
			this.buttonRead = new System.Windows.Forms.Button();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.checkBoxPoll = new System.Windows.Forms.CheckBox();
			this.groupBoxRead = new System.Windows.Forms.GroupBox();
			this.buttonCopy = new System.Windows.Forms.Button();
			this.labelData1 = new System.Windows.Forms.Label();
			this.checkBoxWriteProtect = new System.Windows.Forms.CheckBox();
			this.groupBoxWrite = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.maskedTextBoxWrite = new System.Windows.Forms.MaskedTextBox();
			this.comboBoxPortNames = new System.Windows.Forms.ComboBox();
			this.labelPort = new System.Windows.Forms.Label();
			this.checkBoxWait = new System.Windows.Forms.CheckBox();
			this.timerRead = new System.Windows.Forms.Timer(this.components);
			this.timerPoll = new System.Windows.Forms.Timer(this.components);
			this.timerWrite = new System.Windows.Forms.Timer(this.components);
			this.groupBoxRead.SuspendLayout();
			this.groupBoxWrite.SuspendLayout();
			this.SuspendLayout();
			// 
			// maskedTextBoxRead
			// 
			this.maskedTextBoxRead.AsciiOnly = true;
			this.maskedTextBoxRead.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.maskedTextBoxRead.Location = new System.Drawing.Point(144, 42);
			this.maskedTextBoxRead.Mask = ">AA AA AA AA AA";
			this.maskedTextBoxRead.Name = "maskedTextBoxRead";
			this.maskedTextBoxRead.Size = new System.Drawing.Size(128, 22);
			this.maskedTextBoxRead.TabIndex = 1;
			// 
			// buttonRead
			// 
			this.buttonRead.Location = new System.Drawing.Point(17, 19);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(100, 50);
			this.buttonRead.TabIndex = 2;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(17, 30);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(100, 50);
			this.buttonWrite.TabIndex = 3;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// checkBoxPoll
			// 
			this.checkBoxPoll.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxPoll.AutoSize = true;
			this.checkBoxPoll.Location = new System.Drawing.Point(17, 75);
			this.checkBoxPoll.MinimumSize = new System.Drawing.Size(100, 50);
			this.checkBoxPoll.Name = "checkBoxPoll";
			this.checkBoxPoll.Size = new System.Drawing.Size(100, 50);
			this.checkBoxPoll.TabIndex = 4;
			this.checkBoxPoll.Text = "Poll";
			this.checkBoxPoll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxPoll.UseVisualStyleBackColor = true;
			this.checkBoxPoll.CheckedChanged += new System.EventHandler(this.checkBoxPoll_CheckedChanged);
			// 
			// groupBoxRead
			// 
			this.groupBoxRead.Controls.Add(this.buttonCopy);
			this.groupBoxRead.Controls.Add(this.labelData1);
			this.groupBoxRead.Controls.Add(this.buttonRead);
			this.groupBoxRead.Controls.Add(this.checkBoxPoll);
			this.groupBoxRead.Controls.Add(this.maskedTextBoxRead);
			this.groupBoxRead.Location = new System.Drawing.Point(12, 56);
			this.groupBoxRead.Name = "groupBoxRead";
			this.groupBoxRead.Size = new System.Drawing.Size(303, 149);
			this.groupBoxRead.TabIndex = 5;
			this.groupBoxRead.TabStop = false;
			this.groupBoxRead.Text = "Read";
			// 
			// buttonCopy
			// 
			this.buttonCopy.Location = new System.Drawing.Point(147, 102);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(79, 23);
			this.buttonCopy.TabIndex = 6;
			this.buttonCopy.Text = "Copy ↓";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// labelData1
			// 
			this.labelData1.AutoSize = true;
			this.labelData1.Location = new System.Drawing.Point(144, 22);
			this.labelData1.Name = "labelData1";
			this.labelData1.Size = new System.Drawing.Size(30, 13);
			this.labelData1.TabIndex = 5;
			this.labelData1.Text = "Data";
			// 
			// checkBoxWriteProtect
			// 
			this.checkBoxWriteProtect.AutoSize = true;
			this.checkBoxWriteProtect.Location = new System.Drawing.Point(17, 86);
			this.checkBoxWriteProtect.Name = "checkBoxWriteProtect";
			this.checkBoxWriteProtect.Size = new System.Drawing.Size(88, 17);
			this.checkBoxWriteProtect.TabIndex = 6;
			this.checkBoxWriteProtect.Text = "Write Protect";
			this.checkBoxWriteProtect.UseVisualStyleBackColor = true;
			this.checkBoxWriteProtect.CheckedChanged += new System.EventHandler(this.checkBoxWriteProtect_CheckedChanged);
			// 
			// groupBoxWrite
			// 
			this.groupBoxWrite.Controls.Add(this.label1);
			this.groupBoxWrite.Controls.Add(this.buttonWrite);
			this.groupBoxWrite.Controls.Add(this.maskedTextBoxWrite);
			this.groupBoxWrite.Controls.Add(this.checkBoxWriteProtect);
			this.groupBoxWrite.Location = new System.Drawing.Point(12, 226);
			this.groupBoxWrite.Name = "groupBoxWrite";
			this.groupBoxWrite.Size = new System.Drawing.Size(303, 119);
			this.groupBoxWrite.TabIndex = 7;
			this.groupBoxWrite.TabStop = false;
			this.groupBoxWrite.Text = "Write";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(144, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Data";
			// 
			// maskedTextBoxWrite
			// 
			this.maskedTextBoxWrite.AsciiOnly = true;
			this.maskedTextBoxWrite.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.maskedTextBoxWrite.Location = new System.Drawing.Point(144, 49);
			this.maskedTextBoxWrite.Mask = ">AA AA AA AA AA";
			this.maskedTextBoxWrite.Name = "maskedTextBoxWrite";
			this.maskedTextBoxWrite.Size = new System.Drawing.Size(128, 22);
			this.maskedTextBoxWrite.TabIndex = 6;
			// 
			// comboBoxPortNames
			// 
			this.comboBoxPortNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPortNames.FormattingEnabled = true;
			this.comboBoxPortNames.Location = new System.Drawing.Point(57, 16);
			this.comboBoxPortNames.Name = "comboBoxPortNames";
			this.comboBoxPortNames.Size = new System.Drawing.Size(110, 21);
			this.comboBoxPortNames.TabIndex = 8;
			this.comboBoxPortNames.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortNames_SelectedIndexChanged);
			// 
			// labelPort
			// 
			this.labelPort.AutoSize = true;
			this.labelPort.Location = new System.Drawing.Point(13, 20);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(26, 13);
			this.labelPort.TabIndex = 9;
			this.labelPort.Text = "Port";
			// 
			// checkBoxWait
			// 
			this.checkBoxWait.AutoSize = true;
			this.checkBoxWait.Location = new System.Drawing.Point(199, 19);
			this.checkBoxWait.Name = "checkBoxWait";
			this.checkBoxWait.Size = new System.Drawing.Size(48, 17);
			this.checkBoxWait.TabIndex = 10;
			this.checkBoxWait.Text = "Wait";
			this.checkBoxWait.UseVisualStyleBackColor = true;
			this.checkBoxWait.CheckedChanged += new System.EventHandler(this.checkBoxWait_CheckedChanged);
			// 
			// timerRead
			// 
			this.timerRead.Tick += new System.EventHandler(this.timerRead_Tick);
			// 
			// timerPoll
			// 
			this.timerPoll.Tick += new System.EventHandler(this.timerPoll_Tick);
			// 
			// timerWrite
			// 
			this.timerWrite.Interval = 250;
			this.timerWrite.Tick += new System.EventHandler(this.timerWrite_Tick);
			// 
			// EMCardReaderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 356);
			this.Controls.Add(this.checkBoxWait);
			this.Controls.Add(this.labelPort);
			this.Controls.Add(this.comboBoxPortNames);
			this.Controls.Add(this.groupBoxWrite);
			this.Controls.Add(this.groupBoxRead);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EMCardReaderForm";
			this.Text = "EM Card Encoder";
			this.Load += new System.EventHandler(this.EMCardReaderForm_Load);
			this.groupBoxRead.ResumeLayout(false);
			this.groupBoxRead.PerformLayout();
			this.groupBoxWrite.ResumeLayout(false);
			this.groupBoxWrite.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MaskedTextBox maskedTextBoxRead;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.CheckBox checkBoxPoll;
		private System.Windows.Forms.GroupBox groupBoxRead;
		private System.Windows.Forms.CheckBox checkBoxWriteProtect;
		private System.Windows.Forms.Label labelData1;
		private System.Windows.Forms.GroupBox groupBoxWrite;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxWrite;
		private System.Windows.Forms.Button buttonCopy;
		private System.Windows.Forms.ComboBox comboBoxPortNames;
		private System.Windows.Forms.Label labelPort;
		private System.Windows.Forms.CheckBox checkBoxWait;
		private System.Windows.Forms.Timer timerRead;
		private System.Windows.Forms.Timer timerPoll;
		private System.Windows.Forms.Timer timerWrite;
	}
}

