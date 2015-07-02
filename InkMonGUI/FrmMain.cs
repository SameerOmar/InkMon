#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : FrmMain.cs
// ////////  Created On : 2014/01/31
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI
{
    public partial class FrmMain : Form
    {
        #region //*** Fields ***\\

        private readonly Settings _settings;
        private bool _canQuit;
        private bool _isClosing;

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public FrmMain()
        {
            InitializeComponent();
            _canQuit = false;
            _isClosing = false;

            _settings = Settings.LoadFromFile(Global.SettingsFileName);

            LoadPrintersFromSettings();
            FillOwnerInformation();

            bwServiceNotificationPipe.RunWorkerAsync();
        }

        #endregion

        #region //*** Other Members ***\\

        private void FillOwnerInformation()
        {
            txtName.Text = _settings.Customer.Name;
            txtEmail.Text = _settings.Customer.Email;
            txtPaypalEmail.Text = _settings.Customer.PaypalEmail;
            txtReferenceCode.Text = _settings.Customer.RefCode;
            txtReferralCode.Text = _settings.Customer.RefByCode;
        }

        private void LoadPrintersFromSettings()
        {
            int i = 1;
            lvPrinters.Items.Clear();
            var font = new Font("Arial", 10, FontStyle.Bold);
            foreach (var printer in _settings.Printers)
            {
                var item = new ListViewItem(printer.Caption);
                item.ImageIndex = 0;
                item.SubItems.Add(printer.Level.ToString());
                item.SubItems.Add(printer.Plugin);
                item.Font = font;

                if (i%2 != 0)
                {
                    item.BackColor = Color.Beige;
                }
                i++;
                lvPrinters.Items.Add(item);
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canQuit)
            {
                _isClosing = true;
                return;
            }

            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
            Visible = false;

            niSysIcon.BalloonTipText = "InkMon is minimized, you can open it again from here.";
            niSysIcon.BalloonTipTitle = "InkMon V1.0";
            niSysIcon.BalloonTipIcon = ToolTipIcon.Info;
            niSysIcon.Visible = true;
            niSysIcon.ShowBalloonTip(5000);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will close the UI application but the monitoring service will keep working",
                "Attention!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) ==
                DialogResult.No)
                return;

            _canQuit = true;
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOwnerInformation_Click(object sender, EventArgs e)
        {
            tabPages.SelectedIndex = 1;
        }

        private void btnPrintersList_Click(object sender, EventArgs e)
        {
            tabPages.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPrintersFromSettings();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.Customer.Name = txtName.Text;
            _settings.Customer.Email = txtEmail.Text;
            _settings.Customer.PaypalEmail = txtPaypalEmail.Text;
            _settings.Customer.RefCode = txtReferenceCode.Text;
            _settings.Customer.RefByCode = txtReferralCode.Text;

            _settings.SaveToFile(Global.SettingsFileName);
            MessageBox.Show("Changes was saved to the settings file, Monitoring service will be restarted.");
            var frm = new FrmRestartService();
            frm.ShowDialog(this);
        }

        private void tmServiceStatus_Tick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                var status = ServiceHelper.GetServiceStatus("InkMonService");
                switch (status)
                {
                    case ServiceControllerStatus.Stopped:
                        lblServiceStatus.Text = "Service was stopped";
                        break;

                    case ServiceControllerStatus.Running:
                        lblServiceStatus.Text = "Service is running";
                        break;

                    case ServiceControllerStatus.Paused:
                        lblServiceStatus.Text = "Service was paused";
                        break;

                    default:
                        lblServiceStatus.Text = "Service is " + status;
                        break;
                }
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("http://felixsoft.com/");
        }

        private void btnFacebook_Click(object sender, EventArgs e)
        {
            Process.Start("https://facebook.com/sameer.m.omar");
        }

        private void btnLinkedIn_Click(object sender, EventArgs e)
        {
            Process.Start("https://eg.linkedin.com/in/sameeromar");
        }

        private void bwServiceNotificationPipe_DoWork(object sender, DoWorkEventArgs e)
        {
            var server = new NamedPipeServerStream("InkMonPipe");
            while (!_isClosing)
            {
                try
                {
                    server.WaitForConnection();
                    var reader = new StreamReader(server);
                    while (server.IsConnected)
                    {
                        var line = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(line))
                        {
                            niSysIcon.BalloonTipText = line;
                            niSysIcon.BalloonTipTitle = "InkMon V1.0 - Printer Ink is at low level";
                            niSysIcon.BalloonTipIcon = ToolTipIcon.Info;
                            niSysIcon.Visible = true;
                            niSysIcon.ShowBalloonTip(10000);
                        }
                    }
                    server.Disconnect();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void btnDeletePrinter_Click(object sender, EventArgs e)
        {
            if (lvPrinters.SelectedItems.Count != 0)
            {
                var item = lvPrinters.SelectedItems[0];
                if (MessageBox.Show(string.Format("Printer \"{0}\" will be deleted. Are you sure?", item.Text),
                    "Attention!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) ==
                    DialogResult.No)
                    return;

                var printer = _settings.Printers.Find(a => a.Caption == item.Text);
                if (printer != null)
                {
                    _settings.Printers.Remove(printer);
                    _settings.SaveToFile(Global.SettingsFileName);

                    LoadPrintersFromSettings();
                }
            }
        }

        private void btnAddPrinter_Click(object sender, EventArgs e)
        {
            var frm = new FrmWizard(true);
            frm.CurrentSettings = _settings;
            frm.NewSettings = Settings.LoadFromFile(Global.SettingsFileName);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var printer in frm.NewSettings.Printers)
                {
                    if (_settings.Printers.Count(p => p.Caption == printer.Caption) == 0)
                    {
                        _settings.Printers.Add(printer);
                    }
                }
                _settings.SaveToFile(Global.SettingsFileName);
                LoadPrintersFromSettings();
                var frmService = new FrmRestartService();
                frmService.ShowDialog(this);
            }
        }

        #endregion
    }
}