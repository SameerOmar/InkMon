#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : SpecialFolderPatternConverter.cs
// ////////  Created On : 2014/02/10
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System;
using System.IO;
using log4net.Util;

#endregion

namespace InkMonHelper
{
    public class SpecialFolderPatternConverter : PatternConverter
    {
        #region //*** Other Members ***\\

        protected override void Convert(TextWriter writer, object state)
        {
            Environment.SpecialFolder specialFolder =
                (Environment.SpecialFolder) Enum.Parse(typeof (Environment.SpecialFolder), Option, true);
            writer.Write(Environment.GetFolderPath(specialFolder));
        }

        #endregion
    }
}