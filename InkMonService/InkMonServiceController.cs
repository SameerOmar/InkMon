#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonService
// ////////  File Name : InkMonServiceController.cs
// ////////  Created On : 2014/01/31
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System.ServiceProcess;
using System.Threading;

#endregion

namespace InkMonService
{
    public partial class InkMonServiceController : ServiceBase
    {
        #region //*** Fields ***\\

        private readonly MainWorker _mainWorker;

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public InkMonServiceController()
        {
            InitializeComponent();
            ServiceName = "InkMonService";
            _mainWorker = new MainWorker();
        }

        #endregion

        #region //*** Methods ***\\

        public void StartService()
        {
            _mainWorker.Start();
        }

        #endregion

        #region //*** Other Members ***\\

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        protected override void OnStop()
        {
            _mainWorker.Stop();
            while (_mainWorker.IsJobStarted)
            {
                Thread.Sleep(500);
            }
        }

        #endregion
    }
}