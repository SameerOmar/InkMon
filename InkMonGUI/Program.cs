#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : Program.cs
// ////////  Created On : 2014/01/31
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI
{
    internal static class Program
    {
        #region //*** Other Members ***\\

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, Assembly.GetExecutingAssembly().FullName, out createdNew))
            {
                if (createdNew)
                {


                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Global.Initialize();

                    var settings = Settings.LoadFromFile(Global.SettingsFileName);
                    if (string.IsNullOrWhiteSpace(settings.Customer.RefCode))
                    {
                        var frm = new FrmWizard(false);
                        frm.CurrentSettings = settings;
                        frm.CurrentSettings.Printers.Clear();
                        frm.NewSettings = Settings.LoadFromFile(Global.SettingsFileName);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            frm.NewSettings.SaveToFile(Global.SettingsFileName);
                            var frmService = new FrmRestartService();
                            frmService.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You must get your reference code to start this application.");
                            return;
                        }
                    }
                    Application.ThreadException += Application_ThreadException;
                    Application.Run(new FrmMain());
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        #endregion
    }
}