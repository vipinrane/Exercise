// ***********************************************************************
// Assembly         : DytrexEndirePro.BarcodeScanner.Module
// Author           : Vipin Rane
// Created          : 08-20-2014
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="BarcodeScannerConstants.cs" company="DYTREX Ltd.">
//     Copyright (c) DYTREX Ltd.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Reflection;

namespace TestAppSettingReading
{
    /// <summary>
    /// Specific constants for the barcodeScanner module.
    /// </summary>
    internal static class BarcodeScannerConstants
    {
        #region Constants

        /// <summary>
        /// Determines the codebase of its own module.
        /// </summary>
        internal static readonly string ModuleCodeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;

        /// <summary>
        /// The name of the COM port.
        /// </summary>
        internal const string ComPortName = "BarcodeScannerComPort";

        #endregion
    }
}
