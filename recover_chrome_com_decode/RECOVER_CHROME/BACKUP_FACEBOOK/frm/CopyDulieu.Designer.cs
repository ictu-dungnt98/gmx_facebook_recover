namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x0200001D RID: 29
	public partial class CopyDulieu : global::System.Windows.Forms.Form
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000D408 File Offset: 0x0000B608
		private void InitializeComponent()
		{
			this.comboBox8 = new global::System.Windows.Forms.ComboBox();
			this.comboBox9 = new global::System.Windows.Forms.ComboBox();
			this.comboBox5 = new global::System.Windows.Forms.ComboBox();
			this.comboBox6 = new global::System.Windows.Forms.ComboBox();
			this.comboBox7 = new global::System.Windows.Forms.ComboBox();
			this.comboBox4 = new global::System.Windows.Forms.ComboBox();
			this.comboBox3 = new global::System.Windows.Forms.ComboBox();
			this.comboBox2 = new global::System.Windows.Forms.ComboBox();
			this.txt_input = new global::System.Windows.Forms.TextBox();
			this.button12 = new global::System.Windows.Forms.Button();
			this.button1 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.comboBox8.FormattingEnabled = true;
			this.comboBox8.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox8.Location = new global::System.Drawing.Point(689, 43);
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox8.TabIndex = 41;
			this.comboBox8.SelectedIndexChanged += new global::System.EventHandler(this.comboBox8_SelectedIndexChanged);
			this.comboBox9.FormattingEnabled = true;
			this.comboBox9.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox9.Location = new global::System.Drawing.Point(802, 43);
			this.comboBox9.Name = "comboBox9";
			this.comboBox9.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox9.TabIndex = 40;
			this.comboBox9.SelectedIndexChanged += new global::System.EventHandler(this.comboBox9_SelectedIndexChanged);
			this.comboBox5.FormattingEnabled = true;
			this.comboBox5.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox5.Location = new global::System.Drawing.Point(351, 43);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox5.TabIndex = 39;
			this.comboBox5.SelectedIndexChanged += new global::System.EventHandler(this.comboBox5_SelectedIndexChanged);
			this.comboBox6.FormattingEnabled = true;
			this.comboBox6.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox6.Location = new global::System.Drawing.Point(462, 43);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox6.TabIndex = 38;
			this.comboBox6.SelectedIndexChanged += new global::System.EventHandler(this.comboBox6_SelectedIndexChanged);
			this.comboBox7.FormattingEnabled = true;
			this.comboBox7.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox7.Location = new global::System.Drawing.Point(576, 43);
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox7.TabIndex = 37;
			this.comboBox7.SelectedIndexChanged += new global::System.EventHandler(this.comboBox7_SelectedIndexChanged);
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox4.Location = new global::System.Drawing.Point(238, 43);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox4.TabIndex = 36;
			this.comboBox4.SelectedIndexChanged += new global::System.EventHandler(this.comboBox4_SelectedIndexChanged);
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox3.Location = new global::System.Drawing.Point(125, 43);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox3.TabIndex = 35;
			this.comboBox3.SelectedIndexChanged += new global::System.EventHandler(this.comboBox3_SelectedIndexChanged);
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[]
			{
				"",
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox2.Location = new global::System.Drawing.Point(12, 43);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox2.TabIndex = 34;
			this.comboBox2.SelectedIndexChanged += new global::System.EventHandler(this.comboBox2_SelectedIndexChanged);
			this.txt_input.Location = new global::System.Drawing.Point(12, 81);
			this.txt_input.MaxLength = 999999999;
			this.txt_input.Multiline = true;
			this.txt_input.Name = "txt_input";
			this.txt_input.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txt_input.Size = new global::System.Drawing.Size(897, 342);
			this.txt_input.TabIndex = 33;
			this.txt_input.WordWrap = false;
			this.button12.BackColor = global::System.Drawing.Color.Green;
			this.button12.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button12.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button12.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button12.Location = new global::System.Drawing.Point(707, 433);
			this.button12.Name = "button12";
			this.button12.Size = new global::System.Drawing.Size(202, 37);
			this.button12.TabIndex = 42;
			this.button12.Text = "COPY";
			this.button12.UseVisualStyleBackColor = false;
			this.button12.Click += new global::System.EventHandler(this.button12_Click);
			this.button1.BackColor = global::System.Drawing.Color.Green;
			this.button1.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button1.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button1.Location = new global::System.Drawing.Point(12, 429);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(107, 24);
			this.button1.TabIndex = 43;
			this.button1.Text = "Reset Định dạng";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(922, 487);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button12);
			base.Controls.Add(this.comboBox8);
			base.Controls.Add(this.comboBox9);
			base.Controls.Add(this.comboBox5);
			base.Controls.Add(this.comboBox6);
			base.Controls.Add(this.comboBox7);
			base.Controls.Add(this.comboBox4);
			base.Controls.Add(this.comboBox3);
			base.Controls.Add(this.comboBox2);
			base.Controls.Add(this.txt_input);
			base.Name = "CopyDulieu";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Copy Dữ Liệu";
			base.Load += new global::System.EventHandler(this.CopyDulieu_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400008B RID: 139
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.ComboBox comboBox8;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.ComboBox comboBox9;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.ComboBox comboBox5;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.ComboBox comboBox6;

		// Token: 0x04000090 RID: 144
		private global::System.Windows.Forms.ComboBox comboBox7;

		// Token: 0x04000091 RID: 145
		private global::System.Windows.Forms.ComboBox comboBox4;

		// Token: 0x04000092 RID: 146
		private global::System.Windows.Forms.ComboBox comboBox3;

		// Token: 0x04000093 RID: 147
		private global::System.Windows.Forms.ComboBox comboBox2;

		// Token: 0x04000094 RID: 148
		private global::System.Windows.Forms.TextBox txt_input;

		// Token: 0x04000095 RID: 149
		private global::System.Windows.Forms.Button button12;

		// Token: 0x04000096 RID: 150
		private global::System.Windows.Forms.Button button1;
	}
}
