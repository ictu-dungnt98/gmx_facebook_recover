using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x02000016 RID: 22
	public partial class AddDuLieu : Form
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000B548 File Offset: 0x00009748
		public AddDuLieu()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002531 File Offset: 0x00000731
		private void AddDuLieu_LocationChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000B5A0 File Offset: 0x000097A0
		private void AddDuLieu_Load(object sender, EventArgs e)
		{
			this.cbb = this.comboBox2;
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
			bool flag = File.Exists("dinhdangthemacc.txt");
			if (flag)
			{
				string[] dinhdang = File.ReadAllText("dinhdangthemacc.txt").Split(new char[]
				{
					'|'
				}).ToArray<string>();
				try
				{
					base.Invoke(new Action(delegate()
					{
						this.comboBox2.Text = dinhdang[0];
						this.comboBox3.Text = dinhdang[1];
						this.comboBox4.Text = dinhdang[2];
						this.comboBox5.Text = dinhdang[3];
						this.comboBox6.Text = dinhdang[4];
						this.comboBox7.Text = dinhdang[5];
						this.comboBox8.Text = dinhdang[6];
						this.comboBox9.Text = dinhdang[7];
					}));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000B698 File Offset: 0x00009898
		private void button12_Click(object sender, EventArgs e)
		{
			bool flag = this.comboBox2.Text == "" && this.comboBox3.Text == "" && this.comboBox4.Text == "" && this.comboBox5.Text == "" && this.comboBox6.Text == "" && this.comboBox7.Text == "";
			AddDuLieu.dinhdang = string.Concat(new string[]
			{
				this.comboBox2.Text,
				"|",
				this.comboBox3.Text,
				"|",
				this.comboBox4.Text,
				"|",
				this.comboBox5.Text,
				"|",
				this.comboBox6.Text,
				"|",
				this.comboBox7.Text,
				"|",
				this.comboBox8.Text,
				"|",
				this.comboBox9.Text
			});
			bool flag2 = this.comboBox1.Text == "";
			if (flag2)
			{
				MessageBox.Show("CHƯA CHỌN DANH MỤC CẦN THÊM DỮ LIỆU");
			}
			else
			{
				bool flag3 = flag;
				if (flag3)
				{
					MessageBox.Show("CHƯA CHỌN ĐỊNH DẠNG");
				}
				else
				{
					bool flag4 = this.txt_input.Text == "";
					if (flag4)
					{
						MessageBox.Show("CHƯA NHẬP DỮ LIỆU");
					}
					else
					{
						this.trung = 0;
						string[] source = this.txt_input.Text.Split(new string[]
						{
							Environment.NewLine
						}, StringSplitOptions.RemoveEmptyEntries);
						List<string> list = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\DANHMUC\\" + this.comboBox1.Text + ".txt").ToList<string>();
						Parallel.ForEach<string>(source, delegate(string x)
						{
							this.show1(x, list);
						});
						base.Invoke(new Action(delegate()
						{
							File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\DANHMUC\\" + this.comboBox1.Text + ".txt", this.ac.ToString());
							File.WriteAllText("dinhdangthemacc.txt", AddDuLieu.dinhdang);
						}));
						MessageBox.Show("XONG ! LỌC TRÙNG : " + this.trung.ToString());
					}
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000B90C File Offset: 0x00009B0C
		public void show1(string datas, List<string> list)
		{
			string[] array = AddDuLieu.dinhdang.Split(new char[]
			{
				'|'
			});
			string[] array2 = "username|uid|pass|cookie|2fa key|email|passmail|2fa code".Split(new char[]
			{
				'|'
			});
			string[] array3 = datas.Split(new char[]
			{
				'|'
			});
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			IL_FA:
			while (i < array2.Length)
			{
				for (int j = 0; j < array.Length; j++)
				{
					bool flag = array[j].ToLower() == array2[i];
					if (flag)
					{
						bool flag2 = j != array.Length - 1;
						if (flag2)
						{
							stringBuilder.Append(array3[j] + "|");
						}
						else
						{
							stringBuilder.Append(array3[j]);
						}
						IL_EB:
						Thread.Sleep(1);
						i++;
						goto IL_FA;
					}
				}
				bool flag3 = i != array2.Length - 1;
				if (flag3)
				{
					stringBuilder.Append("|");
					goto IL_EB;
				}
				stringBuilder.Append("");
				goto IL_EB;
			}
			bool flag4 = false;
			string[] array4 = stringBuilder.ToString().Split(new char[]
			{
				'|'
			});
			for (int k = 0; k < list.Count; k++)
			{
				string[] array5 = list[k].Split(new char[]
				{
					'|'
				});
				bool flag5 = AddDuLieu.dinhdang.ToLower().Contains("uid");
				if (flag5)
				{
					bool flag6 = array5[1] == array4[1];
					if (flag6)
					{
						flag4 = true;
						this.trung++;
						break;
					}
				}
				else
				{
					bool flag7 = AddDuLieu.dinhdang.ToLower().Contains("user");
					if (flag7)
					{
						bool flag8 = array5[0] == array4[0];
						if (flag8)
						{
							flag4 = true;
							this.trung++;
							break;
						}
					}
				}
			}
			bool flag9 = !flag4;
			if (flag9)
			{
				object obj = this.obj;
				lock (obj)
				{
					this.ac.AppendLine(stringBuilder.ToString());
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002531 File Offset: 0x00000731
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002531 File Offset: 0x00000731
		private void comboBox2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002531 File Offset: 0x00000731
		private void comboBox2_MouseClick(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002531 File Offset: 0x00000731
		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002531 File Offset: 0x00000731
		private void comboBox3_MouseClick(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x04000061 RID: 97
		public static string dinhdang;

		// Token: 0x04000062 RID: 98
		public static string data;

		// Token: 0x04000063 RID: 99
		public List<string> listcombo = new List<string>();

		// Token: 0x04000064 RID: 100
		public ComboBox cbb = new ComboBox();

		// Token: 0x04000065 RID: 101
		private StringBuilder ac = new StringBuilder();

		// Token: 0x04000066 RID: 102
		private int trung = 0;

		// Token: 0x04000067 RID: 103
		private object obj = new object();
	}
}
