using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK.frm
{
	// Token: 0x0200001D RID: 29
	public partial class CopyDulieu : Form
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000025C9 File Offset: 0x000007C9
		public CopyDulieu()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		private void CopyDulieu_Load(object sender, EventArgs e)
		{
			bool flag = File.Exists("dinhdangcopy.txt");
			if (flag)
			{
				string[] dinhdang = File.ReadAllText("dinhdangcopy.txt").Split(new char[]
				{
					'|'
				});
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
				this.show();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000D048 File Offset: 0x0000B248
		private void button12_Click(object sender, EventArgs e)
		{
			string dinhdang = string.Concat(new string[]
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
			Clipboard.SetText(this.txt_input.Text);
			base.Invoke(new Action(delegate()
			{
				File.WriteAllText("dinhdangcopy.txt", dinhdang);
			}));
			MessageBox.Show("COPY THÀNH CÔNG !");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000D14C File Offset: 0x0000B34C
		public void show()
		{
			base.Invoke(new Action(delegate()
			{
				this.txt_input.Clear();
			}));
			StringBuilder ac = new StringBuilder();
			string text = string.Concat(new string[]
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
			string[] array = text.Split(new char[]
			{
				'|'
			});
			string[] array2 = "username|uid|pass|cookie|2fa key|email|passmail|2fa code".Split(new char[]
			{
				'|'
			});
			string[] array3 = this.acc.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array3.Length; i++)
			{
				string[] array4 = array3[i].Split(new char[]
				{
					'|'
				});
				string text2 = "";
				for (int j = 0; j < array.Length; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						bool flag = array2[k] == array[j].ToLower();
						if (flag)
						{
							bool flag2 = j == 0;
							if (flag2)
							{
								text2 += array4[k];
							}
							else
							{
								bool flag3 = k != 0;
								if (flag3)
								{
									text2 = text2 + "|" + array4[k];
								}
								else
								{
									text2 += array4[k];
								}
							}
						}
					}
				}
				ac.AppendLine(text2);
			}
			base.Invoke(new Action(delegate()
			{
				this.txt_input.Text = ac.ToString();
			}));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000025F1 File Offset: 0x000007F1
		private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000D38C File Offset: 0x0000B58C
		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				base.Invoke(new Action(delegate()
				{
					this.comboBox2.Text = "";
					this.comboBox3.Text = "";
					this.comboBox4.Text = "";
					this.comboBox5.Text = "";
					this.comboBox6.Text = "";
					this.comboBox7.Text = "";
					this.comboBox8.Text = "";
					this.comboBox9.Text = "";
				}));
			}
			catch
			{
			}
			this.show();
		}

		// Token: 0x0400008A RID: 138
		private string acc = Form1.copyac.ToString();
	}
}
