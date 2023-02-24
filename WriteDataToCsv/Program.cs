using JohnsonControlHelper;
using System;
using System.Windows.Forms;

namespace WriteDataToCsv;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new formMain());
	}
}
