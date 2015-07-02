#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : FrmRestartService.cs
// ////////  Created On : 2014/02/13
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace InkMonGUI
{
    public partial class FrmRestartService : Form
    {
        #region //*** Constructors/Deconstructor ***\\

        public FrmRestartService()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
        }

        #endregion

        #region //*** Other Members ***\\

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ServiceHelper.RestartService("InkMonService", 3*60000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblWait.Visible = !lblWait.Visible;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}