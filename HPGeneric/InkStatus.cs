#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : HPUSBGeneric
// ////////  File Name : InkStatus.cs
// ////////  Created On : 2014/01/31
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using InkMonHelper;

#endregion

namespace HPUSBGeneric
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
            get { return "HPUSBGeneric"; }
        }

        public string Brand
        {
            get { return "HP"; }
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
            if (deviceInfo.DeviceId == "")
                return InkStatusResult.Error;

            return ParseDeviceId(deviceInfo.DeviceId);
        }

        #endregion

        #region //*** Other Members ***\\

        private InkStatusResult ParseDeviceId(string deviceId)
        {
            string[] tags = deviceId.Split(';');
            string statusString = "";
            int offset = 0;
            var yellow = new Char[2];
            var magenta = new Char[2];
            var cyan = new Char[2];
            var black = new Char[2];

            try
            {
                if (tags.Length > 1)
                {
                    foreach (string tag in tags)
                    {
                        string[] data = tag.Split(':');
                        switch (data[0])
                        {
                            case "TTMFG":
                                if (data[1] != "HP")
                                    return InkStatusResult.NotSupported;
                                break;

                            case "MDL":
                                _printerModel = data[1];
                                break;

                            case "SN":
                                _printerSerial = data[1];
                                break;

                            case "S":
                                statusString = data[1];
                                break;
                        }
                    }

                    int length = statusString.Length;
                    if (statusString != "" && length > 2)
                    {
                        char[] statusArray = statusString.ToCharArray();
                        if (statusArray[0] == '0' && statusArray[1] == '3')
                        {
                            Debug.WriteLine("Version 3 detected\n");
                            offset = 18;
                        }
                        else if (statusArray[0] == '0' && (statusArray[1] == '0' || statusArray[1] == '1'))
                        {
                            Debug.WriteLine("Version 0 or 1 detected\n");
                            offset = 16;
                        }
                        else if (statusArray[0] == '0' && statusArray[1] == '4')
                        {
                            Debug.WriteLine("Version 0 or 1 detected\n");
                            offset = 22;
                        }
                        else if (statusArray[0] == '0' && statusArray[1] == '2')
                        {
                            Debug.WriteLine("Version 2 detected\n");

                            // Do not know if this is always correct
                            // Worked at least in the old version

                            yellow[0] = statusArray[length - 2];
                            yellow[1] = statusArray[length - 1];

                            magenta[0] = statusArray[length - 6];
                            magenta[1] = statusArray[length - 5];

                            cyan[0] = statusArray[length - 10];
                            cyan[1] = statusArray[length - 9];

                            black[0] = statusArray[length - 14];
                            black[1] = statusArray[length - 13];

                            InkLevel.Add(new InkLevel("Black", Convert.ToInt32(new string(black), 16)));
                            InkLevel.Add(new InkLevel("Cyan", Convert.ToInt32(new string(cyan), 16)));
                            InkLevel.Add(new InkLevel("magenta", Convert.ToInt32(new string(magenta), 16)));
                            InkLevel.Add(new InkLevel("yellow", Convert.ToInt32(new string(yellow), 16)));


                            Debug.WriteLine("Yellow: {0}, Magenta: {1}, Cyan: {2}, Black: {3}\n",
                                Convert.ToInt32(new string(yellow), 16), Convert.ToInt32(new string(magenta), 16),
                                Convert.ToInt32(new string(cyan), 16),
                                Convert.ToInt32(new string(black), 16));

                            return InkStatusResult.OK;
                        }
                        else
                        {
                            Debug.WriteLine("Printer not supported\n");
                            return InkStatusResult.NotSupported;
                        }

                        int colors = Convert.ToInt32("0" + statusArray[offset]);

                        int i = 0; /* current color */
                        int j = offset; /* index in device id */
                        while ((j + 8 < length) && (i < colors))
                        {
                            Debug.WriteLine("Processing color number %d\n", i);

                            string colorTypeAscii = string.Format("{0}{1}", statusArray[j + 1], statusArray[j + 2]);

                            var colorType = (CartridgeType) (Convert.ToInt32(colorTypeAscii, 16) & 0x3f);

                            Debug.WriteLine("Raw Color Type: {0}\n", Convert.ToInt32(colorTypeAscii, 16));

                            /* Only show inks with their own head */
                            /* Not sure if this is the right approach */
                            /* Is needed for Photosmart 8250 to get rid of bogus entry */

                            if ((Convert.ToInt32(colorTypeAscii, 16) & 0x40) > 0)
                            {
                                Debug.WriteLine("Color type: {0}\n", colorType.ToString());

                                string colorValueAscii = string.Format("{0}{1}", statusArray[j + 7], statusArray[j + 8]);
                                int colorValue = Convert.ToInt32(colorValueAscii, 16);

                                Debug.WriteLine("Color value: {0}\n", colorValue);

                                string colorTypeName = colorType.ToString();

                                //if (colorType != CartridgeType.CartridgeNotPresent)
                                //{
                                InkLevel.Add(new InkLevel(colorTypeName, colorValue));
                                Debug.WriteLine("Added to output array\n");
                                //}
                            }

                            j += 8;
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InkStatusResult.Error;
            }

            return InkStatusResult.OK;
        }

        #endregion
    }
}