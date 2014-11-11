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
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFriendlyName = new System.Windows.Forms.Label();
            this.lblFriendlyNameValue = new System.Windows.Forms.Label();
            this.menuItem_Cancel = new System.Windows.Forms.MenuItem();
            this.menuItem_ConfigurationStates = new System.Windows.Forms.MenuItem();
            this.menuItem2_ReadProperties = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lbScanners = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerScanners = new System.Windows.Forms.Timer();
            this.label2 = new System.Windows.Forms.Label();
            this.textScannedData = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
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
            this.lblChangeId = new System.Windows.Forms.Label();
            this.lblChangeIdValue = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnReadAllProperties = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtDataConfirmationMode = new System.Windows.Forms.TextBox();
            this.lblDataConfirmationMode = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnReadConfigProperties = new System.Windows.Forms.Button();
            menuItem_GeneralControlStates = new System.Windows.Forms.MenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuItem_GeneralControlStates
            // 
            menuItem_GeneralControlStates.Text = "General Control States";
            menuItem_GeneralControlStates.Click += new System.EventHandler(this.menuItem_GeneralControlStates_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(3, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(229, 20);
            this.lblStatus.Text = "label3";
            // 
            // lblFriendlyName
            // 
            this.lblFriendlyName.Location = new System.Drawing.Point(3, 201);
            this.lblFriendlyName.Name = "lblFriendlyName";
            this.lblFriendlyName.Size = new System.Drawing.Size(100, 20);
            this.lblFriendlyName.Text = "Friendly Name:";
            // 
            // lblFriendlyNameValue
            // 
            this.lblFriendlyNameValue.Location = new System.Drawing.Point(109, 201);
            this.lblFriendlyNameValue.Name = "lblFriendlyNameValue";
            this.lblFriendlyNameValue.Size = new System.Drawing.Size(136, 20);
            this.lblFriendlyNameValue.Text = "FNameValue";
            // 
            // menuItem_Cancel
            // 
            this.menuItem_Cancel.Text = "Cancel";
            this.menuItem_Cancel.Click += new System.EventHandler(this.menuItem_Cancel_Click);
            // 
            // menuItem_ConfigurationStates
            // 
            this.menuItem_ConfigurationStates.Text = "Configuration States";
            this.menuItem_ConfigurationStates.Click += new System.EventHandler(this.menuItem_ConfigurationStates_Click);
            // 
            // menuItem2_ReadProperties
            // 
            this.menuItem2_ReadProperties.Enabled = false;
            this.menuItem2_ReadProperties.MenuItems.Add(menuItem_GeneralControlStates);
            this.menuItem2_ReadProperties.MenuItems.Add(this.menuItem_ConfigurationStates);
            this.menuItem2_ReadProperties.Text = "Read Properties";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem_Cancel);
            this.mainMenu1.MenuItems.Add(this.menuItem2_ReadProperties);
            // 
            // lbScanners
            // 
            this.lbScanners.Location = new System.Drawing.Point(3, 47);
            this.lbScanners.Name = "lbScanners";
            this.lbScanners.Size = new System.Drawing.Size(95, 44);
            this.lbScanners.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Scanned Data";
            // 
            // timerScanners
            // 
            this.timerScanners.Tick += new System.EventHandler(this.timerScanners_Tick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Status";
            // 
            // textScannedData
            // 
            this.textScannedData.Location = new System.Drawing.Point(3, 140);
            this.textScannedData.Name = "textScannedData";
            this.textScannedData.Size = new System.Drawing.Size(100, 21);
            this.textScannedData.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(132, 167);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 20);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(3, 167);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(72, 20);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "&Scan";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblBluetoothAddress
            // 
            this.lblBluetoothAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblBluetoothAddress.Location = new System.Drawing.Point(3, 221);
            this.lblBluetoothAddress.Name = "lblBluetoothAddress";
            this.lblBluetoothAddress.Size = new System.Drawing.Size(100, 20);
            this.lblBluetoothAddress.Text = "Bluetooth Address:";
            // 
            // lblBluetoothAddressValue
            // 
            this.lblBluetoothAddressValue.Location = new System.Drawing.Point(109, 221);
            this.lblBluetoothAddressValue.Name = "lblBluetoothAddressValue";
            this.lblBluetoothAddressValue.Size = new System.Drawing.Size(100, 20);
            this.lblBluetoothAddressValue.Text = "BD Address";
            // 
            // lblStatisticCounterOfDevice
            // 
            this.lblStatisticCounterOfDevice.Location = new System.Drawing.Point(3, 261);
            this.lblStatisticCounterOfDevice.Name = "lblStatisticCounterOfDevice";
            this.lblStatisticCounterOfDevice.Size = new System.Drawing.Size(100, 20);
            this.lblStatisticCounterOfDevice.Text = "Statistic Counter:";
            // 
            // lblScannerType
            // 
            this.lblScannerType.Location = new System.Drawing.Point(3, 241);
            this.lblScannerType.Name = "lblScannerType";
            this.lblScannerType.Size = new System.Drawing.Size(100, 20);
            this.lblScannerType.Text = "ScannerType:";
            // 
            // lblScannerTypeValue
            // 
            this.lblScannerTypeValue.Location = new System.Drawing.Point(109, 241);
            this.lblScannerTypeValue.Name = "lblScannerTypeValue";
            this.lblScannerTypeValue.Size = new System.Drawing.Size(100, 20);
            this.lblScannerTypeValue.Text = "ScannerType";
            // 
            // lblBatteryLevel
            // 
            this.lblBatteryLevel.Location = new System.Drawing.Point(3, 281);
            this.lblBatteryLevel.Name = "lblBatteryLevel";
            this.lblBatteryLevel.Size = new System.Drawing.Size(100, 20);
            this.lblBatteryLevel.Text = "battery Level:";
            // 
            // pbBattery
            // 
            this.pbBattery.Location = new System.Drawing.Point(89, 284);
            this.pbBattery.Name = "pbBattery";
            this.pbBattery.Size = new System.Drawing.Size(85, 10);
            // 
            // lblBatteryLevelValue
            // 
            this.lblBatteryLevelValue.Location = new System.Drawing.Point(174, 281);
            this.lblBatteryLevelValue.Name = "lblBatteryLevelValue";
            this.lblBatteryLevelValue.Size = new System.Drawing.Size(47, 20);
            this.lblBatteryLevelValue.Text = "Battey Level";
            // 
            // lblFirmwareVersion
            // 
            this.lblFirmwareVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblFirmwareVersion.Location = new System.Drawing.Point(3, 301);
            this.lblFirmwareVersion.Name = "lblFirmwareVersion";
            this.lblFirmwareVersion.Size = new System.Drawing.Size(100, 20);
            this.lblFirmwareVersion.Text = "Firmware Version:";
            // 
            // lblFirmwareVersionValue
            // 
            this.lblFirmwareVersionValue.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblFirmwareVersionValue.Location = new System.Drawing.Point(106, 303);
            this.lblFirmwareVersionValue.Name = "lblFirmwareVersionValue";
            this.lblFirmwareVersionValue.Size = new System.Drawing.Size(115, 20);
            this.lblFirmwareVersionValue.Text = "FirmwareVersionValue";
            // 
            // lblPowerState
            // 
            this.lblPowerState.Location = new System.Drawing.Point(3, 323);
            this.lblPowerState.Name = "lblPowerState";
            this.lblPowerState.Size = new System.Drawing.Size(100, 20);
            this.lblPowerState.Text = "Power State:";
            // 
            // lblPowerStateValue
            // 
            this.lblPowerStateValue.Location = new System.Drawing.Point(109, 323);
            this.lblPowerStateValue.Name = "lblPowerStateValue";
            this.lblPowerStateValue.Size = new System.Drawing.Size(100, 20);
            this.lblPowerStateValue.Text = "PowerState";
            // 
            // btnGetStatisticCounters
            // 
            this.btnGetStatisticCounters.Enabled = false;
            this.btnGetStatisticCounters.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.btnGetStatisticCounters.Location = new System.Drawing.Point(109, 258);
            this.btnGetStatisticCounters.Name = "btnGetStatisticCounters";
            this.btnGetStatisticCounters.Size = new System.Drawing.Size(95, 20);
            this.btnGetStatisticCounters.TabIndex = 18;
            this.btnGetStatisticCounters.Text = "Get Statistic Counters";
            this.btnGetStatisticCounters.Click += new System.EventHandler(this.btnGetStatisticCounters_Click);
            // 
            // lblChangeId
            // 
            this.lblChangeId.Location = new System.Drawing.Point(3, 343);
            this.lblChangeId.Name = "lblChangeId";
            this.lblChangeId.Size = new System.Drawing.Size(100, 20);
            this.lblChangeId.Text = "ChangeId:";
            // 
            // lblChangeIdValue
            // 
            this.lblChangeIdValue.Location = new System.Drawing.Point(109, 343);
            this.lblChangeIdValue.Name = "lblChangeIdValue";
            this.lblChangeIdValue.Size = new System.Drawing.Size(100, 20);
            this.lblChangeIdValue.Text = "ChangeIdValue";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(227, 400);
            this.tabControl1.TabIndex = 95;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnReadAllProperties);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Controls.Add(this.lblChangeIdValue);
            this.tabPage1.Controls.Add(this.textScannedData);
            this.tabPage1.Controls.Add(this.lblChangeId);
            this.tabPage1.Controls.Add(this.btnExit);
            this.tabPage1.Controls.Add(this.btnGetStatisticCounters);
            this.tabPage1.Controls.Add(this.btnScan);
            this.tabPage1.Controls.Add(this.lblPowerStateValue);
            this.tabPage1.Controls.Add(this.lblFriendlyName);
            this.tabPage1.Controls.Add(this.lblPowerState);
            this.tabPage1.Controls.Add(this.lblFriendlyNameValue);
            this.tabPage1.Controls.Add(this.lblFirmwareVersionValue);
            this.tabPage1.Controls.Add(this.lbScanners);
            this.tabPage1.Controls.Add(this.lblFirmwareVersion);
            this.tabPage1.Controls.Add(this.lblBluetoothAddress);
            this.tabPage1.Controls.Add(this.lblBatteryLevelValue);
            this.tabPage1.Controls.Add(this.lblBluetoothAddressValue);
            this.tabPage1.Controls.Add(this.pbBattery);
            this.tabPage1.Controls.Add(this.lblStatisticCounterOfDevice);
            this.tabPage1.Controls.Add(this.lblBatteryLevel);
            this.tabPage1.Controls.Add(this.lblScannerType);
            this.tabPage1.Controls.Add(this.lblScannerTypeValue);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(227, 377);
            this.tabPage1.Text = "tabPage1";
            // 
            // btnReadAllProperties
            // 
            this.btnReadAllProperties.Enabled = false;
            this.btnReadAllProperties.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.btnReadAllProperties.Location = new System.Drawing.Point(104, 47);
            this.btnReadAllProperties.Name = "btnReadAllProperties";
            this.btnReadAllProperties.Size = new System.Drawing.Size(100, 21);
            this.btnReadAllProperties.TabIndex = 33;
            this.btnReadAllProperties.Text = "ReadAllProperties";
            this.btnReadAllProperties.Click += new System.EventHandler(this.btnReadAllProperties_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtDataConfirmationMode);
            this.tabPage2.Controls.Add(this.lblDataConfirmationMode);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.btnReadConfigProperties);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(219, 374);
            this.tabPage2.Text = "tabPage2";
            // 
            // txtDataConfirmationMode
            // 
            this.txtDataConfirmationMode.Location = new System.Drawing.Point(115, 104);
            this.txtDataConfirmationMode.Name = "txtDataConfirmationMode";
            this.txtDataConfirmationMode.Size = new System.Drawing.Size(100, 21);
            this.txtDataConfirmationMode.TabIndex = 81;
            // 
            // lblDataConfirmationMode
            // 
            this.lblDataConfirmationMode.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblDataConfirmationMode.Location = new System.Drawing.Point(7, 105);
            this.lblDataConfirmationMode.Name = "lblDataConfirmationMode";
            this.lblDataConfirmationMode.Size = new System.Drawing.Size(102, 20);
            this.lblDataConfirmationMode.Text = "DataConfirmationMode";
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(42, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(110, 58);
            this.listBox1.TabIndex = 80;
            // 
            // btnReadConfigProperties
            // 
            this.btnReadConfigProperties.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.btnReadConfigProperties.Location = new System.Drawing.Point(42, 7);
            this.btnReadConfigProperties.Name = "btnReadConfigProperties";
            this.btnReadConfigProperties.Size = new System.Drawing.Size(114, 20);
            this.btnReadConfigProperties.TabIndex = 79;
            this.btnReadConfigProperties.Text = "Read Config Properties";
            this.btnReadConfigProperties.Click += new System.EventHandler(this.btnReadConfigProperties_Click);
            // 
            // ScannerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.Name = "ScannerTest";
            this.Text = "Socket ScannerTest";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFriendlyName;
        private System.Windows.Forms.Label lblFriendlyNameValue;
        private System.Windows.Forms.MenuItem menuItem_Cancel;
        private System.Windows.Forms.MenuItem menuItem_ConfigurationStates;
        private System.Windows.Forms.MenuItem menuItem2_ReadProperties;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.ListBox lbScanners;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerScanners;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textScannedData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnScan;
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
        private System.Windows.Forms.Label lblChangeId;
        private System.Windows.Forms.Label lblChangeIdValue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnReadConfigProperties;
        private System.Windows.Forms.Label lblDataConfirmationMode;
        private System.Windows.Forms.Button btnReadAllProperties;
        private System.Windows.Forms.TextBox txtDataConfirmationMode;


    }
}

