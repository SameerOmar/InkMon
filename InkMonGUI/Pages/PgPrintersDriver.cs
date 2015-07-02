#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : PgPrintersDriver.cs
// ////////  Created On : 2014/02/17
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI.Pages
{
    public partial class PgPrintersDriver : UserControl, IPage
    {
        #region //*** Fields ***\\

        private Button _backButton;
        private Button _cancelButton;
        private ListViewItem _lvItem;
        private Button _nextButton;
        private PluginsManager _pluginsManager;

        #endregion

        #region //*** Properties ***\\

        public Settings CurrentSettings { get; set; }
        public Settings NewSettings { get; set; }

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public PgPrintersDriver()
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

            _pluginsManager = new PluginsManager();
            _pluginsManager.LoadPlugins();

            cmbDrivers.Items.Add("Unsupported");
            foreach (var loadedPlugin in _pluginsManager.LoadedPlugins)
            {
                cmbDrivers.Items.Add(loadedPlugin.Name);
            }

            foreach (var printer in NewSettings.Printers)
            {
                SetPrinterDriver(printer);
                var item = new ListViewItem(printer.Caption);
                item.SubItems.Add("40");
                printer.Level = 40;
                item.SubItems.Add(printer.Plugin);
                item.Tag = printer;
                lvPrinters.Items.Add(item);
            }


        }

        public void DoAction()
        {
            _cancelButton.Tag = "1";
            _cancelButton.Text = "Finish";

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

        private void SetPrinterDriver(SettingsPrinter printer)
        {
            var info = new PrinterInfo();
            info.Caption = printer.Caption;
            if (PrinterHelper.GetPrinterInfo(ref info))
            {
                printer.Name = info.Name;
                printer.Port = info.Port;
            }
            var manf = info.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0];
            var pluginName = _pluginsManager.LoadedPlugins[0].Name;
            foreach (var loadedPlugin in _pluginsManager.LoadedPlugins)
            {
                if (loadedPlugin.Brand.ToLower().Contains(manf.ToLower()))
                {
                    pluginName = loadedPlugin.Name;
                    var ports = loadedPlugin.Port.Split(',');
                    var foundMatch = false;
                    foreach (var port in ports)
                    {
                        if (port.Trim().ToLower().Contains(info.Port.Substring(0, port.Trim().Length).ToLower()) ||
                            port.Trim() == "*" || (port.Trim().ToLower() == "network" && info.Network))
                        {
                            foundMatch = true;
                            break;
                        }
                    }
                    if (foundMatch)
                        break;
                }
            }
            printer.Plugin = pluginName;
        }

        private void cmbDrivers_SelectedValueChanged(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            _lvItem.SubItems[2].Text = cmbDrivers.Text;
            ((SettingsPrinter) _lvItem.Tag).Plugin = cmbDrivers.Text;
            // Hide the ComboBox.
            cmbDrivers.Visible = false;

            UpdateUI();
        }

        private void cmbDrivers_Leave(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            _lvItem.SubItems[2].Text = cmbDrivers.Text;
            ((SettingsPrinter) _lvItem.Tag).Plugin = cmbDrivers.Text;

        }

        private void cmbDrivers_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the user presses ESC.
            switch (e.KeyChar)
            {
                case (char) (int) Keys.Escape:
                {
                    // Reset the original text value, and then hide the ComboBox.
                    cmbDrivers.Text = _lvItem.SubItems[2].Text;
                    cmbDrivers.Visible = false;
                    break;
                }

                case (char) (int) Keys.Enter:
                {
                    // Hide the ComboBox.
                    cmbDrivers.Visible = false;
                    break;
                }

            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (var printer in NewSettings.Printers)
            {
                foreach (
                    var item in
                        lvPrinters.Items.Cast<object>().Where(item => ((ListViewItem) item).Text == printer.Caption))
                {
                    printer.Plugin = ((ListViewItem) item).SubItems[2].Text;
                    printer.Level = (byte) Convert.ToInt32("0" + ((ListViewItem) item).SubItems[1].Text);
                    break;
                }
            }
            if (
                lvPrinters.Items.Cast<object>()
                    .Count(item => ((ListViewItem) item).SubItems[2].Text == "" ||
                                   ((ListViewItem) item).SubItems[1].Text == "") == 0)
            {
                _cancelButton.Tag = "1";
                _cancelButton.Text = "Finish";
            }
            else
            {
                _cancelButton.Tag = "0";
                _cancelButton.Text = "Cancel";
            }

        }

        private void lvPrinters_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            _lvItem = lvPrinters.GetItemAt(e.X, e.Y);

            ShowInLineEditor(2, lvPrinters.Columns[0].Width + lvPrinters.Columns[1].Width, cmbDrivers);
            ShowInLineEditor(1, lvPrinters.Columns[0].Width, numLevel);
        }

        private void ShowInLineEditor(int col, int x, Control control)
        {
            var comboboxCol = lvPrinters.Columns[col];
            int colLfet = x + 1;
            // Make sure that an item is clicked.
            if (_lvItem != null)
            {
                // Get the bounds of the item that is clicked.
                Rectangle ClickedItem = _lvItem.Bounds;

                // Verify that the column is completely scrolled off to the left.
                if ((ClickedItem.Left + comboboxCol.Width) < 0)
                {
                    // If the cell is out of view to the left, do nothing.
                    return;
                }

                // Verify that the column is partially scrolled off to the left.
                if (ClickedItem.Left < 0)
                {
                    // Determine if column extends beyond right side of ListView.
                    if ((ClickedItem.Left + comboboxCol.Width) > lvPrinters.Width)
                    {
                        // Set width of column to match width of ListView.
                        ClickedItem.Width = lvPrinters.Width;
                        ClickedItem.X = 0 + colLfet;
                    }
                    else
                    {
                        // Right side of cell is in view.
                        ClickedItem.Width = comboboxCol.Width + ClickedItem.Left;
                        ClickedItem.X = 2 + colLfet;
                    }
                }
                else if (comboboxCol.Width > lvPrinters.Width)
                {
                    ClickedItem.Width = lvPrinters.Width;
                }
                else
                {
                    ClickedItem.Width = comboboxCol.Width;
                    ClickedItem.X = 2 + colLfet;
                }

                // Adjust the top to account for the location of the ListView.
                ClickedItem.Y += lvPrinters.Top;
                ClickedItem.X += lvPrinters.Left;

                // Assign calculated bounds to the ComboBox.
                control.Bounds = ClickedItem;

                // Set default text for ComboBox to match the item that is clicked.
                control.Text = _lvItem.SubItems[col].Text;

                // Display the ComboBox, and make sure that it is on top with focus.
                control.Visible = true;
                control.BringToFront();
                control.Focus();
            }
        }

        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            _lvItem.SubItems[1].Text = numLevel.Value.ToString();
            ((SettingsPrinter) _lvItem.Tag).Level = (byte) numLevel.Value;

            UpdateUI();
        }

        private void numLevel_Leave(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            _lvItem.SubItems[1].Text = numLevel.Text;
            ((SettingsPrinter) _lvItem.Tag).Level = (byte) numLevel.Value;

        }

        private void numLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the user presses ESC.
            switch (e.KeyChar)
            {
                case (char) (int) Keys.Escape:
                {
                    // Reset the original text value, and then hide the Numeric Control.
                    numLevel.Text = _lvItem.SubItems[1].Text;
                    numLevel.Visible = false;
                    break;
                }

                case (char) (int) Keys.Enter:
                {
                    // Hide the Numeric Control.
                    numLevel.Visible = false;
                    break;
                }

            }
            UpdateUI();
        }

        #endregion
    }
}