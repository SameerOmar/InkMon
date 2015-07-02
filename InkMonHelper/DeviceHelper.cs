#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : DeviceHelper.cs
// ////////  Created On : 2014/02/01
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using InkMonHelper.WinApi;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

#endregion

namespace InkMonHelper
{
    public struct DeviceInfo
    {
        #region //*** Fields ***\\

        public string DeviceId;
        public string ExtData;

        #endregion
    }

    public class DeviceHelper
    {
        #region //*** Fields ***\\

        public const int IOCTL_USBPRINT_GET_1284_ID = 0x00220034;
        public const int IOCTL_USBPRINT_GET_LPT_STATUS = 0x00220030;

        private static readonly Guid GUID_PRINTER_INSTALL_CLASS = new Guid(0x4d36e979, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08,
            0x00, 0x2b, 0xe1, 0x03, 0x18);

        private static readonly Guid GUID_DEVINTERFACE_USBPRINT = new Guid(0x28d78fad, 0x5a12, 0x11D1, 0xae, 0x5b, 0x00,
            0x00, 0xf8, 0x03, 0xa8, 0xc2);

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        #endregion

        #region //*** Methods ***\\

        public IntPtr OpenDevice(string deviceName)
        {
            var sec = new SECURITY_ATTRIBUTES();
            IntPtr file = Kernel.CreateFile(deviceName, Kernel.GENERIC_READ | Kernel.GENERIC_WRITE,
                Kernel.FILE_SHARE_READ | Kernel.FILE_SHARE_WRITE, IntPtr.Zero, Kernel.OPEN_EXISTING, 0,
                IntPtr.Zero);
            return file;
        }

        public static string GetUSBPath(string printerName)
        {
            return GetUSBInterfacePath(GetPrinterParentDeviceId(GetPrinterRegistryInstanceID(printerName)));
        }

        public static SafeFileHandle OpenUSBPrinter(string printerName)
        {
            return
                new SafeFileHandle(
                    Kernel.CreateFile(GetUSBPath(printerName), Kernel.GENERIC_READ | Kernel.GENERIC_WRITE,
                        Kernel.FILE_SHARE_READ | Kernel.FILE_SHARE_WRITE, IntPtr.Zero, Kernel.OPEN_EXISTING, 0,
                        IntPtr.Zero), true);
        }

        public static unsafe DeviceInfo GetUSBDeviceInfo(string printerName)
        {
            string response = "";

            var deviceInfo = new DeviceInfo();
            try
            {
                var file = OpenUSBPrinter(printerName);

                var packet = new byte[2048];
                int size = 0;

                var buffer = new byte[255];
                fixed (byte* p = packet)
                {
                    var ptr = (IntPtr) p;

                    int result = Kernel.DeviceIoControl(file.DangerousGetHandle(), // device to be queried
                        IOCTL_USBPRINT_GET_1284_ID, // operation to perform
                        IntPtr.Zero, 0, // no input buffer
                        ptr, packet.Length, // output buffer
                        ref size, // # bytes returned
                        IntPtr.Zero); // synchronous I/O

                    if (size > 0)
                    {
                        deviceInfo.DeviceId = Encoding.ASCII.GetString(packet);
                    }

                    var overlapped = new OVERLAPPED();
                    result = Kernel.ReadFile(file.DangerousGetHandle(), ptr, packet.Length, ref size, ref overlapped);

                    if (size > 0)
                    {
                        deviceInfo.ExtData = Encoding.ASCII.GetString(packet);
                    }
                }

                Kernel.CloseHandle(file.DangerousGetHandle());
            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
            }
            return deviceInfo;
        }

        #endregion

        #region //*** Other Members ***\\

        private static string GetPrinterRegistryInstanceID(string printerName)
        {
            try
            {
                if (string.IsNullOrEmpty(printerName)) throw new ArgumentNullException("printerName");

                const string key_template = @"SYSTEM\CurrentControlSet\Control\Print\Printers\{0}\PNPData";

                using (var hk = Registry.LocalMachine.OpenSubKey(
                    string.Format(key_template, printerName),
                    RegistryKeyPermissionCheck.Default,
                    RegistryRights.QueryValues
                    )
                    )
                {
                    if (hk == null)
                        throw new ArgumentOutOfRangeException("printerName", "This printer does not have PnP data.");

                    return (string) hk.GetValue("DeviceInstanceId");
                }
            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
            }
            return "";
        }

