using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x0200001B RID: 27
	public partial class ChuyenDM : Form
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0000257E File Offset: 0x0000077E
		public ChuyenDM()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		private void ChuyenDM_Load(object sender, EventArgs e)
		{
			base.Invoke(new Action(delegate()
			{
				this.comboBox1.Items.Clear();
			}));
			string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\DANHMUC");
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string n = text.ToString().Split(new char[]
				{
					'\\'
				})[text.ToString().Split(new char[]
				{
					'\\'
				}).Length - 1].Replace(".txt", "");
				bool flag = n != Form1.cbbx;
				if (flag)
				{
					base.Invoke(new Action(delegate()
					{
						this.comboBox1.Items.Add(n);
					}));
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private void button9_Click(object sender, EventArgs e)
		{
			bool flag = this.comboBox1.Text == "";
			if (flag)
			{
				MessageBox.Show("CHƯA NHẬP TÊN DANH MỤC");
			}
			else
			{
				DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn Chuyển?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				bool flag2 = dialogResult == DialogResult.Yes;
				if (flag2)
				{
					ChuyenDM.name = this.comboBox1.Text;
					base.Close();
				}
			}
		}

		// Token: 0x04000083 RID: 131
		public static string name;
	}
}
