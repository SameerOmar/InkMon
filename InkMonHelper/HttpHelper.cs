#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : HttpHelper.cs
// ////////  Created On : 2014/05/23
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.ServiceModel;
using InkMonHelper.NotificationService;
using InkMonHelper.RegistrationService;

#endregion

namespace InkMonHelper
{
    public static class HttpHelper
    {
        #region //*** Properties ***\\

        private static string ServiceUri
        {
            get
            {
//#if DEBUG
//                return "http://localhost:52592/";
//#else
                return "http://felixsoft.com/inkmonwcf/";
//#endif
            }
        }

        #endregion

        #region //*** Methods ***\\

        public static string AccountRegister(string customerName, string email, string paypalEmail, string referralCode,
            string address)
        {
            var result = "";
            try
            {
                var client = new RegistrationClient("BasicHttpBinding_IRegistration",
                    new EndpointAddress(ServiceUri + "/Registration.svc"));
                result = client.RegiterNewAccount(customerName, email, paypalEmail, referralCode, address);
            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
                result = "Error: Internal error with the registration server";
            }
            return result;
        }

        public static bool PostNotification(Settings settings, string notificatioText)
        {
            var result = false;
            try
            {
                var client = new NotificationClient("BasicHttpBinding_INotification",
                    new EndpointAddress(ServiceUri + "/Notification.svc"));
                result = client.CreateNotification(settings.Customer.RefCode, settings.Customer.RefByCode,
                    notificatioText);

            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
            }
            return result;
        }

        #endregion
    }
}