#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : Winspool.cs
// ////////  Created On : 2014/02/09
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace InkMonHelper.WinApi
{
    public static class Winspool
    {
        #region //*** Methods ***\\

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int OpenPrinter(string pPrinterName, out IntPtr phPrinter, ref PRINTER_DEFAULTS pDefault);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetPrinter(IntPtr hPrinter, Int32 dwLevel, IntPtr pPrinter, Int32 dwBuf,
            out Int32 dwNeeded);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern int ClosePrinter(IntPtr hPrinter);

        #endregion

        #region //*\\ Nested Types //*\\

        [StructLayout(LayoutKind.Sequential)]
        public struct PRINTER_DEFAULTS
        {
            public IntPtr pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_2
        {
            [MarshalAs(UnmanagedType.LPTStr)] public string pServerName;

            [MarshalAs(UnmanagedType.LPTStr)] public string pPrinterName;

            [MarshalAs(UnmanagedType.LPTStr)] public string pShareName;

            [MarshalAs(UnmanagedType.LPTStr)] public string pPortName;

            [MarshalAs(UnmanagedType.LPTStr)] public string pDriverName;

            [MarshalAs(UnmanagedType.LPTStr)] public string pComment;

            [MarshalAs(UnmanagedType.LPTStr)] public string pLocation;

            public IntPtr pDevMode;

            [MarshalAs(UnmanagedType.LPTStr)] public string pSepFile;

            [MarshalAs(UnmanagedType.LPTStr)] public string pPrintProcessor;

            [MarshalAs(UnmanagedType.LPTStr)] public string pDatatype;

            [MarshalAs(UnmanagedType.LPTStr)] public string pParameters;

            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AveragePPM;
        }

        #endregion
    }
}