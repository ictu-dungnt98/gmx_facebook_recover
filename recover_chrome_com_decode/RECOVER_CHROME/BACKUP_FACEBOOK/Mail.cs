using System;
using System.Text.RegularExpressions;
using System.Threading;
using AE.Net.Mail;

namespace BACKUP_FACEBOOK
{
	// Token: 0x02000012 RID: 18
	public static class Mail
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000AE7C File Offset: 0x0000907C
		public static string Verify(string email, string pass, string ipmap, int port, bool check)
		{
			string text = "";
			int num = 0;
			using (ImapClient imapClient = new ImapClient())
			{
				for (;;)
				{
					imapClient.Connect(ipmap, port, true, false);
					for (;;)
					{
						try
						{
							imapClient.Login(email, pass);
							break;
						}
						catch
						{
							return "";
						}
					}
					imapClient.SelectMailbox("INBOX");
					if (check)
					{
						break;
					}
					int i;
					for (i = imapClient.GetMessageCount(); i < 2; i = imapClient.GetMessageCount())
					{
						Thread.Sleep(1000);
						imapClient.SelectMailbox("INBOX");
					}
					MailMessage[] messages = imapClient.GetMessages(i - 5, i - 1, false, false);
					foreach (MailMessage mailMessage in messages)
					{
						string body = mailMessage.Body;
						bool flag = body.Contains("www.facebook.com");
						if (flag)
						{
							text = Regex.Match(body, "c=(.*?)&").Groups[1].Value.ToString().Replace(" ", "").Split(new char[]
							{
								'&'
							})[0];
						}
						else
						{
							bool flag2 = body.Contains(" de confirmación:");
							if (flag2)
							{
								text = Regex.Match(body, " de confirmación:(.*).Confirmar").Groups[1].Value.ToString().Replace(" ", "");
							}
							else
							{
								bool flag3 = body.Contains("Confirm");
								if (flag3)
								{
									int num2 = body.IndexOf("code:\r\n\r\n") + "code:\r\n\r\n".Length;
									int num3 = body.LastIndexOf("\r\n\r\nFacebook");
									text = body.Substring(num2, num3 - num2);
								}
								else
								{
									bool flag4 = body.Contains("FB-");
									if (flag4)
									{
										int num4 = body.IndexOf("FB-") + "FB-".Length;
										int num5 = body.LastIndexOf("\r\n\r\nFacebook");
										text = body.Substring(num4, num5 - num4);
									}
									else
									{
										bool flag5 = body.Contains("Mã bảo mật của bạn là:");
										if (flag5)
										{
											int num6 = body.IndexOf("Mã bảo mật của bạn là:") + "Mã bảo mật của bạn là:".Length;
											int num7 = body.LastIndexOf(" Để");
											text = body.Substring(num6, num7 - num6);
										}
										else
										{
											bool flag6 = body.Contains("Your security code is:");
											if (flag6)
											{
												int num8 = body.IndexOf("Your security code is: ") + "Your security code is: ".Length;
												int num9 = body.LastIndexOf(" To help");
												text = body.Substring(num8, num9 - num8);
											}
											else
											{
												MatchCollection matchCollection = Regex.Matches(body, "(\\d+)");
												text = matchCollection[0].Groups[1].Value.ToString();
											}
										}
									}
								}
							}
						}
					}
					imapClient.Dispose();
					bool flag7 = text == "";
					if (!flag7)
					{
						goto IL_341;
					}
					bool flag8 = num == 30;
					if (flag8)
					{
						goto Block_14;
					}
					Thread.Sleep(1000);
					num++;
				}
				bool flag9 = imapClient.GetMessageCount() == 0;
				if (flag9)
				{
					text = "Locked";
				}
				else
				{
					text = "Live";
				}
				Block_14:
				IL_341:;
			}
			return text;
		}
	}
}
