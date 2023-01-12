using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x02000021 RID: 33
	public partial class DeleteDM : Form
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00002642 File Offset: 0x00000842
		public DeleteDM()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		private void button9_Click(object sender, EventArgs e)
		{
			bool flag = this.comboBox1.Text == "";
			if (flag)
			{
				MessageBox.Show("CHƯA NHẬP TÊN DANH MỤC");
			}
			else
			{
				DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn XÓA?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				bool flag2 = dialogResult == DialogResult.Yes;
				if (flag2)
				{
					DeleteDM.name = this.comboBox1.Text;
					base.Close();
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000E05C File Offset: 0x0000C25C
		private void DeleteDM_Load(object sender, EventArgs e)
		{
			base.Invoke(new Action(delegate()
			{
				this.comboBox1.Items.Clear();
			}));
			string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\DANHMUC");
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string t = array[i];
				base.Invoke(new Action(delegate()
				{
					this.comboBox1.Items.Add(t.ToString().Split(new char[]
					{
						'\\'
					})[t.ToString().Split(new char[]
					{
						'\\'
					}).Length - 1].Replace(".txt", ""));
				}));
			}
		}

		// Token: 0x0400009C RID: 156
		public static string name;
	}
}
