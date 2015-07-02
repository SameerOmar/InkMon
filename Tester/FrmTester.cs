#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : Tester
// ////////  File Name : FrmTester.cs
// ////////  Created On : 2014/02/01
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Windows.Forms;
using HPUSBGeneric;
using InkMonHelper;

#endregion

namespace Tester
{
    public partial class FrmTester : Form
    {
        #region //*** Constructors/Deconstructor ***\\

        public FrmTester()
        {
            InitializeComponent();
        }

        #endregion

        #region //*** Other Members ***\\

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new InkStatus();
            a.Initialize("", "HP Deskjet 2050 J510 series");
            var result = a.GetInkLevel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Global.Initialize();
            Settings settings = Settings.LoadFromFile(Global.SettingsFileName);
            var result = HttpHelper.PostNotification(settings, "Test");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var a = new CanonUSBGeneric.InkStatus();
            a.Initialize("", "Canon MG2400 series Printer");
            var result = a.GetInkLevel();
        }

        #endregion
    }
}