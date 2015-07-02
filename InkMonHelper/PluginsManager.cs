#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : PluginsManager.cs
// ////////  Created On : 2014/02/09
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion

namespace InkMonHelper
{
    public class PluginsManager
    {
        #region //*** Properties ***\\

        public List<IInkStatus> LoadedPlugins { get; set; }

        #endregion

        #region //*** Constructors/Deconstructor ***\\

        public PluginsManager()
        {
            LoadedPlugins = new List<IInkStatus>();
        }

        #endregion

        #region //*** Methods ***\\

        public void LoadPlugins()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectoryInfo dir = new DirectoryInfo(path + "\\plugins");

            LoadedPlugins.Clear();

            foreach (FileInfo file in dir.GetFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(file.FullName);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetInterfaces().Count(a => a.Name == "IInkStatus") > 0)
                    {
                        try
                        {
                            var inkStatus = (IInkStatus) Activator.CreateInstance(type);

                            LoadedPlugins.Add(inkStatus);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }
        }

        public IInkStatus GetPluginByName(string name)
        {
            foreach (var plugin in LoadedPlugins)
            {
                if (plugin.GetType().AssemblyQualifiedName.StartsWith(name))
                    return plugin;
            }
            return null;
        }

        #endregion
    }
}