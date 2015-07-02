#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : ExListViewControl.cs
// ////////  Created On : 2014/04/26
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace InkMonGUI.UserControls
{
    internal class ExListViewControl : ListView
    {
        #region //*** Fields ***\\

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private Container components;

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public ExListViewControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
        }

        #endregion

        #region //*** Other Members ***\\

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        protected override void WndProc(ref Message msg)
        {
            // Look for the WM_VSCROLL or the WM_HSCROLL messages.
            if ((msg.Msg == WM_VSCROLL) || (msg.Msg == WM_HSCROLL))
            {
                // Move focus to the ListView to cause ComboBox to lose focus.
                Focus();
            }

            // Pass message to default handler.
            base.WndProc(ref msg);
        }

        #endregion
    }
}