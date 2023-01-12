using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x0200001A RID: 26
	public partial class Capnhatthongtin : Form
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00002566 File Offset: 0x00000766
		public Capnhatthongtin()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		private void button12_Click(object sender, EventArgs e)
		{
			bool flag = this.textBox1.Text == "";
			if (flag)
			{
				MessageBox.Show("CHƯA NHẬP THÔNG TIN");
			}
			else
			{
				bool flag2 = this.comboBox2.Text == "";
				if (flag2)
				{
					MessageBox.Show("CHƯA CHỌN THÔNG TIN");
				}
				else
				{
					Capnhatthongtin.text = this.textBox1.Text;
					Capnhatthongtin.name = this.comboBox2.Text;
					base.Close();
				}
			}
		}

		// Token: 0x0400007C RID: 124
		public static string text;

		// Token: 0x0400007D RID: 125
		public static string name;
	}
}
