#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : PgSelectPrinter.cs
// ////////  Created On : 2014/02/13
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI.Pages
{
    public partial class PgSelectPrinter : UserControl, IPage
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

        public PgSelectPrinter()
        {
            InitializeComponent();
            UpdateApplicationName();
        }

        #endregion

        #region //*** Interface Implementation ***\\

        public event EventHandler MoveToNextPage;
        public event EventHandler MoveToPreviousPage;
        public event EventHandler ExitSetup;

        public void Initialize(Button cancelButton, Button nextButton, Button backButton)
        {
            _cancelButton = cancelButton;
            _nextButton = nextButton;
            _backButton = backButton;


            foreach (var installedPrinter in PrinterSettings.InstalledPrinters)
            {
                if (
                    CurrentSettings.Printers.Count(
                        p =>
                            String.Equals(p.Caption, installedPrinter.ToString(),
                                StringComparison.CurrentCultureIgnoreCase)) == 0)
                {
                    var item = new ListViewItem(installedPrinter.ToString());
                    lvPrinters.Items.Add(item);
                }
            }
        }

        public void DoAction()
        {
            _backButton.Enabled = false;
            _nextButton.Enabled = false;
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

        private void lvPrinters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _nextButton.Enabled = lvPrinters.CheckedItems.Count > 0;
            NewSettings.Printers.Clear();
            foreach (ListViewItem checkedItem in lvPrinters.CheckedItems)
            {
                NewSettings.Printers.Add(new SettingsPrinter {Caption = checkedItem.Text});
            }
        }

        #endregion
    }
}