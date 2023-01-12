namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x02000021 RID: 33
	public partial class DeleteDM : global::System.Windows.Forms.Form
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000E10C File Offset: 0x0000C30C
		private void InitializeComponent()
		{
			this.comboBox1 = new global::System.Windows.Forms.ComboBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.button9 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new global::System.Drawing.Point(109, 27);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new global::System.Drawing.Size(161, 21);
			this.comboBox1.TabIndex = 16;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(19, 30);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(84, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Chọn Danh mục";
			this.button9.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.button9.BackColor = global::System.Drawing.Color.FromArgb(192, 0, 0);
			this.button9.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button9.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button9.Location = new global::System.Drawing.Point(109, 63);
			this.button9.Name = "button9";
			this.button9.Size = new global::System.Drawing.Size(115, 29);
			this.button9.TabIndex = 17;
			this.button9.Text = "XÓA";
			this.button9.UseVisualStyleBackColor = false;
			this.button9.Click += new global::System.EventHandler(this.button9_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(298, 104);
			base.Controls.Add(this.button9);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.label3);
			this.MaximumSize = new global::System.Drawing.Size(314, 143);
			this.MinimumSize = new global::System.Drawing.Size(314, 143);
			base.Name = "DeleteDM";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xóa Danh mục";
			base.Load += new global::System.EventHandler(this.DeleteDM_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400009D RID: 157
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400009E RID: 158
		private global::System.Windows.Forms.ComboBox comboBox1;

		// Token: 0x0400009F RID: 159
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000A0 RID: 160
		private global::System.Windows.Forms.Button button9;
	}
}
