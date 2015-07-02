#region File Header

// ///////////////////////////////////////////////////////////////////////////////////// //
// ////////  Solution : InkMon
// ////////  Project : InkMonHelper
// ////////  File Name : IInkStatus.cs
// ////////  Created On : 2014/01/31
// ////////  Author : Sameer M. Omar
// ////////  Email : Sameer@felixsoft.com
// ///////////////////////////////////////////////////////////////////////////////////// //

#endregion

#region Using Directives

using System.Collections.Generic;

#endregion

public struct InkLevel
{
    #region //*** Fields ***\\

    public string Name;
    public int Value;

    #endregion

    #region //*** Constructors/Deconstructor ***\\

    public InkLevel(string name, int value)
    {
        Name = name;
        Value = value;
    }

    #endregion
}

public enum InkStatusResult
{
    OK,
    Error,
    NotSupported
}

public enum CartridgeType
{
    CartridgeNotPresent,
    CartridgeBlack,
    CartridgeColor,
    CartridgePhoto,
    CartridgeCyan,
    CartridgeMagenta,
    CartridgeYellow,
    CartridgePhotblack,
    CartridgePhotocyan,
    CartridgePhotomagenta,
    CartridgePhotoyellow,
    CartridgeRed,
    CartridgeGreen,
    CartridgeBlue,
    CartridgeLightblack,
    CartridgeLighcyan,
    CartridgeLightmagenta,
    CartridgeLightLightblack,
    CartridgeMatteblack,
    CartridgeGlossoptimizer,
    CartridgeUnknown,
    CartridgeKcm,
    CartridgeGgk,
    CartridgeKcmy,
    CartridgeLcLm,
    CartridgeYm,
    CartridgeCk,
    CartridgeLgpk,
    CartridgeLg,
    CartridgeG,
    CartridgePg,
    CartridgeWhite = 31,
    CartridgeOther01,
    CartridgeOther02,
    CartridgeOther03,
    CartridgeOther04,
    CartridgeOther05,
    CartridgeOther06,
    CartridgeOther07,
    CartridgeOther08,
    CartridgeOther09,
    CartridgeOther10
}

namespace InkMonHelper
{

    public interface IInkStatus
    {
        #region //*** Properties ***\\

        string Name { get; }
        string Brand { get; }
        string Model { get; }
        string Port { get; }
        List<InkLevel> InkLevel { get; }

        #endregion

        #region //*** Methods ***\\

        bool Initialize(string model, string printerName);
        InkStatusResult GetInkLevel();

        #endregion
    }
}