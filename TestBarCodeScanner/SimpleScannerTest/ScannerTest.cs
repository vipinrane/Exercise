using System;
using System.Windows.Forms;

using ScanAPI;

namespace SimpleScannerTest
{
    public partial class ScannerTest :  Form, ScanApiHelper.ScanApiHelper.ScanApiHelperNotification
    {
        private const int SCANAPI_TIMER_PERIOD = 100;		// milliseconds

        private ScanApiHelper.ScanApiHelper _scanApiHelper;
        private ScanApiHelper.DeviceInfo connectedDevice;
        private bool _bInitialized;
        private ScannerTest.ScannerRecord Scanner { get; set; }
        private delegate void MethodInvoker();

        // for the Scan Test window to receive the decoded data
        public delegate void DecodedDataOutputDelegate(string strDecodedData);
        public delegate void StandardTextOutputDelegate(string strStatus);

        // for the ProgressBar window
        public delegate void UpdateProgressBarDelegate(bool bForceToClose);
        public event UpdateProgressBarDelegate UpdateProgressBarEvent;

        public ScannerRecord SelectedScanner { get; set; }

        public class ScannerRecord
        {
            private ScanApiHelper.DeviceInfo _deviceInfo;

            public ScanApiHelper.DeviceInfo DeviceInfo
            {
                get { return _deviceInfo; }
            }

            public ScannerProperties OriginalProperties { get; set; }
            public ScannerProperties ModifiedProperties { get; set; }

            public ScannerRecord(ScanApiHelper.DeviceInfo device)
            {
                _deviceInfo = device;
                OriginalProperties = new ScannerProperties();
                ModifiedProperties = new ScannerProperties();
            }


            public override string ToString()
            {
                return _deviceInfo.Name;
            }

        }

        public ScannerTest()
        {
            InitializeComponent();
            lblStatus.Text = "Initializing...";
            _scanApiHelper = new ScanApiHelper.ScanApiHelper();
            

            _scanApiHelper.SetNotification(this);
            _bInitialized = false;
            Load += new EventHandler(SingleEntry_Load);

        }

        //public ScannerTest(ScannerTest thisApp, SimpleScannerTest.ScannerTest.ScannerRecord scanner)
        //{
        //    _thisApp = thisApp;
        //    Scanner = scanner;
        //    InitializeComponent();
        //    _thisApp.DeviceRemovalNotification += new ScannerTest.DeviceRemovalNotificationDelegate(DeviceRemovalNotification);
        //}

        private void SingleEntry_Load(object sender, System.EventArgs e)
        {
            // Start ScanAPI Helper
            _scanApiHelper.Open();
            timerScanners.Interval = SCANAPI_TIMER_PERIOD;
            timerScanners.Enabled = true;
            //timerScanners.Start();
        }
        
        // ScanAPI Helper provides a series of Callbacks
        // indicating some asynchronous events has occured
        #region ScanApiHelperNotification Members

