#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : PgOwnerInfo.cs
// ////////  Created On : 2014/02/17
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI.Pages
{
    public partial class PgOwnerInfo : UserControl, IPage
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

        public PgOwnerInfo()
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

            FillOwnerInformation();
        }

        public void DoAction()
        {
            _cancelButton.Tag = "0";
            _cancelButton.Text = "Cancel";
            _backButton.Enabled = false;
            _nextButton.Enabled = !btnGetReferenceCode.Enabled;
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

        private void FillOwnerInformation()
        {
            txtName.Text = CurrentSettings.Customer.Name;
            txtEmail.Text = CurrentSettings.Customer.Email;
            txtPaypalEmail.Text = CurrentSettings.Customer.PaypalEmail;
            txtReferenceCode.Text = CurrentSettings.Customer.RefCode;
            txtReferralCode.Text = CurrentSettings.Customer.RefByCode;

            btnGetReferenceCode.Enabled = (txtReferenceCode.Text.Trim() == "");
        }

        private void ChangeSettings()
        {
            NewSettings.Customer.Name = txtName.Text;
            NewSettings.Customer.Email = txtEmail.Text;
            NewSettings.Customer.PaypalEmail = txtPaypalEmail.Text;
            NewSettings.Customer.RefCode = txtReferenceCode.Text;
            NewSettings.Customer.RefByCode = txtReferralCode.Text;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ChangeSettings();
        }

        private void PgOwnerInfo_Validating(object sender, CancelEventArgs e)
        {
            var controls = new[] {txtName, txtEmail, txtPaypalEmail, txtReferenceCode};
            errorProvider1.Clear();
            foreach (var control in controls.Where(c => String.IsNullOrEmpty(c.Text)))
            {
                errorProvider1.SetError(control, "Please fill the required field");
                e.Cancel = true;
            }
        }

        private void btnGetReferenceCode_Click(object sender, EventArgs e)
        {
            var controls = new[] {txtName, txtEmail, txtPaypalEmail};
            errorProvider1.Clear();
            foreach (var control in controls.Where(c => String.IsNullOrEmpty(c.Text)))
            {
                errorProvider1.SetError(control, "Please fill the required field");
                return;
            }
            var resault = HttpHelper.AccountRegister(txtName.Text, txtEmail.Text, txtPaypalEmail.Text,
                txtReferralCode.Text, "");
            if (resault.StartsWith("Error:"))
                MessageBox.Show(resault);
            else
            {
                txtReferenceCode.Text = resault;
                NewSettings.SaveToFile(Global.SettingsFileName);
            }

            btnGetReferenceCode.Enabled = (txtReferenceCode.Text.Trim() == "");
            _nextButton.Enabled = !btnGetReferenceCode.Enabled;
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            toolTip1.Show("So you can receive payment for your referrals we need\n" +
                          "a little information from you. This is so we can pay you\n" +
                          "for your referrals. We promise not to spam you or sell your\n" +
                          "contact details to anyone: Your details are safe with us.\n" +
                          "When you refer someone to our service and they make a purchase\n" +
                          "you will get paid ONLY providing the details below are accurate.\n" +
                          "\n" +
                          "(You can change these) later if you need to.", btnShowInfo);
        }

        #endregion
    }
}