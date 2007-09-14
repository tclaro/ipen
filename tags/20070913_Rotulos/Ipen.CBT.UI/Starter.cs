using System;

namespace CBT
{
	/// <summary>
	/// Summary description for Starter.
	/// </summary>
	public abstract class Starter
	{
		[STAThread]
		static void Main() 
		{
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new frmPrincipal());
        }
	}
}