        // a scanner has connected to the host
        public void OnDeviceArrival(long result, ScanApiHelper.DeviceInfo newDevice)
        {
            if (SktScanErrors.SKTSUCCESS(result))
            {
                UpdateStatusText("New Scanner: " + newDevice.Name);
                connectedDevice = newDevice;
            }
            else
            {
                string strMsg = String.Format("Unable to open scanner, error = %d.", result);
                MessageBox.Show(strMsg, "SingleEntry", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
            }

            SetReadPropertyMenuItem();
        }

        // a scanner has disconnected from the host
        public void OnDeviceRemoval(ScanApiHelper.DeviceInfo deviceRemoved)
        {
            connectedDevice = null;
            UpdateStatusText("Scanner Removed: " + deviceRemoved.Name);
        }

        // a ScanAPI error occurs.
        public void OnError(long result, string errMsg)
        {
            MessageBox.Show("ScanAPI Error: " + Convert.ToString(result) + " [" + (errMsg != null ? errMsg : "") + "]",
                "Scanner Settings", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        // some decoded data have been received
        public void OnDecodedData(ScanApiHelper.DeviceInfo device, ISktScanDecodedData decodedData)
        {
            UpdateDecodedDataText(decodedData.DataToUTF8String);
        }

        // ScanAPI is now initialized and fully functionnal
        // (ScanAPI has some internal testing that might take
        // few seconds to complete)
        public void OnScanApiInitializeComplete(long result)
        {            
            if (SktScanErrors.SKTSUCCESS(result))
            {
                _bInitialized = true;
                UpdateStatusText("SktScanAPI opened!");
            }
            else
            {
                UpdateStatusText("SktScanOpen failed!");
            }
        }
        public void UpdateStatusText(string strStatus)
        {
            if (InvokeRequired)
                Invoke(new StandardTextOutputDelegate(UpdateStatusText), new object[] { strStatus });
            else
                lblStatus.Text = strStatus;
        }
        public void UpdateDecodedDataText(string strDecodedData)
        {
            if (InvokeRequired)
                Invoke(new DecodedDataOutputDelegate(UpdateDecodedDataText), new object[] { strDecodedData });
            else
                textScannedData.Text = strDecodedData;
        }
        // ScanAPI has now terminate, it is safe to
        // close the application now
        public void OnScanApiTerminated()
        {
            //timerScanner.Stop();
            _bInitialized = false;
            Close();// we can now close this form
        }

        // the ScanAPI Helper encounters an error during
        // the retrieval of a ScanObject
        public void OnErrorRetrievingScanObject(long result)
        {
            MessageBox.Show("Unable to retrieve a ScanAPI ScanObject: " + Convert.ToString(result),
                "Scanner Settings", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        #endregion
        private void btnPair_Click(object sender, EventArgs e)
        {
        }
        private void TriggerCompleteCallback(long result, ISktScanObject scanObj)
        {
            MessageBox.Show("In trigger complete callback");
        }

        // if ScanAPI is fully initialized then we can
        // receive ScanObject from ScanAPI.
        private void timerScanners_Tick(object sender, EventArgs e)
        {
            if (_bInitialized == true)
                _scanApiHelper.DoScanAPIReceive();
        }

        // called on the start scan button
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (connectedDevice != null)
                _scanApiHelper.PostStartDecode(connectedDevice, TriggerCompleteCallback);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void menuItem_GeneralControlStates_Click(object sender, EventArgs e)
        {
            ReadProperties();
            
        }

        private void menuItem_ConfigurationStates_Click(object sender, EventArgs e)
        {
            ReadGeneralPageControlStates();
        }

        // ReadGeneralPageControlStates
        //
        // populate the contols on the dialog.
        //
        private void ReadGeneralPageControlStates()
        {
            lblFirmwareVersionValue.Text = SelectedScanner.OriginalProperties.General.FirmwareVersion;
            lblScannerTypeValue.Text = SelectedScanner.OriginalProperties.General.ScannerType;
            lblFriendlyNameValue.Text = SelectedScanner.OriginalProperties.General.FriendlyName;
            
            pbBattery.Minimum = SktScan.helper.SKTBATTERY_GETMINLEVEL((long)SelectedScanner.OriginalProperties.General.BatteryLevel);
            pbBattery.Maximum = SktScan.helper.SKTBATTERY_GETMAXLEVEL((long)SelectedScanner.OriginalProperties.General.BatteryLevel);
            pbBattery.Value = SktScan.helper.SKTBATTERY_GETCURLEVEL((long)SelectedScanner.OriginalProperties.General.BatteryLevel);
            string strPercent = "0%";
            if (SelectedScanner.OriginalProperties.General.BatteryLevel > 0)
                strPercent = String.Format("{0}%", pbBattery.Value * 100 / (pbBattery.Maximum - pbBattery.Minimum));
            lblBatteryLevelValue.Text = strPercent;

            lblBluetoothAddressValue.Text = SelectedScanner.OriginalProperties.General.BluetoothAddress;
            lblPowerStateValue.Text = SelectedScanner.OriginalProperties.General.PowerState;
            lblChangeIdValue.Text = SelectedScanner.OriginalProperties.General.ChangeId.ToString();
              
//ChangeIdDevice
//ConnectReasonDevice - Not supported

//PowerStateDevice - Not supported

            return;
        }

        /// <summary>
        /// local method that sets the ReadProperty Menuitem with appropriate submenus[Like Name,BatteryLevel]
        /// </summary>
        private void SetReadPropertyMenuItem()
        {
            if (_scanApiHelper.IsDeviceConnected())
            {
                menuItem2_ReadProperties.Enabled = true;
                btnGetStatisticCounters.Enabled = true;
            }
            else
            {
                menuItem2_ReadProperties.Enabled = false;
                btnGetStatisticCounters.Enabled = false;
            }
        }

        private void ReadProperties()
        {
            //Check device object and its properties

            if (connectedDevice != null)
            {
                SelectedScanner = new ScannerRecord(connectedDevice);
                // Read all the properties we're interested in from the scanner.
                RequestAllSettings(SelectedScanner.DeviceInfo);
            }
        }

       
        // RequestAllSettings
        //
        // "All settings" refers to all of the properties in the scanner that this GUI program is interested in:
        //
        //		* Bluetooth friendly name
        //		* Scanner type
        //		* Firware revisioin
        //		* Battery level
        //		* Decode action (beep, flash, rumble mode)
        //		* State of all symbologies (enabled, disabled, or not supported by the scanner)
        //
        // A request is generated to get the Bluetooth Friendly name. If the request succeeds, a Scanner IO status
        // dialog is displayed. The property item is retrieved in a timer handler for the Scanner List dialog, and
        // the result is processed by a handler for the friendly name. That handler requests the next item of
        // interest, the Scanner Type. When that information arrives, the Scanner Type handler is called, and
        // it requests the next item of interest, and so on, until the last item we want has been retrieved.
        // At that point, the Scanner IO status dialog is removed.
        //
        // When DialogBoxParam() returns, the value will be IDOK if all items were retrieved successfully, or
        // IDCANCEL if an error occurred, or if the user clicked the Cancel button in the Status IO dialog.
        //
        // The return value of this function is TRUE if all items were retrieved, and FALSE if the initial call
        // to SktScanGet() fails, or IDCANCEL was returned from DialogBoxParam().
        private void RequestAllSettings(ScanApiHelper.DeviceInfo device)
        {
            int count = 0;

            _scanApiHelper.PostGetFriendlyName(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetBtAddress(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetDeviceType(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetFirmware(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetBattery(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetDecodeAction(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetPostambleDevice(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetChangeIdDevice(device, CommandContextCallback);
            count++;

            _scanApiHelper.PostGetCapabilitiesDevice(device, CommandContextCallback);
            count++;

            //int str = ScanAPI.SktScan.helper.SKTPOWER_GETSTATE(ISktScanProperty.propId.kSktScanPropIdNotificationsDevice);
            //count++;

            for (int id = 1; id < ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID; id++)
            {
                _scanApiHelper.PostGetSymbologyInfo(device, id, CommandContextCallback);
                count++;
            }

            ProgressBar progressBar = new ProgressBar(this, true, count);
            progressBar.ProgressBarCompleteEvent += new ProgressBar.ProgressBarCompleteDelegate(ProgressBarCompleteDisplayProperty);
            if (progressBar.ShowDialog() == DialogResult.Cancel)
                _scanApiHelper.RemoveCommands(connectedDevice);

        }


        private void SaveModifiedSettings()
        {
            int count = 0;
            // check if the friendly name has to be saved
            if (SelectedScanner.OriginalProperties.General.FriendlyName.CompareTo(
                SelectedScanner.ModifiedProperties.General.FriendlyName) != 0)
            {
                // first reflect the name modification in the list box
                SelectedScanner.DeviceInfo.Name =
                    SelectedScanner.ModifiedProperties.General.FriendlyName;
                lbScanners.Text = SelectedScanner.DeviceInfo.Name;

                // then post the ScanAPI command to change the friendly name
                _scanApiHelper.PostSetFriendlyName(SelectedScanner.DeviceInfo,
                    SelectedScanner.ModifiedProperties.General.FriendlyName, CommandContextCallback);
                count++;
            }

            if (SelectedScanner.OriginalProperties.General.ChangeId.CompareTo(
                SelectedScanner.ModifiedProperties.General.ChangeId) != 0)
            {
                SelectedScanner.OriginalProperties.General.ChangeId = SelectedScanner.ModifiedProperties.General.ChangeId;
            }


            // check if the Local Decode Action has to be modified
            if (SelectedScanner.OriginalProperties.Configuration.ScanConfirmation !=
                SelectedScanner.ModifiedProperties.Configuration.ScanConfirmation)
            {
                _scanApiHelper.PostSetDecodeAction(SelectedScanner.DeviceInfo,
                    SelectedScanner.ModifiedProperties.Configuration.ScanConfirmation, CommandContextCallback);
                count++;
            }

            // check if the Suffix has to be modified
            if (SelectedScanner.OriginalProperties.Configuration.Suffix !=
                SelectedScanner.ModifiedProperties.Configuration.Suffix)
            {
                _scanApiHelper.PostSetPostamble(SelectedScanner.DeviceInfo,
                    SelectedScanner.ModifiedProperties.Configuration.Suffix, CommandContextCallback);
                count++;
            }

            // check if the Symbology has to be modified
            SymbologyProperties originalSymbologies = SelectedScanner.OriginalProperties.Symbologies;
            SymbologyProperties modifiedSymbologies = SelectedScanner.ModifiedProperties.Symbologies;
            for (int i = 0; i < ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID; i++)
            {
                if (originalSymbologies.GetSymbology(i) != null)
                {
                    if (originalSymbologies.GetSymbology(i).Status !=
                        modifiedSymbologies.GetSymbology(i).Status)
                    {
                        _scanApiHelper.PostSetSymbologyInfo(SelectedScanner.DeviceInfo,
                            i, modifiedSymbologies.GetSymbology(i).Status ==
                            ISktScanSymbology.status.kSktScanSymbologyStatusEnable, CommandContextCallback);
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                ProgressBar progressBar = new ProgressBar(this, false, count);
                if (progressBar.ShowDialog() == DialogResult.Cancel)
                    _scanApiHelper.RemoveCommands(SelectedScanner.DeviceInfo);
            }
        }

        private void ProgressBarCompleteDisplayProperty()
        {
            //call 'SetModifiedProperties' copies original properties to modified properties due to which call 'SaveModifiedSettings' will do nothing.
            //This is the time being provision made to have minimal code change in order to Read and Write the properties.
            SetModifiedProperties();
            SaveModifiedSettings();
            ReadGeneralPageControlStates();
            int result = ScanAPI.SktScan.helper.SKTPOWER_GETSTATE(ScanAPI.ISktScanProperty.values.notifications.kSktScanNotificationsPowerState);////ScanAPI.ISktScanProperty.values.powerStates.

            //// start the ScannerProperty form asynchronously
            //BeginInvoke((MethodInvoker)delegate
            //{
            //    ScannerTest property = new ScannerTest();
            //    if (property.ShowDialog() == DialogResult.OK)
            //    {
            //        SaveModifiedSettings();
            //        ReadGeneralPageControlStates();
            //    }
            //}
            //);
        }

        /// <summary>
        /// This function copies the original properties and set to modified properties.
        /// </summary>
        private void SetModifiedProperties()
        {
            SelectedScanner.ModifiedProperties = SelectedScanner.OriginalProperties;
        }

        #region CommandContextCallback Members

        // CommandContextCallback from ScanAPI Helper
        //
        // This callback is specified each time a ScanAPIHelper.Post command is called.
        //
        // Each Command that are sent to the device for either doing a Get Property or a Set Property
        // will be eventually sent the corresponding Get Complete or Set Complete.
        //
        // This function examines the get or set complete event given in the scanObj arg, and handle it depending
        // of what it is
        public void CommandContextCallback(long result, ISktScanObject scanObj)
        {

            // update the progress bar each time
            // a Get or Set Complete is received from the device
            if (UpdateProgressBarEvent != null)
                UpdateProgressBarEvent(false);


            ScannerRecord selectedScanner = SelectedScanner;

            if (connectedDevice != null)
            {
                switch (scanObj.Property.ID)
                {
                    case ISktScanProperty.propId.kSktScanPropIdFriendlyNameDevice:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.General.FriendlyName = scanObj.Property.String.Value;
                            }
                            else
                            {
                                // unable to get the friendly name even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Friendly Name: " + result);
                            }
                        }
                        else
                        {
                            if (!SktScanErrors.SKTSUCCESS(result))
                            {
                                // unable to set the friendly name even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to modify the Friendly Name: " + result);
                            }
                            else
                            {
                                // this is now a good time to update the list box with the
                                // new friendly name
                                SelectedScanner.DeviceInfo.Name =
                                    SelectedScanner.ModifiedProperties.General.FriendlyName;
                                lbScanners.Items.Remove(SelectedScanner);
                                int index = lbScanners.Items.Add(SelectedScanner);
                                lbScanners.SelectedIndex = index;
                            }
                        }
                        break;

                    case ISktScanProperty.propId.kSktScanPropIdBluetoothAddressDevice:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.General.BluetoothAddress =
                                    String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
                                    scanObj.Property.Array.Value[0],
                                    scanObj.Property.Array.Value[1],
                                    scanObj.Property.Array.Value[2],
                                    scanObj.Property.Array.Value[3],
                                    scanObj.Property.Array.Value[4],
                                    scanObj.Property.Array.Value[5]);
                            }
                            else
                            {
                                // unable to get the Bluetooth address even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Bluetooth Address: " + result);
                            }
                        }
                        break;

                    case ISktScanProperty.propId.kSktScanPropIdDeviceType:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.General.ScannerType =
                                        SelectedScanner.DeviceInfo.Type;
                            }
                            else
                            {
                                // unable to get the Device Type even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Device Type: " + result);
                            }
                        }
                        break;

                    case ISktScanProperty.propId.kSktScanPropIdVersionDevice:
                        if (SktScanErrors.SKTSUCCESS(result))
                        {
                            selectedScanner.OriginalProperties.General.FirmwareVersion =
                                String.Format("{0}.{1}.{2} {3} {4}{5}{6}",
                                scanObj.Property.Version.wMajor,
                                scanObj.Property.Version.wMiddle,
                                scanObj.Property.Version.wMinor,
                                scanObj.Property.Version.dwBuild,
                                scanObj.Property.Version.wMonth,
                                scanObj.Property.Version.wDay,
                                scanObj.Property.Version.wYear);
                        }
                        else
                        {
                            // unable to get the Device Firmware Version even after multiple retries
                            // by ScanAPI Helper
                            StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Firmware Version: " + result);
                        }
                        break;
                    case ISktScanProperty.propId.kSktScanPropIdBatteryLevelDevice:
                        if (SktScanErrors.SKTSUCCESS(result))
                        {
                            selectedScanner.OriginalProperties.General.BatteryLevel =
                                    scanObj.Property.Ulong;
                        }
                        else
                        {
                            // unable to get the Battery Level even after multiple retries
                            // by ScanAPI Helper
                            StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Battery Level: " + result);
                        }
                        break;
                    case ISktScanProperty.propId.kSktScanPropIdLocalDecodeActionDevice:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.Configuration.ScanConfirmation =
                                        (int)scanObj.Property.Byte;
                            }
                            else
                            {
                                // unable to get the Scan Confirmation even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Scan confirmation: " + result);
                            }
                        }
                        else
                        {
                            if (!SktScanErrors.SKTSUCCESS(result))
                            {
                                // unable to change the Scan Confirmation even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to modify the Scan confirmation: " + result);
                            }
                        }
                        break;
                    case ISktScanProperty.propId.kSktScanPropIdCapabilitiesDevice:
                        if (SktScanErrors.SKTSUCCESS(result))
                        {
                            selectedScanner.OriginalProperties.Configuration.DoesRumble =
                                ((scanObj.Property.Ulong & ISktScanProperty.values.capabilityLocalFunctions.kSktScanCapabilityLocalFunctionRumble) ==
                                ISktScanProperty.values.capabilityLocalFunctions.kSktScanCapabilityLocalFunctionRumble);
                        }
                        else
                        {
                            // unable to get the Capabilities even after multiple retries
                            // by ScanAPI Helper
                            StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Suffix: " + result);
                        }
                        break;
                    case ISktScanProperty.propId.kSktScanPropIdPostambleDevice:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.Configuration.Suffix =
                                        scanObj.Property.String.Value;
                            }
                            else
                            {
                                // unable to get the Postamble even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the scanner suffix: " + result);
                            }
                        }
                        else
                        {
                            if (!SktScanErrors.SKTSUCCESS(result))
                            {
                                // unable to set the Postamble even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to modify the scanner suffix: " + result);
                            }
                        }
                        break;
                    case ISktScanProperty.propId.kSktScanPropIdSymbologyDevice:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.Symbologies.SetSymbology(scanObj.Property.Symbology);
                                selectedScanner.ModifiedProperties.Symbologies.SetSymbology(scanObj.Property.Symbology);
                            }
                            else
                            {
                                // unable to get the Symbology even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Symbology " + scanObj.Property.Symbology.ID + " : " + result);
                            }
                        }
                        else
                        {
                            if (!SktScanErrors.SKTSUCCESS(result))
                            {
                                // unable to set the Symbology even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to modify the Symbology " + scanObj.Property.Symbology.ID + " : " + result);
                            }
                        }
                        break;

                    case ISktScanProperty.values.notifications.kSktScanNotificationsPowerState:
                    case ISktScanProperty.propId.kSktScanPropIdPowerStateDevice:
                        if (SktScanErrors.SKTSUCCESS(result))
                        {
                            int powerState=ScanAPI.SktScan.helper.SKTPOWER_GETSTATE(ISktScanProperty.propId.kSktScanPropIdNotificationsDevice);
                            //switch (powerState)
                            //{
                            //    case ScanAPI.ISktScanProperty.values.powerStates
                            //}
                            //ScanAPI.SktScan.helper.SKTBATTERY_GETCURLEVEL
                            selectedScanner.OriginalProperties.General.PowerState = powerState.ToString();
                        }
                        else
                        {
                            // unable to get the Power State Level even after multiple retries
                            // by ScanAPI Helper
                            StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Power State Level: " + result);
                        }
                        break;
                    case ISktScanProperty.propId.kSktScanPropIdChangeIdDevice:
                        if (scanObj.Msg.ID == ISktScanMsg.kSktScanMsgGetComplete)
                        {
                            if (SktScanErrors.SKTSUCCESS(result))
                            {
                                selectedScanner.OriginalProperties.General.ChangeId = scanObj.Property.Ulong;
                            }
                            else
                            {
                                // unable to get the Bluetooth address even after multiple retries
                                // by ScanAPI Helper
                                StopRetrievingPropertiesAndDisplayError("Failed to retrieve the Change ID: " + result);
                            }
                        }
                        break;
                }
            }
        }

        // stop the progress bar and remove the command waiting to be sent
        // to the device and display an error
        private void StopRetrievingPropertiesAndDisplayError(string errorText)
        {
            if (UpdateProgressBarEvent != null)
                UpdateProgressBarEvent(true);

            // remove all the pending commands for this scanner
            _scanApiHelper.RemoveCommands(connectedDevice);

            MessageBox.Show(errorText,
                "Error Retrieving Scanner properties",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

        }

        #endregion

        private void btnGetStatisticCounters_Click(object sender, EventArgs e)
        {
            _scanApiHelper.PostGetStatistics(connectedDevice, OnGetStatistics);
        }

        private void OnGetStatistics(long result, ISktScanObject scanObj)
        {
            if (scanObj.Property.Type == ISktScanProperty.types.kSktScanPropTypeArray)
            {
                int nArraySize = scanObj.Property.Array.Size;
                // Should have a counter to string table, see ScanApi.PDF section 16.25
                // Statitics table is a UInt16 count, followed by UInt32 pairs, Counter ID and Value
                byte[] bArray = scanObj.Property.Array.Value;
                int nValues = (bArray[0] << 8) + bArray[1];
                string strStatValues = String.Format("Stat Values {0}(Counter:value):\n", nValues);
                for (var i = 2; i < nArraySize - 8; i += 4)
                {
                    UInt32 Counter = (UInt32)(bArray[i] << 24) + (UInt32)(bArray[i + 1] << 16) + (UInt32)(bArray[i + 2] << 8) + (UInt32)bArray[i + 3];
                    i += 4;
                    UInt32 Value = (UInt32)(bArray[i] << 24) + (UInt32)(bArray[i + 1] << 16) + (UInt32)(bArray[i + 2] << 8) + (UInt32)bArray[i + 3];
                    strStatValues += String.Format("{0}: {1}\n ", getStatisticCountID()[(int)Counter], Value);
                }
                MessageBox.Show(strStatValues,"Statistic Counters");
            }
        }

        private string[] getStatisticCountID()
        {
            return new string[]
              {
				    "Unknown",
				    "Connect",
				    "Disconnect",
				    "Unbond",
				    "FactoryReset",
				    "Scans",
				    "ScanButtonUp",
				    "ScanButtonDown",
				    "PowerButtonUp",
				    "PowerButtonDown",
				    "PowerOnACTimeInMinutes",
				    "PowerOnBatTimeInMinutes",
				    "RfcommSend",
				    "RfcommReceive",
				    "RfcommReceiveDiscarded",
				    "UartSend",
				    "UartReceive",
				    "UartReceiveDiscarded",
	                "ButtonLeftPress",
	                "ButtonLeftRelease",
	                "ButtonRightPress",
	                "ButtonRightRelease",
	                "RingUnitDetachEvents",
	                "RingUnitAttachEvents",
                    "DecodedBytes",
	                "AbnormalShutdowns",
	                "BatteryChargeCycles",
	                "BatteryChangeCount",
                    "PowerOnCount",
                    "PowerOffCount",
                    "StandModeChanges"
              };
        }



        #region Scanner Property
        //// close this dialog if the scanner is removed
        //private void DeviceRemovalNotification(ScanApiHelper.DeviceInfo removedDevice)
        //{
        //    if (Scanner.DeviceInfo == removedDevice)
        //    {
        //        if (InvokeRequired == true)
        //            Invoke((MethodInvoker)delegate { Close(); });
        //        else
        //            Close();
        //    }
        //}
      
        #endregion

    }

    public class ScannerProperties
    {
        public GeneralProperties General { get; set; }
        public ConfigurationProperties Configuration { get; set; }
        public SymbologyProperties Symbologies { get; set; }
        public ScannerProperties()
        {
            General = new GeneralProperties();
            Configuration = new ConfigurationProperties();
            Symbologies = new SymbologyProperties();
        }
    }

    public class GeneralProperties
    {
        public String FriendlyName { get; set; }
        public String BluetoothAddress { get; set; }
        public String ScannerType { get; set; }
        public String FirmwareVersion { get; set; }
        public int BatteryLevel { get; set; }
        public String PowerState { get; set; }
        public int ChangeId { get; set; }
    }

    public class ConfigurationProperties
    {
        public int ScanConfirmation { get; set; }
        public String Suffix { get; set; }
        public bool DoesRumble { get; set; }
    }

    public class SymbologyProperties
    {
        private ScanApiHelper.SymbologyInfo[] _symbologies;
        public SymbologyProperties()
        {
            _symbologies = new ScanApiHelper.SymbologyInfo[ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID];
            for (int i = 1; i < ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID; i++)
            {
                _symbologies[i] = new ScanApiHelper.SymbologyInfo();
                _symbologies[i].ID = i;
            }
        }

        public void SetSymbology(ISktScanSymbology symbology)
        {
            int index = symbology.ID;
            if (index < ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID)
            {
                _symbologies[index].ID = index;
                _symbologies[index].Name = symbology.Name;
                _symbologies[index].Status = symbology.Status;
            }
        }

        public ScanApiHelper.SymbologyInfo GetSymbology(int index)
        {
            return _symbologies[index];
        }
    }
}
