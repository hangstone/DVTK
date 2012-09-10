// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright � 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dvt
{
	/// <summary>
	/// Summary description for TimerMessageForm.
	/// </summary>
	public class TimerMessageForm : System.Windows.Forms.Form
	{
		System.Windows.Forms.Timer _Timer = null;

		private System.Windows.Forms.TextBox TextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TimerMessageForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.TextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// TextBox
			// 
			this.TextBox.Enabled = false;
			this.TextBox.Location = new System.Drawing.Point(20, 21);
			this.TextBox.Name = "TextBox";
			this.TextBox.ReadOnly = true;
			this.TextBox.Size = new System.Drawing.Size(328, 20);
			this.TextBox.TabIndex = 0;
			this.TextBox.Text = "";
			this.TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// TimerMessageForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 62);
			this.ControlBox = false;
			this.Controls.Add(this.TextBox);
			this.Name = "TimerMessageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = " ";
			this.ResumeLayout(false);

		}
		#endregion

		public void ShowDialogSpecifiedTime(string theText, int theNumberOfMiliSeconds)
		{
			TextBox.Text = theText;

			_Timer = new System.Windows.Forms.Timer();
			_Timer.Tick += new EventHandler(this.CloseFormBecauseOfTimer);
 
			// Sets the timer interval to 5 seconds.
			_Timer.Interval = theNumberOfMiliSeconds;
			_Timer.Start();

			ShowDialog();
		}

		private void CloseFormBecauseOfTimer(Object myObject, EventArgs myEventArgs)
		{
			Close();
		}

		private void TextBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
