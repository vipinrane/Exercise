namespace SimpleScannerTest
{
    partial class ScannerTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.MenuItem menuItem_GeneralControlStates;
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.textScannedData = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.timerScanners = new System.Windows.Forms.Timer();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem_Cancel = new System.Windows.Forms.MenuItem();
            this.menuItem2_ReadProperties = new System.Windows.Forms.MenuItem();
            this.menuItem_ConfigurationStates = new System.Windows.Forms.MenuItem();
            this.lblFriendlyName = new System.Windows.Forms.Label();
            this.lblFriendlyNameValue = new System.Windows.Forms.Label();
            this.lbScanners = new System.Windows.Forms.ListBox();
            this.lblBluetoothAddress = new System.Windows.Forms.Label();
            this.lblBluetoothAddressValue = new System.Windows.Forms.Label();
            this.lblStatisticCounterOfDevice = new System.Windows.Forms.Label();
            this.lblScannerType = new System.Windows.Forms.Label();
            this.lblScannerTypeValue = new System.Windows.Forms.Label();
            this.lblBatteryLevel = new System.Windows.Forms.Label();
            this.pbBattery = new System.Windows.Forms.ProgressBar();
            this.lblBatteryLevelValue = new System.Windows.Forms.Label();
            this.lblFirmwareVersion = new System.Windows.Forms.Label();
            this.lblFirmwareVersionValue = new System.Windows.Forms.Label();
            this.lblPowerState = new System.Windows.Forms.Label();
            this.lblPowerStateValue = new System.Windows.Forms.Label();
            this.btnGetStatisticCounters = new System.Windows.Forms.Button();
            menuItem_GeneralControlStates = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // menuItem_GeneralControlStates
            // 
            menuItem_GeneralControlStates.Text = "General Control States";
            menuItem_GeneralControlStates.Click += new System.EventHandler(this.menuItem_GeneralControlStates_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Scanned Data";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Status";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(8, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(229, 20);
            this.lblStatus.Text = "label3";
            // 
            // textScannedData
            // 
            this.textScannedData.Location = new System.Drawing.Point(8, 146);
            this.textScannedData.Name = "textScannedData";
            this.textScannedData.Size = new System.Drawing.Size(100, 21);
            this.textScannedData.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(137, 173);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 20);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(8, 173);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(72, 20);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "&Scan";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // timerScanners
            // 
            this.timerScanners.Tick += new System.EventHandler(this.timerScanners_Tick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem_Cancel);
            this.mainMenu1.MenuItems.Add(this.menuItem2_ReadProperties);
            // 
            // menuItem_Cancel
            // 
            this.menuItem_Cancel.Text = "Cancel";
            // 
            // menuItem2_ReadProperties
            // 
            this.menuItem2_ReadProperties.Enabled = false;
            this.menuItem2_ReadProperties.MenuItems.Add(menuItem_GeneralControlStates);
            this.menuItem2_ReadProperties.MenuItems.Add(this.menuItem_ConfigurationStates);
            this.menuItem2_ReadProperties.Text = "Read Properties";
            // 
            // menuItem_ConfigurationStates
            // 
            this.menuItem_ConfigurationStates.Text = "Configuration States";
            this.menuItem_ConfigurationStates.Click += new System.EventHandler(this.menuItem_ConfigurationStates_Click);
            // 
            // lblFriendlyName
            // 
            this.lblFriendlyName.Location = new System.Drawing.Point(8, 207);
            this.lblFriendlyName.Name = "lblFriendlyName";
            this.lblFriendlyName.Size = new System.Drawing.Size(100, 20);
            this.lblFriendlyName.Text = "Friendly Name:";
            // 
            // lblFriendlyNameValue
            // 
            this.lblFriendlyNameValue.Location = new System.Drawing.Point(114, 207);
            this.lblFriendlyNameValue.Name = "lblFriendlyNameValue";
            this.lblFriendlyNameValue.Size = new System.Drawing.Size(136, 20);
            this.lblFriendlyNameValue.Text = "FNameValue";
            // 
            // lbScanners
            // 
            this.lbScanners.Location = new System.Drawing.Point(8, 53);
            this.lbScanners.Name = "lbScanners";
            this.lbScanners.Size = new System.Drawing.Size(95, 44);
            this.lbScanners.TabIndex = 9;
            // 
            // lblBluetoothAddress
            // 
            this.lblBluetoothAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblBluetoothAddress.Location = new System.Drawing.Point(8, 227);
            this.lblBluetoothAddress.Name = "lblBluetoothAddress";
            this.lblBluetoothAddress.Size = new System.Drawing.Size(100, 20);
            this.lblBluetoothAddress.Text = "Bluetooth Address:";
            // 
            // lblBluetoothAddressValue
            // 
            this.lblBluetoothAddressValue.Location = new System.Drawing.Point(114, 227);
            this.lblBluetoothAddressValue.Name = "lblBluetoothAddressValue";
            this.lblBluetoothAddressValue.Size = new System.Drawing.Size(100, 20);
            this.lblBluetoothAddressValue.Text = "BD Address";
            // 
            // lblStatisticCounterOfDevice
            // 
            this.lblStatisticCounterOfDevice.Location = new System.Drawing.Point(8, 267);
            this.lblStatisticCounterOfDevice.Name = "lblStatisticCounterOfDevice";
            this.lblStatisticCounterOfDevice.Size = new System.Drawing.Size(100, 20);
            this.lblStatisticCounterOfDevice.Text = "Statistic Counter:";
            // 
            // lblScannerType
            // 
            this.lblScannerType.Location = new System.Drawing.Point(8, 247);
            this.lblScannerType.Name = "lblScannerType";
            this.lblScannerType.Size = new System.Drawing.Size(100, 20);
            this.lblScannerType.Text = "ScannerType:";
            // 
            // lblScannerTypeValue
            // 
            this.lblScannerTypeValue.Location = new System.Drawing.Point(114, 247);
            this.lblScannerTypeValue.Name = "lblScannerTypeValue";
            this.lblScannerTypeValue.Size = new System.Drawing.Size(100, 20);
            this.lblScannerTypeValue.Text = "ScannerType";
            // 
            // lblBatteryLevel
            // 
            this.lblBatteryLevel.Location = new System.Drawing.Point(8, 287);
            this.lblBatteryLevel.Name = "lblBatteryLevel";
            this.lblBatteryLevel.Size = new System.Drawing.Size(100, 20);
            this.lblBatteryLevel.Text = "battery Level:";
            // 
            // pbBattery
            // 
            this.pbBattery.Location = new System.Drawing.Point(94, 290);
            this.pbBattery.Name = "pbBattery";
            this.pbBattery.Size = new System.Drawing.Size(85, 10);
            // 
            // lblBatteryLevelValue
            // 
            this.lblBatteryLevelValue.Location = new System.Drawing.Point(179, 287);
            this.lblBatteryLevelValue.Name = "lblBatteryLevelValue";
            this.lblBatteryLevelValue.Size = new System.Drawing.Size(47, 20);
            this.lblBatteryLevelValue.Text = "Battey Level";
            // 
            // lblFirmwareVersion
            // 
            this.lblFirmwareVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblFirmwareVersion.Location = new System.Drawing.Point(8, 307);
            this.lblFirmwareVersion.Name = "lblFirmwareVersion";
            this.lblFirmwareVersion.Size = new System.Drawing.Size(100, 20);
            this.lblFirmwareVersion.Text = "Firmware Version:";
            // 
            // lblFirmwareVersionValue
            // 
            this.lblFirmwareVersionValue.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblFirmwareVersionValue.Location = new System.Drawing.Point(111, 309);
            this.lblFirmwareVersionValue.Name = "lblFirmwareVersionValue";
            this.lblFirmwareVersionValue.Size = new System.Drawing.Size(115, 20);
            this.lblFirmwareVersionValue.Text = "FirmwareVersionValue";
            // 
            // lblPowerState
            // 
            this.lblPowerState.Location = new System.Drawing.Point(8, 329);
            this.lblPowerState.Name = "lblPowerState";
            this.lblPowerState.Size = new System.Drawing.Size(100, 20);
            this.lblPowerState.Text = "Power State:";
            // 
            // lblPowerStateValue
            // 
            this.lblPowerStateValue.Location = new System.Drawing.Point(114, 329);
            this.lblPowerStateValue.Name = "lblPowerStateValue";
            this.lblPowerStateValue.Size = new System.Drawing.Size(100, 20);
            this.lblPowerStateValue.Text = "PowerState";
            // 
            // btnGetStatisticCounters
            // 
            this.btnGetStatisticCounters.Enabled = false;
            this.btnGetStatisticCounters.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.btnGetStatisticCounters.Location = new System.Drawing.Point(114, 264);
            this.btnGetStatisticCounters.Name = "btnGetStatisticCounters";
            this.btnGetStatisticCounters.Size = new System.Drawing.Size(95, 20);
            this.btnGetStatisticCounters.TabIndex = 18;
            this.btnGetStatisticCounters.Text = "Get Statistic Counters";
            this.btnGetStatisticCounters.Click += new System.EventHandler(this.btnGetStatisticCounters_Click);
            // 
            // ScannerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 450);
            this.Controls.Add(this.btnGetStatisticCounters);
            this.Controls.Add(this.lblPowerStateValue);
            this.Controls.Add(this.lblPowerState);
            this.Controls.Add(this.lblFirmwareVersionValue);
            this.Controls.Add(this.lblFirmwareVersion);
            this.Controls.Add(this.lblBatteryLevelValue);
            this.Controls.Add(this.pbBattery);
            this.Controls.Add(this.lblBatteryLevel);
            this.Controls.Add(this.lblScannerTypeValue);
            this.Controls.Add(this.lblScannerType);
            this.Controls.Add(this.lblStatisticCounterOfDevice);
            this.Controls.Add(this.lblBluetoothAddressValue);
            this.Controls.Add(this.lblBluetoothAddress);
            this.Controls.Add(this.lbScanners);
            this.Controls.Add(this.lblFriendlyNameValue);
            this.Controls.Add(this.lblFriendlyName);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.textScannedData);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "ScannerTest";
            this.Text = "Socket ScannerTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox textScannedData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Timer timerScanners;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem_Cancel;
        private System.Windows.Forms.MenuItem menuItem2_ReadProperties;
        private System.Windows.Forms.MenuItem menuItem_ConfigurationStates;
        private System.Windows.Forms.Label lblFriendlyName;
        private System.Windows.Forms.Label lblFriendlyNameValue;
        private System.Windows.Forms.ListBox lbScanners;
        private System.Windows.Forms.Label lblBluetoothAddress;
        private System.Windows.Forms.Label lblBluetoothAddressValue;
        private System.Windows.Forms.Label lblStatisticCounterOfDevice;
        private System.Windows.Forms.Label lblScannerType;
        private System.Windows.Forms.Label lblScannerTypeValue;
        private System.Windows.Forms.Label lblBatteryLevel;
        private System.Windows.Forms.ProgressBar pbBattery;
        private System.Windows.Forms.Label lblBatteryLevelValue;
        private System.Windows.Forms.Label lblFirmwareVersion;
        private System.Windows.Forms.Label lblFirmwareVersionValue;
        private System.Windows.Forms.Label lblPowerState;
        private System.Windows.Forms.Label lblPowerStateValue;
        private System.Windows.Forms.Button btnGetStatisticCounters;
    }
}