        private static string GetPrinterParentDeviceId(string RegistryInstanceID)
        {
            if (string.IsNullOrEmpty(RegistryInstanceID)) throw new ArgumentNullException("RegistryInstanceID");

            IntPtr hdi = SetupApi.SetupDiGetClassDevs(GUID_PRINTER_INSTALL_CLASS, RegistryInstanceID, IntPtr.Zero,
                SetupApi.DIGCF_PRESENT);
            if (hdi.Equals(INVALID_HANDLE_VALUE)) throw new Win32Exception();

            try
            {
                var printer_data = new SetupApi.SP_DEVINFO_DATA();
                printer_data.cbSize = (uint) Marshal.SizeOf(typeof (SetupApi.SP_DEVINFO_DATA));

                if (SetupApi.SetupDiEnumDeviceInfo(hdi, 0, ref printer_data) == 0)
                    throw new Win32Exception(); // Only one device in the set

                uint cmret = 0;

                uint parent_devinst = 0;
                cmret = Cfgmgr32.CM_Get_Parent(out parent_devinst, printer_data.DevInst, 0);
                if (cmret != SetupApi.CR_SUCCESS)
                    throw new Exception(string.Format("Failed to get parent of the device '{0}'. Error code: 0x{1:X8}",
                        RegistryInstanceID, cmret));


                uint parent_device_id_size = 0;
                cmret = Cfgmgr32.CM_Get_Device_ID_Size(out parent_device_id_size, parent_devinst, 0);
                if (cmret != SetupApi.CR_SUCCESS)
                    throw new Exception(
                        string.Format(
                            "Failed to get size of the device ID of the parent of the device '{0}'. Error code: 0x{1:X8}",
                            RegistryInstanceID, cmret));

                parent_device_id_size++; // To include the null character

                var parent_device_id = new string('\0', (int) parent_device_id_size);
                cmret = Cfgmgr32.CM_Get_Device_ID(parent_devinst, parent_device_id, parent_device_id_size, 0);
                if (cmret != SetupApi.CR_SUCCESS)
                    throw new Exception(
                        string.Format(
                            "Failed to get device ID of the parent of the device '{0}'. Error code: 0x{1:X8}",
                            RegistryInstanceID, cmret));

                return parent_device_id;
            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
                return "";
            }
            finally
            {
                SetupApi.SetupDiDestroyDeviceInfoList(hdi);
            }
        }

        private static string GetUSBInterfacePath(string systemDeviceInstanceID)
        {
            if (string.IsNullOrEmpty(systemDeviceInstanceID))
                throw new ArgumentNullException("systemDeviceInstanceID");

            IntPtr hdi = SetupApi.SetupDiGetClassDevs(GUID_DEVINTERFACE_USBPRINT, systemDeviceInstanceID, IntPtr.Zero,
                SetupApi.DIGCF_PRESENT | SetupApi.DIGCF_DEVICEINTERFACE);
            if (hdi.Equals(INVALID_HANDLE_VALUE)) throw new Win32Exception();

            try
            {
                var device_data = new SetupApi.SP_DEVINFO_DATA();
                device_data.cbSize = (uint) Marshal.SizeOf(typeof (SetupApi.SP_DEVINFO_DATA));

                if (SetupApi.SetupDiEnumDeviceInfo(hdi, 0, ref device_data) == 0)
                    throw new Win32Exception(); // Only one device in the set

                var interface_data = new SetupApi.SP_DEVICE_INTERFACE_DATA();
                interface_data.cbSize = (uint) Marshal.SizeOf(typeof (SetupApi.SP_DEVICE_INTERFACE_DATA));

                if (
                    SetupApi.SetupDiEnumDeviceInterfaces(hdi, ref device_data, GUID_DEVINTERFACE_USBPRINT, 0,
                        ref interface_data) == 0)
                    throw new Win32Exception(); // Only one interface in the set


                // Get required buffer size
                uint required_size = 0;
                SetupApi.SetupDiGetDeviceInterfaceDetail(hdi, ref interface_data, IntPtr.Zero, 0, out required_size,
                    IntPtr.Zero);

                int last_error_code = Marshal.GetLastWin32Error();
                if (last_error_code != SetupApi.ERROR_INSUFFICIENT_BUFFER)
                    throw new Win32Exception(last_error_code);

                IntPtr interface_detail_data = Marshal.AllocCoTaskMem((int) required_size);

                try
                {

                    switch (IntPtr.Size)
                    {
                        case 4:
                            Marshal.WriteInt32(interface_detail_data, 4 + Marshal.SystemDefaultCharSize);
                            break;
                        case 8:
                            Marshal.WriteInt32(interface_detail_data, 8);
                            break;

                        default:
                            throw new NotSupportedException("Architecture not supported.");
                    }

                    if (
                        SetupApi.SetupDiGetDeviceInterfaceDetail(hdi, ref interface_data, interface_detail_data,
                            required_size, out required_size, IntPtr.Zero) == 0)
                        throw new Win32Exception();

                    return
                        Marshal.PtrToStringAuto(
                            new IntPtr(interface_detail_data.ToInt64() +
                                       Marshal.OffsetOf(typeof (SetupApi.SP_DEVICE_INTERFACE_DETAIL_DATA), "DevicePath")
                                           .ToInt64()));
                }
                finally
                {
                    Marshal.FreeCoTaskMem(interface_detail_data);
                }
            }
            finally
            {
                SetupApi.SetupDiDestroyDeviceInfoList(hdi);
            }
        }

        #endregion
    }
}