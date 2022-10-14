using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.VisualBasic.ApplicationServices;

namespace AutoScanCompare
{
    static class Program
    {

		//private static Mutex mutex = null;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
			/*
			const string appName = "MyAppName";
			bool createdNew;

			mutex = new Mutex(true, appName, out createdNew);

			if (!createdNew)
			{
				//app is already running! Exiting the application  
				MessageBox.Show("Test");
				
				return;
			}*/
			//System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			string[] args = Environment.GetCommandLineArgs();

			if (args.Count() <= 1)
                Application.Run(new Form1());
            else
			{
				SingleInstanceController controller = new SingleInstanceController();
				controller.Run(args);
				//Application.Run(new Form2(args));
			}
		}
    }


	public class SingleInstanceController : WindowsFormsApplicationBase
	{
		public SingleInstanceController()
		{
			IsSingleInstance = true;

			StartupNextInstance += this_StartupNextInstance;
		}

		void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
		{
			Form2 form = MainForm as Form2; //My derived form type
			form.LoadNewFile(e.CommandLine.ToArray());
		}

		protected override void OnCreateMainForm()
		{
			MainForm = new Form2();
		}
	}

}
