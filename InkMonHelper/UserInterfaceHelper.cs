#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : UserInterfaceHelper.cs
// ////////  Created On : 2014/02/10
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

#endregion

namespace InkMonHelper
{
    public static class UserInterfaceHelper
    {
        #region //*** Methods ***\\

        public static void NotifYUser(Settings settings, SettingsPrinter printer, List<InkLevel> inkLevel)
        {
            string title = "Printer Ink is at low level";
            string body = "";
            //body += title + "\n\r\n\r";
            body += "Printer Details:\n\r";
            body += printer.Name + "\n\r";

            foreach (var level in inkLevel)
            {
                if (level.Value <= printer.Level)
                {
                    body += string.Format("{0} - {1:00%}\n\r", level.Name, level.Value);
                }
            }
            body += "\n\r\n\r*****";

            try
            {
                var pipeClient = new NamedPipeClientStream("InkMonPipe");

                pipeClient.Connect(3000);

                if (pipeClient.IsConnected)
                {
                    var buf = Encoding.ASCII.GetBytes(body);
                    pipeClient.Write(buf, 0, buf.Length);
                    pipeClient.WaitForPipeDrain();
                    pipeClient.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}