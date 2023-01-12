using System;
using System.Reflection;
using System.Windows.Forms;

namespace BACKUP_FACEBOOK
{
	// Token: 0x02000013 RID: 19
	public static class ExtensionMethods
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000B214 File Offset: 0x00009414
		public static void DoubleBuffered(this DataGridView dgv, bool setting)
		{
			Type type = dgv.GetType();
			PropertyInfo property = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			property.SetValue(dgv, setting, null);
		}
	}
}
