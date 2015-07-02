#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : Win32.cs
// ////////  Created On : 2014/02/01
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

namespace InkMonHelper.WinApi
{
    public struct RECT
    {
        #region //*** Fields ***\\

        public int Bottom;
        public int Left;
        public int Right;
        public int Top;

        #endregion
    }

    public struct POINT
    {
        #region //*** Fields ***\\

        public int x;
        public int y;

        #endregion
    }

    public struct SIZE
    {
        #region //*** Fields ***\\

        public int cx;
        public int cy;

        #endregion
    }

    public struct FILETIME
    {
        #region //*** Fields ***\\

        public int dwHighDateTime;
        public int dwLowDateTime;

        #endregion
    }

    public struct SYSTEMTIME
    {
        #region //*** Fields ***\\

        public short wDay;
        public short wDayOfWeek;
        public short wHour;
        public short wMilliseconds;
        public short wMinute;
        public short wMonth;
        public short wSecond;
        public short wYear;

        #endregion
    }
}