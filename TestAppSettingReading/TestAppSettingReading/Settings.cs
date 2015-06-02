// ***********************************************************************
// Assembly         : Dytrex.EndirePro.BarcodeScanner.Model
// Author           : Vipin Rane
// Created          : 05-Nov-2014
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="Settings.cs" company="DYTREX Ltd.">
//     Copyright (c) DYTREX Ltd.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
//using Dytrex.EndirePro.Config;

namespace TestAppSettingReading
{
    /// <summary>
    /// Summary description for Settings.
    /// </summary>
    public class Settings
    {
        #region Config Properties[NamedValue properties]

        /// <summary>
        /// Gets or Sets string value to Dataconfirmation Mode.
        /// </summary>
        public static string DataConfirmationMode
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.DataConfirmationMode); }
            //get { return m_settings.Get("DataConfirmationMode"); }
            //set { m_settings.Set("DataConfirmationMode", value); }
        }

        /// <summary>
        /// Gets or Sets string value to PreambleDevice.
        /// </summary>
        public static string PreambleDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.PreambleDevice); }
            //get { return m_settings.Get ( "PreambleDevice" ); }
            //set { m_settings.Set ( "PreambleDevice", value ); }
        }

        /// <summary>
        /// Gets or Sets string value to PostambleDevice.
        /// </summary>
        public static string PostambleDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.PostambleDevice); }
            //get { return m_settings.Get ( "PostambleDevice" ); }
            //set { m_settings.Set ( "PostambleDevice", value ); }
        }

        /// <summary>
        /// Gets or Sets string value to SecurityModeDevice.
        /// </summary>
        public static string SecurityModeDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.SecurityModeDevice); }
            //get { return m_settings.Get ( "SecurityModeDevice" ); }
            //set { m_settings.Set ( "SecurityModeDevice", value ); }
        }

        #region SoundConfigDevice

        #region GoodScan

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScan-ActionType.
        /// </summary>
        public static string SCDGoodScanActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.ActionType); }
            //get { return m_settings.Get ( "SCDGoodScanActionType" ); }
            //set { m_settings.Set ( "SCDGoodScanActionType", value ); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScan-Frequency.
        /// </summary>
        public static string SCDGoodScanFrequency
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.Frequency); }
            //get { return m_settings.Get ( "SCDGoodScanFrequency" ); }
            //set { m_settings.Set ( "SCDGoodScanFrequency", value ); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScan-Duration.
        /// </summary>
        public static string SCDGoodScanDuration
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.Duration); }
            //get { return m_settings.Get ( "SCDGoodScanDuration" ); }
            //set { m_settings.Set ( "SCDGoodScanDuration", value ); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScan-Pause.
        /// </summary>
        public static string SCDGoodScanPause
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceGoodScan,ConfigConstants.Pause); }
            //get { return m_settings.Get ( "SCDGoodScanPause" ); }
            //set { m_settings.Set ( "SCDGoodScanPause", value ); }
        }

        #endregion

        #region GoodScanLocal

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScan-ActionType.
        /// </summary>
        public static string SCDGoodScanLocalActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceGoodScanLocal,ConfigConstants.ActionType); }
            //get { return m_settings.Get("SCDGoodScanLocalActionType"); }
            //set { m_settings.Set("SCDGoodScanLocalActionType", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScanLocal-Frequency.
        /// </summary>
        public static string SCDGoodScanLocalFrequency
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceGoodScanLocal,ConfigConstants.Frequency); }
            //get { return m_settings.Get("SCDGoodScanLocalFrequency"); }
            //set { m_settings.Set("SCDGoodScanLocalFrequency", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScanLocal-Duration.
        /// </summary>
        public static string SCDGoodScanLocalDuration
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceGoodScanLocal,ConfigConstants.Duration); }
            //get { return m_settings.Get("SCDGoodScanLocalDuration"); }
            //set { m_settings.Set("SCDGoodScanLocalDuration", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-GoodScanLocal-Pause.
        /// </summary>
        public static string SCDGoodScanLocalPause
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceGoodScanLocal,ConfigConstants.Pause); }
            //get { return m_settings.Get("SCDGoodScanLocalPause"); }
            //set { m_settings.Set("SCDGoodScanLocalPause", value); }
        }

        #endregion

        #region BadScan

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScan-ActionType.
        /// </summary>
        public static string SCDBadScanActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.ActionType); }
            //get { return m_settings.Get("SCDBadScanActionType"); }
            //set { m_settings.Set("SCDBadScanActionType", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScan-Frequency.
        /// </summary>
        public static string SCDBadScanFrequency
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.Frequency); }
            //get { return m_settings.Get("SCDBadScanFrequency"); }
            //set { m_settings.Set("SCDBadScanFrequency", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScan-Duration.
        /// </summary>
        public static string SCDBadScanDuration
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.Duration); }
            //get { return m_settings.Get("SCDBadScanDuration"); }
            //set { m_settings.Set("SCDBadScanDuration", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScan-Pause.
        /// </summary>
        public static string SCDBadScanPause
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.Pause); }
            //get { return m_settings.Get("SCDBadScanPause"); }
            //set { m_settings.Set("SCDBadScanPause", value); }
        }

        #endregion

        #region BadScanLocal

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScanLocal-ActionType.
        /// </summary>
        public static string SCDBadScanLocalActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceBadScanLocal,ConfigConstants.ActionType); }
            //get { return m_settings.Get("SCDBadScanLocalActionType"); }
            //set { m_settings.Set("SCDBadScanLocalActionType", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScanLocal-Frequency.
        /// </summary>
        public static string SCDBadScanLocalFrequency
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceBadScanLocal,ConfigConstants.Frequency); }
            //get { return m_settings.Get("SCDBadScanLocalFrequency"); }
            //set { m_settings.Set("SCDBadScanLocalFrequency", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScanLocal-Duration.
        /// </summary>
        public static string SCDBadScanLocalDuration
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceBadScanLocal,ConfigConstants.Duration); }
            //get { return m_settings.Get("SCDBadScanLocalDuration"); }
            //set { m_settings.Set("SCDBadScanLocalDuration", value); }
        }

        /// <summary>
        /// Gets or Sets string value to SoundConfigDevice-BadScanLocal-Pause.
        /// </summary>
        public static string SCDBadScanLocalPause
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.SoundConfigDeviceBadScanLocal,ConfigConstants.Pause); }
            //get { return m_settings.Get("SCDBadScanLocalPause"); }
            //set { m_settings.Set("SCDBadScanLocalPause", value); }
        }

        #endregion

        #endregion

        #region TimersDevice
        /// <summary>
        /// Gets or Sets string value to TimersDevice.
        /// </summary>
        public static string TimerMask
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.TimersDevice,ConfigConstants.TimersMask); }
            //get { return m_settings.Get("TimersMask"); }
            //set { m_settings.Set("TimersMask", value); }
        }
        public static string TimerTriggerLockout
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.TimersDevice,ConfigConstants.TriggerLockout); }
            //get { return m_settings.Get("TimerTriggerLockout"); }
            //set { m_settings.Set("TimerTriggerLockout", value); }
        }
        public static string TimerDisconnectedTimeout
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.TimersDevice,ConfigConstants.DisconnectedTimeout); }
            //get { return m_settings.Get("TimerDisconnectedTimeout"); }
            //set { m_settings.Set("TimerDisconnectedTimeout", value); }
        }
        public static string TimerConnectedTimeout
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.TimersDevice,ConfigConstants.ConnectedTimeout); }
            //get { return m_settings.Get("TimerConnectedTimeout"); }
            //set { m_settings.Set("TimerConnectedTimeout", value); }
        }
        #endregion

        /// <summary>
        /// Gets or Sets string value to LocalAcknowledgmentDevice.
        /// </summary>
        public static string LocalAcknowledgmentDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,null,ConfigConstants.LocalAcknowledgmentDevice); }
            //get { return m_settings.Get("LocalAcknowledgmentDevice"); }
            //set { m_settings.Set("LocalAcknowledgmentDevice", value); }
        }

        /// <summary>
        /// Gets or Sets string value to LocalDecodeActionDevice.
        /// </summary>
        public static string LocalDecodeActionDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,null,ConfigConstants.LocalDecodeActionDevice); }
            //get { return m_settings.Get("LocalDecodeActionDevice"); }
            //set { m_settings.Set("LocalDecodeActionDevice", value); }
        }

        #region RumbleConfigDevice

        #region Rumbleconfig - GoodScan
        /// <summary>
        /// Gets string value to RumbleConfigDeviceGoodScanActionType.
        /// </summary>
        public static string RumbleConfigDeviceGoodScanActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.RumbleConfigDeviceGoodScan,ConfigConstants.ActionType); }
        }

        /// <summary>
        /// Gets string value to RumbleConfigDeviceGoodScanNumberOfRumbles.
        /// </summary>
        public static string RumbleConfigDeviceGoodScanNumberOfRumbles
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.RumbleConfigDeviceGoodScan,ConfigConstants.NumberOfRumbles); }
        }
        /// <summary>
        /// Gets string value to RumbleConfigDeviceGoodScanDurationPauseData.
        /// </summary>
        public static string RumbleConfigDeviceGoodScanDurationPauseData
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.RumbleConfigDeviceGoodScan,ConfigConstants.DurationPauseData); }
        }
        #endregion

        #region Rumbleconfig - GoodScanLocal
        /// <summary>
        /// Gets string value to RumbleConfigDeviceGoodScanLocalActionType.
        /// </summary>
        public static string RumbleConfigDeviceGoodScanLocalActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceGoodScanLocal, ConfigConstants.ActionType); }
        }

        /// <summary>
        /// Gets string value to RumbleConfigDeviceGoodScanLocalNumberOfRumbles.
        /// </summary>
        public static string RumbleConfigDeviceGoodScanLocalNumberOfRumbles
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceGoodScanLocal, ConfigConstants.NumberOfRumbles); }
        }
        /// <summary>
        /// Gets string value to RumbleConfigDeviceGoodScanLocalDurationPauseData.
        /// </summary>
        public static string RumbleConfigDeviceGoodScanLocalDurationPauseData
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceGoodScanLocal, ConfigConstants.DurationPauseData); }
        }
        #endregion

        #region Rumbleconfig - BadScan
        /// <summary>
        /// Gets string value to RumbleConfigDeviceBadScanActionType.
        /// </summary>
        public static string RumbleConfigDeviceBadScanActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceBadScan, ConfigConstants.ActionType); }
        }

        /// <summary>
        /// Gets string value to RumbleConfigDeviceBadScanNumberOfRumbles.
        /// </summary>
        public static string RumbleConfigDeviceBadScanNumberOfRumbles
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceBadScan, ConfigConstants.NumberOfRumbles); }
        }
        /// <summary>
        /// Gets string value to RumbleConfigDeviceBadScanDurationPauseData.
        /// </summary>
        public static string RumbleConfigDeviceBadScanDurationPauseData
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceBadScan, ConfigConstants.DurationPauseData); }
        }
        #endregion

        #region Rumbleconfig - BadScanLocal
        /// <summary>
        /// Gets string value to RumbleConfigDeviceBadScanLocalActionType.
        /// </summary>
        public static string RumbleConfigDeviceBadScanLocalActionType
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceBadScanLocal, ConfigConstants.ActionType); }
        }

        /// <summary>
        /// Gets string value to RumbleConfigDeviceBadScanLocalNumberOfRumbles.
        /// </summary>
        public static string RumbleConfigDeviceBadScanLocalNumberOfRumbles
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceBadScanLocal, ConfigConstants.NumberOfRumbles); }
        }
        /// <summary>
        /// Gets string value to RumbleConfigDeviceBadScanLocalDurationPauseData.
        /// </summary>
        public static string RumbleConfigDeviceBadScanLocalDurationPauseData
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.RumbleConfigDeviceBadScanLocal, ConfigConstants.DurationPauseData); }
        }
        #endregion

        #endregion 

        #region DataStoreDevice
        /// <summary>
        /// Holds Datastore Identifier from 0 to 15, current version supports only 0th identifier.
        /// </summary>
        public static string DataStoreIdentifierKey
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.DataStoreDevice, ConfigConstants.IdentifierKey); }
        }

        /// <summary>
        /// Holds the size of datastore, max 64.
        /// </summary>
        public static string DataLength
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.DataStoreDevice, ConfigConstants.DataLength); }
        }

        /// <summary>
        /// Holds the actual data to datastore.
        /// </summary>
        public static string DataStoreData
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.DataStoreDevice, ConfigConstants.Data); }
        }
        #endregion

        /// <summary>
        /// Gets or Sets string value to NotificationsDevice.
        /// </summary>
        public static string NotificationsDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,null,ConfigConstants.NotificationsDevice); }
            //get { return m_settings.Get("NotificationsDevice"); }
            //set { m_settings.Set("NotificationsDevice", value); }
        }

        /// <summary>
        /// Gets or Sets string value to ConnectionBeepConfigDevice.
        /// </summary>
        public static string ConnectionBeepConfigDevice
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,null,ConfigConstants.ConnectionBeepConfigDevice); }
            //get { return m_settings.Get("ConnectionBeepConfigDevice"); }
            //set { m_settings.Set("ConnectionBeepConfigDevice", value); }
        }

        #endregion

        #region ScanAPI Parameters
        /// <summary>
        /// Gets the Timer period in milliseconds.
        /// </summary>
        public static string ScanAPITimerPeriod
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.ScanAPI,ConfigConstants.TimerPeriod); }
        }

        /// <summary>
        /// Gets Comports for ScanAPI(Host).
        /// </summary>
        public static string ScanAPIComport
        {
            get { return ConfigHelper.GetAppSetting(BarcodeScannerConstants.ModuleCodeBase,ConfigConstants.ScanAPI,ConfigConstants.Comport); }
        }
        #endregion
    }
}
