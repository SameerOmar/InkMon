#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonService
// ////////  File Name : Program.cs
// ////////  Created On : 2014/01/31
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System.ServiceProcess;
using System.Threading;
using InkMonHelper;

#endregion

namespace InkMonService
{
    internal static class Program
    {
        #region //*** Other Members ***\\

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            Global.Initialize();

#if DEBUG
            var mainWorker = new MainWorker();
            mainWorker.Start();

            while (mainWorker.IsWorking)
            {
                Thread.Sleep(500);
            }
#else            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new InkMonServiceController()
            };


            ServiceBase.Run(ServicesToRun);
#endif
        }

        #endregion
    }
}