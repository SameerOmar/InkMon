#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : Global.cs
// ////////  Created On : 2014/02/09
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using log4net;

#endregion

namespace InkMonHelper
{
    public static class Global
    {
        #region //*** Properties ***\\

        public static string SettingsFileName { get; private set; }
        public static ILog Logger { get; private set; }

        #endregion

        #region //*** Methods ***\\

        public static void Initialize()
        {
            SettingsFileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                               "\\inkmon\\settings.xml";
            Logger = LogManager.GetLogger("General");
        }

        #endregion
    }
}