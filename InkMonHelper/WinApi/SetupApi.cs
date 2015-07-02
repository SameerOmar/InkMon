#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : SetupApi.cs
// ////////  Created On : 2014/02/06
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

    public abstract class SetupApi
    {
        #region //*** Fields ***\\

        public const uint DIGCF_PRESENT = 0x00000002U;
        public const uint DIGCF_DEVICEINTERFACE = 0x00000010U;
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        public const uint CR_SUCCESS = 0;

        #endregion

        #region //*** Methods ***\\

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs([In, MarshalAs(UnmanagedType.LPStruct)] Guid ClassGuid,
            string Enumerator, IntPtr hwndParent, uint Flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex,
            ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet,
            [In] ref SP_DEVINFO_DATA DeviceInfoData, [In, MarshalAs(UnmanagedType.LPStruct)] Guid InterfaceClassGuid,
            uint MemberIndex, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet,
            [In] ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData,
            uint DeviceInterfaceDetailDataSize, out uint RequiredSize, IntPtr DeviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        #endregion

        #region //*\\ Nested Types //*\\

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA // Only used for Marshal.SizeOf. NOT!
        {
            public uint cbSize;
            public char DevicePath;
        }

        #endregion
    }

    public abstract class Cfgmgr32
    {
        #region //*** Methods ***\\

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = false, ExactSpelling = true)]
        public static extern uint CM_Get_Parent(out uint pdnDevInst, uint dnDevInst, uint ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern uint CM_Get_Device_ID(uint dnDevInst, string Buffer, uint BufferLen, uint ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = false, ExactSpelling = true)]
        public static extern uint CM_Get_Device_ID_Size(out uint pulLen, uint dnDevInst, uint ulFlags);

        #endregion
    }
}