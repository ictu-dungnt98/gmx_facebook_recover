namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x0200001B RID: 27
	public partial class ChuyenDM : global::System.Windows.Forms.Form
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000CD28 File Offset: 0x0000AF28
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000CD60 File Offset: 0x0000AF60
		private void InitializeComponent()
		{
			this.button9 = new global::System.Windows.Forms.Button();
			this.comboBox1 = new global::System.Windows.Forms.ComboBox();
			this.label3 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.button9.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.button9.BackColor = global::System.Drawing.Color.FromArgb(0, 192, 192);
			this.button9.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button9.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button9.Location = new global::System.Drawing.Point(116, 52);
			this.button9.Name = "button9";
			this.button9.Size = new global::System.Drawing.Size(115, 29);
			this.button9.TabIndex = 20;
			this.button9.Text = "CHUYỂN";
			this.button9.UseVisualStyleBackColor = false;
			this.button9.Click += new global::System.EventHandler(this.button9_Click);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new global::System.Drawing.Point(116, 18);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new global::System.Drawing.Size(161, 21);
			this.comboBox1.TabIndex = 19;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(26, 21);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(84, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Chọn Danh mục";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(324, 102);
			base.Controls.Add(this.button9);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.label3);
			base.Name = "ChuyenDM";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Chuyển Danh Mục";
			base.Load += new global::System.EventHandler(this.ChuyenDM_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000084 RID: 132
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000085 RID: 133
		private global::System.Windows.Forms.Button button9;

		// Token: 0x04000086 RID: 134
		private global::System.Windows.Forms.ComboBox comboBox1;

		// Token: 0x04000087 RID: 135
		private global::System.Windows.Forms.Label label3;
	}
}
