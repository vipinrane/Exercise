// ***********************************************************************
// Assembly         : DytrexEndirePro.BarcodeScanner.Module
// Author           : Alexander Burghardt
// Created          : 11-24-2014
//
// Last Modified By : Alexander Burghardt
// Last Modified On : 11-24-2014
// ***********************************************************************
// <copyright file="ConfigConstants.cs" company="DYTREX Ltd.">
//     Copyright (c) DYTREX Ltd.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace TestAppSettingReading
{
    /// <summary>
    /// Enum ConfigConstants
    /// </summary>
    internal enum ConfigConstants
    {
        /// <summary>
        /// The data confirmation mode
        /// </summary>
        DataConfirmationMode,
        /// <summary>
        /// The preamble device
        /// </summary>
        PreambleDevice,
        /// <summary>
        /// The postamble device
        /// </summary>
        PostambleDevice,
        /// <summary>
        /// The security mode device
        /// </summary>
        SecurityModeDevice,
        /// <summary>
        /// The sound configuration device good scan
        /// </summary>
        SoundConfigDeviceGoodScan,
        /// <summary>
        /// The sound configuration device good scan local
        /// </summary>
        SoundConfigDeviceGoodScanLocal,
        /// <summary>
        /// The sound configuration device bad scan
        /// </summary>
        SoundConfigDeviceBadScan,
        /// <summary>
        /// The sound configuration device bad scan local
        /// </summary>
        SoundConfigDeviceBadScanLocal,
        /// <summary>
        /// The action type
        /// </summary>
        ActionType,
        /// <summary>
        /// The frequency
        /// </summary>
        Frequency,
        /// <summary>
        /// The duration
        /// </summary>
        Duration,
        /// <summary>
        /// The pause
        /// </summary>
        Pause,
        /// <summary>
        /// The timers device
        /// </summary>
        TimersDevice,
        /// <summary>
        /// The timers mask
        /// </summary>
        TimersMask,
        /// <summary>
        /// The trigger lockout
        /// </summary>
        TriggerLockout,
        /// <summary>
        /// The disconnected timeout
        /// </summary>
        DisconnectedTimeout,
        /// <summary>
        /// The connected timeout
        /// </summary>
        ConnectedTimeout,
        /// <summary>
        /// The local acknowledgment device
        /// </summary>
        LocalAcknowledgmentDevice,

        /// <summary>
        /// The local decode action of the device
        /// </summary>
        LocalDecodeActionDevice,
        
        /// <summary>
        /// The rumble configuration device
        /// </summary>
        RumbleConfigDevice,

        /// <summary>
        /// The rumble configuration device for GoodScan
        /// </summary>
        RumbleConfigDeviceGoodScan,

        /// <summary>
        /// The rumble configuration device for GoodScanLocal
        /// </summary>
        RumbleConfigDeviceGoodScanLocal,

        /// <summary>
        /// The rumble configuration device for BadScan
        /// </summary>
        RumbleConfigDeviceBadScan,

        /// <summary>
        /// The rumble configuration device for BadScanLocal
        /// </summary>
        RumbleConfigDeviceBadScanLocal,

        /// <summary>
        /// Number of rumbles
        /// </summary>
        NumberOfRumbles,

        /// <summary>
        /// Collection of Duration-Pause.
        /// </summary>
        DurationPauseData,

        /// <summary>
        /// The data store device
        /// </summary>
        DataStoreDevice,
        /// <summary>
        /// Holds Datastore identifier from 0 to 15 out which only 0th identifier is supported.
        /// </summary>
        IdentifierKey,
        /// <summary>
        /// Holds the size of the store, max 64.
        /// </summary>
        DataLength,
        /// <summary>
        /// Holds the actual data in store.
        /// </summary>
        Data,

        /// <summary>
        /// The notifications device
        /// </summary>
        NotificationsDevice,
        /// <summary>
        /// The connection beep configuration device
        /// </summary>
        ConnectionBeepConfigDevice,

        /// <summary>
        /// ScanAPI
        /// </summary>
        ScanAPI,

        /// <summary>
        /// Time period in milliseconds.
        /// </summary>
        TimerPeriod,

        /// <summary>
        /// Comport used by ScanAPI to communicate with device.
        /// </summary>
        Comport

    }
}
