#region Copyright Syncfusion Inc. 2001 - 2016
// Copyright Syncfusion Inc. 2001 - 2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Spring.AccioHelpers;
using Spring.View.MainView.LoginView;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Spring
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@31392e342e30VWUcNs2WlbEH2rOhqfAsaLLoQ60+yWpw1tdxvkTqCfg=");

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);


            //Application.Run(new DockingManagerForm());

            if ((string)AccioEasyHelpers.GetReadValFromConfigXML("platform") == "forms")
            {
                //check if the thread of the app is already running avoid multiple exe at same time!
                PragmaChecker.UniqueEXERun();
            }
            else
            {
                //support for virtui [thinUI]
                new Cybele.Thinfinity.VirtualUI().Start();
                Application.Run(new LoginForm());

            }
        }

    }

    static class PragmaChecker
    {
        public static void UniqueEXERun()
        {
            Mutex mutex = new System.Threading.Mutex(false, "ACCIO_FORM_RUNNING");
            try
            {
                if (mutex.WaitOne(0, false))
                {
                    //we should test if data file is there or not if not let there be a message telling the user !
                    try
                    {
                        var data = AccioEasyHelpers.ReadTxTFiles(AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring.exe").Length) + "init\\params.info");
                    }
                    catch { MessageBox.Show("The data file is missing or files in there not exist."); }

                  

                    //run the actual class [represents form main form]
                    Application.Run(new LoginForm());
                   // Application.Run(new DockingManagerForm());

                }
                else
                {
                    MessageBox.Show("An instance of the application is already running!", "No possible to open another window", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                    mutex = null;
                }
            }
        }
    }
}
