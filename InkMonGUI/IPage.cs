#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonGUI
// ////////  File Name : IPage.cs
// ////////  Created On : 2014/02/13
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.Windows.Forms;
using InkMonHelper;

#endregion

namespace InkMonGUI
{
    public interface IPage
    {
        #region //*** Properties ***\\

        Settings CurrentSettings { get; set; }
        Settings NewSettings { get; set; }

        #endregion

        #region //*** Methods ***\\

        void Initialize(Button cancelButton, Button nextButton, Button backButton);
        void DoAction();

        #endregion

        #region //*** Other Members ***\\

        event EventHandler MoveToNextPage;
        event EventHandler MoveToPreviousPage;
        event EventHandler ExitSetup;

        #endregion
    }
}