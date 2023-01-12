using System;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK
{
	// Token: 0x02000014 RID: 20
	internal static class Program
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000024FE File Offset: 0x000006FE
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
