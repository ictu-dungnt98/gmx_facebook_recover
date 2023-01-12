using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x02000015 RID: 21
	public partial class AddDM : Form
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002519 File Offset: 0x00000719
		public AddDM()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000B248 File Offset: 0x00009448
		private void button12_Click(object sender, EventArgs e)
		{
			bool flag = this.txt_tendm.Text == "";
			if (flag)
			{
				MessageBox.Show("CHƯA NHẬP TÊN DANH MỤC");
			}
			else
			{
				AddDM.name = this.txt_tendm.Text;
				base.Close();
			}
		}

		// Token: 0x0400005C RID: 92
		public static string name;
	}
}
