#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : FrmWizard.cs
// ////////  Created On : 2014/02/17
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Windows.Forms;
using InkMonGUI.Pages;
using InkMonHelper;

#endregion

namespace InkMonGUI
{
    public partial class FrmWizard : Form
    {
        #region //*** Fields ***\\

        private readonly IPage[] _pages =
        {
            new PgWelcome(), /*new PgOwnerInfo(),*/ new PgSelectPrinter(),
            new PgPrintersDriver()
        };

        private int _currentPage;

        #endregion

        #region //*** Properties ***\\

        public Settings CurrentSettings { get; set; }
        public Settings NewSettings { get; set; }

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public FrmWizard(bool addprinter)
        {
            InitializeComponent();

            _currentPage = addprinter ? 2 : 0;
            btnCancel.Tag = "0";
            foreach (IPage page in _pages)
            {
                page.MoveToNextPage += page_MoveToNextPage;
                page.MoveToPreviousPage += page_MoveToPreviousPage;
                page.ExitSetup += page_ExitSetup;
            }
        }

        #endregion

        #region //*** Other Members ***\\

        private void page_ExitSetup(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void page_MoveToPreviousPage(object sender, EventArgs e)
        {
            _currentPage--;
            SetCurrentPage();
        }

        private void page_MoveToNextPage(object sender, EventArgs e)
        {
            _currentPage++;
            SetCurrentPage();
        }

        private void SetCurrentPage()
        {
            IPage currentPage = _pages[_currentPage];

            currentPage.CurrentSettings = CurrentSettings;
            currentPage.NewSettings = NewSettings;

            btnNext.Enabled = _currentPage < _pages.Length - 1;
            btnBack.Enabled = _currentPage > 0;

            pnlPagesContainer.Controls.Clear();
            currentPage.Initialize(btnCancel, btnNext, btnBack);
            ((Control) currentPage).Dock = DockStyle.Fill;
            pnlPagesContainer.Controls.Add((Control) currentPage);
            btnCancel.Tag = "0";
            currentPage.DoAction();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Tag == "1")
                DialogResult = DialogResult.OK;
            else
            {
                if (MessageBox.Show("This will end the Wizard, are you sure?", "Attention", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            SetCurrentPage();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _currentPage--;
            SetCurrentPage();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            SetCurrentPage();
        }

        #endregion
    }
}