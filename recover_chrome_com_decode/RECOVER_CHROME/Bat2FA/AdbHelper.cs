using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using KAutoHelper;

namespace Bat2FA
{
	// Token: 0x02000023 RID: 35
	internal class AdbHelper
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000E40C File Offset: 0x0000C60C
		public static List<string> GetDevice(string LDPath)
		{
			List<string> list = new List<string>();
			try
			{
				string text = AdbHelper.Getstring(LDPath, "adb devices");
				bool flag = text != "";
				if (flag)
				{
					string[] array = text.Split(new string[]
					{
						Environment.NewLine
					}, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array.Length; i++)
					{
						bool flag2 = array[i].Contains("\tdevice");
						if (flag2)
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

		// Token: 0x060000D6 RID: 214 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public static string Getstring(string path, string string_0)
		{
			string result = "";
			for (int i = 0; i < 25; i++)
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
					result = process.StandardOutput.ReadToEnd();
					process.Dispose();
					GC.Collect();
					break;
				}
				catch (Exception ex)
				{
				}
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public static string ADB(string path, string string_0, string index)
		{
			string result = "";
			for (int i = 0; i < 25; i++)
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
					process.StandardInput.WriteLine(string.Concat(new string[]
					{
						"ldconsole.exe adb --index ",
						index,
						"  --command \"",
						string_0,
						"\""
					}));
					process.StandardInput.Flush();
					process.StandardInput.Close();
					result = process.StandardOutput.ReadToEnd();
					process.Dispose();
					GC.Collect();
					break;
				}
				catch (Exception ex)
				{
				}
			}
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public static void Runcmd2(string path, string string_0)
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
			catch
			{
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000266E File Offset: 0x0000086E
		public static void Runcmd(string path, string index, string string_0)
		{
			AdbHelper.ADB(path, string_0, index);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000E764 File Offset: 0x0000C964
		public static string CheckActivity(string index, string LDPath)
		{
			return AdbHelper.ADB(LDPath, " shell dumpsys activity activities | grep mResumedActivity", index);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000E784 File Offset: 0x0000C984
		public static Task<string> CheckActivityTask(string deviceID, string LDPath)
		{
			return Task.Run<string>(() => AdbHelper.Getstring(LDPath, "adb -s " + deviceID + " shell \"dumpsys activity activities | grep mResumedActivity\""));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000267A File Offset: 0x0000087A
		public static void AddProxy(string LDPath, string deviceID, string proxy)
		{
			AdbHelper.Runcmd(LDPath, deviceID, "shell settings put global http_proxy " + proxy);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002690 File Offset: 0x00000890
		public static void Back(string LDPath, string deviceID, string key)
		{
			AdbHelper.Runcmd(LDPath, deviceID, "shell input keyevent " + key);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000026A6 File Offset: 0x000008A6
		public static void QuitLD(string LDPath, int index)
		{
			AdbHelper.Runcmd2(LDPath, "ldconsole.exe quit --index " + index.ToString());
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000026C1 File Offset: 0x000008C1
		public static void QuitAllLD(string LDPath)
		{
			AdbHelper.Runcmd2(LDPath, "ldconsole.exe quitall");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000026D0 File Offset: 0x000008D0
		public static void OpenApp(string LDPath, string index, string packagename)
		{
			AdbHelper.Runcmd2(LDPath, "ldconsole.exe runapp --index " + index + " --packagename " + packagename);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000026EB File Offset: 0x000008EB
		public static void ClearDataApp(string LDPath, string index, string packagename)
		{
			AdbHelper.Runcmd(LDPath, index, "shell pm clear " + packagename);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002701 File Offset: 0x00000901
		public static void ForceApp(string LDPath, string index, string packagename)
		{
			AdbHelper.Runcmd(LDPath, index, "shell am force-stop " + packagename);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public static bool GetScreenResolution(string index, string LDpath)
		{
			string text = AdbHelper.ADB(LDpath, "shell dumpsys display | grep \"mCurrentDisplayRect\"", index);
			return text.Contains("mCurrentDisplayRect");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		public static bool CheckRunning(string index, string LDpath)
		{
			bool result = true;
			string text = AdbHelper.Getstring(LDpath, "ldconsole.exe isrunning --index " + index);
			bool flag = text.Contains("stop");
			if (flag)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002717 File Offset: 0x00000917
		public static void ResetServer(string LDpath)
		{
			AdbHelper.Runcmd2(LDpath, "adb kill-server");
			AdbHelper.Runcmd2(LDpath, "adb start-server");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000E82C File Offset: 0x0000CA2C
		public static string CheckDevices(string deviceID, string deviceID2, int indexLD, string LDpath)
		{
			string result = "";
			try
			{
				string text;
				bool flag;
				do
				{
					text = AdbHelper.Getstring(LDpath, "adb devices");
					flag = (text.Contains(deviceID) || text.Contains(deviceID2));
				}
				while (!flag);
				Thread.Sleep(2000);
				bool flag2 = text == null;
				if (flag2)
				{
					return "";
				}
				string value = LDpath.Substring(0, 2);
				int num = text.IndexOf("List of devices attached ") + "List of devices attached ".Length;
				int num2 = text.LastIndexOf(value);
				string text2 = text.Substring(num, num2 - num);
				text2 = text2.Replace("\t", "");
				string[] array = text2.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = array[i].Contains(deviceID + "device");
					if (flag3)
					{
						result = deviceID;
						break;
					}
					bool flag4 = array[i].Contains(deviceID2 + "device");
					if (flag4)
					{
						result = deviceID2;
						break;
					}
					bool flag5 = array[i].Contains(deviceID + "offline");
					if (flag5)
					{
						break;
					}
					bool flag6 = array[i].Contains(deviceID2 + "offline");
					if (flag6)
					{
						break;
					}
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public static List<string> GetListindex(string LDPath)
		{
			List<string> list = new List<string>();
			try
			{
				string text = AdbHelper.Getstring(LDPath, "ldconsole.exe runninglist");
				bool flag = text != "";
				if (flag)
				{
					string[] array = text.Trim().Split(new string[]
					{
						Environment.NewLine
					}, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array.Length; i++)
					{
						bool flag2 = array[i].Contains("LDPlayer-");
						if (flag2)
						{
							list.Add(array[i].Replace("LDPlayer-", ""));
						}
						else
						{
							bool flag3 = array[i] == "LDPlayer";
							if (flag3)
							{
								list.Add("0");
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public static string CheckDevices1(string deviceID, string deviceID2, int indexLD, string LDpath)
		{
			string result = "";
			try
			{
				string text = AdbHelper.Getstring(LDpath, "adb devices");
				bool flag = text.Contains(deviceID) || text.Contains(deviceID2);
				if (flag)
				{
				}
				bool flag2 = text == null;
				if (flag2)
				{
					return "";
				}
				string value = LDpath.Substring(0, 2);
				int num = text.IndexOf("List of devices attached ") + "List of devices attached ".Length;
				int num2 = text.LastIndexOf(value);
				string text2 = text.Substring(num, num2 - num);
				text2 = text2.Replace("\t", "");
				string[] array = text2.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = array[i].Contains(deviceID + "device");
					if (flag3)
					{
						result = deviceID;
						break;
					}
					bool flag4 = array[i].Contains(deviceID2 + "device");
					if (flag4)
					{
						result = deviceID2;
						break;
					}
					bool flag5 = array[i].Contains(deviceID + "offline");
					if (flag5)
					{
						break;
					}
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public static List<string> CheckIndex(string LDpath)
		{
			List<string> list = new List<string>();
			try
			{
				string text = AdbHelper.Getstring(LDpath, "ldconsole.exe list2");
				string[] array = text.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag = array[i].Contains(",") && !array[i].Contains("LDPlayer,");
					if (flag)
					{
						string item = array[i].Split(new char[]
						{
							','
						})[0];
						list.Add(item);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public static bool CheckIMAGEFOLDER(string ịndex, string path, string LDPath)
		{
			string text = AppDomain.CurrentDomain.BaseDirectory + "IMAGE\\" + path;
			Bitmap screen = AdbHelper.capturescreen(ịndex, LDPath);
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
				bool flag = AdbHelper.CheckExistImage(LDPath, false, ịndex, bmp, screen, text + "\\");
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		public static bool Checkpackage(string path, string string_0, string index, string package)
		{
			return AdbHelper.ADB(path, string_0, index).Contains(package);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		public static Bitmap capturescreen(string index, string LDpath)
		{
			Bitmap bitmap;
			bool flag;
			do
			{
				bitmap = AdbHelper.screenshot(index, LDpath);
				flag = (bitmap == null);
			}
			while (flag);
			return bitmap;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public static Bitmap screenshot(string index, string LDpath)
		{
			Bitmap result;
			for (;;)
			{
				AdbHelper.ADB(LDpath, string.Concat(new string[]
				{
					" shell screencap -p /sdcard/screenshoot",
					index,
					".jpg\r\nldconsole.exe adb --index ",
					index,
					"  --command \"pull /sdcard/screenshoot",
					index,
					".jpg\"\r\nldconsole.exe adb --index ",
					index,
					"  --command \"shell rm -f /sdcard/screenshoot",
					index,
					".jpg\""
				}), index);
				string filename = LDpath + "\\screenshoot" + index + ".jpg";
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
				File.Delete(LDpath + "\\screenshoot" + index + ".jpg");
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public static Task<bool> CheckExistImageTask(string LDpath, bool click, string deviceID, string bmp, Bitmap screen, string pathimage)
		{
			return Task.Run<bool>(delegate()
			{
				bool result;
				try
				{
					Bitmap subBitmap = (Bitmap)Image.FromFile(pathimage + bmp);
					Point? point = ImageScanOpenCV.FindOutPoint(screen, subBitmap, 0.9);
					bool flag = point != null;
					if (flag)
					{
						bool click2 = click;
						if (click2)
						{
							AdbHelper.tap(LDpath, deviceID, (point.Value.X + 3).ToString(), point.Value.Y.ToString());
						}
						result = true;
					}
					else
					{
						result = false;
					}
				}
				catch
				{
					result = false;
				}
				return result;
			});
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public static bool CheckExistImage(string LDpath, bool click, string deviceID, string bmp, Bitmap screen, string pathimage)
		{
			bool result;
			try
			{
				Bitmap subBitmap = (Bitmap)Image.FromFile(pathimage + bmp);
				Point? point = ImageScanOpenCV.FindOutPoint(screen, subBitmap, 0.9);
				bool flag = point != null;
				if (flag)
				{
					if (click)
					{
						AdbHelper.tap(LDpath, deviceID, (point.Value.X + 3).ToString(), point.Value.Y.ToString());
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		public static string dump(string index, string LDpath)
		{
			int num = 0;
			string text = "127.0.0.1:" + (Convert.ToInt32(index) * 2 + 5555).ToString();
			string text2 = "emulator-" + (Convert.ToInt32(index) * 2 + 5554).ToString();
			string text3;
			for (;;)
			{
				text3 = AdbHelper.ADB(LDpath, "shell uiautomator dump /sdcard/1.xml\r\nldconsole.exe adb --index " + index + "  --command \"shell cat /sdcard/1.xml\"", index);
				bool flag = text3.Contains("No such file") || !text3.Contains("?xml");
				if (!flag)
				{
					goto IL_A4;
				}
				bool flag2 = num >= 2;
				if (flag2)
				{
					break;
				}
				num++;
			}
			return "";
			IL_A4:
			return text3;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000F070 File Offset: 0x0000D270
		public static string gettextbounds(string noidung, string deviceID, string text, bool click, string LDpath)
		{
			int num = 0;
			string text2;
			string text3;
			for (;;)
			{
				IL_03:
				bool flag = false;
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
							bool flag2 = matchCollection[j].ToString().ToLower().Contains(text.ToLower());
							if (flag2)
							{
								return Regex.Match(matchCollection[j].ToString(), "text=\"(.*?)\"").Groups[1].Value.ToString();
							}
						}
						bool flag3 = !flag;
						if (flag3)
						{
							bool flag4 = num == 0;
							if (flag4)
							{
								num++;
								goto IL_03;
							}
						}
					}
					catch
					{
						goto IL_03;
					}
					AdbHelper.Runcmd(LDpath, deviceID, "shell rm /sdcard/1.xml");
					bool flag5 = text2 != "" && text3 != "";
					if (flag5)
					{
						goto Block_3;
					}
					flag = false;
				}
				goto IL_138;
			}
			Block_3:
			if (click)
			{
				AdbHelper.tap(LDpath, deviceID, text2, text3);
			}
			IL_138:
			return "";
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		public static Task<bool> getboundsTask(string noidung, string deviceID, string text, bool click, string LDpath)
		{
			return Task.Run<bool>(delegate()
			{
				bool flag = false;
				string text2 = "";
				string text3 = "";
				Thread.Sleep(500);
				for (int i = 1; i > 0; i--)
				{
					try
					{
						MatchCollection matchCollection = Regex.Matches(noidung, "<node(.*?)>");
						for (int j = 0; j < matchCollection.Count; j++)
						{
							bool flag2 = matchCollection[j].ToString().ToLower().Contains(text.ToLower());
							if (flag2)
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
						bool flag3 = !flag;
						if (flag3)
						{
							return false;
						}
					}
					catch
					{
						return false;
					}
					AdbHelper.Runcmd(LDpath, deviceID, "shell rm /sdcard/1.xml");
					bool flag4 = text2 != "" && text3 != "";
					if (flag4)
					{
						bool click2 = click;
						if (click2)
						{
							AdbHelper.tap(LDpath, deviceID, text2, text3);
						}
						break;
					}
					flag = false;
				}
				return flag;
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000F220 File Offset: 0x0000D420
		public static bool getbounds(string noidung, string index, string text, bool click, string LDpath)
		{
			bool flag = false;
			string text2 = "";
			string text3 = "";
			try
			{
				MatchCollection matchCollection = Regex.Matches(noidung, "<node(.*?)>");
				for (int i = 0; i < matchCollection.Count; i++)
				{
					bool flag2 = matchCollection[i].ToString().ToLower().Contains(text.ToLower());
					if (flag2)
					{
						string value = Regex.Match(matchCollection[i].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Replace("[", "").Split(new char[]
						{
							','
						})[0];
						string value2 = Regex.Match(matchCollection[i].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Replace("]", "").Split(new char[]
						{
							','
						})[1].Split(new char[]
						{
							'['
						})[0];
						string value3 = Regex.Match(matchCollection[i].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Split(new char[]
						{
							'['
						})[2].Split(new char[]
						{
							','
						})[0];
						string value4 = Regex.Match(matchCollection[i].ToString(), "bounds=\"(.*?)\"").Groups[1].Value.Split(new char[]
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
				bool flag3 = !flag;
				if (flag3)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
			AdbHelper.Runcmd(LDpath, index, "shell rm /sdcard/1.xml");
			bool flag4 = text2 != "" && text3 != "";
			bool result;
			if (flag4)
			{
				if (click)
				{
					AdbHelper.tap(LDpath, index, text2, text3);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		public static bool getbounds2(string noidung, string deviceID, string text, bool click, string LDpath)
		{
			int num = 0;
			bool flag;
			string text2;
			string text3;
			for (;;)
			{
				IL_03:
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
							bool flag2 = matchCollection[j].ToString().ToLower().Contains(text.ToLower());
							if (flag2)
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
								text2 = ((Convert.ToInt32(value) + Convert.ToInt32(value3)) / 2 + 150).ToString();
								text3 = ((Convert.ToInt32(value2) + Convert.ToInt32(value4)) / 2).ToString();
								flag = true;
								break;
							}
						}
						bool flag3 = !flag;
						if (flag3)
						{
							bool flag4 = num == 0;
							if (flag4)
							{
								num++;
								goto IL_03;
							}
						}
					}
					catch
					{
						goto IL_03;
					}
					AdbHelper.Runcmd(LDpath, deviceID, "shell rm /sdcard/1.xml");
					bool flag5 = text2 != "" && text3 != "";
					if (flag5)
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

		// Token: 0x060000F5 RID: 245 RVA: 0x0000F788 File Offset: 0x0000D988
		public static bool getboundsByList(string deviceID, List<string> list, bool click, string indexLD, string LDpath)
		{
			int num = 0;
			bool flag;
			string text;
			string text2;
			for (;;)
			{
				IL_03:
				string input = AdbHelper.dump(deviceID, LDpath).ToLower();
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
								bool flag2 = matchCollection[j].ToString().ToLower().Contains(text3.ToString().ToLower());
								if (flag2)
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
						bool flag3 = !flag;
						if (flag3)
						{
							bool flag4 = num == 0;
							if (flag4)
							{
								num++;
								goto IL_03;
							}
						}
					}
					catch
					{
						goto IL_03;
					}
					AdbHelper.Runcmd(LDpath, deviceID, "shell rm /sdcard/" + indexLD + ".xml");
					bool flag5 = text != "" && text2 != "";
					if (flag5)
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
				AdbHelper.tap(LDpath, deviceID, text, text2);
			}
			return flag;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002732 File Offset: 0x00000932
		public static void tap(string LDpath, string deviceID, string X, string Y)
		{
			AdbHelper.Runcmd(LDpath, deviceID, "shell input tap " + X + " " + Y);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		public static void swipe(string LDpath, string deviceID, int X, int Y, int X1, int Y1)
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
				Y1.ToString()
			}));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000FB20 File Offset: 0x0000DD20
		public static void inputtext(string LDpath, string deviceID, string text)
		{
			AdbHelper.Runcmd(LDpath, deviceID, "shell ime set com.android.adbkeyboard/.AdbIME");
			Thread.Sleep(500);
			AdbHelper.Runcmd(LDpath, deviceID, "shell am broadcast -a ADB_CLEAR_TEXT");
			AdbHelper.Runcmd(LDpath, deviceID, Encoding.UTF8.GetString(Convert.FromBase64String("IHNoZWxsIGFtIGJyb2FkY2FzdCAtYSBBREJfSU5QVVRfQjY0IC0tZXMgbXNnICc =")) + Convert.ToBase64String(Encoding.UTF8.GetBytes(text)) + "'");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000274E File Offset: 0x0000094E
		public static void sort(string LDpath)
		{
			AdbHelper.Runcmd2(LDpath, "ldconsole.exe sortWnd");
		}
	}
}
