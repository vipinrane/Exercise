using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;


namespace Dytrex.EndirePro.BarcodeScanner.Model
{
    /// <summary>
    /// Summary description for Settings.
    /// </summary>
    public class SettingsNew
    {
        private static NameValueCollection m_settings;
        private static string m_settingsPath;

        static SettingsNew()
        {
            m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            m_settingsPath += @"\ConfigPropertiesNew.xml";

            if (!File.Exists(m_settingsPath))
                throw new FileNotFoundException(m_settingsPath + " could not be found.");

            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(m_settingsPath);
            XmlElement root = xdoc.DocumentElement;
            System.Xml.XmlNodeList nodeList = root.ChildNodes.Item(0).ChildNodes;

            XElement xelement = XElement.Load(m_settingsPath);
            IEnumerable<XElement> configProperties = xelement.Elements();
             // Add settings to the NameValueCollection.
            m_settings = new NameValueCollection();
           
            //m_settings.Add("PreambleDevice", nodeList.Item(1).Attributes["value"].Value);
            //m_settings.Add("PostambleDevice", nodeList.Item(2).Attributes["value"].Value);
            //m_settings.Add("SecurityModeDevice", nodeList.Item(3).Attributes["value"].Value);
            //m_settings.Add("SoundConfigDeviceGoodScan", nodeList.Item(4).Attributes["value"].Value);
            //m_settings.Add("SoundConfigDeviceGoodScanLocal", nodeList.Item(4).Attributes["value"].Value);
            //m_settings.Add("SoundConfigDeviceBadScan", nodeList.Item(4).Attributes["value"].Value);
            //m_settings.Add("SoundConfigDeviceBadScanLocal", nodeList.Item(4).Attributes["value"].Value);
            //m_settings.Add("TimersDevice", nodeList.Item(5).Attributes["value"].Value);
            //m_settings.Add("LocalAcknowledgmentDevice", nodeList.Item(6).Attributes["value"].Value);
            //m_settings.Add("LocalDecodeActionDevice", nodeList.Item(7).Attributes["value"].Value);
            //m_settings.Add("RumbleConfigDevice", nodeList.Item(8).Attributes["value"].Value);
            //m_settings.Add("DataStoreDevice", nodeList.Item(9).Attributes["value"].Value);
            //m_settings.Add("NotificationsDevice", nodeList.Item(10).Attributes["value"].Value);
            //m_settings.Add("ConnectionBeepConfigDevice", nodeList.Item(11).Attributes["value"].Value);

            foreach (var configProperty in configProperties)
            {
                string propertyName = configProperty.Element("Name").Value;
                switch (propertyName)
                {
                    case "DataConfirmationMode":
                        m_settings.Add("DataConfirmationMode", configProperty.Element("parameterData").Attribute("value").Value);
                        break;

                    case "PreambleDevice":
                        m_settings.Add("PreambleDevice", configProperty.Element("parameterData").Attribute("value").Value);
                        break;

                    case "PostambleDevice":
                        m_settings.Add("PostambleDevice", configProperty.Element("parameterData").Attribute("value").Value);
                        break;

                    case "SecurityModeDevice":
                        m_settings.Add("SecurityModeDevice", configProperty.Element("parameterData").Attribute("value").Value);
                        break;

                    case "SoundConfigDeviceGoodScan":
                        foreach (XElement xEleParameters in configProperty.Descendants("ParametersWithData"))
                        {
                            m_settings.Add("SCDGoodScanActionType",xEleParameters.Element("ActionType").Value);
                            m_settings.Add("SCDGoodScanFrequency", xEleParameters.Element("Frequency").Value);
                            m_settings.Add("SCDGoodScanDuration", xEleParameters.Element("Duration").Value);
                            m_settings.Add("SCDGoodScanPause", xEleParameters.Element("Pause").Value);
                        }
                        break;

                    case "SoundConfigDeviceGoodScanLocal":
                        foreach (XElement xEleParameters in configProperty.Descendants("ParametersWithData"))
                        {
                            m_settings.Add("SCDGoodScanLocalActionType", xEleParameters.Element("ActionType").Value);
                            m_settings.Add("SCDGoodScanLocalFrequency", xEleParameters.Element("Frequency").Value);
                            m_settings.Add("SCDGoodScanLocalDuration", xEleParameters.Element("Duration").Value);
                            m_settings.Add("SCDGoodScanLocalPause", xEleParameters.Element("Pause").Value);
                        }
                        break;

                    case "SoundConfigDeviceBadScan":
                        foreach (XElement xEleParameters in configProperty.Descendants("ParametersWithData"))
                        {
                            m_settings.Add("SCDBadScanActionType", xEleParameters.Element("ActionType").Value);
                            m_settings.Add("SCDBadScanFrequency", xEleParameters.Element("Frequency").Value);
                            m_settings.Add("SCDBadScanDuration", xEleParameters.Element("Duration").Value);
                            m_settings.Add("SCDBadScanPause", xEleParameters.Element("Pause").Value);
                        }
                        break;

                    case "SoundConfigDeviceBadScanLocal":
                        foreach (XElement xEleParameters in configProperty.Descendants("ParametersWithData"))
                        {
                            m_settings.Add("SCDBadScanLocalActionType", xEleParameters.Element("ActionType").Value);
                            m_settings.Add("SCDBadScanLocalFrequency", xEleParameters.Element("Frequency").Value);
                            m_settings.Add("SCDBadScanLocalDuration", xEleParameters.Element("Duration").Value);
                            m_settings.Add("SCDBadScanLocalPause", xEleParameters.Element("Pause").Value);
                        }
                        break;

                    case "TimersDevice":
                        break;

                    case "LocalAcknowledgmentDevice":
                        break;

                    case "LocalDecodeActionDevice":
                        break;

                    case "RumbleConfigDevice":
                        break;

                    case "DataStoreDevice":
                        break;

                    case "NotificationsDevice":
                        break;

                    case "ConnectionBeepConfigDevice":
                        break;
                    
                }

            }

            
        }

