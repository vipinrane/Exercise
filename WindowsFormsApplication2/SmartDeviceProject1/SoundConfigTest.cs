using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ScanAPI;

namespace SmartDeviceProject1
{
    public partial class SoundConfigTest : Form, ScanApiHelper.ScanApiHelper.ScanApiHelperNotification
    {
        private ScanApiHelper.ScanApiHelper _scanApiHelper;
        private ScanApiHelper.DeviceInfo connectedDevice;
        private bool _bInitialized;
        public System.Windows.Forms.Timer timerScanners = null;

        public SoundConfigTest()
        {
            InitializeComponent();
            InitializeScanner();
        }
        public void InitializeScanner()
        {
            _scanApiHelper = new ScanApiHelper.ScanApiHelper();
            _scanApiHelper.SetNotification(this);
            _bInitialized = false;// Start ScanAPI Helper
            SingleEntry_Load();
        }
        private void SingleEntry_Load()
        {
            // Start ScanAPI Helper
            _scanApiHelper.Open();
            timerScanners = new System.Windows.Forms.Timer();

            timerScanners.Interval = 100;
            timerScanners.Tick += new EventHandler(timerScanners_Tick);
            timerScanners.Enabled = true;
        }

        // if ScanAPI is fully initialized then we can receive ScanObject from ScanAPI.
        void timerScanners_Tick(object sender, EventArgs e)
        {
            if (_bInitialized == true)
                _scanApiHelper.DoScanAPIReceive();

        }

        private void btnGetSoundConfig_Click(object sender, EventArgs e)
        {
            //    public const int kSktScanSoundActionTypeBadScan = 2;
            //    public const int kSktScanSoundActionTypeBadScanLocal = 3;
            //    public const int kSktScanSoundActionTypeGoodScan = 0;
            //    public const int kSktScanSoundActionTypeGoodScanLocal = 1;
            Byte bAction = ISktScanProperty.values.soundActionType.kSktScanSoundActionTypeBadScanLocal;
            if (connectedDevice != null)
                _scanApiHelper.PostGetSoundConfig(connectedDevice, bAction, OnGetGetSoundConfig);
        }


        public void OnGetGetSoundConfig(long result, ISktScanObject scanObj)
        {
            string strMsg = "";
            if (SktScanErrors.SKTSUCCESS(result))
            {
                string strAction = "<unknown>";
                if (scanObj.Property.Type == ISktScanProperty.types.kSktScanPropTypeArray)
                {
                    if (scanObj.Property.Array.Size >= 2)
                    {
                        int Action = scanObj.Property.Array.Value[0] << 8;
                        Action += scanObj.Property.Array.Value[1];
                        strAction = Action.ToString();
                    }
                }
                strMsg = "Success: " + strAction;
            }
            else
            {
                strMsg = "Error = " + result;
            }
            MessageBox.Show("Result for OnGetSoundConfig - " + strMsg);
        }


        // ScanAPI Helper provides a series of Callbacks
        // indicating some asynchronous events has occured
        #region ScanApiHelperNotification Members

        // a scanner has connected to the host
        public void OnDeviceArrival(long result, ScanApiHelper.DeviceInfo newDevice)
        {
            if (SktScanErrors.SKTSUCCESS(result))
            {
                connectedDevice = newDevice;
                label1.Text = "ScanAPI Opened!";
            }
            else
            {
                label1.Text = "Error in ScanAPI Opening!";
            }
        }

        // a scanner has disconnected from the host
        public void OnDeviceRemoval(ScanApiHelper.DeviceInfo deviceRemoved)
        {
            connectedDevice = null;
            
        }

        // a ScanAPI error occurs.
        public void OnError(long result, string errMsg)
        {
           
        }

        // some decoded data have been received
        public void OnDecodedData(ScanApiHelper.DeviceInfo device, ISktScanDecodedData decodedData)
        {
            
        }

        // ScanAPI is now initialized and fully functionnal
        // (ScanAPI has some internal testing that might take few seconds to complete)
        public void OnScanApiInitializeComplete(long result)
        {
            if (SktScanErrors.SKTSUCCESS(result))
            {
                _bInitialized = true;
               
            }
            else
            {
                
            }
        }

        // ScanAPI has now terminate, it is safe to
        // close the application now
        public void OnScanApiTerminated()
        {
            //timerScanner.Stop();
            _bInitialized = false;
            //Close();// we can now close this form
        }

        // the ScanAPI Helper encounters an error during
        // the retrieval of a ScanObject
        public void OnErrorRetrievingScanObject(long result)
        {
            
        }

        #endregion

    }
}