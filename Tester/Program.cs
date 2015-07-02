#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : Tester
// ////////  File Name : Program.cs
// ////////  Created On : 2014/02/01
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace Tester
{
    internal static class Program
    {
        #region //*** Other Members ***\\

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Global.Initialize();
            Application.Run(new FrmTester());
        }

        #endregion
    }
}