        public static string DataConfirmationMode
        {
            get { return m_settings.Get("DataConfirmationMode"); }
            set { m_settings.Set("DataConfirmationMode", value); }
        }

        public static string PreambleDevice
        {
            get { return m_settings.Get("PreambleDevice"); }
            set { m_settings.Set("PreambleDevice", value); }
        }

        public static string PostambleDevice
        {
            get { return m_settings.Get("PostambleDevice"); }
            set { m_settings.Set("PostambleDevice", value); }
        }

        public static string SecurityModeDevice
        {
            get { return m_settings.Get("SecurityModeDevice"); }
            set { m_settings.Set("SecurityModeDevice", value); }
        }

        #region SoundConfigDevice

        #region GoodScan

        /// <summary>
        /// SoundConfigDevice GoodScan ActionType
        /// </summary>
        public static string SCDGoodScanActionType
        {
            get { return m_settings.Get("SCDGoodScanActionType"); }
            set { m_settings.Set("SCDGoodScanActionType", value); }
        }

        /// <summary>
        /// SoundConfigDevice GoodScan Frequency
        /// </summary>
        public static string SCDGoodScanFrequency
        {
            get { return m_settings.Get("SCDGoodScanFrequency"); }
            set { m_settings.Set("SCDGoodScanFrequency", value); }
        }

        /// <summary>
        /// SoundConfigDevice GoodScan Duration
        /// </summary>
        public static string SCDGoodScanDuration
        {
            get { return m_settings.Get("SCDGoodScanDuration"); }
            set { m_settings.Set("SCDGoodScanDuration", value); }
        }

        /// <summary>
        /// SoundConfigDevice GoodScan Pause
        /// </summary>
        public static string SCDGoodScanPause
        {
            get { return m_settings.Get("SCDGoodScanPause"); }
            set { m_settings.Set("SCDGoodScanPause", value); }
        }

        #endregion

        #region GoodScanLocal

        /// <summary>
        /// SoundConfigDevice GoodScan ActionType
        /// </summary>
        public static string SCDGoodScanLocalActionType
        {
            get { return m_settings.Get("SCDGoodScanLocalActionType"); }
            set { m_settings.Set("SCDGoodScanLocalActionType", value); }
        }

        /// <summary>
        /// SoundConfigDevice GoodScanLocal Frequency
        /// </summary>
        public static string SCDGoodScanLocalFrequency
        {
            get { return m_settings.Get("SCDGoodScanLocalFrequency"); }
            set { m_settings.Set("SCDGoodScanLocalFrequency", value); }
        }

