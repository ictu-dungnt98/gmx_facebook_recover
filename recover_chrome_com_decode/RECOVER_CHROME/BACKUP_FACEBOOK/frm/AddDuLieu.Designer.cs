namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x02000016 RID: 22
	public partial class AddDuLieu : global::System.Windows.Forms.Form
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000BB58 File Offset: 0x00009D58
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000BB90 File Offset: 0x00009D90
		private void InitializeComponent()
		{
			this.txt_input = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.button12 = new global::System.Windows.Forms.Button();
			this.comboBox1 = new global::System.Windows.Forms.ComboBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.comboBox2 = new global::System.Windows.Forms.ComboBox();
			this.comboBox3 = new global::System.Windows.Forms.ComboBox();
			this.comboBox4 = new global::System.Windows.Forms.ComboBox();
			this.comboBox5 = new global::System.Windows.Forms.ComboBox();
			this.comboBox6 = new global::System.Windows.Forms.ComboBox();
			this.comboBox7 = new global::System.Windows.Forms.ComboBox();
			this.comboBox8 = new global::System.Windows.Forms.ComboBox();
			this.comboBox9 = new global::System.Windows.Forms.ComboBox();
			base.SuspendLayout();
			this.txt_input.Location = new global::System.Drawing.Point(12, 38);
			this.txt_input.MaxLength = 999999999;
			this.txt_input.Multiline = true;
			this.txt_input.Name = "txt_input";
			this.txt_input.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.txt_input.Size = new global::System.Drawing.Size(897, 356);
			this.txt_input.TabIndex = 0;
			this.txt_input.WordWrap = false;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(9, 22);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(83, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Dữ liệu đầu vào";
			this.button12.BackColor = global::System.Drawing.Color.Green;
			this.button12.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button12.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button12.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button12.Location = new global::System.Drawing.Point(707, 470);
			this.button12.Name = "button12";
			this.button12.Size = new global::System.Drawing.Size(202, 37);
			this.button12.TabIndex = 22;
			this.button12.Text = "Import";
			this.button12.UseVisualStyleBackColor = false;
			this.button12.Click += new global::System.EventHandler(this.button12_Click);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new global::System.Drawing.Point(521, 470);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new global::System.Drawing.Size(161, 21);
			this.comboBox1.TabIndex = 24;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(431, 473);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(84, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Chọn Danh mục";
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
				"2FA CODE"
			});
			this.comboBox2.Location = new global::System.Drawing.Point(11, 413);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox2.TabIndex = 25;
			this.comboBox2.SelectedIndexChanged += new global::System.EventHandler(this.comboBox2_SelectedIndexChanged);
			this.comboBox2.Click += new global::System.EventHandler(this.comboBox2_Click);
			this.comboBox2.MouseClick += new global::System.Windows.Forms.MouseEventHandler(this.comboBox2_MouseClick);
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox3.Location = new global::System.Drawing.Point(124, 413);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox3.TabIndex = 26;
			this.comboBox3.SelectedIndexChanged += new global::System.EventHandler(this.comboBox3_SelectedIndexChanged);
			this.comboBox3.MouseClick += new global::System.Windows.Forms.MouseEventHandler(this.comboBox3_MouseClick);
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox4.Location = new global::System.Drawing.Point(237, 413);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox4.TabIndex = 27;
			this.comboBox5.FormattingEnabled = true;
			this.comboBox5.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox5.Location = new global::System.Drawing.Point(350, 413);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox5.TabIndex = 30;
			this.comboBox6.FormattingEnabled = true;
			this.comboBox6.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox6.Location = new global::System.Drawing.Point(461, 413);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox6.TabIndex = 29;
			this.comboBox7.FormattingEnabled = true;
			this.comboBox7.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox7.Location = new global::System.Drawing.Point(575, 413);
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox7.TabIndex = 28;
			this.comboBox8.FormattingEnabled = true;
			this.comboBox8.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox8.Location = new global::System.Drawing.Point(688, 413);
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox8.TabIndex = 32;
			this.comboBox9.FormattingEnabled = true;
			this.comboBox9.Items.AddRange(new object[]
			{
				"UserName",
				"Uid",
				"Pass",
				"Cookie",
				"2FA Key",
				"Email",
				"PassMail",
				"2FA CODE"
			});
			this.comboBox9.Location = new global::System.Drawing.Point(801, 413);
			this.comboBox9.Name = "comboBox9";
			this.comboBox9.Size = new global::System.Drawing.Size(107, 21);
			this.comboBox9.TabIndex = 31;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(917, 519);
			base.Controls.Add(this.comboBox8);
			base.Controls.Add(this.comboBox9);
			base.Controls.Add(this.comboBox5);
			base.Controls.Add(this.comboBox6);
			base.Controls.Add(this.comboBox7);
			base.Controls.Add(this.comboBox4);
			base.Controls.Add(this.comboBox3);
			base.Controls.Add(this.comboBox2);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.button12);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txt_input);
			base.Name = "AddDuLieu";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thêm Dữ Liệu";
			base.Load += new global::System.EventHandler(this.AddDuLieu_Load);
			base.LocationChanged += new global::System.EventHandler(this.AddDuLieu_LocationChanged);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000068 RID: 104
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.TextBox txt_input;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.Button button12;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.ComboBox comboBox1;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.ComboBox comboBox2;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.ComboBox comboBox3;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.ComboBox comboBox4;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.ComboBox comboBox5;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.ComboBox comboBox6;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.ComboBox comboBox7;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.ComboBox comboBox8;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.ComboBox comboBox9;
	}
}
