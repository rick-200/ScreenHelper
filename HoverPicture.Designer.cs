namespace ScreenHelper
{
	partial class HoverPicture
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
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmi_copy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_save = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_ocr = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_barcode = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_edit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_topmost = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_copy,
            this.tsmi_save,
            this.tsmi_ocr,
            this.tsmi_barcode,
            this.tsmi_edit,
            this.tsmi_topmost});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(211, 176);
			// 
			// tsmi_copy
			// 
			this.tsmi_copy.Name = "tsmi_copy";
			this.tsmi_copy.Size = new System.Drawing.Size(210, 24);
			this.tsmi_copy.Text = "复制";
			this.tsmi_copy.Click += new System.EventHandler(this.tsmi_copy_Click);
			// 
			// tsmi_save
			// 
			this.tsmi_save.Name = "tsmi_save";
			this.tsmi_save.Size = new System.Drawing.Size(210, 24);
			this.tsmi_save.Text = "保存";
			this.tsmi_save.Click += new System.EventHandler(this.tsmi_save_Click);
			// 
			// tsmi_ocr
			// 
			this.tsmi_ocr.Name = "tsmi_ocr";
			this.tsmi_ocr.Size = new System.Drawing.Size(210, 24);
			this.tsmi_ocr.Text = "文字识别";
			this.tsmi_ocr.Click += new System.EventHandler(this.tsmi_ocr_Click);
			// 
			// tsmi_barcode
			// 
			this.tsmi_barcode.Name = "tsmi_barcode";
			this.tsmi_barcode.Size = new System.Drawing.Size(210, 24);
			this.tsmi_barcode.Text = "二维码识别";
			this.tsmi_barcode.Click += new System.EventHandler(this.tsmi_barcode_Click);
			// 
			// tsmi_edit
			// 
			this.tsmi_edit.Name = "tsmi_edit";
			this.tsmi_edit.Size = new System.Drawing.Size(210, 24);
			this.tsmi_edit.Text = "编辑";
			this.tsmi_edit.Click += new System.EventHandler(this.tsmi_edit_Click);
			// 
			// tsmi_topmost
			// 
			this.tsmi_topmost.Name = "tsmi_topmost";
			this.tsmi_topmost.Size = new System.Drawing.Size(210, 24);
			this.tsmi_topmost.Text = "置顶";
			this.tsmi_topmost.Click += new System.EventHandler(this.tsmi_topmost_Click);
			// 
			// HoverPicture
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.ControlBox = false;
			this.Name = "HoverPicture";
			this.TopMost = true;
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem tsmi_copy;
		private ToolStripMenuItem tsmi_ocr;
		private ToolStripMenuItem tsmi_barcode;
		private ToolStripMenuItem tsmi_edit;
		private ToolStripMenuItem tsmi_save;
		private ToolStripMenuItem tsmi_topmost;
	}
}