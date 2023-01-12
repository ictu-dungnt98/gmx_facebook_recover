namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x0200001A RID: 26
	public partial class Capnhatthongtin : global::System.Windows.Forms.Form
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000C864 File Offset: 0x0000AA64
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000C89C File Offset: 0x0000AA9C
		private void InitializeComponent()
		{
			this.comboBox2 = new global::System.Windows.Forms.ComboBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.button12 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE",
				"Ghi Chú"
			});
			this.comboBox2.Location = new global::System.Drawing.Point(108, 26);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new global::System.Drawing.Size(147, 21);
			this.comboBox2.TabIndex = 35;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(26, 29);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(76, 13);
			this.label2.TabIndex = 36;
			this.label2.Text = "Chọn thông tin";
			this.textBox1.Location = new global::System.Drawing.Point(29, 53);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new global::System.Drawing.Size(244, 65);
			this.textBox1.TabIndex = 37;
			this.button12.BackColor = global::System.Drawing.Color.Green;
			this.button12.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button12.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button12.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button12.Location = new global::System.Drawing.Point(77, 124);
			this.button12.Name = "button12";
			this.button12.Size = new global::System.Drawing.Size(148, 37);
			this.button12.TabIndex = 38;
			this.button12.Text = "Cập nhật";
			this.button12.UseVisualStyleBackColor = false;
			this.button12.Click += new global::System.EventHandler(this.button12_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(301, 165);
			base.Controls.Add(this.button12);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.comboBox2);
			base.Name = "Capnhatthongtin";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Cập nhật thông tin";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400007E RID: 126
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400007F RID: 127
		private global::System.Windows.Forms.ComboBox comboBox2;

		// Token: 0x04000080 RID: 128
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000081 RID: 129
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x04000082 RID: 130
		private global::System.Windows.Forms.Button button12;
	}
}
