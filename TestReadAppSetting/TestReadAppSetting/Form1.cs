using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TestModule;

namespace TestReadAppSetting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReadAppSetting_Click(object sender, EventArgs e)
        {
            try
            {
                //string DataconfirmationMode = Settings.DataConfirmationMode;
                StringBuilder strAllProperties = new StringBuilder();
                strAllProperties.Append(ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.DataConfirmationMode));
                strAllProperties.Append(","+ ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.PreambleDevice));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.PostambleDevice));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.SecurityModeDevice));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, null, ConfigConstants.PreambleDevice));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.ActionType));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.Frequency));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.Duration));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScan, ConfigConstants.Pause));

                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScanLocal, ConfigConstants.ActionType));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScanLocal, ConfigConstants.Frequency));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScanLocal, ConfigConstants.Duration));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceGoodScanLocal, ConfigConstants.Pause));

                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.ActionType));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.Frequency));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.Duration));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScan, ConfigConstants.Pause));

                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScanLocal, ConfigConstants.ActionType));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScanLocal, ConfigConstants.Frequency));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScanLocal, ConfigConstants.Duration));
                strAllProperties.Append("," + ConfigHelper.GetAppSetting<ConfigConstants>(BarcodeScannerConstants.ModuleCodeBase, ConfigConstants.SoundConfigDeviceBadScanLocal, ConfigConstants.Pause));

                string All = strAllProperties.ToString();
                //var allProperties = ConfigHelper.GetAppSetting<String>(BarcodeScannerConstants.ModuleCodeBase, null);
            }
            catch (Exception ex)
            {

            }
        }
    }
}