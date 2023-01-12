namespace BACKUP_FACEBOOK
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000237C File Offset: 0x0000057C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000A078 File Offset: 0x00008278
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::BACKUP_FACEBOOK.Form1));
			this.contextMenuStrip2 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.cHỌNToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.tẤTCẢToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.bÔIĐENToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.bỎCHỌNToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.tẤTCẢToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.bÔIĐENToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView2 = new global::System.Windows.Forms.DataGridView();
			this.chon1 = new global::System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.stt1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uid = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pass = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.trangthai1 = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.cHỌNToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.cHỌNTẤTCẢToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.bỎCHỌNTẤTCẢToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.xÓAToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.xÓADÒNGTICKCHỌNToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.xÓADÒNGBÔIĐENToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.tHÊMDỮLIỆUToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.tẠOBẢNGMỚIToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btn_stop = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.button7 = new global::System.Windows.Forms.Button();
			this.button5 = new global::System.Windows.Forms.Button();
			this.rich_ok = new global::System.Windows.Forms.RichTextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.lb_ok = new global::System.Windows.Forms.Label();
			this.lb_loi = new global::System.Windows.Forms.Label();
			this.contextMenuStrip2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.cHỌNToolStripMenuItem1,
				this.bỎCHỌNToolStripMenuItem
			});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			componentResourceManager.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
			this.cHỌNToolStripMenuItem1.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.tẤTCẢToolStripMenuItem,
				this.bÔIĐENToolStripMenuItem
			});
			this.cHỌNToolStripMenuItem1.Name = "cHỌNToolStripMenuItem1";
			componentResourceManager.ApplyResources(this.cHỌNToolStripMenuItem1, "cHỌNToolStripMenuItem1");
			this.tẤTCẢToolStripMenuItem.Name = "tẤTCẢToolStripMenuItem";
			componentResourceManager.ApplyResources(this.tẤTCẢToolStripMenuItem, "tẤTCẢToolStripMenuItem");
			this.bÔIĐENToolStripMenuItem.Name = "bÔIĐENToolStripMenuItem";
			componentResourceManager.ApplyResources(this.bÔIĐENToolStripMenuItem, "bÔIĐENToolStripMenuItem");
			this.bỎCHỌNToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.tẤTCẢToolStripMenuItem1,
				this.bÔIĐENToolStripMenuItem1
			});
			this.bỎCHỌNToolStripMenuItem.Name = "bỎCHỌNToolStripMenuItem";
			componentResourceManager.ApplyResources(this.bỎCHỌNToolStripMenuItem, "bỎCHỌNToolStripMenuItem");
			this.tẤTCẢToolStripMenuItem1.Name = "tẤTCẢToolStripMenuItem1";
			componentResourceManager.ApplyResources(this.tẤTCẢToolStripMenuItem1, "tẤTCẢToolStripMenuItem1");
			this.bÔIĐENToolStripMenuItem1.Name = "bÔIĐENToolStripMenuItem1";
			componentResourceManager.ApplyResources(this.bÔIĐENToolStripMenuItem1, "bÔIĐENToolStripMenuItem1");
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToResizeRows = false;
			componentResourceManager.ApplyResources(this.dataGridView2, "dataGridView2");
			this.dataGridView2.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[]
			{
				this.chon1,
				this.stt1,
				this.uid,
				this.pass,
				this.trangthai1
			});
			this.dataGridView2.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowHeadersVisible = false;
			this.dataGridView2.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView2.CellClick += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
			this.dataGridView2.CellContentClick += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
			this.dataGridView2.CellValueChanged += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
			this.dataGridView2.CellValuePushed += new global::System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView2_CellValuePushed);
			componentResourceManager.ApplyResources(this.chon1, "chon1");
			this.chon1.Name = "chon1";
			componentResourceManager.ApplyResources(this.stt1, "stt1");
			this.stt1.Name = "stt1";
			componentResourceManager.ApplyResources(this.uid, "uid");
			this.uid.Name = "uid";
			componentResourceManager.ApplyResources(this.pass, "pass");
			this.pass.Name = "pass";
			this.trangthai1.AutoSizeMode = global::System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.trangthai1, "trangthai1");
			this.trangthai1.Name = "trangthai1";
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.cHỌNToolStripMenuItem,
				this.cHỌNTẤTCẢToolStripMenuItem,
				this.bỎCHỌNTẤTCẢToolStripMenuItem,
				this.xÓAToolStripMenuItem,
				this.tHÊMDỮLIỆUToolStripMenuItem,
				this.tẠOBẢNGMỚIToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.cHỌNToolStripMenuItem.Name = "cHỌNToolStripMenuItem";
			componentResourceManager.ApplyResources(this.cHỌNToolStripMenuItem, "cHỌNToolStripMenuItem");
			this.cHỌNToolStripMenuItem.Click += new global::System.EventHandler(this.cHỌNToolStripMenuItem_Click);
			this.cHỌNTẤTCẢToolStripMenuItem.Name = "cHỌNTẤTCẢToolStripMenuItem";
			componentResourceManager.ApplyResources(this.cHỌNTẤTCẢToolStripMenuItem, "cHỌNTẤTCẢToolStripMenuItem");
			this.cHỌNTẤTCẢToolStripMenuItem.Click += new global::System.EventHandler(this.cHỌNTẤTCẢToolStripMenuItem_Click);
			this.bỎCHỌNTẤTCẢToolStripMenuItem.Name = "bỎCHỌNTẤTCẢToolStripMenuItem";
			componentResourceManager.ApplyResources(this.bỎCHỌNTẤTCẢToolStripMenuItem, "bỎCHỌNTẤTCẢToolStripMenuItem");
			this.bỎCHỌNTẤTCẢToolStripMenuItem.Click += new global::System.EventHandler(this.bỎCHỌNTẤTCẢToolStripMenuItem_Click);
			this.xÓAToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.xÓADÒNGTICKCHỌNToolStripMenuItem,
				this.xÓADÒNGBÔIĐENToolStripMenuItem
			});
			componentResourceManager.ApplyResources(this.xÓAToolStripMenuItem, "xÓAToolStripMenuItem");
			this.xÓAToolStripMenuItem.ForeColor = global::System.Drawing.Color.FromArgb(192, 0, 0);
			this.xÓAToolStripMenuItem.Name = "xÓAToolStripMenuItem";
			this.xÓADÒNGTICKCHỌNToolStripMenuItem.Name = "xÓADÒNGTICKCHỌNToolStripMenuItem";
			componentResourceManager.ApplyResources(this.xÓADÒNGTICKCHỌNToolStripMenuItem, "xÓADÒNGTICKCHỌNToolStripMenuItem");
			this.xÓADÒNGTICKCHỌNToolStripMenuItem.Click += new global::System.EventHandler(this.xÓADÒNGTICKCHỌNToolStripMenuItem_Click);
			this.xÓADÒNGBÔIĐENToolStripMenuItem.Name = "xÓADÒNGBÔIĐENToolStripMenuItem";
			componentResourceManager.ApplyResources(this.xÓADÒNGBÔIĐENToolStripMenuItem, "xÓADÒNGBÔIĐENToolStripMenuItem");
			this.xÓADÒNGBÔIĐENToolStripMenuItem.Click += new global::System.EventHandler(this.xÓADÒNGBÔIĐENToolStripMenuItem_Click);
			this.tHÊMDỮLIỆUToolStripMenuItem.Name = "tHÊMDỮLIỆUToolStripMenuItem";
			componentResourceManager.ApplyResources(this.tHÊMDỮLIỆUToolStripMenuItem, "tHÊMDỮLIỆUToolStripMenuItem");
			this.tHÊMDỮLIỆUToolStripMenuItem.Click += new global::System.EventHandler(this.tHÊMDỮLIỆUToolStripMenuItem_Click);
			this.tẠOBẢNGMỚIToolStripMenuItem.Name = "tẠOBẢNGMỚIToolStripMenuItem";
			componentResourceManager.ApplyResources(this.tẠOBẢNGMỚIToolStripMenuItem, "tẠOBẢNGMỚIToolStripMenuItem");
			this.tẠOBẢNGMỚIToolStripMenuItem.Click += new global::System.EventHandler(this.tẠOBẢNGMỚIToolStripMenuItem_Click);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.btn_stop, "btn_stop");
			this.btn_stop.BackColor = global::System.Drawing.Color.FromArgb(192, 0, 0);
			this.btn_stop.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.btn_stop.Name = "btn_stop";
			this.btn_stop.UseVisualStyleBackColor = false;
			this.btn_stop.Click += new global::System.EventHandler(this.button9_Click);
			componentResourceManager.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new global::System.EventHandler(this.button2_Click_1);
			componentResourceManager.ApplyResources(this.button7, "button7");
			this.button7.BackColor = global::System.Drawing.Color.Green;
			this.button7.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button7.Name = "button7";
			this.button7.UseVisualStyleBackColor = false;
			this.button7.Click += new global::System.EventHandler(this.button7_Click_1);
			componentResourceManager.ApplyResources(this.button5, "button5");
			this.button5.BackColor = global::System.Drawing.Color.Green;
			this.button5.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button5.Name = "button5";
			this.button5.UseVisualStyleBackColor = false;
			this.button5.Click += new global::System.EventHandler(this.button5_Click_2);
			componentResourceManager.ApplyResources(this.rich_ok, "rich_ok");
			this.rich_ok.Name = "rich_ok";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.lb_ok, "lb_ok");
			this.lb_ok.BackColor = global::System.Drawing.Color.FromArgb(128, 255, 128);
			this.lb_ok.Name = "lb_ok";
			componentResourceManager.ApplyResources(this.lb_loi, "lb_loi");
			this.lb_loi.BackColor = global::System.Drawing.Color.FromArgb(255, 128, 128);
			this.lb_loi.Name = "lb_loi";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.lb_loi);
			base.Controls.Add(this.lb_ok);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.rich_ok);
			base.Controls.Add(this.button5);
			base.Controls.Add(this.button7);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.btn_stop);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.dataGridView2);
			base.Name = "Form1";
			base.Load += new global::System.EventHandler(this.Form1_Load);
			this.contextMenuStrip2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400001E RID: 30
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.DataGridView dataGridView2;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000021 RID: 33
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000022 RID: 34
		private global::System.Windows.Forms.ToolStripMenuItem cHỌNToolStripMenuItem;

		// Token: 0x04000023 RID: 35
		private global::System.Windows.Forms.ToolStripMenuItem bỎCHỌNTẤTCẢToolStripMenuItem;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.ToolStripMenuItem cHỌNTẤTCẢToolStripMenuItem;

		// Token: 0x04000025 RID: 37
		private global::System.Windows.Forms.Button btn_stop;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.ToolStripMenuItem xÓAToolStripMenuItem;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.ToolStripMenuItem xÓADÒNGTICKCHỌNToolStripMenuItem;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.ToolStripMenuItem xÓADÒNGBÔIĐENToolStripMenuItem;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.ToolStripMenuItem tHÊMDỮLIỆUToolStripMenuItem;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.Button button2;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.ToolStripMenuItem tẠOBẢNGMỚIToolStripMenuItem;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip2;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.ToolStripMenuItem cHỌNToolStripMenuItem1;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.ToolStripMenuItem tẤTCẢToolStripMenuItem;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.ToolStripMenuItem bÔIĐENToolStripMenuItem;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.ToolStripMenuItem bỎCHỌNToolStripMenuItem;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.ToolStripMenuItem tẤTCẢToolStripMenuItem1;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.ToolStripMenuItem bÔIĐENToolStripMenuItem1;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.Button button7;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.Button button5;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.DataGridViewCheckBoxColumn chon1;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.DataGridViewTextBoxColumn stt1;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.DataGridViewTextBoxColumn uid;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.DataGridViewTextBoxColumn pass;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.DataGridViewTextBoxColumn trangthai1;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.RichTextBox rich_ok;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.Label lb_ok;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.Label lb_loi;
	}
}
