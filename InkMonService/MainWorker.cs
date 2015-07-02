#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonService
// ////////  File Name : MainWorker.cs
// ////////  Created On : 2014/02/06
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Threading;
using InkMonHelper;

#endregion

namespace InkMonService
{
    public class MainWorker
    {
        #region //*** Fields ***\\

        private readonly PluginsManager _pluginsManager;
        private readonly Settings _setting;
        private Timer _timer;

        #endregion

        #region //*** Properties ***\\

        public bool IsWorking { get; set; }
        public bool IsJobStarted { get; set; }

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public MainWorker()
        {
            try
            {
                _pluginsManager = new PluginsManager();
                _pluginsManager.LoadPlugins();


                _setting = Settings.LoadFromFile(Global.SettingsFileName);

            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
            }
        }

        #endregion

        #region //*** Methods ***\\

        public void Start()
        {
            IsWorking = true;
            _timer = new Timer(state => BackgroundWorker(), 0, 0, _setting.General.Interval*60000);
        }

        public void Stop()
        {
            IsWorking = false;
            _timer.Dispose();
        }

        #endregion

        #region //*** Other Members ***\\

        private void BackgroundWorker()
        {
            IsJobStarted = true;
            foreach (var printer in _setting.Printers)
            {
                try
                {
                    var plugin = _pluginsManager.GetPluginByName(printer.Plugin);
                    if (plugin != null)
                    {
                        plugin.Initialize("", printer.Caption);
                        var inkLevel = plugin.GetInkLevel();
                        if (inkLevel == InkStatusResult.OK)
                        {
                            foreach (var level in plugin.InkLevel)
                            {
                                if (level.Value <= printer.Level)
                                {
                                    SendNotification(printer, plugin.InkLevel);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Global.Logger.Fatal(e.Message);
                }
            }
            IsJobStarted = false;
        }

        private void SendNotification(SettingsPrinter printer, List<InkLevel> inkLevel)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(printer.LastNotification) || DateTime.Parse(printer.LastNotification).ToUniversalTime() < DateTime.Now.AddDays(-7))
                {
                    printer.LastNotification = DateTime.Now.ToString("u");
                    _setting.SaveToFile(Global.SettingsFileName);
                    string msg = "User Details:\r\n\r\n";
                    msg += _setting.Customer.Name + "\r\n\r\n";
                    msg += _setting.Customer.Email + "\r\n\r\n";

                    msg += "Printer Details:\r\n\r\n";
                    msg += printer.Name + "\r\n";

                    foreach (var level in inkLevel)
                    {
                        if (level.Value <= printer.Level)
                        {
                            msg += string.Format("{0} - {1:00%}\r\n", level.Name, level.Value);
                        }
                    }

                    msg += "\r\n\r\n";
                    msg += "Referring Details:\r\n\r\n";
                    msg += "Person Referring Code:  " + _setting.Customer.RefByCode;


                    HttpHelper.PostNotification(_setting, msg);
                    if (_setting.General.UserNotification.ToLower() == "true")
                    {
                        UserInterfaceHelper.NotifYUser(_setting, printer, inkLevel);
                    }
                }
            }
            catch (Exception e)
            {
                Global.Logger.Fatal(e.Message);
            }
        }

        #endregion
    }
}