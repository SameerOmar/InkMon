#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : PgWelcome.cs
// ////////  Created On : 2014/02/13
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI.Pages
{
    public partial class PgWelcome : UserControl, IPage
    {
        #region //*** Fields ***\\

        private Button _backButton;
        private Button _cancelButton;
        private Button _nextButton;

        #endregion

        #region //*** Properties ***\\

        public Settings CurrentSettings { get; set; }
        public Settings NewSettings { get; set; }

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public PgWelcome()
        {
            InitializeComponent();
            UpdateApplicationName();
        }

        #endregion

        #region //*** Interface Implementation ***\\

        public event EventHandler ExitSetup;
        public event EventHandler MoveToNextPage;
        public event EventHandler MoveToPreviousPage;

        public void Initialize(Button cancelButton, Button nextButton, Button backButton)
        {
            _cancelButton = cancelButton;
            _nextButton = nextButton;
            _backButton = backButton;
        }

        public void DoAction()
        {
        }

        #endregion

        #region //*** Other Members ***\\

        private void UpdateApplicationName()
        {
            foreach (object ctrl in Controls)
            {
                if (ctrl.GetType() == typeof (Label))
                {
                    var label = ctrl as Label;
                    label.Text = label.Text.Replace("#ApplicationName#", Application.ProductName);
                }
            }
        }

        #endregion
    }
}