namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x02000015 RID: 21
	public partial class AddDM : global::System.Windows.Forms.Form
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000B298 File Offset: 0x00009498
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000B2D0 File Offset: 0x000094D0
		private void InitializeComponent()
		{
			this.txt_tendm = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.button12 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.txt_tendm.Location = new global::System.Drawing.Point(54, 32);
			this.txt_tendm.Name = "txt_tendm";
			this.txt_tendm.Size = new global::System.Drawing.Size(187, 20);
			this.txt_tendm.TabIndex = 18;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(96, 16);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(103, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "Nhập tên Danh mục";
			this.button12.BackColor = global::System.Drawing.Color.Green;
			this.button12.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.button12.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button12.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.button12.Location = new global::System.Drawing.Point(103, 63);
			this.button12.Name = "button12";
			this.button12.Size = new global::System.Drawing.Size(87, 25);
			this.button12.TabIndex = 20;
			this.button12.Text = "Thêm";
			this.button12.UseVisualStyleBackColor = false;
			this.button12.Click += new global::System.EventHandler(this.button12_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(298, 104);
			base.Controls.Add(this.button12);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txt_tendm);
			this.MaximumSize = new global::System.Drawing.Size(314, 143);
			this.MinimumSize = new global::System.Drawing.Size(314, 143);
			base.Name = "AddDM";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thêm Danh Mục";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400005D RID: 93
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.TextBox txt_tendm;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.Button button12;
	}
}
