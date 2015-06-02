using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EditCell
{
    /// <summary>
    /// Wrapper for the native Pocket PC power status interface.
    /// </summary>
    /// <remarks>
    /// See the MSDN <see href="http://msdn.microsoft.com/en-us/library/aa453172.aspx">article</see> for more information.
    /// </remarks>
    public class PowerStatus
    {
#pragma warning disable 649, 1591
        /*
        //[StructLayout(LayoutKind.Sequential)]
        public class SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
            public uint BatteryVoltage;
            public uint BatteryCurrent;
            public uint BatteryAverageCurrent;
            public uint BatteryAverageInterval;
            public uint BatterymAHourConsumed;
            public uint BatteryTemperature;
            public uint BackupBatteryVoltage;
            public byte BatteryChemistry;
        }
        */

        //[StructLayout(LayoutKind.Sequential)]
        public class SYSTEM_POWER_STATUS_EX
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifetime;
            public uint BatteryFullLifetime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifetime;
            public uint BackupBatteryFullLifetime;
        }
#pragma warning restore

        /// <summary>
        /// Returns the current device power status.
        /// </summary>
        /// <remarks>
        /// See MSDN <see href="http://msdn.microsoft.com/en-us/library/aa453172.aspx">documentation</see>.
        /// </remarks>
        /// <param name="lpSystemPowerStatus">Power status struct.</param>
        /// <param name="fUpdate">Whether to use cached information.</param>
        /// <returns>This function returns TRUE if successful; otherwise, it returns FALSE.</returns>
        [DllImport("coredll")]
        private static extern uint GetSystemPowerStatusEx(SYSTEM_POWER_STATUS_EX lpSystemPowerStatus,
            bool fUpdate);

        /*
        /// <summary>
        /// Returns the current device power status.
        /// </summary>
        /// <param name="lpSystemPowerStatus"></param>
        /// <param name="dwLen"></param>
        /// <param name="fUpdate"></param>
        /// <returns></returns>
        [DllImport("coredll")]
        private static extern uint GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 lpSystemPowerStatus,
            uint dwLen, bool fUpdate);
        */

		/// <summary>
		/// Gets the current battery status as int
		/// </summary>
		public static int BatteryLife
		{
			get
			{
				PowerStatus.SYSTEM_POWER_STATUS_EX status = new PowerStatus.SYSTEM_POWER_STATUS_EX();
				if (PowerStatus.GetSystemPowerStatusEx(status, false) == 1)
					return status.BatteryLifePercent;
				return 0; // Error.
			}
		}
    }
}
