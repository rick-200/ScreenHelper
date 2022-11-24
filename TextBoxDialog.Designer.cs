namespace ScreenHelper
{
	partial class TextBoxDialog
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
			this.tb_main = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tb_main
			// 
			this.tb_main.Location = new System.Drawing.Point(12, 12);
			this.tb_main.Multiline = true;
			this.tb_main.Name = "tb_main";
			this.tb_main.ReadOnly = true;
			this.tb_main.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tb_main.Size = new System.Drawing.Size(776, 426);
			this.tb_main.TabIndex = 0;
			// 
			// TextBoxDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tb_main);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TextBoxDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TextBoxDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TextBox tb_main;
	}
}