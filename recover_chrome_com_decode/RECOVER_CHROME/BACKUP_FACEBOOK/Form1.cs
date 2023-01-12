using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BACKUP_FACEBOOK.frm;
using Bat2FA;
using KAutoHelper;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OtpNet;
using xNet;

namespace BACKUP_FACEBOOK
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : Form
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000027D8 File Offset: 0x000009D8
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002082 File Offset: 0x00000282
		private void button1_Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "LDPath.txt");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020A3 File Offset: 0x000002A3
		private void Form1_Load(object sender, EventArgs e)
		{
			this.LoadData();
			this.btn_stop.Enabled = false;
			this.btn_stop.BackColor = Color.Gray;
			File.Exists("password.txt");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D2 File Offset: 0x000002D2
		public void LoadDanhMuc()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020D2 File Offset: 0x000002D2
		private void button2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Trangthai(int i, string text)
		{
			this.dataGridView2.Rows[i].Cells["trangthai1"].Value = text;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Trangthai2(int i, string text)
		{
			this.dataGridView2.Rows[i].Cells["trangthai1"].Value = text;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020D2 File Offset: 0x000002D2
		public void Backup(DataGridViewRow row)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002984 File Offset: 0x00000B84
		public void LoadData()
		{
			if (!File.Exists("DATA.txt"))
			{
				return;
			}
			string[] array = File.ReadAllLines("DATA.txt").ToArray<string>();
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != "")
					{
						string[] array2 = array[i].Split(new char[]
						{
							'|'
						});
						this.dataGridView2.Rows.Add(new object[]
						{
							true,
							this.dataGridView2.Rows.Count + 1,
							array2[0],
							array2[1],
							array2[2]
						});
					}
				}
				this.dataGridView2.DoubleBuffered(true);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A40 File Offset: 0x00000C40
		public void Runcmd(string string_0, string path, string string_2, string string_3)
		{
			try
			{
				string str = string.Concat(new string[]
				{
					path,
					"\\adb -s ",
					string_0,
					string_3,
					string_2
				});
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = "/c " + str,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					RedirectStandardError = true,
					RedirectStandardInput = true,
					RedirectStandardOutput = true
				};
				process.Start();
				process.Dispose();
				process.Close();
			}
			catch (Exception ex)
			{
				Exception ex3;
				Exception ex2 = ex3;
				Exception ex = ex2;
				base.Invoke(new Action(delegate()
				{
					File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ErrorLog.txt", string.Concat(new string[]
					{
						"==>[",
						DateTime.Now.ToString(),
						"] ",
						ex.ToString(),
						"\r\n"
					}));
				}));
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B10 File Offset: 0x00000D10
		public string CheckUID(string deviceID)
		{
			string result = "";
			int num = 0;
			string text;
			for (;;)
			{
				text = this.Getstring(this.LDPath, Encoding.UTF8.GetString(Convert.FromBase64String("YWRiIC1zIA==")) + deviceID + Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsICJjYXQgL2RhdGEvZGF0YS9jb20uZmFjZWJvb2sua2F0YW5hL2FwcF9saWdodF9wcmVmcy9jb20uZmFjZWJvb2sua2F0YW5hL2F1dGhlbnRpY2F0aW9uIg==")));
				if (text != "" && text.Contains("EAAA"))
				{
					break;
				}
				if (num >= 3)
				{
					return result;
				}
				num++;
			}
			return Regex.Split(Regex.Split(text, "\"name\":\"")[1], "\",\"expires\":\"")[0].Replace("c_user\",\"value\":\"", "");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public bool Check()
		{
			this.querow2.Clear();
			foreach (object obj in ((IEnumerable)this.dataGridView2.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if (dataGridViewRow.Cells[0].Value.Equals(true))
				{
					this.querow2.Enqueue(dataGridViewRow);
				}
			}
			if (!this.querow2.Any<DataGridViewRow>())
			{
				MessageBox.Show("CHƯA CHỌN MAIL CẦN CHẠY!");
				return false;
			}
			Form1.listmailphu = File.ReadAllLines("mailphu.txt").ToList<string>();
			if (Form1.listmailphu.Any<string>())
			{
				return true;
			}
			MessageBox.Show("CHƯA NHẬP MAIL PHỤ!");
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C84 File Offset: 0x00000E84
		public string Getmailphu()
		{
			object obj = this.obj;
			string result;
			lock (obj)
			{
				string text = Form1.listmailphu[0];
				Form1.listmailphu.Remove(text);
				base.Invoke(new Action(delegate()
				{
					File.WriteAllLines("mailphu.txt", Form1.listmailphu.ToArray());
				}));
				result = text;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002D00 File Offset: 0x00000F00
		public List<string> GetDevice()
		{
			List<string> list = new List<string>();
			try
			{
				string text = this.Getstring(this.LDPath, "adb devices");
				if (text != "")
				{
					string[] array = text.Split(new string[]
					{
						Environment.NewLine
					}, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Contains("\tdevice"))
						{
							list.Add(array[i].Replace("\tdevice", ""));
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002D94 File Offset: 0x00000F94
		public string Getstring(string path, string string_0)
		{
			string result = "";
			try
			{
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo
				{
					WorkingDirectory = path,
					FileName = "cmd.exe",
					CreateNoWindow = true,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					RedirectStandardInput = true,
					RedirectStandardOutput = true
				};
				process.Start();
				process.StandardInput.WriteLine(string_0);
				process.StandardInput.Flush();
				process.StandardInput.Close();
				result = process.StandardOutput.ReadToEnd();
				process.Dispose();
				GC.Collect();
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E44 File Offset: 0x00001044
		private void button3_Click(object sender, EventArgs e)
		{
			if (!this.Check())
			{
				return;
			}
			this.querow2.Clear();
			foreach (object obj in ((IEnumerable)this.dataGridView2.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if (dataGridViewRow.Cells[0].Value.Equals(true))
				{
					this.querow2.Enqueue(dataGridViewRow);
				}
			}
			if (this.querow2.Count > 0)
			{
				List<Thread> list = new List<Thread>();
				while (this.querow.Count != 0 && this.querow2.Count != 0)
				{
					Thread thread = new Thread(delegate()
					{
						try
						{
							this.querow.Dequeue();
							int count = this.querow2.Count;
						}
						catch
						{
						}
					});
					thread.Start();
					thread.IsBackground = true;
					list.Add(thread);
					Thread.Sleep(100);
				}
				foreach (Thread thread2 in list)
				{
					thread2.Join();
				}
				MessageBox.Show("LOGIN THÀNH CÔNG !");
				return;
			}
			MessageBox.Show("CHƯA CHỌN ACCOUNT CẦN LOGIN!");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F90 File Offset: 0x00001190
		public void Login()
		{
			DataGridViewRow dataGridViewRow = this.querow2.Dequeue();
			int index = dataGridViewRow.Index;
			int num = Convert.ToInt32(dataGridViewRow.Cells["LD"].Value.ToString().Replace("LD-Player-", ""));
			int num2 = num * 2 + 5555;
			string text = "127.0.0.1:" + num2.ToString();
			"emulator-" + (num2 * 2 + 5554).ToString();
			string text2 = dataGridViewRow.Cells["uid"].Value.ToString();
			int i = index;
			if (!(text2 != ""))
			{
				return;
			}
			this.Trangthai(index, "Clear Facebook");
			this.Runcmd(text, this.LDPath, "", " shell am force-stop com.facebook.katana");
			this.Runcmd2(this.LDPath, Encoding.UTF8.GetString(Convert.FromBase64String("YWRiIC1zIA==")) + text + Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsIHJtIC1yIC9kYXRhL2RhdGEvY29tLmZhY2Vib29rLmthdGFuYS9kYXRhYmFzZXM=")));
			Thread.Sleep(1000);
			this.Runcmd2(this.LDPath, Encoding.UTF8.GetString(Convert.FromBase64String("YWRiIC1zIA==")) + text + Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsIHJtIC1yIC9kYXRhL2RhdGEvY29tLmZhY2Vib29rLmthdGFuYS9hcHBfbGlnaHRfcHJlZnM=")));
			Thread.Sleep(1000);
			this.Runcmd2(this.LDPath, Encoding.UTF8.GetString(Convert.FromBase64String("YWRiIC1zIA==")) + text + Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsIHJtIC1yIC9kYXRhL2RhdGEvY29tLmZhY2Vib29rLmthdGFuYS9zaGFyZV9wcmVmcw==")));
			this.Trangthai(index, "Check live UID");
			this.Trangthai2(i, "Check live UID");
			if (this.CheckLive(text2))
			{
				this.Trangthai(index, "UID Live");
				this.Trangthai2(i, "UID Live");
				Thread.Sleep(1000);
				this.Trangthai(index, "Login DATA");
				if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DATA\\" + text2 + ".data"))
				{
					if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "DATA\\" + text2))
					{
						Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "DATA\\" + text2, true);
					}
					ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "DATA\\" + text2 + ".data", AppDomain.CurrentDomain.BaseDirectory + "DATA\\" + text2);
					this.Runcmd(text, this.LDPath, "", string.Concat(new string[]
					{
						" push ",
						AppDomain.CurrentDomain.BaseDirectory,
						"DATA\\",
						text2,
						"\\app_light_prefs /data/data/com.facebook.katana/app_light_prefs/com.facebook.katana/"
					}));
					this.Runcmd(text, this.LDPath, "", string.Concat(new string[]
					{
						" push ",
						AppDomain.CurrentDomain.BaseDirectory,
						"DATA\\",
						text2,
						"\\shared_prefs /data/data/com.facebook.katana/shared_prefs"
					}));
					Thread.Sleep(4000);
					Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "DATA\\" + text2, true);
					this.Runcmd2(this.LDPath, "ldconsole.exe runapp --index " + num.ToString() + " --packagename com.facebook.katana");
					Thread.Sleep(2000);
					for (;;)
					{
						IL_383:
						if (this.isstop || !this.CheckActivity(text).Contains(".Launcher"))
						{
							int num3 = 0;
							int num4 = 0;
							while (!this.isstop)
							{
								string noidung = this.dump(text, this.LDPath);
								if (this.getbounds(noidung, text, "Menu", false, this.LDPath))
								{
									if (num4 >= 2)
									{
										goto Block_7;
									}
									num4++;
								}
								else
								{
									if (this.getbounds(noidung, text, "Session Expired", false, this.LDPath))
									{
										goto Block_8;
									}
									if (this.CheckIMAGEFOLDER(text, "CHECKPOINT"))
									{
										goto Block_9;
									}
									if (num3 >= 1)
									{
										Form1.Runcmd(this.LDPath, text, "shell am force-stop com.facebook.katana");
										Thread.Sleep(1000);
										this.Runcmd2(this.LDPath, "ldconsole.exe runapp --index " + num.ToString() + " --packagename com.facebook.katana");
										Thread.Sleep(1000);
										goto IL_383;
									}
								}
								num3++;
								Thread.Sleep(1000);
							}
							goto IL_4C9;
						}
						this.Runcmd2(this.LDPath, "ldconsole.exe runapp --index " + num.ToString() + " --packagename com.facebook.katana");
						Thread.Sleep(2000);
					}
					Block_7:
					this.Trangthai(index, "Login Thành công : " + text2);
					this.Trangthai2(i, "Login thành công");
					goto IL_4C9;
					Block_8:
					this.Trangthai(index, "Out Cookie");
					this.Trangthai2(i, "Out Cookie");
					return;
					Block_9:
					this.Trangthai(index, "Checkpoint");
					this.Trangthai2(i, "Checkpoint");
					return;
					IL_4C9:
					Thread.Sleep(2000);
					if (!this.themmail)
					{
						bool flag = this.flag2fa;
						return;
					}
				}
			}
			else
			{
				this.Trangthai(index, "UID Die");
				this.Trangthai2(i, "UID Die");
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000349C File Offset: 0x0000169C
		public bool CheckIMAGEFOLDER(string deviceID, string path)
		{
			string text = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\" + path;
			Bitmap screen = this.capturescreen(deviceID, this.LDPath);
			string[] array = Directory.GetFiles(text).ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				string bmp = array[i].Split(new char[]
				{
					'\\'
				})[array[i].Split(new char[]
				{
					'\\'
				}).Length - 1];
				if (this.CheckExistImage(this.LDPath, false, deviceID, bmp, screen, text + "\\"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003538 File Offset: 0x00001738
		public bool CheckIMAGEFOLDERClick(string deviceID, string path, Bitmap screen)
		{
			try
			{
				string[] array = Directory.GetFiles(path).ToArray<string>();
				for (int i = 0; i < array.Length; i++)
				{
					string bmp = array[i].Split(new char[]
					{
						'\\'
					})[array[i].Split(new char[]
					{
						'\\'
					}).Length - 1];
					if (this.CheckExistImage(this.LDPath, true, deviceID, bmp, screen, path + "\\"))
					{
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000035C4 File Offset: 0x000017C4
		public bool CheckLive(string new_user_id)
		{
			try
			{
				string text = new HttpRequest
				{
					ConnectTimeout = 5000
				}.Get("https://graph.facebook.com/" + new_user_id + "/picture?redirect=false", null).ToString();
				if (!string.IsNullOrEmpty(text))
				{
					return text.Contains("height") && text.Contains("width");
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000020FC File Offset: 0x000002FC
		public void ClearDataApp(string LDPath, string deviceID, string packagename)
		{
			Form1.Runcmd(LDPath, deviceID, "shell pm clear " + packagename);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000363C File Offset: 0x0000183C
		public bool CheckExistImage(string LDpath, bool click, string deviceID, string bmp, Bitmap screen, string pathimage)
		{
			bool result;
			try
			{
				Bitmap subBitmap = (Bitmap)Image.FromFile(pathimage + bmp);
				Point? point = ImageScanOpenCV.FindOutPoint(screen, subBitmap, 0.9);
				if (point == null)
				{
					result = false;
				}
				else
				{
					if (click)
					{
						AdbHelper.tap(LDpath, deviceID, (point.Value.X + 3).ToString(), point.Value.Y.ToString());
					}
					result = true;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000036D4 File Offset: 0x000018D4
		public void Runcmd2(string path, string string_0)
		{
			for (;;)
			{
				try
				{
					Process process = new Process();
					process.StartInfo = new ProcessStartInfo
					{
						WorkingDirectory = path,
						FileName = "cmd.exe",
						CreateNoWindow = true,
						UseShellExecute = false,
						WindowStyle = ProcessWindowStyle.Hidden,
						RedirectStandardInput = true,
						RedirectStandardOutput = true
					};
					process.Start();
					process.StandardInput.WriteLine(string_0);
					process.StandardInput.Flush();
					process.StandardInput.Close();
					process.Dispose();
					process.Close();
				}
				catch (Exception ex)
				{
					Exception ex3;
					Exception ex2 = ex3;
					Exception ex = ex2;
					base.Invoke(new Action(delegate()
					{
						File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ErrorLog.txt", string.Concat(new string[]
						{
							"==>[",
							DateTime.Now.ToString(),
							"] ",
							ex.ToString(),
							"\r\n"
						}));
					}));
					continue;
				}
				break;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002110 File Offset: 0x00000310
		public string CheckActivity(string deviceID)
		{
			return this.Getstring(this.LDPath, "adb -s " + deviceID + " shell \"dumpsys activity activities | grep mResumedActivity\"");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000020D2 File Offset: 0x000002D2
		private void button5_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003794 File Offset: 0x00001994
		public void Cleardata(DataGridViewRow row)
		{
			int index = row.Index;
			string string_ = row.Cells["deviceid"].Value.ToString();
			this.Trangthai(index, "Bắt đầu CLEAR DATA");
			this.Runcmd(string_, this.LDPath, "", " shell pm clear com.facebook.katana");
			Thread.Sleep(1000);
			this.Trangthai(index, "CLEAR DATA THÀNH CÔNG");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000037FC File Offset: 0x000019FC
		public void GetCookie(DataGridViewRow row)
		{
			int index = row.Index;
			string deviceID = row.Cells["deviceid"].Value.ToString();
			this.Trangthai(index, "Bắt đầu GET COOKIE TOKEN");
			List<string> list = this.getcookie_token1(deviceID);
			if (list[2] == "")
			{
				this.Trangthai(index, "KHÔNG TÌM THẤY COOKIE");
				return;
			}
			this.Trangthai(index, string.Concat(new string[]
			{
				list[2],
				"|",
				list[0],
				"|",
				list[1]
			}));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000038A0 File Offset: 0x00001AA0
		public List<string> getcookie_token1(string deviceID)
		{
			List<string> list = new List<string>();
			try
			{
				string text = this.Getstring(this.LDPath, Encoding.UTF8.GetString(Convert.FromBase64String("YWRiIC1zIA==")) + deviceID + Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsICJjYXQgL2RhdGEvZGF0YS9jb20uZmFjZWJvb2sua2F0YW5hL2FwcF9saWdodF9wcmVmcy9jb20uZmFjZWJvb2sua2F0YW5hL2F1dGhlbnRpY2F0aW9uIg==")));
				if (text != "" && text.Contains("EAAA"))
				{
					string input = text;
					string item = "EAAAA" + Regex.Split(Regex.Split(input, "EAAAA")[1], "\u0005")[0];
					string[] array = Regex.Split(input, "\"name\":\"");
					string text2 = "c_user=" + Regex.Split(array[1], "\",\"expires\":\"")[0].Replace("c_user\",\"value\":\"", "");
					string text3 = "xs=" + Regex.Split(array[2], "\",\"expires\":\"")[0].Replace("xs\",\"value\":\"", "");
					string text4 = "fr=" + Regex.Split(array[3], "\",\"expires\":\"")[0].Replace("fr\",\"value\":\"", "");
					string text5 = "datr=" + Regex.Split(array[4], "\",\"expires\":\"")[0].Replace("datr\",\"value\":\"", "");
					string item2 = string.Concat(new string[]
					{
						text2,
						";",
						text3,
						";",
						text4,
						";",
						text5
					});
					string item3 = text2.Split(new char[]
					{
						'='
					})[1];
					list.Add(item2);
					list.Add(item);
					list.Add(item3);
				}
			}
			catch (Exception ex)
			{
				Exception ex3;
				Exception ex2 = ex3;
				Exception ex = ex2;
				base.Invoke(new Action(delegate()
				{
					File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ErrorLog.txt", string.Concat(new string[]
					{
						"==>[",
						DateTime.Now.ToString(),
						"] ",
						ex.ToString(),
						"\r\n"
					}));
				}));
			}
			return list;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003A9C File Offset: 0x00001C9C
		private void cHỌNToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.dataGridView2.Rows))
			{
				((DataGridViewRow)obj).Cells[0].Value = false;
			}
			foreach (object obj2 in this.dataGridView2.SelectedRows)
			{
				((DataGridViewRow)obj2).Cells[0].Value = true;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003B64 File Offset: 0x00001D64
		private void bỎCHỌNTẤTCẢToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.dataGridView2.Rows))
			{
				((DataGridViewRow)obj).Cells[0].Value = false;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private void cHỌNTẤTCẢToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.dataGridView2.Rows))
			{
				((DataGridViewRow)obj).Cells[0].Value = true;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003C3C File Offset: 0x00001E3C
		private void button7_Click(object sender, EventArgs e)
		{
			if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Mail.txt"))
			{
				File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "Mail.txt", "");
			}
			Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "Mail.txt");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public void Themmail2(string deviceID, string UID, int i, int row1)
		{
			if (deviceID.Contains("127"))
			{
				int num = (Convert.ToInt32(deviceID.Replace("127.0.0.1:", "")) - 5555) / 2;
			}
			else if (deviceID.Contains("emulator"))
			{
				int num2 = (Convert.ToInt32(deviceID.Replace("emulator-", "")) - 5554) / 2;
			}
			if (this.flag2fa)
			{
				return;
			}
			Thread.Sleep(2000);
			while (!this.isstop && this.CheckActivity(deviceID).Contains(".Launcher"))
			{
			}
			for (;;)
			{
				IL_86:
				Form1.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://notification_settings_email");
				Thread.Sleep(2000);
				if (this.quemail.Count == 0)
				{
					this.Trangthai(i, "Hết Email");
					this.Trangthai2(row1, "Hết Email");
				}
				IL_4E3:
				while (!this.isstop)
				{
					if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "+ ADD EMAIL", true, this.LDPath))
					{
						this.Trangthai(i, "Nhấn ADD EMAIL");
						try
						{
							string text = this.quemail.Dequeue();
							while (!this.isstop)
							{
								if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "NEXT", false, this.LDPath))
								{
									string text2 = text.Split(new char[]
									{
										'|'
									})[0];
									this.Trangthai(i, "Nhập EMAIL: " + text2);
									Form1.tap(this.LDPath, deviceID, "150", "82");
									Thread.Sleep(1000);
									this.inputtext(this.LDPath, deviceID, text2);
									Thread.Sleep(1000);
									this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "\"NEXT\"", true, this.LDPath);
									this.Trangthai(i, "NHẤN NEXT");
									Thread.Sleep(1000);
									while (!this.isstop)
									{
										string noidung = this.dump(deviceID, this.LDPath);
										if (!this.getbounds(noidung, deviceID, "NEXT", true, this.LDPath))
										{
											IL_49F:
											while (!this.isstop)
											{
												string noidung2 = this.dump(deviceID, this.LDPath);
												if (this.getbounds(noidung2, deviceID, "Enter the code from your email", false, this.LDPath))
												{
													this.Trangthai(i, "Đợi Code");
													for (;;)
													{
														try
														{
															string text3 = Mail.Verify(text2, text.Split(new char[]
															{
																'|'
															})[1].Replace("\r", ""), "imap.outlook.com", 993, false).Replace(" ", "");
															if (text3 != "")
															{
																this.Trangthai(i, "Đã có Code: " + text3);
																Form1.tap(this.LDPath, deviceID, "100", "180");
																Thread.Sleep(1000);
																this.Trangthai(i, "Nhập Code: " + text3);
																this.inputtext(this.LDPath, deviceID, text3);
																Thread.Sleep(1000);
																Form1.tap(this.LDPath, deviceID, "150", "235");
																this.Trangthai(i, "Nhấn Confirm");
																Thread.Sleep(6000);
																this.Trangthai(i, "Thêm mail thành công : " + text2);
																this.Trangthai2(row1, "Thêm mail thành công : " + text2);
																this.dataGridView2.Rows[row1].Cells["email"].Value = text2;
																this.dataGridView2.Rows[row1].Cells["passmail"].Value = text.Split(new char[]
																{
																	'|'
																})[1].Replace("\r", "");
																this.SaveData();
																object obj = this.obj;
																lock (obj)
																{
																	this.Deletemail(text);
																}
																this.Trangthai(i, "Thêm mail thành công");
																this.Trangthai2(row1, "Thêm mail thành công");
																goto IL_4EE;
															}
															this.Trangthai(i, "Không về Code hoặc Lỗi mail");
															return;
														}
														catch
														{
															this.Trangthai(i, "Lỗi Lấy Code. Liên hệ Kit Đỗ fix lỗi");
															continue;
														}
														break;
													}
												}
												if (this.getbounds(noidung2, deviceID, "+ ADD EMAIL", false, this.LDPath))
												{
													goto IL_4E3;
												}
											}
											goto IL_4B7;
										}
										if (this.getbounds(noidung, deviceID, "Enter a valid", false, this.LDPath))
										{
											object obj = this.obj;
											lock (obj)
											{
												this.Deletemail(text);
											}
											this.Trangthai(i, "Lỗi Mail! Đổi Mail");
											goto IL_86;
										}
										if (this.getbounds(noidung, deviceID, "+ ADD EMAIL", false, this.LDPath))
										{
											goto IL_4E3;
										}
									}
									goto IL_49F;
								}
							}
							IL_4B7:
							break;
						}
						catch
						{
							if (this.quemail.Count == 0)
							{
								this.Trangthai(i, "Hết Email");
								break;
							}
							this.Trangthai(i, "Lỗi lấy Email");
						}
					}
				}
				break;
			}
			IL_4EE:
			this.Trangthai(i, "THÊM EMAIL THÀNH CÔNG : " + UID);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004218 File Offset: 0x00002418
		public void Deletemail(string mail)
		{
			List<string> list = File.ReadAllLines("Mail.txt").ToList<string>();
			list.Remove(mail);
			File.WriteAllLines("Mail.txt", list);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004248 File Offset: 0x00002448
		public void Go2fa(string deviceID, string uid, int row1)
		{
			string text = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\";
			this.Trangthai2(row1, "Gỡ Security");
			for (;;)
			{
				IL_21:
				Form1.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				while (!this.isstop)
				{
					string noidung = this.dump(deviceID, this.LDPath);
					if (this.getbounds(noidung, deviceID, "Password and Security", true, this.LDPath))
					{
						goto Block_1;
					}
					if (this.getbounds(noidung, deviceID, "Security and Login", true, this.LDPath))
					{
						goto Block_2;
					}
					if (num >= 3)
					{
						goto IL_21;
					}
					num++;
					this.Trangthai2(row1, "Tìm Password and Security");
				}
				break;
			}
			IL_118:
			while (!this.isstop)
			{
				if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use two-factor authentication", true, this.LDPath))
				{
					this.Trangthai2(row1, "Vào Use two-factor authentication");
					IL_161:
					while (!this.isstop)
					{
						Bitmap screen = this.capturescreen(deviceID, this.LDPath);
						if (this.CheckExistImage(this.LDPath, true, deviceID, "secu.png", screen, text))
						{
							this.Trangthai2(row1, "Nhấn Security Key");
							IL_1AC:
							while (!this.isstop)
							{
								Bitmap screen2 = this.capturescreen(deviceID, this.LDPath);
								if (this.CheckExistImage(this.LDPath, true, deviceID, "remove.png", screen2, text))
								{
									this.Trangthai2(row1, "Nhấn Remove Key");
									IL_1F5:
									while (!this.isstop)
									{
										if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Turn off", true, this.LDPath))
										{
											this.Trangthai2(row1, "Nhấn Turn off");
											IL_330:
											while (!this.isstop)
											{
												string noidung2 = this.dump(deviceID, this.LDPath);
												Bitmap screen3 = this.screenshot(deviceID, this.LDPath);
												if (this.getbounds(noidung2, deviceID, "enter your password", false, this.LDPath))
												{
													this.Trangthai2(row1, "Nhập pass");
													this.inputtext(this.LDPath, deviceID, this.dataGridView2.Rows[row1].Cells["pass"].Value.ToString());
													Thread.Sleep(1000);
													while (!this.isstop)
													{
														if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "\"CONTINUE\"", true, this.LDPath))
														{
															this.Trangthai2(row1, "Nhấn CONTINUE");
															Thread.Sleep(2000);
															break;
														}
														this.Trangthai2(row1, "Tìm CONTINUE");
													}
												}
												else if (this.getbounds(noidung2, deviceID, "Authentication app", false, this.LDPath))
												{
													this.Trangthai2(row1, "Gỡ SECURITY KEY THÀNH CÔNG");
													break;
												}
												if (this.CheckExistImage(this.LDPath, false, deviceID, "recovery.png", screen3, text))
												{
													this.Trangthai2(row1, "Gỡ SECURITY KEY THÀNH CÔNG");
													break;
												}
												this.Trangthai2(row1, "Tìm Turn off");
											}
											this.Runcmd(deviceID, this.LDPath, "", " shell am start -W -a android.intent.action.VIEW -d  fb://root");
											Thread.Sleep(1000);
											return;
										}
										this.Trangthai2(row1, "Tìm Turn off");
									}
									goto IL_330;
								}
								this.Trangthai2(row1, "Tìm Remove Key");
							}
							goto IL_1F5;
						}
						this.Trangthai2(row1, "Tìm Security Key");
					}
					goto IL_1AC;
				}
				this.swipe(this.LDPath, deviceID, 100, 350, 100, 300);
				Thread.Sleep(1000);
				this.Trangthai2(row1, "Tìm Use two-factor authentication");
			}
			goto IL_161;
			Block_1:
			this.Trangthai2(row1, "Vào Password and Security");
			goto IL_118;
			Block_2:
			this.Trangthai2(row1, "Vào Security and Login");
			goto IL_118;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000045B4 File Offset: 0x000027B4
		public void Go2faKoLuu(string deviceID, int row1)
		{
			string text = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\";
			this.Trangthai(row1, "Gỡ Security");
			for (;;)
			{
				IL_21:
				Form1.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				while (!this.isstop)
				{
					string noidung = this.dump(deviceID, this.LDPath);
					if (this.getbounds(noidung, deviceID, "Password and Security", true, this.LDPath))
					{
						this.Trangthai(row1, "Vào Password and Security");
					}
					else if (this.getbounds(noidung, deviceID, "Security and Login", true, this.LDPath))
					{
						this.Trangthai(row1, "Vào Security and Login");
					}
					else
					{
						if (num < 3)
						{
							num++;
							this.Trangthai(row1, "Tìm Password and Security");
							continue;
						}
						goto IL_21;
					}
					IL_118:
					while (!this.isstop)
					{
						if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use two-factor authentication", true, this.LDPath))
						{
							this.Trangthai(row1, "Vào Use two-factor authentication");
							break;
						}
						this.swipe(this.LDPath, deviceID, 100, 350, 100, 300);
						Thread.Sleep(1000);
						this.Trangthai(row1, "Tìm Use two-factor authentication");
					}
					int num2 = 0;
					while (!this.isstop)
					{
						Bitmap screen = this.capturescreen(deviceID, this.LDPath);
						if (this.CheckExistImage(this.LDPath, true, deviceID, "secu.png", screen, text))
						{
							this.Trangthai(row1, "Nhấn Security Key");
							IL_1DF:
							while (!this.isstop)
							{
								Bitmap screen2 = this.capturescreen(deviceID, this.LDPath);
								if (this.CheckExistImage(this.LDPath, true, deviceID, "remove.png", screen2, text))
								{
									this.Trangthai(row1, "Nhấn Remove Key");
									IL_228:
									while (!this.isstop)
									{
										if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Turn off", true, this.LDPath))
										{
											this.Trangthai(row1, "Nhấn Turn off");
											IL_32F:
											while (!this.isstop)
											{
												string noidung2 = this.dump(deviceID, this.LDPath);
												Bitmap screen3 = this.screenshot(deviceID, this.LDPath);
												if (this.getbounds(noidung2, deviceID, "enter your password", false, this.LDPath))
												{
													this.Trangthai(row1, "Nhập pass");
													Thread.Sleep(1000);
													while (!this.isstop)
													{
														if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "\"CONTINUE\"", true, this.LDPath))
														{
															this.Trangthai(row1, "Nhấn CONTINUE");
															Thread.Sleep(2000);
															break;
														}
														this.Trangthai(row1, "Tìm CONTINUE");
													}
												}
												else if (this.getbounds(noidung2, deviceID, "Authentication app", false, this.LDPath))
												{
													this.Trangthai(row1, "Gỡ SECURITY KEY THÀNH CÔNG");
													goto IL_21;
												}
												if (this.CheckExistImage(this.LDPath, false, deviceID, "recovery.png", screen3, text))
												{
													this.Trangthai(row1, "Gỡ SECURITY KEY THÀNH CÔNG");
													goto IL_21;
												}
												this.Trangthai(row1, "Checking Gỡ");
											}
											goto Block_18;
										}
										this.Trangthai(row1, "Tìm Turn off");
									}
									goto IL_32F;
								}
								this.Trangthai(row1, "Tìm Remove Key");
							}
							goto IL_228;
						}
						this.Trangthai(row1, "Tìm Security Key " + num2.ToString());
						if (num2 >= 10)
						{
							goto Block_7;
						}
						num2++;
						Thread.Sleep(1000);
					}
					goto IL_1DF;
				}
				goto IL_118;
			}
			Block_7:
			this.Trangthai(row1, "Không có Security Key");
			return;
			Block_18:
			this.Runcmd(deviceID, this.LDPath, "", " shell am start -W -a android.intent.action.VIEW -d  fb://root");
			Thread.Sleep(1000);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000491C File Offset: 0x00002B1C
		public void Bat2fa(string deviceID, string uid, int row1)
		{
			this.Trangthai2(row1, "Bật Security Key");
			for (;;)
			{
				IL_0C:
				Form1.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				while (!this.isstop)
				{
					string noidung = this.dump(deviceID, this.LDPath);
					if (this.getbounds(noidung, deviceID, "Password and Security", true, this.LDPath))
					{
						goto Block_1;
					}
					if (this.getbounds(noidung, deviceID, "Security and Login", true, this.LDPath))
					{
						goto Block_2;
					}
					if (num >= 3)
					{
						goto IL_0C;
					}
					this.Trangthai2(row1, "Tìm Password and Security");
				}
				break;
			}
			IL_102:
			while (!this.isstop)
			{
				if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use two-factor authentication", true, this.LDPath))
				{
					this.Trangthai2(row1, "Vào Use two-factor authentication");
					IL_161:
					while (!this.isstop)
					{
						if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Authentication app", false, this.LDPath))
						{
							this.swipe(this.LDPath, deviceID, 100, 350, 100, 50);
							Thread.Sleep(1000);
							IL_29C:
							while (!this.isstop)
							{
								string noidung2 = this.dump(deviceID, this.LDPath);
								if (this.getbounds(noidung2, deviceID, "Security key", true, this.LDPath))
								{
									this.Trangthai2(row1, "Chọn Security key");
									Thread.Sleep(1000);
									this.getbounds(noidung2, deviceID, "Continue", true, this.LDPath);
									this.Trangthai2(row1, "Continue");
									Thread.Sleep(1000);
								}
								else
								{
									if (!this.getbounds(noidung2, deviceID, "Add a backup method", false, this.LDPath))
									{
										this.swipe(this.LDPath, deviceID, 100, 350, 100, 50);
										Thread.Sleep(1000);
										this.Trangthai2(row1, "Tìm Security key");
										continue;
									}
									this.swipe(this.LDPath, deviceID, 100, 350, 100, 50);
									Thread.Sleep(1000);
									Form1.tap(this.LDPath, deviceID, "150", "420");
									this.Trangthai2(row1, "Chọn Security key");
									Thread.Sleep(1000);
									this.getbounds(noidung2, deviceID, "Continue", true, this.LDPath);
									this.Trangthai2(row1, "Continue");
									Thread.Sleep(1000);
								}
								for (;;)
								{
									IL_2E8:
									if (!this.isstop)
									{
										if (!this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Register security key", true, this.LDPath))
										{
											this.Trangthai2(row1, "Tìm Register security key");
											continue;
										}
										this.Trangthai2(row1, "Chọn Register security key");
									}
									for (;;)
									{
										IL_36C:
										if (!this.isstop)
										{
											string noidung3 = this.dump(deviceID, this.LDPath);
											if (this.getbounds(noidung3, deviceID, "GET STARTED", true, this.LDPath))
											{
												this.Trangthai2(row1, "Chọn GET STARTED");
											}
											else
											{
												if (this.getbounds(noidung3, deviceID, "Try again", false, this.LDPath))
												{
													goto Block_15;
												}
												this.Trangthai2(row1, "Tìm GET STARTED");
												continue;
											}
										}
										while (!this.isstop)
										{
											if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use this device with screen lock", true, this.LDPath))
											{
												this.Trangthai2(row1, "Chọn Use this device with screen lock");
												IL_3FE:
												while (!this.isstop)
												{
													if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use screen lock", true, this.LDPath))
													{
														this.Trangthai2(row1, "Chọn Use screen lock");
														IL_484:
														while (!this.isstop)
														{
															if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Verify your identity", false, this.LDPath))
															{
																this.Trangthai2(row1, "Nhập Mã khóa màn hình");
																string text = File.ReadAllText("khoamanhinh.txt");
																this.inputtext(this.LDPath, deviceID, text);
																Thread.Sleep(1000);
																Form1.Runcmd(this.LDPath, deviceID, "shell input keyevent 66");
																Thread.Sleep(1000);
																break;
															}
															this.Trangthai2(row1, "Tìm Verify your identity");
														}
														int num2 = 0;
														while (!this.isstop)
														{
															string noidung4 = this.dump(deviceID, this.LDPath);
															if (this.getbounds(noidung4, deviceID, "Done", true, this.LDPath))
															{
																goto Block_22;
															}
															if (this.getbounds(noidung4, deviceID, "GET STARTED", false, this.LDPath))
															{
																goto IL_36C;
															}
															if (this.getbounds(noidung4, deviceID, "Register security key", false, this.LDPath))
															{
																if (num2 >= 2)
																{
																	goto IL_2E8;
																}
																num2++;
																Thread.Sleep(1000);
															}
															else
															{
																if (this.getbounds(noidung4, deviceID, "Try again", false, this.LDPath))
																{
																	goto Block_26;
																}
																if (this.getbounds(noidung4, deviceID, "Turn off", false, this.LDPath))
																{
																	break;
																}
																this.Trangthai2(row1, "Tìm Done");
															}
														}
														goto IL_581;
													}
													this.Trangthai2(row1, "Tìm Use screen lock");
												}
												goto IL_484;
											}
											this.Trangthai2(row1, "Tìm Use this device with screen lock");
										}
										goto IL_3FE;
									}
								}
								Block_15:
								Form1.Runcmd(this.LDPath, deviceID, "shell input keyevent 4");
								Thread.Sleep(1000);
								goto IL_581;
								Block_22:
								this.Trangthai2(row1, "Nhấn Done");
								Thread.Sleep(1000);
								goto IL_581;
								Block_26:
								Form1.Runcmd(this.LDPath, deviceID, "shell input keyevent 4");
								Thread.Sleep(1000);
								IL_581:
								string text2 = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\";
								while (!this.isstop)
								{
									Bitmap screen = this.capturescreen(deviceID, this.LDPath);
									if (this.CheckExistImage(this.LDPath, true, deviceID, "recovery.png", screen, text2))
									{
										this.Trangthai2(row1, "Nhấn Recovery codes");
										Thread.Sleep(1000);
										IL_6D3:
										while (!this.isstop)
										{
											string noidung5 = this.dump(deviceID, this.LDPath);
											Bitmap screen2 = this.capturescreen(deviceID, this.LDPath);
											if (this.getbounds(noidung5, deviceID, "Copy codes", false, this.LDPath))
											{
												this.Trangthai2(row1, "Nhấn Copy codes");
												object obj = this.obj;
												lock (obj)
												{
													if (!this.Copy(deviceID, uid, row1))
													{
														return;
													}
												}
												Thread.Sleep(1000);
												break;
											}
											if (this.CheckExistImage(this.LDPath, true, deviceID, "showcode.png", screen2, text2))
											{
												this.Trangthai2(row1, "Nhấn Show codes");
												Thread.Sleep(1000);
											}
											else
											{
												this.Trangthai2(row1, "Tìm Copy codes");
											}
										}
										this.Trangthai2(row1, "Dăng xuất");
										this.Runcmd(deviceID, this.LDPath, "", " shell am force-stop com.facebook.katana");
										Thread.Sleep(1000);
										return;
									}
									this.swipe(this.LDPath, deviceID, 100, 350, 100, 250);
									Thread.Sleep(2000);
									this.Trangthai2(row1, "Tìm Recovery codes");
								}
								goto IL_6D3;
							}
							goto IL_2E8;
						}
						this.Trangthai2(row1, "Tìm Authentication app");
					}
					goto IL_29C;
				}
				this.swipe(this.LDPath, deviceID, 100, 350, 100, 300);
				Thread.Sleep(1000);
				this.Trangthai2(row1, "Tìm Use two-factor authentication");
			}
			goto IL_161;
			Block_1:
			this.Trangthai2(row1, "Vào Password and Security");
			goto IL_102;
			Block_2:
			this.Trangthai2(row1, "Vào Password and Security");
			goto IL_102;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00005044 File Offset: 0x00003244
		public void Bat2faKoLuu(string deviceID, int row1)
		{
			this.Trangthai(row1, "Bật Security Key");
			for (;;)
			{
				IL_0C:
				Form1.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				while (!this.isstop)
				{
					string noidung = this.dump(deviceID, this.LDPath);
					if (this.getbounds(noidung, deviceID, "Password and Security", true, this.LDPath))
					{
						goto Block_1;
					}
					if (this.getbounds(noidung, deviceID, "Security and Login", true, this.LDPath))
					{
						goto Block_2;
					}
					if (num >= 4)
					{
						goto IL_0C;
					}
					num++;
					this.Trangthai(row1, "Tìm Password and Security");
				}
				break;
			}
			IL_109:
			while (!this.isstop)
			{
				if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use two-factor authentication", true, this.LDPath))
				{
					this.Trangthai(row1, "Vào Use two-factor authentication");
					IL_168:
					while (!this.isstop)
					{
						if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Authentication app", false, this.LDPath))
						{
							this.swipe(this.LDPath, deviceID, 100, 350, 100, 50);
							Thread.Sleep(1000);
							IL_29E:
							while (!this.isstop)
							{
								string noidung2 = this.dump(deviceID, this.LDPath);
								if (this.getbounds(noidung2, deviceID, "Security key", true, this.LDPath))
								{
									this.Trangthai(row1, "Chọn Security key");
									Thread.Sleep(1000);
									this.getbounds(noidung2, deviceID, "Continue", true, this.LDPath);
									this.Trangthai(row1, "Continue");
									Thread.Sleep(1000);
								}
								else
								{
									if (!this.getbounds(noidung2, deviceID, "Add a backup method", false, this.LDPath))
									{
										this.swipe(this.LDPath, deviceID, 100, 350, 100, 50);
										Thread.Sleep(1000);
										this.Trangthai(row1, "Tìm Security key");
										continue;
									}
									this.swipe(this.LDPath, deviceID, 100, 350, 100, 50);
									Thread.Sleep(1000);
									Form1.tap(this.LDPath, deviceID, "150", "420");
									this.Trangthai(row1, "Chọn Security key");
									Thread.Sleep(1000);
									this.getbounds(noidung2, deviceID, "Continue", true, this.LDPath);
									this.Trangthai(row1, "Continue");
									Thread.Sleep(1000);
								}
								for (;;)
								{
									IL_2EA:
									if (!this.isstop)
									{
										if (!this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Register security key", true, this.LDPath))
										{
											this.Trangthai(row1, "Tìm Register security key");
											continue;
										}
										this.Trangthai(row1, "Chọn Register security key");
									}
									for (;;)
									{
										IL_36A:
										if (!this.isstop)
										{
											string noidung3 = this.dump(deviceID, this.LDPath);
											if (this.getbounds(noidung3, deviceID, "GET STARTED", true, this.LDPath))
											{
												this.Trangthai(row1, "Chọn GET STARTED");
											}
											else
											{
												if (this.getbounds(noidung3, deviceID, "Try again", false, this.LDPath))
												{
													goto Block_15;
												}
												this.Trangthai(row1, "Tìm GET STARTED");
												continue;
											}
										}
										while (!this.isstop)
										{
											string noidung4 = this.dump(deviceID, this.LDPath);
											if (this.getbounds(noidung4, deviceID, "Use this device with screen lock", true, this.LDPath))
											{
												this.Trangthai(row1, "Chọn Use this device with screen lock");
												IL_43E:
												while (!this.isstop)
												{
													if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use screen lock", true, this.LDPath))
													{
														this.Trangthai(row1, "Chọn Use screen lock");
														IL_4C4:
														while (!this.isstop)
														{
															if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Verify your identity", false, this.LDPath))
															{
																this.Trangthai(row1, "Nhập Mã khóa màn hình");
																string text = File.ReadAllText("khoamanhinh.txt");
																this.inputtext(this.LDPath, deviceID, text);
																Thread.Sleep(1000);
																Form1.Runcmd(this.LDPath, deviceID, "shell input keyevent 66");
																Thread.Sleep(1000);
																break;
															}
															this.Trangthai(row1, "Tìm Verify your identity");
														}
														int num2 = 0;
														while (!this.isstop)
														{
															string noidung5 = this.dump(deviceID, this.LDPath);
															if (this.getbounds(noidung5, deviceID, "Done", true, this.LDPath))
															{
																goto Block_24;
															}
															if (this.getbounds(noidung5, deviceID, "GET STARTED", false, this.LDPath))
															{
																goto IL_36A;
															}
															if (this.getbounds(noidung5, deviceID, "Register security key", false, this.LDPath))
															{
																if (num2 >= 2)
																{
																	goto IL_2EA;
																}
																num2++;
																Thread.Sleep(1000);
															}
															else
															{
																if (this.getbounds(noidung5, deviceID, "Try again", false, this.LDPath))
																{
																	goto Block_28;
																}
																if (this.getbounds(noidung5, deviceID, "Turn off", false, this.LDPath))
																{
																	break;
																}
																this.Trangthai(row1, "Tìm Done");
															}
														}
														return;
													}
													this.Trangthai(row1, "Tìm Use screen lock");
												}
												goto IL_4C4;
											}
											if (this.getbounds(noidung4, deviceID, "Use security key with USB", false, this.LDPath) && !this.getbounds(noidung4, deviceID, "Use this device with screen lock", false, this.LDPath))
											{
												goto Block_19;
											}
											this.Trangthai(row1, "Tìm Use this device with screen lock");
										}
										goto IL_43E;
									}
								}
								Block_15:
								Form1.Runcmd(this.LDPath, deviceID, "shell input keyevent 4");
								Thread.Sleep(1000);
								return;
								Block_19:
								this.Trangthai(row1, "Không tìm thấy Use this device with screen lock");
								return;
								Block_24:
								this.Trangthai(row1, "Nhấn Done");
								Thread.Sleep(1000);
								return;
								Block_28:
								Form1.Runcmd(this.LDPath, deviceID, "shell input keyevent 4");
								Thread.Sleep(1000);
								return;
							}
							goto IL_2EA;
						}
						this.Trangthai(row1, "Tìm Authentication app");
					}
					goto IL_29E;
				}
				this.swipe(this.LDPath, deviceID, 100, 350, 100, 300);
				Thread.Sleep(1000);
				this.Trangthai(row1, "Tìm Use two-factor authentication");
			}
			goto IL_168;
			Block_1:
			this.Trangthai(row1, "Vào Password and Security");
			goto IL_109;
			Block_2:
			this.Trangthai(row1, "Vào Password and Security");
			goto IL_109;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00005610 File Offset: 0x00003810
		public bool Copy(string deviceID, string uid, int row1)
		{
			int i = 0;
			IL_25B:
			while (i < 2)
			{
				string cop;
				while (!this.isstop)
				{
					if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Copy codes", true, this.LDPath))
					{
						Thread.Sleep(1000);
						IL_C1:
						while (!this.isstop)
						{
							string noidung = this.dump(deviceID, this.LDPath);
							if (this.getbounds(noidung, deviceID, "Done", false, this.LDPath) || this.getbounds(noidung, deviceID, "Recovery codes copied", false, this.LDPath))
							{
								Form1.tap(this.LDPath, deviceID, "150", "440");
								Thread.Sleep(3000);
								break;
							}
						}
						cop = Clipboard.GetText().Replace("\n", " ");
						int num;
						if (cop != "" && cop.Split(new char[]
						{
							' '
						}).Length >= 9)
						{
							num = (cop.Contains("0") ? 1 : 0);
						}
						else
						{
							num = 0;
						}
						if (num != 0)
						{
							string ac = string.Concat(new string[]
							{
								this.dataGridView2.Rows[row1].Cells["uid"].Value.ToString(),
								"|",
								this.dataGridView2.Rows[row1].Cells["pass"].Value.ToString(),
								"|",
								this.dataGridView2.Rows[row1].Cells["ma2fa"].Value.ToString()
							});
							base.Invoke(new Action(delegate()
							{
								File.AppendAllText("SecurityKey.txt", ac + "|" + cop.Replace("\n", " ") + "\r\n");
								this.dataGridView2.Rows[row1].Cells["ma2facode"].Value = cop.Replace("\n", " ");
							}));
							this.Trangthai2(row1, "Get Security Key THÀNH CÔNG");
							this.SaveData();
							return true;
						}
						Clipboard.Clear();
						i++;
						goto IL_25B;
					}
				}
				goto IL_C1;
			}
			this.Trangthai2(row1, "Lỗi không COPY được. Vui lòng đóng LD và chạy lại.");
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000212E File Offset: 0x0000032E
		public Bitmap capturescreen(string deviceID, string LDpath)
		{
			return AdbHelper.capturescreen(deviceID, LDpath);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00005894 File Offset: 0x00003A94
		public Bitmap screenshot(string deviceID, string LDpath)
		{
			string text;
			Bitmap result;
			for (;;)
			{
				text = ((!deviceID.Contains("127")) ? deviceID.Replace("emulator-", "") : deviceID.Replace("127.0.0.1:", ""));
				this.Getstring(LDpath, string.Concat(new string[]
				{
					"adb -s ",
					deviceID,
					" shell screencap -p /sdcard/screenshoot",
					text,
					".jpg\r\nadb -s ",
					deviceID,
					" pull /sdcard/screenshoot",
					text,
					".jpg\r\nadb -s ",
					deviceID,
					" shell rm -f /sdcard/screenshoot",
					text,
					".jpg"
				}));
				string filename = LDpath + "\\screenshoot" + text + ".jpg";
				try
				{
					using (Bitmap bitmap = new Bitmap(filename))
					{
						result = new Bitmap(bitmap);
					}
				}
				catch
				{
					continue;
				}
				break;
			}
			try
			{
				File.Delete(LDpath + "\\screenshoot" + text + ".jpg");
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000059B0 File Offset: 0x00003BB0
		public void swipe(string LDpath, string deviceID, int X, int Y, int X1, int Y1)
		{
			AdbHelper.Runcmd(LDpath, deviceID, string.Concat(new string[]
			{
				"shell input swipe ",
				X.ToString(),
				" ",
				Y.ToString(),
				" ",
				X1.ToString(),
				" ",
				Y1.ToString(),
				" 700"
			}));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005A20 File Offset: 0x00003C20
		public string GetCodeFromMail(string API_Key, string email, string passmail)
		{
			string text = "";
			int num = 0;
			while (!this.isstop)
			{
				try
				{
					for (;;)
					{
						IL_0E:
						using (WebResponse response = WebRequest.Create(string.Concat(new string[]
						{
							"http://fbvip.org/api/ordercode.php?apiKey=",
							API_Key,
							"&type=1&user=",
							email,
							"&pass=",
							passmail
						})).GetResponse())
						{
							using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
							{
								try
								{
									string text2 = streamReader.ReadToEnd();
									if (text2.Contains("thành công"))
									{
										text = JObject.Parse(text2)["id"].ToString();
										for (;;)
										{
											using (WebResponse response2 = WebRequest.Create("http://fbvip.org/api/getcode.php?apiKey=" + API_Key + "&id=" + text).GetResponse())
											{
												using (StreamReader streamReader2 = new StreamReader(response2.GetResponseStream()))
												{
													try
													{
														string text3 = streamReader2.ReadToEnd();
														if (text3.Contains("Đang lấy code"))
														{
															Thread.Sleep(5000);
														}
														else
														{
															text = JObject.Parse(text3)["code"].ToString();
															if (!(text != "") && num != 180)
															{
																num++;
																Thread.Sleep(1000);
																goto IL_0E;
															}
															return text;
														}
													}
													catch
													{
													}
												}
												continue;
											}
											break;
										}
									}
									break;
								}
								catch
								{
								}
							}
							continue;
						}
						break;
					}
				}
				catch
				{
				}
				Thread.Sleep(100);
			}
			return text;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00005C48 File Offset: 0x00003E48
		public void inputtext(string LDpath, string deviceID, string text)
		{
			Form1.Runcmd(LDpath, deviceID, "shell ime set com.android.adbkeyboard/.AdbIME");
			Thread.Sleep(500);
			Form1.Runcmd(LDpath, deviceID, "shell am broadcast -a ADB_CLEAR_TEXT");
			Form1.Runcmd(LDpath, deviceID, Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsIGFtIGJyb2FkY2FzdCAtYSBBREJfSU5QVVRfQjY0IC0tZXMgbXNnICc =")) + Convert.ToBase64String(Encoding.UTF8.GetBytes(text)) + "'");
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00005CAC File Offset: 0x00003EAC
		public string dump(string deviceID, string LDpath)
		{
			int num = 0;
			for (;;)
			{
				string text = this.Getstring(LDpath, string.Concat(new string[]
				{
					"adb -s ",
					deviceID,
					" shell uiautomator dump /sdcard/1.xml\r\nadb -s ",
					deviceID,
					" shell cat /sdcard/1.xml"
				}));
				if (!text.Contains("No such file") && text.Contains("?xml"))
				{
					return text;
				}
				if (num >= 2)
				{
					break;
				}
				num++;
			}
			return "";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00005D1C File Offset: 0x00003F1C
		public bool getbounds(string noidung, string deviceID, string text, bool click, string LDpath)
		{
			int num = 0;
			bool flag;
			string text2;
			string text3;
			for (;;)
			{
				IL_02:
				flag = false;
				text2 = "";
				text3 = "";
				Thread.Sleep(500);
				for (int i = 1; i > 0; i--)
				{
					try
					{
						MatchCollection matchCollection = Regex.Matches(noidung, "<node(.*?)>");
						for (int j = 0; j < matchCollection.Count; j++)
						{
							if (matchCollection[j].ToString().ToLower().Contains(text.ToLower()))
							{
								string value = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Replace("[", "").Split(new char[]
								{
									','
								})[0];
								string value2 = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Replace("]", "").Split(new char[]
								{
									','
								})[1].Split(new char[]
								{
									'['
								})[0];
								string value3 = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Split(new char[]
								{
									'['
								})[2].Split(new char[]
								{
									','
								})[0];
								string value4 = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Split(new char[]
								{
									'['
								})[2].Split(new char[]
								{
									','
								})[1].Replace("]", "");
								text2 = ((Convert.ToInt32(value) + Convert.ToInt32(value3)) / 2).ToString();
								text3 = ((Convert.ToInt32(value2) + Convert.ToInt32(value4)) / 2).ToString();
								flag = true;
								break;
							}
						}
						if (!flag && num == 0)
						{
							num++;
							goto IL_02;
						}
					}
					catch
					{
						goto IL_02;
					}
					Form1.Runcmd(LDpath, deviceID, "shell rm /sdcard/1.xml");
					if (text2 != "" && text3 != "")
					{
						goto Block_3;
					}
					flag = false;
				}
				return flag;
			}
			Block_3:
			if (click)
			{
				AdbHelper.tap(LDpath, deviceID, text2, text3);
			}
			return flag;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00005FA0 File Offset: 0x000041A0
		public bool getboundsByList(string deviceID, List<string> list, bool click, string indexLD, string LDpath)
		{
			int num = 0;
			bool flag;
			string text;
			string text2;
			for (;;)
			{
				IL_02:
				string input = this.dump(deviceID, LDpath).ToLower();
				flag = false;
				text = "";
				text2 = "";
				for (int i = 1; i > 0; i--)
				{
					try
					{
						MatchCollection matchCollection = Regex.Matches(input, "<node(.*?)>");
						for (int j = 0; j < matchCollection.Count; j++)
						{
							foreach (string text3 in list)
							{
								if (matchCollection[j].ToString().ToLower().Contains(text3.ToString().ToLower()))
								{
									string value = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Replace("[", "").Split(new char[]
									{
										','
									})[0];
									string value2 = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Replace("]", "").Split(new char[]
									{
										','
									})[1].Split(new char[]
									{
										'['
									})[0];
									string value3 = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Split(new char[]
									{
										'['
									})[2].Split(new char[]
									{
										','
									})[0];
									string value4 = Regex.Match(matchCollection[j].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Split(new char[]
									{
										'['
									})[2].Split(new char[]
									{
										','
									})[1].Replace("]", "");
									text = ((Convert.ToInt32(value) + Convert.ToInt32(value3)) / 2).ToString();
									text2 = ((Convert.ToInt32(value2) + Convert.ToInt32(value4)) / 2).ToString();
									flag = true;
									break;
								}
							}
						}
						if (!flag && num == 0)
						{
							num++;
							goto IL_02;
						}
					}
					catch
					{
						goto IL_02;
					}
					Form1.Runcmd(LDpath, deviceID, "shell rm /sdcard/" + indexLD + ".xml");
					if (text != "" && text2 != "")
					{
						goto Block_3;
					}
					flag = false;
				}
				return flag;
			}
			Block_3:
			if (click)
			{
				Form1.tap(LDpath, deviceID, text, text2);
			}
			return flag;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002137 File Offset: 0x00000337
		public static void tap(string LDpath, string deviceID, string X, string Y)
		{
			Form1.Runcmd(LDpath, deviceID, "shell input tap " + X + " " + Y);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002151 File Offset: 0x00000351
		public static void Runcmd(string path, string deviceid, string string_0)
		{
			AdbHelper.ADB(path, string_0, deviceid);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000215C File Offset: 0x0000035C
		private void button9_Click(object sender, EventArgs e)
		{
			this.taskStop.Cancel();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000020D2 File Offset: 0x000002D2
		private void button10_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000020D2 File Offset: 0x000002D2
		private void button12_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000020D2 File Offset: 0x000002D2
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00006288 File Offset: 0x00004488
		private void button11_Click(object sender, EventArgs e)
		{
			new DeleteDM().ShowDialog();
			if (DeleteDM.name == null || !(DeleteDM.name != ""))
			{
				return;
			}
			File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\DANHMUC\\" + DeleteDM.name + ".txt");
			base.Invoke(new Action(delegate()
			{
			}));
			MessageBox.Show("Xóa thành công Danh mục : " + DeleteDM.name);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00006318 File Offset: 0x00004518
		private void xÓADÒNGTICKCHỌNToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn chắc chắn muốn XÓA?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
			{
				return;
			}
			foreach (object obj in ((IEnumerable)this.dataGridView2.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if (dataGridViewRow.Cells[0].Value.Equals(true))
				{
					string str = dataGridViewRow.Cells["UID"].Value.ToString();
					try
					{
						if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\DATA\\" + str + ".data"))
						{
							File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\DATA\\" + str + ".data");
						}
					}
					catch
					{
					}
				}
			}
			for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
			{
				if (this.dataGridView2.Rows[i].Cells[0].Value.Equals(true))
				{
					this.dataGridView2.Rows.RemoveAt(i);
					i--;
				}
			}
			this.SaveData();
			MessageBox.Show("Xóa thành công");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000648C File Offset: 0x0000468C
		public void SaveData()
		{
			StringBuilder data = new StringBuilder();
			for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
			{
				string value = string.Concat(new string[]
				{
					this.dataGridView2.Rows[i].Cells[2].Value.ToString(),
					"|",
					this.dataGridView2.Rows[i].Cells[3].Value.ToString(),
					"|",
					this.dataGridView2.Rows[i].Cells[4].Value.ToString()
				});
				data.AppendLine(value);
			}
			base.Invoke(new Action(delegate()
			{
				File.WriteAllText("DATA.txt", data.ToString());
			}));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00006584 File Offset: 0x00004784
		private void tHÊMDỮLIỆUToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string[] array = Clipboard.GetText().Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != "" && array[i] != " ")
					{
						if (array[i].Contains("|"))
						{
							string[] array2 = array[i].Split(new char[]
							{
								'|'
							});
							this.dataGridView2.Rows.Add(new object[]
							{
								true,
								this.dataGridView2.Rows.Count + 1,
								array2[0],
								array2[1],
								""
							});
						}
						else if (array[i].Contains(":"))
						{
							string[] array3 = array[i].Split(new char[]
							{
								':'
							});
							this.dataGridView2.Rows.Add(new object[]
							{
								true,
								this.dataGridView2.Rows.Count + 1,
								array3[0],
								array3[1],
								""
							});
						}
					}
				}
				this.dataGridView2.DoubleBuffered(true);
				this.SaveData();
				return;
			}
			MessageBox.Show("KHÔNG CÓ ACCOUNT TRONG FILE TXT");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000066EC File Offset: 0x000048EC
		private void cOPYDỮLIỆUToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form1.copyac = new StringBuilder();
			for (int i = this.dataGridView2.SelectedRows.Count - 1; i >= 0; i--)
			{
				string text = "";
				for (int j = 2; j < this.dataGridView2.Columns.Count; j++)
				{
					if (j != this.dataGridView2.Columns.Count - 1)
					{
						text = ((this.dataGridView2.SelectedRows[i].Cells[j].Value == null) ? (text + "|") : (text + this.dataGridView2.SelectedRows[i].Cells[j].Value.ToString() + "|"));
					}
					else if (this.dataGridView2.SelectedRows[i].Cells[j].Value != null)
					{
						text += this.dataGridView2.SelectedRows[i].Cells[j].Value.ToString();
					}
				}
				Form1.copyac.AppendLine(text);
			}
			if (Form1.copyac != null && Form1.copyac.ToString() != "")
			{
				new CopyDulieu().ShowDialog();
				return;
			}
			MessageBox.Show("CHƯA CHỌN DỮ LIỆU");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00006858 File Offset: 0x00004A58
		private void xÓADÒNGBÔIĐENToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn chắc chắn muốn XÓA?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
			{
				return;
			}
			foreach (object obj in this.dataGridView2.SelectedRows)
			{
				string str = ((DataGridViewRow)obj).Cells["UID"].Value.ToString();
				try
				{
					if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\DATA\\" + str + ".data"))
					{
						File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\DATA\\" + str + ".data");
					}
				}
				catch
				{
				}
			}
			foreach (object obj2 in this.dataGridView2.SelectedRows)
			{
				DataGridViewBand dataGridViewBand = (DataGridViewBand)obj2;
				this.dataGridView2.Rows.RemoveAt(dataGridViewBand.Index);
			}
			this.SaveData();
			MessageBox.Show("Xóa thành công");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000020D2 File Offset: 0x000002D2
		private void cHUYỂNDANHMỤCToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002169 File Offset: 0x00000369
		private void gET2FACODEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.flag2fa = true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000699C File Offset: 0x00004B9C
		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (this.dataGridView2.Rows.Count <= 0)
			{
				return;
			}
			try
			{
				if (this.dataGridView2.CurrentCell.ColumnIndex == 6 && this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells[6].Value.ToString().Contains("LD"))
				{
					string b = this.dataGridView2.CurrentCell.Value.ToString();
					int rowIndex = this.dataGridView2.CurrentCell.RowIndex;
					for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
					{
						if (i != rowIndex && this.dataGridView2.Rows[i].Cells[0].Value.Equals(true) && this.dataGridView2.Rows[i].Cells[6].Value != null && this.dataGridView2.Rows[i].Cells[6].Value.ToString() == b)
						{
							MessageBox.Show("TRÙNG LD với Account khác");
							this.dataGridView2.Rows[rowIndex].Cells[6].Value = "";
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000020D2 File Offset: 0x000002D2
		private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002172 File Offset: 0x00000372
		private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			int count = this.dataGridView2.Rows.Count;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000020D2 File Offset: 0x000002D2
		private void dataGridView2_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002187 File Offset: 0x00000387
		private void button2_Click_1(object sender, EventArgs e)
		{
			if (!File.Exists("mailphu.txt"))
			{
				File.WriteAllText("mailphu.txt", "");
			}
			Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "mailphu.txt");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000021C3 File Offset: 0x000003C3
		private void tẠOBẢNGMỚIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.dataGridView2.Rows.Clear();
			this.SaveData();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000020D2 File Offset: 0x000002D2
		private void btn_getkey_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000021DB File Offset: 0x000003DB
		private void button3_Click_1(object sender, EventArgs e)
		{
			this.Runcmd2(this.LDPath, "adb kill-server");
			this.Runcmd2(this.LDPath, "adb start-server");
			Thread.Sleep(1000);
			MessageBox.Show("OK");
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002214 File Offset: 0x00000414
		private void button5_Click_1(object sender, EventArgs e)
		{
			if (!File.Exists("MAIL.txt"))
			{
				File.WriteAllText("MAIL.txt", "");
			}
			Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "MAIL.txt");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00006B40 File Offset: 0x00004D40
		public string Getmail()
		{
			object obj = this.obj;
			string result;
			lock (obj)
			{
				List<string> list = File.ReadAllLines("MAIL.txt").ToList<string>();
				if (!list.Any<string>())
				{
					result = "";
				}
				else
				{
					string text = list[0];
					list.Remove(text);
					File.WriteAllLines("MAIL.txt", list.ToArray());
					result = text;
				}
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public string Getmail1()
		{
			object obj = this.obj;
			string result;
			lock (obj)
			{
				List<string> list = File.ReadAllLines("MAILRECOVERY.txt").ToList<string>();
				if (!list.Any<string>())
				{
					result = "";
				}
				else
				{
					string text = list[0];
					list.Remove(text);
					File.WriteAllLines("MAILRECOVERY.txt", list.ToArray());
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00006C40 File Offset: 0x00004E40
		public void LuuChangeAll(string uid, string pass, string ma2fa, string mail, string nameld)
		{
			object obj = this.obj;
			lock (obj)
			{
				try
				{
					if (!File.Exists("CHANGEALL.txt"))
					{
						File.WriteAllText("CHANGEALL.txt", "");
					}
					List<string> list = File.ReadAllLines("CHANGEALL.txt").ToList<string>();
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.ToString().Split(new char[]
							{
								'|'
							})[0] == uid)
							{
								list.Remove(uid);
								break;
							}
						}
					}
					list.Add(string.Concat(new string[]
					{
						uid,
						"|",
						pass,
						"|",
						ma2fa,
						"|",
						mail,
						"|",
						nameld
					}));
					File.WriteAllLines("CHANGEALL.txt", list.ToArray());
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006D6C File Offset: 0x00004F6C
		public void LogOutSession(int i, string deviceID, int index, string packagename, string pathimage)
		{
			for (;;)
			{
				IL_00:
				Form1.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				while (!this.isstop)
				{
					string noidung = this.dump(deviceID, this.LDPath);
					if (this.getbounds(noidung, deviceID, "Security and Login", true, this.LDPath) || this.getbounds(noidung, deviceID, "Password and Security", true, this.LDPath))
					{
						goto IL_59;
					}
					if (num >= 3)
					{
						goto IL_00;
					}
					num++;
					this.Trangthai(i, "Tìm Security and Login");
				}
				break;
			}
			IL_C1:
			while (!this.isstop)
			{
				if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "See all", true, this.LDPath))
				{
					this.Trangthai(i, "Vào See all");
					IL_129:
					while (!this.isstop)
					{
						if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Log Out Of All Sessions", true, this.LDPath))
						{
							this.Trangthai(i, " Log Out Of All Sessions");
							IL_173:
							while (!this.isstop)
							{
								Bitmap screen = this.capturescreen(deviceID, this.LDPath);
								if (this.CheckExistImage(this.LDPath, true, deviceID, "logout.png", screen, pathimage))
								{
									this.Trangthai(i, "Log Out");
									IL_1B8:
									while (!this.isstop)
									{
										if (this.getbounds(this.dump(deviceID, this.LDPath), deviceID, "Use two-factor", false, this.LDPath))
										{
											this.Trangthai(i, "Log Out All Sesion thành công");
											return;
										}
										this.Trangthai(i, "Tìm Use Two");
									}
									return;
								}
								this.Trangthai(i, "Tìm Log Out");
							}
							goto IL_1B8;
						}
						this.swipe(this.LDPath, deviceID, 100, 450, 100, 100);
						Thread.Sleep(1000);
						this.Trangthai(i, "Tìm Log Out Of All Sessions");
					}
					goto IL_173;
				}
				this.Trangthai(i, "Tìm See all");
			}
			goto IL_129;
			IL_59:
			this.Trangthai(i, "Vào Security and Login");
			goto IL_C1;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00006F3C File Offset: 0x0000513C
		public string Bat2faAction(int i, string deviceID, int index, string packagename, string pathimage, CancellationToken ct)
		{
			string text = "";
			for (;;)
			{
				IL_06:
				AdbHelper.Runcmd(this.LDPath, deviceID, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				ct.ThrowIfCancellationRequested();
				while (!ct.IsCancellationRequested)
				{
					string text2 = AdbHelper.dump(deviceID, this.LDPath);
					Bitmap screen = this.capturescreen(deviceID, this.LDPath);
					if (AdbHelper.getbounds(text2, deviceID, "Security and Login", true, this.LDPath) || AdbHelper.getbounds(text2, deviceID, "Password and Security", true, this.LDPath))
					{
						this.Trangthai2(i, "Vào Security and Login");
						break;
					}
					if (num > 30)
					{
						goto IL_06;
					}
					if (!this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen, pathimage) && !this.CheckExistImage(this.LDPath, true, deviceID, "onlyallow.png", screen, pathimage))
					{
						num++;
						this.Trangthai2(i, "Tìm Security and Login " + text2);
					}
				}
				int num2 = 0;
				ct.ThrowIfCancellationRequested();
				while (!ct.IsCancellationRequested)
				{
					string text3 = AdbHelper.dump(deviceID, this.LDPath);
					Bitmap screen2 = this.capturescreen(deviceID, this.LDPath);
					if (this.getbounds(text3, deviceID, "Use two-factor authentication", true, this.LDPath))
					{
						this.Trangthai2(i, "Vào Use two-factor authentication");
						break;
					}
					if (num2 > 10)
					{
						goto IL_06;
					}
					if (!this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen2, pathimage) && !this.CheckExistImage(this.LDPath, true, deviceID, "onlyallow.png", screen2, pathimage))
					{
						num2++;
						this.swipe(this.LDPath, deviceID, 150, 350, 150, 200);
						Thread.Sleep(2000);
						this.Trangthai2(i, "Tìm Use two-factor authentication " + text3);
					}
				}
				for (;;)
				{
					IL_1BE:
					int num3 = 0;
					ct.ThrowIfCancellationRequested();
					while (!ct.IsCancellationRequested)
					{
						Bitmap screen3 = this.capturescreen(deviceID, this.LDPath);
						if (this.CheckExistImage(this.LDPath, true, deviceID, "off.png", screen3, pathimage))
						{
							this.Trangthai2(i, "Tắt 2FA");
							Thread.Sleep(1000);
							ct.ThrowIfCancellationRequested();
							while (!ct.IsCancellationRequested)
							{
								Bitmap screen4 = this.capturescreen(deviceID, this.LDPath);
								if (this.CheckExistImage(this.LDPath, true, deviceID, "off1.png", screen4, pathimage))
								{
									while (!ct.IsCancellationRequested)
									{
										Bitmap screen5 = this.capturescreen(deviceID, this.LDPath);
										if (this.CheckExistImage(this.LDPath, true, deviceID, "pass2fa.png", screen5, pathimage))
										{
											this.Trangthai2(i, "Nhập Password");
											Thread.Sleep(500);
											Thread.Sleep(500);
											this.CheckExistImage(this.LDPath, true, deviceID, "continue2fa1.png", screen5, pathimage);
											Thread.Sleep(500);
										}
										else
										{
											if (this.CheckExistImage(this.LDPath, true, deviceID, "use.png", screen5, pathimage))
											{
												break;
											}
											if (!this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen5, pathimage))
											{
												this.CheckExistImage(this.LDPath, true, deviceID, "onlyallow.png", screen5, pathimage);
											}
										}
									}
									break;
								}
								if (!this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen4, pathimage))
								{
									this.CheckExistImage(this.LDPath, true, deviceID, "onlyallow.png", screen4, pathimage);
								}
							}
						}
						else if (!this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen3, pathimage) && !this.CheckExistImage(this.LDPath, true, deviceID, "onlyallow.png", screen3, pathimage))
						{
							if (this.CheckExistImage(this.LDPath, true, deviceID, "continue2fa2.png", screen3, pathimage))
							{
								this.Trangthai2(i, "Continue");
								break;
							}
							if (num3 > 10)
							{
								goto IL_06;
							}
						}
						num3++;
					}
					ct.ThrowIfCancellationRequested();
					int num4 = 0;
					while (!ct.IsCancellationRequested)
					{
						Bitmap screen6 = this.capturescreen(deviceID, this.LDPath);
						string noidung = this.dump(deviceID, this.LDPath);
						if (this.getbounds(noidung, deviceID, "Set up", false, this.LDPath) || this.getbounds(noidung, deviceID, "Two-factor authentication", false, this.LDPath) || this.CheckExistImage(this.LDPath, false, deviceID, "qr2.png", screen6, pathimage))
						{
							this.Trangthai2(i, "GET QRCODE");
							int num5 = 0;
							for (;;)
							{
								try
								{
									ct.ThrowIfCancellationRequested();
									Thread.Sleep(1000);
									Bitmap bitmap = this.capturescreen(deviceID, this.LDPath);
									Bitmap bitmap2 = bitmap;
									string text4 = new QRCodeDecoder().Decode(new QRCodeBitmapImage(bitmap2));
									if (text4.Contains("secret="))
									{
										bitmap2.Dispose();
										text = Regex.Match(text4, "secret=[0-9a-zA-Z_.%-]{0,}").ToString().Replace("secret=", "");
										Totp totp = new Totp(Base32Encoding.ToBytes(text.Replace(" ", "")), 30, OtpHashMode.Sha1, 6, null);
										string text5;
										do
										{
											ct.ThrowIfCancellationRequested();
											text5 = totp.ComputeTotp();
										}
										while (!(text5 != ""));
										this.CheckExistImage(this.LDPath, true, deviceID, "continue2fa2.png", bitmap, pathimage);
										Thread.Sleep(1000);
										int num6 = 0;
										ct.ThrowIfCancellationRequested();
										while (!ct.IsCancellationRequested)
										{
											Bitmap screen7 = this.capturescreen(deviceID, this.LDPath);
											if (this.CheckExistImage(this.LDPath, true, deviceID, "enter.png", screen7, pathimage))
											{
												for (;;)
												{
													IL_58B:
													Form1.tap(this.LDPath, deviceID, "150", "160");
													Thread.Sleep(500);
													AdbHelper.inputtext(this.LDPath, deviceID, text5);
													Thread.Sleep(500);
													int num7 = 0;
													ct.ThrowIfCancellationRequested();
													while (!ct.IsCancellationRequested)
													{
														Bitmap screen8 = this.capturescreen(deviceID, this.LDPath);
														if (!this.CheckExistImage(this.LDPath, true, deviceID, "continue2fa2.png", screen8, pathimage))
														{
															break;
														}
														if (num7 >= 5)
														{
															Form1.Back(this.LDPath, deviceID, "4");
															break;
														}
														num7++;
														Thread.Sleep(1000);
													}
													Thread.Sleep(1000);
													ct.ThrowIfCancellationRequested();
													while (!ct.IsCancellationRequested)
													{
														Bitmap screen9 = this.capturescreen(deviceID, this.LDPath);
														if (this.CheckExistImage(this.LDPath, true, deviceID, "done.png", screen9, pathimage) || this.CheckExistImage(this.LDPath, false, deviceID, "off.png", screen9, pathimage))
														{
															goto IL_67F;
														}
														if (this.CheckExistImage(this.LDPath, true, deviceID, "enter.png", screen9, pathimage))
														{
															goto IL_58B;
														}
														if (this.CheckExistImage(this.LDPath, true, deviceID, "pass2fa.png", screen9, pathimage))
														{
															this.Trangthai2(i, "Nhập Password");
															Thread.Sleep(500);
															AdbHelper.inputtext(this.LDPath, deviceID, this.dataGridView2.Rows[i].Cells["pass"].Value.ToString());
															Thread.Sleep(500);
															this.CheckExistImage(this.LDPath, true, deviceID, "continue2fa1.png", screen9, pathimage);
															Thread.Sleep(500);
														}
														Thread.Sleep(1000);
													}
													goto Block_35;
												}
												IL_67F:
												this.Trangthai2(i, "Bật 2FA Thành công");
												Block_35:
												break;
											}
											if (this.CheckExistImage(this.LDPath, true, deviceID, "continue2fa2.png", screen7, pathimage))
											{
												if (num6 >= 4)
												{
													this.Trangthai2(i, "Checkpoint");
													return "cp";
												}
												num6++;
											}
											string noidung2 = this.dump(deviceID, this.LDPath);
											if (this.getbounds(noidung2, deviceID, "Please check", false, this.LDPath) || this.getbounds(noidung2, deviceID, "Something", false, this.LDPath))
											{
												this.Trangthai2(i, "Checkpoint");
												return "cp";
											}
											Thread.Sleep(1000);
										}
										return text;
									}
									num5++;
									if (num5 == 5)
									{
										this.Trangthai2(i, "Lỗi QR CODE 2FA...");
										Form1.Back(this.LDPath, deviceID, "4");
										goto IL_1BE;
									}
									bitmap2.Dispose();
									continue;
								}
								catch
								{
									num5++;
									if (num5 >= 5)
									{
										this.Trangthai2(i, "Lỗi QR CODE 2FA...");
										Form1.Back(this.LDPath, deviceID, "4");
										goto IL_1BE;
									}
									continue;
								}
								break;
							}
						}
						if (!this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen6, pathimage) && !this.CheckExistImage(this.LDPath, true, deviceID, "ONLY.png", screen6, pathimage) && num4 >= 4)
						{
							Form1.Back(this.LDPath, deviceID, "4");
							goto IL_1BE;
						}
						num4++;
						Thread.Sleep(1000);
					}
					return text;
				}
			}
			return text;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002250 File Offset: 0x00000450
		public void HamXuLyMain()
		{
			this.HamXuLy_string("Ví dụ ");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000225E File Offset: 0x0000045E
		public string HamXuLy_string(string ThamSo)
		{
			return Regex.Match("Nguyễn Nam Lê", " (.*?) ").Groups[1].Value.ToString();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002284 File Offset: 0x00000484
		public int HamXuLy_Int(int ThamSo)
		{
			return ThamSo + 1;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002289 File Offset: 0x00000489
		public List<string> HamXuLy_List()
		{
			return new List<string>();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002290 File Offset: 0x00000490
		public static void Back(string LDPath, string deviceID, string key)
		{
			AdbHelper.Runcmd(LDPath, deviceID, "shell input keyevent  " + key);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00007810 File Offset: 0x00005A10
		public string Randomst()
		{
			Random random = new Random();
			string text = "abcdefghiklmnopqrstuvwz";
			string text2 = "";
			for (int i = 0; i < 10; i++)
			{
				if (random.Next(0, 2) == 0)
				{
					string str = text2;
					string str2 = text[random.Next(0, text.Length)].ToString();
					text2 = str + str2;
				}
				else
				{
					string str3 = text2;
					string str4 = text[random.Next(0, text.Length)].ToString().ToUpper();
					text2 = str3 + str4;
				}
			}
			return text2;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000022A4 File Offset: 0x000004A4
		private void button6_Click_1(object sender, EventArgs e)
		{
			if (!File.Exists("MAILRECOVERY.txt"))
			{
				File.WriteAllText("MAILRECOVERY.txt", "");
			}
			Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "MAILRECOVERY.txt");
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000022E0 File Offset: 0x000004E0
		public void DisableBtn()
		{
			base.Invoke(new Action(delegate()
			{
				this.button5.Enabled = false;
				this.button5.BackColor = Color.Gray;
				this.btn_stop.Enabled = true;
				this.btn_stop.BackColor = Color.Red;
			}));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000022F5 File Offset: 0x000004F5
		public void EnableBtn()
		{
			base.Invoke(new Action(delegate()
			{
				this.button5.Enabled = true;
				this.button5.BackColor = Color.Green;
				this.btn_stop.Enabled = false;
				this.btn_stop.BackColor = Color.Gray;
			}));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000230A File Offset: 0x0000050A
		private void button7_Click_1(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "OUTPUT\\Success.txt");
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000020D2 File Offset: 0x000002D2
		private void btn_st(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000020D2 File Offset: 0x000002D2
		private void btn_st_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000078A0 File Offset: 0x00005AA0
		private void button5_Click_2(object sender, EventArgs e)
		{
			this.taskStop = new CancellationTokenSource();
			CancellationToken ct = this.taskStop.Token;
			if (!this.Check())
			{
				return;
			}
			this.er = 0;
			this.success = 0;
			Process[] processesByName = Process.GetProcessesByName("chromedriver");
			for (int i = 0; i < processesByName.Length; i++)
			{
				processesByName[i].Kill();
			}
			Task.Factory.StartNew(delegate()
			{
				this.DisableBtn();
				try
				{
					this.Run(ct);
				}
				catch
				{
				}
				this.EnableBtn();
				MessageBox.Show("XONG");
			});
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00007928 File Offset: 0x00005B28
		public void Run(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested && Form1.listmailphu.Any<string>() && this.querow2.Any<DataGridViewRow>())
			{
				Form1.tasks.Add(Task.Factory.StartNew(delegate()
				{
					try
					{
						this.Action(ct, this.querow2.Dequeue().Index);
					}
					catch
					{
					}
				}, ct));
				Task.Delay(200, ct).Wait();
			}
			Task.WhenAll(Form1.tasks).Wait();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000079BC File Offset: 0x00005BBC
		public ChromeDriver khaibaochrome()
		{
			ChromeOptions chromeOptions = new ChromeOptions();
			ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
			chromeDriverService.HideCommandPromptWindow = true;
			chromeOptions.AddArguments(new string[]
			{
				"--no-sandbox"
			});
			chromeOptions.AddArgument("--disable-notifications");
			chromeOptions.AddArgument("--disable-web-security");
			chromeOptions.AddArgument("--disable-translate");
			chromeOptions.AddArgument("--disable-notifications");
			chromeOptions.AddArgument("--disable-blink-features");
			chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
			chromeOptions.AddArgument("--disable-infobars");
			chromeOptions.AddArgument("--ignore-certificate-errors");
			chromeOptions.AddArgument("--allow-running-insecure-content");
			chromeOptions.AddArgument("--lang=vi");
			chromeOptions.AddUserProfilePreference("useAutomationExtension", true);
			return new ChromeDriver(chromeDriverService, chromeOptions);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00007A78 File Offset: 0x00005C78
		public void Action(CancellationToken ct, int i)
		{
			string email = this.dataGridView2.Rows[i].Cells["uid"].Value.ToString();
			string pass = this.dataGridView2.Rows[i].Cells["pass"].Value.ToString();
			Action <>9__0;
			Action <>9__1;
			Action <>9__2;
			Action <>9__3;
			ChromeDriver chromeDriver;
			for (;;)
			{
				IL_6D:
				chromeDriver = null;
				try
				{
					chromeDriver = this.khaibaochrome();
					ct.ThrowIfCancellationRequested();
					this.Trangthai2(i, "Mở Chrome");
					chromeDriver.Navigate().GoToUrl("https://www.gmx.com/#.1559516-header-navlogin2-1");
					int num = 0;

					while (!ct.IsCancellationRequested)
					{
						try
						{
							if (chromeDriver.FindElement(By.XPath("//*[@class='permission-core-iframe']")).Displayed)
							{
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.XPath("//*[@class='permission-core-iframe']")));
								try
								{
									chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
								}
								catch
								{
								}
								try
								{
									chromeDriver.FindElement(By.Id("onetrust-pc-btn-handler")).Click();
								}
								catch
								{
								}
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.XPath("/html/body/iframe")));
								try
								{
									chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
								}
								catch
								{
								}
								try
								{
									chromeDriver.FindElement(By.Id("onetrust-pc-btn-handler")).Click();
								}
								catch
								{
								}
								try
								{
									chromeDriver.FindElement(By.Id("accept-recommended-btn-handler")).Click();
								}
								catch
								{
								}
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.XPath("/html/body/iframe")));
							}
						}
						catch
						{
						}
						
						try
						{
							chromeDriver.FindElement(By.XPath("/html/body/div/div[2]/div/footer/div[1]/div[1]/button")).Click();
							Task.Delay(500, ct).Wait();
						}
						catch
						{
						}
						try
						{
							chromeDriver.FindElement(By.XPath("/html/body/div/div[2]/div/footer/div[1]/div[1]/button")).Click();
							Task.Delay(500, ct).Wait();
						}
						catch
						{
						}
						try
						{
							chromeDriver.FindElement(By.XPath("/html/body/div/div[2]/div/footer/div[1]/div[1]/button")).Click();
							Task.Delay(500, ct).Wait();
						}
						catch
						{
						}
						
						try
						{
							try
							{
								chromeDriver.SwitchTo().DefaultContent();
							}
							catch
							{
							}
							chromeDriver.FindElement(By.Name("username")).SendKeys(email);
							Task.Delay(500, ct).Wait();
							chromeDriver.FindElement(By.Name("password")).SendKeys(pass);
							Task.Delay(500, ct).Wait();
							this.Trangthai2(i, "Nhập Mail/Pass");
							chromeDriver.FindElements(By.XPath("//*[@type='submit']"))[0].Click();
							Task.Delay(2500, ct).Wait();
							this.Trangthai2(i, "Login");
							break;
						}
						catch
						{
						}

						try
						{
							if (chromeDriver.FindElements(By.XPath("//*[@data-item-name='mail']")).Count > 0)
							{
								break;
							}
						}
						catch
						{
						}

						if (num >= 20)
						{
							this.CloseChrome(chromeDriver);
							goto IL_6D;
						}
						num++;

						Task.Delay(1000, ct).Wait();
						try
						{
							chromeDriver.SwitchTo().DefaultContent();
						}
						catch
						{
						}
					}

					int num2 = 0;
					while (!ct.IsCancellationRequested)
					{
						if (chromeDriver.PageSource.Contains("Passwort vergessen"))
						{
							this.Trangthai2(i, "Sai Password");
							this.dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
							Action method;
							if ((method = <>9__0) == null)
							{
								method = (<>9__0 = delegate()
								{
									File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "OUTPUT\\Saipass.txt", email + "|" + pass + "\r\n");
								});
							}
							base.Invoke(method);
							goto IL_100A;
						}
						if (chromeDriver.PageSource.Contains("Vorsorgliche Sicherheitssperre"))
						{
							this.Trangthai2(i, "Die");
							this.dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
							Action method2;
							if ((method2 = <>9__1) == null)
							{
								method2 = (<>9__1 = delegate()
								{
									File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "OUTPUT\\Die.txt", email + "|" + pass + "\r\n");
								});
							}
							base.Invoke(method2);
							goto IL_100A;
						}
						try
						{
							chromeDriver.FindElement(By.XPath("//*[@data-item-name='mail']")).Click();
							this.Trangthai2(i, "Vào Email");
							break;
						}
						catch
						{
						}
						goto IL_433;
						IL_789:
						while (!ct.IsCancellationRequested)
						{
							try
							{
								chromeDriver.SwitchTo().DefaultContent();
							}
							catch
							{
							}
							try
							{
								if (chromeDriver.WindowHandles.Count > 1)
								{
									chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.First<string>());
									break;
								}
								Task.Delay(1000, ct).Wait();
								string url = chromeDriver.Url;
								chromeDriver.ExecuteScript("window.open();", Array.Empty<object>());
								chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.Last<string>());
								chromeDriver.Navigate().GoToUrl(url);
								Task.Delay(1000, ct).Wait();
								chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.First<string>());
								break;
							}
							catch
							{
							}
							continue;

							for (;;)
							{
								IL_8CA: // click setting / MailCollectorLink
								if (!ct.IsCancellationRequested)
								{
									try
									{
										chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("mail")));
										chromeDriver.FindElement(By.XPath("//*[@data-webdriver='FolderNavigation:MailCollectorLink']")).Click();
										this.Trangthai2(i, "Vào Email 1");
										goto IL_A0C;
									}
									catch
									{
									}
									try
									{
										chromeDriver.SwitchTo().DefaultContent();
									}
									catch
									{
									}
									try
									{
										chromeDriver.FindElement(By.XPath("//*[@class='ftd-box-promote_close icon delete']")).Click();
									}
									catch
									{
									}
									try
									{
										chromeDriver.FindElement(By.XPath("//*[@data-item-name='mail']")).Click();
										this.Trangthai2(i, "Vào Email");
									}
									catch
									{
									}
									try
									{
										if (chromeDriver.FindElement(By.Name("permission_dialog")).Displayed)
										{
											chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("permission_dialog")));
											chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Id("permission-iframe")));
											try
											{
												chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
											}
											catch
											{
											}
											chromeDriver.FindElement(By.XPath("//*[text()='Zustimmen und weiter']")).Click();
										}
									}
									catch
									{
									}
									try
									{
										chromeDriver.SwitchTo().DefaultContent();
									}
									catch
									{
									}
									continue;
								}
								IL_A0C:
								while (!ct.IsCancellationRequested)
								{
									try
									{
										chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("mail")));
										chromeDriver.FindElement(By.XPath("//*[text()='Alias Addresses']")).Click();
										this.Trangthai2(i, "Vào Email 2");
										break;
									}
									catch
									{
									}
									try
									{
										chromeDriver.SwitchTo().DefaultContent();
									}
									catch
									{
									}
									try
									{
										chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("mail")));
										chromeDriver.FindElement(By.XPath("//*[@data-webdriver='FolderNavigation:MailCollectorLink']")).Click();
										this.Trangthai2(i, "Vào Email 1");
									}
									catch
									{
									}
									try
									{
										if (chromeDriver.FindElement(By.Name("permission_dialog")).Displayed)
										{
											chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("permission_dialog")));
											chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Id("permission-iframe")));
											try
											{
												chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
											}
											catch
											{
											}
											chromeDriver.FindElement(By.XPath("//*[text()='Zustimmen und weiter']")).Click();
										}
									}
									catch
									{
									}
									try
									{
										chromeDriver.SwitchTo().DefaultContent();
									}
									catch
									{
									}
								}
								for (;;)
								{
									IL_A18:
									string text = "";
									string text2 = "";
									while (!ct.IsCancellationRequested)
									{
										try
										{
											chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("mail")));
											if (chromeDriver.FindElement(By.XPath("//*[@class='form-element form-element-textfield textfield']")).Displayed)
											{
												while (!ct.IsCancellationRequested)
												{
													try
													{
														if (chromeDriver.FindElements(By.XPath("//*[@class='table_field table_col-12']")).Count > 2)
														{
															this.Trangthai2(i, "Tìm thấy email để xóa");
															using (IEnumerator<IWebElement> enumerator = chromeDriver.FindElements(By.XPath("//*[@class='table_field table_col-12']")).GetEnumerator())
															{
																IL_C2F:
																while (enumerator.MoveNext())
																{
																	IWebElement webElement = enumerator.Current;
																	if (!webElement.Text.Contains("Default sender address") && webElement.Text.Contains("@"))
																	{
																		Actions actions = new Actions(chromeDriver);
																		actions.MoveToElement(webElement).Perform();
																		Task.Delay(1500, ct).Wait();
																		this.Trangthai2(i, "Tìm xóa");
																		int num3 = 0;
																		while (!ct.IsCancellationRequested)
																		{
																			try
																			{
																				ReadOnlyCollection<IWebElement> readOnlyCollection = chromeDriver.FindElements(By.XPath("//*[@class='table-hover_icon icon-link']"));
																				if (readOnlyCollection.Count > 0)
																				{
																					readOnlyCollection[1].Click();
																					this.Trangthai2(i, "Click xóa 1");
																					break;
																				}
																			}
																			catch
																			{
																				this.Trangthai2(i, "Lỗi xóa 1");
																			}
																			goto IL_B66;
																			IL_C26:
																			while (!ct.IsCancellationRequested)
																			{
																				try
																				{
																					chromeDriver.FindElement(By.XPath("//*[@data-webdriver='primary']")).Click();
																					Task.Delay(3500, ct).Wait();
																					goto IL_C5C;
																				}
																				catch
																				{
																					this.Trangthai2(i, "Tìm xóa 2");
																				}
																				if (num3 >= 10)
																				{
																					chromeDriver.Navigate().Refresh();
																					goto IL_8CA;
																				}
																				num3++;
																				Task.Delay(500, ct).Wait();
																			}
																			goto IL_C2F;
																			IL_B66:
																			Task.Delay(500, ct).Wait();
																			try
																			{
																				actions.MoveToElement(webElement).Perform();
																			}
																			catch
																			{
																			}
																			if (num3 >= 10)
																			{
																				chromeDriver.Navigate().Refresh();
																				goto IL_8CA;
																			}
																			num3++;
																			Task.Delay(500, ct).Wait();
																		}
																		goto IL_C26;
																	}
																}
																continue;
															}
															break;
															continue;
														}
														break;
													}
													catch
													{
													}
												}
												IL_C5C:
												this.Trangthai2(i, "Get Mail Phụ");
												text2 = this.Getmailphu().Replace("\t", "");
												this.Trangthai2(i, "Get Mail Phụ: " + text2);
												if (text2.Contains("@"))
												{
													text = text2.Split(new char[]
													{
														'@'
													})[0];
												}
												string text3 = text2.Split(new char[]
												{
													'@'
												})[1];
												chromeDriver.FindElements(By.XPath("//*[@class='form-element form-element-textfield textfield']"))[0].Clear();
												chromeDriver.FindElements(By.XPath("//*[@class='form-element form-element-textfield textfield']"))[0].SendKeys(text.Trim());
												Task.Delay(500, ct).Wait();
												chromeDriver.FindElements(By.XPath("//*[@class='form-element form-element-select']"))[0].SendKeys(text3.Replace("\t", ""));
												Task.Delay(500, ct).Wait();
												chromeDriver.FindElements(By.XPath("//*[@class='m-button button-secondary button-size-normal']"))[0].Click();
												Task.Delay(2500, ct).Wait();
												break;
											}
										}
										catch
										{
											if (Form1.listmailphu.Count == 0)
											{
												this.Trangthai2(i, "Hết Mail Phụ");
												goto IL_100A;
											}
											this.Trangthai2(i, "Không tìm thấy email để xóa");
										}
										goto IL_DB3;
										IL_FF9:
										while (!ct.IsCancellationRequested)
										{
											if (chromeDriver.PageSource.Contains("You have been logged"))
											{
												chromeDriver.Navigate().Refresh();
												this.Trangthai2(i, "Login Lại");
												goto IL_A18;
											}
											try
											{
												if (chromeDriver.FindElements(By.XPath("//*[@class='table_field table_col-12']")).Count > 2)
												{
													Task.Delay(500, ct).Wait();
													this.Trangthai2(i, "Add Mail Phụ Thành công");
													string a = this.GetCode(ct, i, text2.Trim(), chromeDriver);
													chromeDriver.Close();
													if (a == "ok")
													{
														Task.Delay(2500, ct).Wait();
														ct.ThrowIfCancellationRequested();
														this.Trangthai2(i, "Check code");
														string text4 = this.getcodechrome(chromeDriver, i, ct, text2);
														if (text4 != "")
														{
															string ok = text2 + "|" + text4;
															this.success++;
															base.Invoke(new Action(delegate()
															{
																File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "OUTPUT\\Success.txt", ok + "\r\n");
																this.lb_ok.Text = "Thành công: " + this.success.ToString();
															}));
															this.addrich(ok);
														}
													}
													while (!ct.IsCancellationRequested)
													{
														try
														{
															chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.First<string>());
															break;
														}
														catch
														{
														}
													}
													goto IL_A18;
												}
											}
											catch
											{
											}
											try
											{
												if (chromeDriver.PageSource.Contains("ist nicht verfügbar"))
												{
													this.Trangthai2(i, "Mail phụ đã tồn tại");
													this.er++;
													Action method3;
													if ((method3 = <>9__2) == null)
													{
														method3 = (<>9__2 = delegate()
														{
															this.lb_loi.Text = "Lỗi: " + this.er.ToString();
														});
													}
													base.Invoke(method3);
													goto IL_A18;
												}
												if (chromeDriver.PageSource.Contains("is not available"))
												{
													this.Trangthai2(i, "Mail phụ is not available");
													this.er++;
													Action method4;
													if ((method4 = <>9__3) == null)
													{
														method4 = (<>9__3 = delegate()
														{
															this.lb_loi.Text = "Lỗi: " + this.er.ToString();
														});
													}
													base.Invoke(method4);
													goto IL_A18;
												}
											}
											catch
											{
											}
										}
										goto Block_50;
										IL_DB3:
										try
										{
											chromeDriver.SwitchTo().DefaultContent();
										}
										catch
										{
										}
										try
										{
											chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles.First<string>());
										}
										catch
										{
										}
									}
									goto IL_FF9;
								}
							}
							Block_50:
							goto IL_100A;
						}
						goto IL_8CA;
						IL_433:
						try
						{
							if (chromeDriver.FindElement(By.Name("permission_dialog")).Displayed)
							{
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("permission_dialog")));
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Id("permission-iframe")));
								try
								{
									chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
								}
								catch
								{
								}
								chromeDriver.FindElement(By.XPath("//*[text()='Zustimmen und weiter']")).Click();
							}
						}
						catch
						{
						}
						try
						{
							chromeDriver.SwitchTo().DefaultContent();
						}
						catch
						{
						}
						try
						{
							if (chromeDriver.FindElements(By.XPath("//*[@class='captcha__image']")).Count > 0)
							{
								this.Trangthai2(i, "Dính Captcha");
								this.CloseChrome(chromeDriver);
								return;
							}
						}
						catch
						{
						}
						try
						{
							if (chromeDriver.FindElement(By.Name("splash_nav")).Displayed)
							{
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.Name("splash_nav")));
								chromeDriver.FindElement(By.XPath("//*[@class='l inactive service-hover show-text close icon']")).Click();
							}
						}
						catch
						{
						}
						try
						{
							chromeDriver.SwitchTo().DefaultContent();
						}
						catch
						{
						}
						try
						{
							if (chromeDriver.FindElement(By.XPath("//*[@class='permission-core-iframe']")).Displayed)
							{
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.XPath("//*[@class='permission-core-iframe']")));
								try
								{
									chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
								}
								catch
								{
								}
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.XPath("/html/body/iframe")));
								try
								{
									chromeDriver.FindElement(By.XPath("//*[@class='btn btn-secondary ghost']")).Click();
								}
								catch
								{
								}
								chromeDriver.SwitchTo().Frame(chromeDriver.FindElement(By.XPath("/html/body/iframe")));
							}
						}
						catch
						{
						}
						try
						{
							chromeDriver.FindElement(By.XPath("/html/body/div/div[2]/div/footer/div[1]/div[1]/button")).Click();
							Task.Delay(500, ct).Wait();
						}
						catch
						{
						}
						try
						{
							chromeDriver.FindElement(By.XPath("/html/body/div/div[2]/div/footer/div[1]/div[1]/button")).Click();
							Task.Delay(500, ct).Wait();
						}
						catch
						{
						}
						try
						{
							chromeDriver.FindElement(By.XPath("/html/body/div/div[2]/div/footer/div[1]/div[1]/button")).Click();
							Task.Delay(500, ct).Wait();
						}
						catch
						{
						}
						try
						{
							chromeDriver.SwitchTo().DefaultContent();
						}
						catch
						{
						}
						if (num2 >= 20)
						{
							this.CloseChrome(chromeDriver);
							goto IL_6D;
						}
						num2++;
						Task.Delay(1000, ct).Wait();
					}
					goto IL_789;
				}
				catch
				{
				}
				break;
			}
			IL_100A:
			this.CloseChrome(chromeDriver);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00008FAC File Offset: 0x000071AC
		public void addrich(string ok)
		{
			object obj = this.obj;
			lock (obj)
			{
				try
				{
					base.Invoke(new Action(delegate()
					{
						if (this.rich_ok.Text.Length >= 99999999)
						{
							this.rich_ok.Clear();
						}
						this.rich_ok.AppendText(ok + "\r\n");
					}));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000901C File Offset: 0x0000721C
		public string getcodechrome(ChromeDriver driver, int i, CancellationToken ct, string mailphu)
		{
			int j = 0;
			IL_367:
			while (!ct.IsCancellationRequested)
			{
				try
				{
					driver.SwitchTo().Window(driver.WindowHandles.Last<string>());
					IL_353:
					while (j < 20)
					{
						driver.Navigate().Refresh();
						Task.Delay(1000, ct).Wait();
						int num = 0;
						while (!ct.IsCancellationRequested)
						{
							try
							{
								driver.SwitchTo().Frame(driver.FindElement(By.Name("mail")));
								driver.FindElement(By.XPath("//*[contains(@class,'refresh navigation-tool-icon-link')]")).Click();
								Task.Delay(1000, ct).Wait();
							}
							catch
							{
							}
							try
							{
								driver.SwitchTo().DefaultContent();
							}
							catch
							{
							}
							try
							{
								driver.SwitchTo().Frame(driver.FindElement(By.Name("mail")));
								ReadOnlyCollection<IWebElement> readOnlyCollection = driver.FindElements(By.XPath("//*[contains(@class,'name')]"));
								ReadOnlyCollection<IWebElement> readOnlyCollection2 = driver.FindElements(By.XPath("//*[contains(@class,'subject')]"));
								if (readOnlyCollection[0].Text.Contains("Facebook") && 
									Regex.Match(readOnlyCollection2[0].Text, "(\\d+)").Groups[1].Value.ToString().Length == 6)
								{
									readOnlyCollection[0].Click();
									Task.Delay(1000, ct).Wait();
									break;
								}
							}
							catch
							{
							}
							try
							{
								driver.SwitchTo().DefaultContent();
							}
							catch
							{
							}
							if (num >= 10)
							{
								goto IL_353;
							}
							num++;
							Task.Delay(1000, ct).Wait();
						}
						int num2 = 0;
						while (!ct.IsCancellationRequested)
						{
							try
							{
								driver.SwitchTo().DefaultContent();
							}
							catch
							{
							}
							try
							{
								driver.SwitchTo().Frame(driver.FindElement(By.Name("mail")));
								driver.SwitchTo().Frame(driver.FindElement(By.Name("mail-display-content")));
								ReadOnlyCollection<IWebElement> readOnlyCollection3 = driver.FindElements(By.XPath("//*[contains(@href,'deref-gmx.com/mail/client/')]"));
								if (readOnlyCollection3.Count > 0)
								{
									Task.Delay(1000, ct).Wait();
									if (!driver.PageSource.Contains(mailphu.Split(new char[]
									{
										'@'
									})[0]))
									{
										this.Trangthai2(i, "Không tìm thấy " + mailphu);
										Task.Delay(1000, ct).Wait();
										j++;
										goto IL_353;
									}
									string attribute = readOnlyCollection3[readOnlyCollection3.Count - 1].GetAttribute("href");
									string str = Regex.Match(attribute, "cancel%2F%3Fn%3D(.*?)%").Groups[1].Value.ToString();
									string text = Regex.Match(attribute, "id%3D(.*?)%").Groups[1].Value.ToString();
									if (text != "")
									{
										try
										{
											driver.SwitchTo().DefaultContent();
										}
										catch
										{
										}
										driver.SwitchTo().Frame(driver.FindElement(By.Name("mail")));
										driver.FindElement(By.Id("toolbarButtonDelete")).Click();
										return text + "|" + str;
									}
								}
							}
							catch
							{
							}
							try
							{
								driver.SwitchTo().DefaultContent();
							}
							catch
							{
							}
							if (num2 >= 20)
							{
								goto IL_353;
							}
							num2++;
							Task.Delay(1000, ct).Wait();
						}
						goto IL_367;
					}
					return "";
				}
				catch
				{
				}
			}
			return "";
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00009480 File Offset: 0x00007680
		public string GetCode(CancellationToken ct, int i, string mailphu, ChromeDriver driver)
		{
			driver.ExecuteScript("window.open();", Array.Empty<object>());
			driver.SwitchTo().Window(driver.WindowHandles.Last<string>());
			this.dataGridView2.Rows[i].Cells["uid"].Value.ToString();
			this.dataGridView2.Rows[i].Cells["pass"].Value.ToString();
			driver.Navigate().GoToUrl("https://www.facebook.com/login/identify/?ctx=recover&ars=facebook_login&from_login_screen=0");
			try
			{
				this.Trangthai(i, "Vào trang Recovery");
				int num = 0;
				while (!ct.IsCancellationRequested)
				{
					try
					{
						driver.FindElements(By.Name("email"))[1].Clear();
						driver.FindElements(By.Name("email"))[1].SendKeys(mailphu);
						Task.Delay(1000, ct).Wait();
						driver.FindElement(By.Name("did_submit")).Click();
						Task.Delay(1000, ct).Wait();
						this.Trangthai(i, "Tìm kiếm");
						break;
					}
					catch
					{
					}
					try
					{
						ReadOnlyCollection<IWebElement> readOnlyCollection = driver.FindElements(By.XPath("//*[@type='submit']"));
						Task.Delay(1000, ct).Wait();
						readOnlyCollection[2].Click();
					}
					catch
					{
					}
					if (num >= 60)
					{
						this.Trangthai(i, "Timeout");
						this.RedRow(i);
						return "";
					}
					num++;
					Task.Delay(1000, ct).Wait();
				}
				int num2 = 0;
				while (!ct.IsCancellationRequested)
				{
					try
					{
						if (driver.FindElement(By.Name("recover_method")).Displayed)
						{
							driver.FindElements(By.Name("reset_action"))[1].Click();
							Task.Delay(1000, ct).Wait();
							this.Trangthai(i, "Tiếp tục");
							break;
						}
						this.Trangthai(i, "Không có liên kết facebook");
						this.RedRow(i);
						return "";
					}
					catch
					{
					}

					try
					{
						if (driver.FindElement(By.Name("email")).Displayed)
						{
							this.Trangthai(i, "Không có liên kết facebook");
							this.RedRow(i);
							return "";
						}
					}
					catch
					{
					}

					try
					{
						driver.FindElement(By.Name("reset_action")).Click();
						Task.Delay(1000, ct).Wait();
						this.Trangthai(i, "Tiếp tục");
						break;
					}
					catch
					{
					}

					if (num2 >= 60)
					{
						this.Trangthai(i, "Timeout");
						this.RedRow(i);
						return "";
					}
					num2++;
					Task.Delay(1000, ct).Wait();
				}
				
				int num3 = 0;
				while (!ct.IsCancellationRequested)
				{
					try
					{
						if (driver.FindElement(By.Id("recovery_code_entry")).Displayed)
						{
							return "ok";
						}
					}
					catch
					{
					}
					if (num3 >= 60)
					{
						this.Trangthai(i, "Timeout");
						this.RedRow(i);
						return "";
					}
					num3++;
					Task.Delay(1000, ct).Wait();
				}
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000987C File Offset: 0x00007A7C
		public void CloseChrome(ChromeDriver driver)
		{
			try
			{
				driver.Quit();
				driver.Dispose();
			}
			catch
			{
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000232B File Offset: 0x0000052B
		public void GreenRow(int i)
		{
			this.dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000234D File Offset: 0x0000054D
		public void RedRow(int i)
		{
			this.dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000098AC File Offset: 0x00007AAC
		public void BatKhoamanhinh(string index, string uid, int row1)
		{
			this.Trangthai2(row1, "Bắt đầu Bật Khóa Màn Hình");
			for (;;)
			{
				IL_0C:
				AdbHelper.Runcmd(this.LDPath, index, "shell am start -W -a android.intent.action.VIEW -d fb://settings");
				Thread.Sleep(2000);
				int num = 0;
				while (!this.isstop)
				{
					string noidung = AdbHelper.dump(index, this.LDPath);
					if (AdbHelper.getbounds(noidung, index, "Password and Security", true, this.LDPath))
					{
						goto Block_1;
					}
					if (AdbHelper.getbounds(noidung, index, "Security and Login", true, this.LDPath))
					{
						goto Block_2;
					}
					if (!AdbHelper.getbounds(noidung, index, "Only allow", true, this.LDPath))
					{
						if (num >= 13)
						{
							goto IL_0C;
						}
						num++;
						this.Trangthai2(row1, "Tìm Password and Security");
					}
				}
				break;
			}
			IL_13C:
			while (!this.isstop)
			{
				string noidung2 = AdbHelper.dump(index, this.LDPath);
				if (AdbHelper.getbounds(noidung2, index, "Use two-factor authentication", true, this.LDPath))
				{
					this.Trangthai2(row1, "Vào Use two-factor authentication");
					IL_1B5:
					while (!this.isstop)
					{
						string noidung3 = AdbHelper.dump(index, this.LDPath);
						if (AdbHelper.getbounds(noidung3, index, "Authentication app", false, this.LDPath))
						{
							AdbHelper.swipe(this.LDPath, index, 100, 350, 100, 50);
							Thread.Sleep(1000);
							IL_302:
							while (!this.isstop)
							{
								string noidung4 = AdbHelper.dump(index, this.LDPath);
								if (AdbHelper.getbounds(noidung4, index, "Security key", true, this.LDPath))
								{
									this.Trangthai2(row1, "Chọn Security key");
									Thread.Sleep(1000);
									AdbHelper.getbounds(noidung4, index, "Continue", true, this.LDPath);
									this.Trangthai2(row1, "Continue");
									Thread.Sleep(1000);
								}
								else if (AdbHelper.getbounds(noidung4, index, "Add a backup method", false, this.LDPath))
								{
									AdbHelper.swipe(this.LDPath, index, 100, 350, 100, 50);
									Thread.Sleep(1000);
									AdbHelper.tap(this.LDPath, index, "150", "420");
									this.Trangthai2(row1, "Chọn Security key");
									Thread.Sleep(1000);
									AdbHelper.getbounds(noidung4, index, "Continue", true, this.LDPath);
									this.Trangthai2(row1, "Continue");
									Thread.Sleep(1000);
								}
								else
								{
									if (!AdbHelper.getbounds(noidung4, index, "Only allow", true, this.LDPath))
									{
										AdbHelper.swipe(this.LDPath, index, 100, 350, 100, 50);
										Thread.Sleep(1000);
										this.Trangthai2(row1, "Tìm Security key");
										continue;
									}
									continue;
								}
								for (;;)
								{
									IL_366:
									if (!this.isstop)
									{
										string noidung5 = AdbHelper.dump(index, this.LDPath);
										if (AdbHelper.getbounds(noidung5, index, "Register security key", true, this.LDPath))
										{
											this.Trangthai2(row1, "Chọn Register security key");
										}
										else
										{
											if (!AdbHelper.getbounds(noidung5, index, "Only allow", true, this.LDPath))
											{
												this.Trangthai2(row1, "Tìm Register security key");
												continue;
											}
											continue;
										}
									}
									for (;;)
									{
										IL_400:
										if (!this.isstop)
										{
											string noidung6 = AdbHelper.dump(index, this.LDPath);
											if (AdbHelper.getbounds(noidung6, index, "GET STARTED", true, this.LDPath))
											{
												this.Trangthai2(row1, "Chọn GET STARTED");
											}
											else
											{
												if (AdbHelper.getbounds(noidung6, index, "Try again", false, this.LDPath))
												{
													goto Block_20;
												}
												if (!AdbHelper.getbounds(noidung6, index, "Only allow", true, this.LDPath))
												{
													this.Trangthai2(row1, "Tìm GET STARTED");
													continue;
												}
												continue;
											}
										}
										while (!this.isstop)
										{
											string noidung7 = AdbHelper.dump(index, this.LDPath);
											if (AdbHelper.getbounds(noidung7, index, "Use this device with screen lock", true, this.LDPath))
											{
												this.Trangthai2(row1, "Chọn Use this device with screen lock");
												IL_4A8:
												while (!this.isstop)
												{
													if (AdbHelper.getbounds(AdbHelper.dump(index, this.LDPath), index, "Use screen lock", true, this.LDPath))
													{
														this.Trangthai2(row1, "Chọn Use screen lock");
														IL_52B:
														while (!this.isstop)
														{
															if (AdbHelper.getbounds(AdbHelper.dump(index, this.LDPath), index, "Verify your identity", false, this.LDPath))
															{
																this.Trangthai2(row1, "Nhập Mã khóa màn hình");
																string text = File.ReadAllText("khoamanhinh.txt");
																AdbHelper.inputtext(this.LDPath, index, text);
																Thread.Sleep(1000);
																AdbHelper.Runcmd(this.LDPath, index, "shell input keyevent 66");
																Thread.Sleep(1000);
																break;
															}
															this.Trangthai2(row1, "Tìm Verify your identity");
														}
														int num2 = 0;
														while (!this.isstop)
														{
															string noidung8 = AdbHelper.dump(index, this.LDPath);
															if (AdbHelper.getbounds(noidung8, index, "Done", true, this.LDPath))
															{
																goto Block_29;
															}
															if (AdbHelper.getbounds(noidung8, index, "GET STARTED", false, this.LDPath))
															{
																goto IL_400;
															}
															if (AdbHelper.getbounds(noidung8, index, "Register security key", false, this.LDPath))
															{
																if (num2 >= 2)
																{
																	goto IL_366;
																}
																num2++;
																Thread.Sleep(1000);
															}
															else
															{
																if (AdbHelper.getbounds(noidung8, index, "Try again", false, this.LDPath))
																{
																	goto Block_33;
																}
																if (this.getbounds(noidung8, index, "Turn off", false, this.LDPath))
																{
																	break;
																}
																this.Trangthai2(row1, "Tìm Done");
															}
														}
														goto IL_623;
													}
													this.Trangthai2(row1, "Tìm Use screen lock");
												}
												goto IL_52B;
											}
											if (!AdbHelper.getbounds(noidung7, index, "Only allow", true, this.LDPath))
											{
												this.Trangthai2(row1, "Tìm Use this device with screen lock");
											}
										}
										goto IL_4A8;
									}
								}
								Block_20:
								AdbHelper.Runcmd(this.LDPath, index, "shell input keyevent 4");
								Thread.Sleep(1000);
								goto IL_623;
								Block_29:
								this.Trangthai2(row1, "Nhấn Done");
								Thread.Sleep(1000);
								goto IL_623;
								Block_33:
								AdbHelper.Runcmd(this.LDPath, index, "shell input keyevent 4");
								Thread.Sleep(1000);
								IL_623:
								string text2 = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\";
								while (!this.isstop)
								{
									Bitmap screen = this.capturescreen(index, this.LDPath);
									if (this.CheckExistImage(this.LDPath, true, index, "recovery.png", screen, text2))
									{
										this.Trangthai2(row1, "Nhấn Recovery codes");
										Thread.Sleep(1000);
										IL_775:
										while (!this.isstop)
										{
											string noidung9 = this.dump(index, this.LDPath);
											Bitmap screen2 = this.capturescreen(index, this.LDPath);
											if (this.getbounds(noidung9, index, "Copy codes", false, this.LDPath))
											{
												this.Trangthai2(row1, "Nhấn Copy codes");
												object obj = this.obj;
												lock (obj)
												{
													if (!this.Copy(index, uid, row1))
													{
														return;
													}
												}
												Thread.Sleep(1000);
												break;
											}
											if (this.CheckExistImage(this.LDPath, true, index, "showcode.png", screen2, text2))
											{
												this.Trangthai2(row1, "Nhấn Show codes");
												Thread.Sleep(1000);
											}
											else
											{
												this.Trangthai2(row1, "Tìm Copy codes");
											}
										}
										this.Trangthai2(row1, "Dăng xuất");
										this.Runcmd(index, this.LDPath, "", " shell am force-stop com.facebook.katana");
										Thread.Sleep(1000);
										return;
									}
									this.swipe(this.LDPath, index, 100, 350, 100, 250);
									Thread.Sleep(2000);
									this.Trangthai2(row1, "Tìm Recovery codes");
								}
								goto IL_775;
							}
							goto IL_366;
						}
						if (!AdbHelper.getbounds(noidung3, index, "Only allow", true, this.LDPath))
						{
							this.Trangthai2(row1, "Tìm Authentication app");
						}
					}
					goto IL_302;
				}
				if (!AdbHelper.getbounds(noidung2, index, "Only allow", true, this.LDPath))
				{
					AdbHelper.swipe(this.LDPath, index, 100, 350, 100, 300);
					Thread.Sleep(1000);
					this.Trangthai2(row1, "Tìm Use two-factor authentication");
				}
			}
			goto IL_1B5;
			Block_1:
			this.Trangthai2(row1, "Vào Password and Security");
			goto IL_13C;
			Block_2:
			this.Trangthai2(row1, "Vào Password and Security");
			goto IL_13C;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000236F File Offset: 0x0000056F
		private void button6_Click_2(object sender, EventArgs e)
		{
			AdbHelper.sort(this.LDPath);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000020D2 File Offset: 0x000002D2
		private void txt_pass_TextChanged_1(object sender, EventArgs e)
		{
		}

		// Token: 0x04000004 RID: 4
		private string LDPath = "";

		// Token: 0x04000005 RID: 5
		private Queue<DataGridViewRow> querow = new Queue<DataGridViewRow>();

		// Token: 0x04000006 RID: 6
		private Queue<DataGridViewRow> querow2 = new Queue<DataGridViewRow>();

		// Token: 0x04000007 RID: 7
		private bool themmail;

		// Token: 0x04000008 RID: 8
		private bool flag2fa;

		// Token: 0x04000009 RID: 9
		public List<string> cbbs = new List<string>();

		// Token: 0x0400000A RID: 10
		public List<string> listdvid = new List<string>();

		// Token: 0x0400000B RID: 11
		private Queue<string> quemail = new Queue<string>();

		// Token: 0x0400000C RID: 12
		private List<Thread> listthread = new List<Thread>();

		// Token: 0x0400000D RID: 13
		public List<string> TextCheckpoint = new List<string>
		{
			"xác nhận danh tính",
			"your account is temporarily unavailable",
			"your account is temporarily locked",
			"your account has been disabled",
			"tài khoản của bạn tạm thời bị khóa",
			"ngày khóa tài khoản",
			"\"learn more\"",
			"download your information",
			"go to community standards",
			"choose a security check",
			"provide your birthday",
			"identify photos of friends",
			"get a code sent to your email",
			"been locked",
			"your account has been locked",
			"enter number",
			"enter the code",
			"let us know",
			"check the login details shown. was it you?"
		};

		// Token: 0x0400000E RID: 14
		private object obj = new object();

		// Token: 0x0400000F RID: 15
		private bool isstop;

		// Token: 0x04000010 RID: 16
		public static StringBuilder copyac;

		// Token: 0x04000011 RID: 17
		public static string cbbx;

		// Token: 0x04000012 RID: 18
		private bool spamip;

		// Token: 0x04000013 RID: 19
		private object ol = new object();

		// Token: 0x04000014 RID: 20
		private static List<Task> tasks = new List<Task>();

		// Token: 0x04000015 RID: 21
		private CancellationTokenSource taskStop = new CancellationTokenSource();

		// Token: 0x04000016 RID: 22
		private Random rd = new Random();

		// Token: 0x04000017 RID: 23
		private Queue<string> queproxy = new Queue<string>();

		// Token: 0x04000018 RID: 24
		private Queue<string> queyoutube = new Queue<string>();

		// Token: 0x04000019 RID: 25
		private string pathimage = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\";

		// Token: 0x0400001A RID: 26
		private static List<string> listmailphu = new List<string>();

		// Token: 0x0400001B RID: 27
		private string packagename = "com.facebook.katana";

		// Token: 0x0400001C RID: 28
		private int success;

		// Token: 0x0400001D RID: 29
		private int er;

		// Token: 0x0400002C RID: 44
		private DataGridViewTextBoxColumn email;

		// Token: 0x0400002D RID: 45
		private DataGridViewTextBoxColumn code;
	}
}
