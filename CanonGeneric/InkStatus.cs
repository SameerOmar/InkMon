#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : CanonUSBGeneric
// ////////  File Name : InkStatus.cs
// ////////  Created On : 2014/02/25
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using InkMonHelper;

#endregion

namespace CanonUSBGeneric
{
    public class InkStatus : IInkStatus
    {
        #region //*** Fields ***\\

        private string _printerModel;
        private string _printerName;
        private string _printerSerial;

        #endregion

        #region //*** Properties ***\\

        public string Name
        {
            get { return "CanonUSBGeneric"; }
        }

        public string Brand
        {
            get { return "Canon"; }
        }

        public string Model
        {
            get { return "*"; }
        }

        public string Port
        {
            get { return "USB"; }
        }

        public List<InkLevel> InkLevel { get; private set; }

        #endregion

        #region //*** Interface Implementation ***\\

        public bool Initialize(string model, string printerName)
        {
            _printerName = printerName;
            InkLevel = new List<InkLevel>();
            return true;
        }

        public InkStatusResult GetInkLevel()
        {
            var deviceInfo = DeviceHelper.GetUSBDeviceInfo(_printerName);
            if (deviceInfo.ExtData == "")
                return InkStatusResult.Error;

            return ParsePrinterStatus(deviceInfo.ExtData);
        }

        #endregion

        #region //*** Other Members ***\\

        private InkStatusResult ParsePrinterStatus(string printerStatus)
        {
            var inkStatusResult = new InkStatusResult();
            inkStatusResult = InkStatusResult.NotSupported;

            if (printerStatus.Contains("IJPrinterStatus"))
            {
                string data = printerStatus.Substring(printerStatus.IndexOf("CDATA["));
                if (data != "")
                {
                    data = data.Substring(0, data.IndexOf("]]"));
                    if (data.Contains("CIR:"))
                    {
                        string cir = data.Substring(data.IndexOf("CIR:"));
                        cir = cir.Substring(0, cir.IndexOf(";"));
                        inkStatusResult = DecodeCIR(cir);
                    }
                }
            }

            return inkStatusResult;
        }

        private InkStatusResult DecodeCIR(string cir)
        {
            string data = cir.Substring(4);
            var colors = data.Split(',');

            foreach (var color in colors)
            {
                var cartridgeInfo = color.Split('=');
                switch (cartridgeInfo[0])
                {
                    case "K":
                        InkLevel.Add(new InkLevel("Black", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "BK":
                        InkLevel.Add(new InkLevel("Black", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "PBK":
                        InkLevel.Add(new InkLevel("PhotoBlack", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "LC":
                        InkLevel.Add(new InkLevel("PhotoCyan", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "LM":
                        InkLevel.Add(new InkLevel("PhotoMagenta", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "Y":
                        InkLevel.Add(new InkLevel("Yellow", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "M":
                        InkLevel.Add(new InkLevel("Magenta", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "C":
                        InkLevel.Add(new InkLevel("Cyan", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;

                    case "CL":
                        InkLevel.Add(new InkLevel("Color", Convert.ToInt32("0" + cartridgeInfo[1])));
                        break;
                }
            }

            return (InkLevel.Count > 0 ? InkStatusResult.OK : InkStatusResult.NotSupported);
        }

        #endregion
    }
}