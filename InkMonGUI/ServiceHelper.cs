#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : ServiceHelper.cs
// ////////  Created On : 2014/02/13
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.ServiceProcess;
using InkMonHelper;

#endregion

namespace InkMonGUI
{
    public class ServiceHelper
    {
        #region //*** Methods ***\\

        public static void RestartService(string serviceName, int timeoutMilliseconds)
        {
            var service = new ServiceController(serviceName);
            try
            {
                var millisec1 = Environment.TickCount;
                var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                // count the rest of the timeout
                var millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception e)
            {
                Global.Logger.Error(e.Message);
            }
        }

        public static ServiceControllerStatus GetServiceStatus(string serviceName)
        {
            var service = new ServiceController(serviceName);
            try
            {
                return service.Status;
            }
            catch (Exception e)
            {
                Global.Logger.Error(e.Message);
            }
            return ServiceControllerStatus.Stopped;
        }

        #endregion
    }
}