using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestModule
{
    public static class BarcodeScannerConstants
    {
        #region Constants

        /// <summary>
        /// Determines the codebase of its own module.
        /// </summary>
        public static readonly string ModuleCodeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;

        /// <summary>
        /// The name of the COM port.
        /// </summary>
        public const string ComPortName = "BarcodeScannerComPort";

        #endregion
    }
}
