#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonService
// ////////  File Name : ServiceInstaller.cs
// ////////  Created On : 2014/02/13
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

#endregion

namespace InkMonService
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        #region //*** Constructors/Deconstructor ***\\

        public ServiceInstaller()
        {
            InitializeComponent();
        }

        #endregion

        #region //*** Other Members ***\\

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            using (var serviceController = new ServiceController(serviceInstaller1.ServiceName, Environment.MachineName)
                )
                serviceController.Start();
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
            using (var serviceController = new ServiceController(serviceInstaller1.ServiceName, Environment.MachineName)
                )
                serviceController.Stop();
        }

        #endregion
    }
}