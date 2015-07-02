#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : PrinterHelper.cs
// ////////  Created On : 2014/02/09
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Drawing.Printing;
using System.Management;

#endregion

namespace InkMonHelper
{
    public enum Status
    {
        None,
        OK,
        Error
    }

    public struct PrinterInfo
    {
        #region //*** Fields ***\\

        public string Caption;
        public bool Color;
        public string Driver;
        public int Level;
        public string Name;
        public bool Network;
        public string Port;
        public Status Status;

        #endregion
    }

    public static class PrinterHelper
    {
        #region //*** Methods ***\\

        public static bool GetPrinterInfo(ref PrinterInfo printerInfo)
        {
            printerInfo.Status = Status.None;

            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerInfo.Caption);
            var searcher = new ManagementObjectSearcher(query);
            var coll = searcher.Get();

            foreach (ManagementObject printer in coll)
            {
                var ps = new PrinterSettings {PrinterName = printerInfo.Caption};
                if (ps.IsValid)
                {
                    printerInfo.Status = Status.OK;
                    printerInfo.Port = printer.Properties["PortName"].Value.ToString();
                    printerInfo.Driver = printer.Properties["DeviceID"].Value.ToString();
                    printerInfo.Color = ps.SupportsColor;
                    printerInfo.Name = printer.Properties["DeviceID"].Value.ToString();
                    printerInfo.Network = Convert.ToBoolean(printer.Properties["Network"].Value);
                }
                else
                {
                    printerInfo.Status = Status.Error;
                }
            }
            return true;
        }

        #endregion
    }
}