        /// <summary>
        /// SoundConfigDevice GoodScanLocal Duration
        /// </summary>
        public static string SCDGoodScanLocalDuration
        {
            get { return m_settings.Get("SCDGoodScanLocalDuration"); }
            set { m_settings.Set("SCDGoodScanLocalDuration", value); }
        }

        /// <summary>
        /// SoundConfigDevice GoodScanLocal Pause
        /// </summary>
        public static string SCDGoodScanLocalPause
        {
            get { return m_settings.Get("SCDGoodScanLocalPause"); }
            set { m_settings.Set("SCDGoodScanLocalPause", value); }
        }

        #endregion

        #region BadScan

        /// <summary>
        /// SoundConfigDevice BadScan ActionType
        /// </summary>
        public static string SCDBadScanActionType
        {
            get { return m_settings.Get("SCDBadScanActionType"); }
            set { m_settings.Set("SCDBadScanActionType", value); }
        }

        /// <summary>
        /// SoundConfigDevice BadScan Frequency
        /// </summary>
        public static string SCDBadScanFrequency
        {
            get { return m_settings.Get("SCDBadScanFrequency"); }
            set { m_settings.Set("SCDBadScanFrequency", value); }
        }

        /// <summary>
        /// SoundConfigDevice BadScan Duration
        /// </summary>
        public static string SCDBadScanDuration
        {
            get { return m_settings.Get("SCDBadScanDuration"); }
            set { m_settings.Set("SCDBadScanDuration", value); }
        }

        /// <summary>
        /// SoundConfigDevice BadScan Pause
        /// </summary>
        public static string SCDBadScanPause
        {
            get { return m_settings.Get("SCDBadScanPause"); }
            set { m_settings.Set("SCDBadScanPause", value); }
        }

        #endregion

        #region BadScanLocal

        /// <summary>
        /// SoundConfigDevice BadScanLocal ActionType
        /// </summary>
        public static string SCDBadScanLocalActionType
        {
            get { return m_settings.Get("SCDBadScanLocalActionType"); }
            set { m_settings.Set("SCDBadScanLocalActionType", value); }
        }

        /// <summary>
        /// SoundConfigDevice BadScanLocal Frequency
        /// </summary>
        public static string SCDBadScanLocalFrequency
        {
            get { return m_settings.Get("SCDBadScanLocalFrequency"); }
            set { m_settings.Set("SCDBadScanLocalFrequency", value); }
        }

        /// <summary>
        /// SoundConfigDevice BadScanLocal Duration
        /// </summary>
        public static string SCDBadScanLocalDuration
        {
            get { return m_settings.Get("SCDBadScanLocalDuration"); }
            set { m_settings.Set("SCDBadScanLocalDuration", value); }
        }

        /// <summary>
        /// SoundConfigDevice BadScanLocal Pause
        /// </summary>
        public static string SCDBadScanLocalPause
        {
            get { return m_settings.Get("SCDBadScanLocalPause"); }
            set { m_settings.Set("SCDBadScanLocalPause", value); }
        }

        #endregion

        #endregion

        public static string TimersDevice
        {
            get { return m_settings.Get("TimersDevice"); }
            set { m_settings.Set("TimersDevice", value); }
        }

        public static string LocalAcknowledgmentDevice
        {
            get { return m_settings.Get("LocalAcknowledgmentDevice"); }
            set { m_settings.Set("LocalAcknowledgmentDevice", value); }
        }

        public static string LocalDecodeActionDevice
        {
            get { return m_settings.Get("LocalDecodeActionDevice"); }
            set { m_settings.Set("LocalDecodeActionDevice", value); }
        }

        public static string RumbleConfigDevice
        {
            get { return m_settings.Get("RumbleConfigDevice"); }
            set { m_settings.Set("RumbleConfigDevice", value); }
        }

        public static string DataStoreDevice
        {
            get { return m_settings.Get("DataStoreDevice"); }
            set { m_settings.Set("DataStoreDevice", value); }
        }

        public static string NotificationsDevice
        {
            get { return m_settings.Get("NotificationsDevice"); }
            set { m_settings.Set("NotificationsDevice", value); }
        }

        public static string ConnectionBeepConfigDevice
        {
            get { return m_settings.Get("ConnectionBeepConfigDevice"); }
            set { m_settings.Set("ConnectionBeepConfigDevice", value); }
        }
    }
}
