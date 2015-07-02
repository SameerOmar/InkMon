#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : TablessControl.cs
// ////////  Created On : 2014/04/26
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Windows.Forms;

#endregion

namespace InkMonGUI.UserControls
{

    internal class TablessControl : TabControl
    {
        #region //*** Other Members ***\\

        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr) 1;
            else base.WndProc(ref m);
        }

        #endregion
    }
}