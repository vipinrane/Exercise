/*
 * Title:
 * ScanAPIHelper.cs
 * 
 * Copyright (c) 2011 Socket Mobile Inc.
 * All rights reserved.
 *
 * Description:
 * this class provides a set of common functions to retrieve
 * or configure a scanner or ScanAPI and to receive decoded
 * data from a scanner.<p>
 * This helper manages a commands list so the application
 * can send multiple command in a row, the helper will send
 * them one at a time. Each command has an optional callback 
 * function that will be called each time a command complete.
 * By example, to get a device friendly name, use the 
 * PostGetFriendlyName method and pass a callback function in 
 * which you can update the UI with the newly fetched friendly 
 * name. This operation will be completely asynchronous.<p>
 * ScanAPI Helper manages a list of device information. Most of 
 * the time only one device is connected to the host. This list
 * could be configured to have always one item, that will be a 
 * "No device connected" item in the case where there is no device
 * connected, or simply a device name when there is one device
 * connected. Use isDeviceConnected method to know if there is at
 * least one device connected to the host.<br> 
 * Common usage scenario of ScanAPIHelper:<br>
 * <li> create an instance of ScanApiHelper: _scanApi=new ScanApiHelper();
 * <li> [optional] if a UI device list is used a no device connected 
 * string can be specified:_scanApi.setNoDeviceText(getString(R.string.no_device_connected));
 * <li> register for notification: _scanApi.setNotification(_scanApiNotification);
 * <li> derive from ScanApiHelperNotification to handle the notifications coming
 * from ScanAPI including "Device Arrival", "Device Removal", "Decoded Data" etc...
 * <li> open ScanAPI to start using it:_scanApi.open();
 * <li> check the ScanAPI initialization result in the notifications: 
 * _scanApiNotification.onScanApiInitializeComplete(long result){}
 * <li> monitor a scanner connection by using the notifications:
 * _scanApiNotification.onDeviceArrival(long result,DeviceInfo newDevice){}
 * _scanApiNotification.onDeviceRemoval(DeviceInfo deviceRemoved){}
 * <li> retrieve the decoded data from a scanner
 * _scanApiNotification.onDecodedData(DeviceInfo device,ISktScanDecodedData decodedData){}
 * <li> once the application is done using ScanAPI, close it using:
 * _scanApi.close();
 *
 * Revision 	Who 		History
 * 04/19/11		EricG		First release
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScanAPI;
using System.Threading;
//using System.Windows.Forms;

namespace ScanApiHelper
{
    class Debug
    {
        public const int kLevelTrace = 1;
        public const int kLevelWarning = 2;
        public const int kLevelError = 3;
        public static void MSG(int level, String traces)
        {
            if (level == kLevelTrace)
                System.Diagnostics.Debug.WriteLine(traces, "INFO");
            else if (level == kLevelWarning)
                System.Diagnostics.Debug.WriteLine(traces, "WARNING");
            else if (level == kLevelError)
                System.Diagnostics.Debug.WriteLine(traces, "ERROR");
            else
                System.Diagnostics.Debug.WriteLine(traces, "VERBOSE");
        }
    }

    public delegate void ICommandContextCallback(long result, ISktScanObject scanObj);

    class CommandContext
    {
        public const int statusReady = 1;
        public const int statusNotCompleted = 2;
        public const int statusCompleted = 3;

        private ICommandContextCallback _callback = null;
        private bool _getOperation = false;
        private ISktScanObject _scanObj;
        private int _status;
        private ISktScanDevice _scanDevice;
        private int _retries;
        private DeviceInfo _deviceInfo;
        private int _symbologyId;

        public CommandContext(bool getOperation, ISktScanObject scanObj, ISktScanDevice scanDevice, DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            this._getOperation = getOperation;
            scanObj.Property.Context = this;
            this._scanObj = scanObj;
            this._callback = callback;
            this._status = statusReady;
            this._scanDevice = scanDevice;
            this._retries = 0;
            this._deviceInfo = deviceInfo;
            this._symbologyId = 0;
        }

        public bool Operation
        {
            get { return _getOperation; }
        }

        public ISktScanObject ScanObject
        {
            get { return _scanObj; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int Retries
        {
            get { return _retries; }
        }

        public ISktScanDevice ScanDevice
        {
            get { return _scanDevice; }
        }

        public DeviceInfo DeviceInfo
        {
            get { return _deviceInfo; }
        }

        public void DoCallback(long result, ISktScanObject scanObj)
        {
            _status = statusCompleted;
            if (_callback != null)
                _callback(result, scanObj);
        }

        public int SymbologyId
        {
            get { return _symbologyId; }
            set { _symbologyId = value; }
        }


        public long DoGetOrSetProperty()
        {
            long result = SktScanErrors.ESKT_NOERROR;
            if (ScanDevice == null)
                result = SktScanErrors.ESKT_INVALIDPARAMETER;

            if (SktScanErrors.SKTSUCCESS(result))
            {
                if (Operation == true)
                {
                    Debug.MSG(Debug.kLevelTrace, "About to do a get for ID:0x" + Convert.ToString(ScanObject.Property.ID, 16));
                    result = ScanDevice.GetProperty(ScanObject);
                }
                else
                {
                    Debug.MSG(Debug.kLevelTrace, "About to do a set for ID:0x" + Convert.ToString(ScanObject.Property.ID, 16));
                    result = ScanDevice.SetProperty(ScanObject);
                }
            }
            _retries++;
            if (SktScanErrors.SKTSUCCESS(result))
            {
                _status = statusNotCompleted;
            }
            return result;
        }

    }

    public class SymbologyInfo
    {
        public String Name { get; set; }
        public int Status { get; set; }
        public int ID { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class DeviceInfo
    {
        public interface Notification
        {
            void OnNotify(DeviceInfo device);
        }

        private ISktScanDevice _device;
        private String _name;
        private String _bdAddress;
        private long _type;
        private String _version;
        private String _batteryLevel;
        private int _decodeValue;
        private bool _rumble;
        private String _suffix;
        private SymbologyInfo[] _symbologyInfo;
        private int _symbologyIndex;
        private Notification _notification;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String BtAddress
        {
            get { return _bdAddress; }
            set { _bdAddress = value; Notify(); }
        }

        public ISktScanDevice SktScanDevice
        {
            get { return _device; }
        }

        public String Type
        {
            get
            {
                String type;
                if (_type == SktScanDeviceType.kSktScanDeviceTypeScanner7)
                    type = "CHS Scanner";
                else if (_type == SktScanDeviceType.kSktScanDeviceTypeScanner7x)
                    type = "CHS 7x Scanner";
                else if (_type == SktScanDeviceType.kSktScanDeviceTypeScanner7xi)
                    type = "CHS 7xi Scanner";
                else if (_type == SktScanDeviceType.kSktScanDeviceTypeScanner9)
                    type = "CRS Scanner";
                //else if (_type == SktScanDeviceType.kSktScanDeviceTypeScanner8ci)
                //    type = "CHS 8ci Scanner";
                else
                    type = "Unknown scanner type!";
                return type;
            }
        }

        public String Version
        {
            get { return _version; }
            set { _version = value; Notify(); }
        }

        public String BatteryLevel
        {
            get { return _batteryLevel; }
            set { _batteryLevel = value; Notify(); }
        }

        public int DecodeValue
        {
            get { return _decodeValue; }
        }

        public bool Rumble
        {
            get { return _rumble; }
        }

        public String Suffix
        {
            get { return _suffix; }
            set { _suffix = value; Notify(); }
        }

        public int SymbologyIndex
        {
            get { return _symbologyIndex; }
            set { _symbologyIndex = value; }
        }

        public int GetSymbologyStatus(int symbologyId)
        {
            return _symbologyInfo[symbologyId].Status;
        }

        public void SetSymbologyStatus(int symbologyId, int status)
        {
            _symbologyInfo[symbologyId].Status = status;
        }

        public String GetSymbologyName(int symbologyId)
        {
            return _symbologyInfo[symbologyId].Name;
        }

        public void SetSymbologyName(int symbologyId, String name)
        {
            _symbologyInfo[symbologyId].Name = name;
            Notify();
        }

        public DeviceInfo(String name, ISktScanDevice device, long type)
        {
            _device = device;
            _name = name;
            _bdAddress = "Not available";
            _type = type;
            _version = "Unknown";
            _batteryLevel = "Unknown";
            _decodeValue = 0;
            _rumble = true;
            _suffix = "\n";
            _notification = null;
            _symbologyInfo = new SymbologyInfo[ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID];
            for (int i = 0; i < _symbologyInfo.Length; i++)
            {
                _symbologyInfo[i] = new SymbologyInfo();
            }
        }

        public void SetNotification(Notification notification)
        {
            _notification = notification;
        }

        private void Notify()
        {
            if (_notification != null)
                _notification.OnNotify(this);
        }

        public override string ToString()
        {
            return _name;
        }
    }

    /**
     * this class provides a set of common functions to retrieve
     * or configure a scanner or ScanAPI and to receive decoded
     * data from a scanner.<p>
     * This helper manages a commands list so the application
     * can send multiple command in a row, the helper will send
     * them one at a time. Each command has an optional callback 
     * function that will be called each time a command complete.
     * By example, to get a device friendly name, use the 
     * PostGetFriendlyName method and pass a callback function in 
     * which you can update the UI with the newly fetched friendly 
     * name. This operation will be completely asynchronous.<p>
     * ScanAPI Helper manages a list of device information. Most of 
     * the time only one device is connected to the host. This list
     * could be configured to have always one item, that will be a 
     * "No device connected" item in the case where there is no device
     * connected, or simply a device name when there is one device
     * connected. Use isDeviceConnected method to know if there is at
     * least one device connected to the host.<br> 
     * Common usage scenario of ScanAPIHelper:<br>
     * <li> create an instance of ScanApiHelper: _scanApi=new ScanApiHelper();
     * <li> [optional] if a UI device list is used a no device connected 
     * string can be specified:_scanApi.setNoDeviceText(getString(R.string.no_device_connected));
     * <li> register for notification: _scanApi.setNotification(_scanApiNotification);
     * <li> derive from ScanApiHelperNotification to handle the notifications coming
     * from ScanAPI including "Device Arrival", "Device Removal", "Decoded Data" etc...
     * <li> open ScanAPI to start using it:_scanApi.open();
     * <li> check the ScanAPI initialization result in the notifications: 
     * _scanApiNotification.onScanApiInitializeComplete(long result){}
     * <li> monitor a scanner connection by using the notifications:
     * _scanApiNotification.onDeviceArrival(long result,DeviceInfo newDevice){}
     * _scanApiNotification.onDeviceRemoval(DeviceInfo deviceRemoved){}
     * <li> retrieve the decoded data from a scanner
     * _scanApiNotification.onDecodedData(DeviceInfo device,ISktScanDecodedData decodedData){}
     * <li> once the application is done using ScanAPI, close it using:
     * _scanApi.close();
     * @author ericg
     *
     */
    public class ScanApiHelper
    {
        /**
         * notification coming from ScanApiHelper the application
         * can override for its own purpose
         * @author ericg
         *
         */
        public interface ScanApiHelperNotification
        {
            /**
             * called each time a device connects to the host
             * @param result contains the result of the connection
             * @param newDevice contains the device information
             */
            void OnDeviceArrival(long result, DeviceInfo newDevice);

            /**
             * called each time a device disconnect from the host
             * @param deviceRemoved contains the device information
             */
            void OnDeviceRemoval(DeviceInfo deviceRemoved);

            /**
             * called each time ScanAPI is reporting an error
             * @param result contains the error code
             */
            void OnError(long result, string errMsg);

            /**
             * called each time ScanAPI receives decoded data from scanner
             * @param deviceInfo contains the device information from which
             * the data has been decoded
             * @param decodedData contains the decoded data information
             */
            void OnDecodedData(DeviceInfo device, ISktScanDecodedData decodedData);

            /**
             * called when ScanAPI initialization has been completed
             * @param result contains the initialization result
             */
            void OnScanApiInitializeComplete(long result);

            /**
             * called when ScanAPI has been terminated. This will be
             * the last message received from ScanAPI
             */
            void OnScanApiTerminated();

            /**
             * called when an error occurs during the retrieval
             * of a ScanObject from ScanAPI.
             * @param result contains the retrieval error code
             */
            void OnErrorRetrievingScanObject(long result);
        }

        public const int MAX_RETRIES = 5;
        private List<CommandContext> _commandContexts;
        private ISktScanApi _scanApi;
        private bool _scanApiOpen;
        private ScanApiHelperNotification _notification;
        private List<DeviceInfo> _devicesList;
        private DeviceInfo _noDeviceConnected;
        private byte _dataConfirmationMode =
            ISktScanProperty.values.confirmationMode.kSktScanDataConfirmationModeDevice;

        enum SAC_RESULT
        {
            SAC_OK,
            SAC_ASSIGNFAIL,
            SAC_ASSIGNUNEXPECT,
            SAC_ASSIGNREPLY,
            SAC_ASSIGNWAIT,
            SAC_STRINGERROR
        };


        public ScanApiHelper()
        {
            _commandContexts = new List<CommandContext>();
            _scanApi = SktClassFactory.createScanApiInstance();
            _notification = null;
            _devicesList = new List<DeviceInfo>();
            _noDeviceConnected = new DeviceInfo("", null, SktScanDeviceType.kSktScanDeviceTypeNone);
            _scanApiOpen = false;
        }


        //public ScanApiHelper(string strNewComPorts)
        //{
        //    // Call the function with the desired COM port string
        //    if (SetAssignedComPort(strNewComPorts, &nErrInfo) != SAC_RESULT.SAC_OK)
        //    {
        //        // Report the error from SetAssignedComPort
        //    }

        //    _commandContexts = new List<CommandContext>();
        //    _scanApi = SktClassFactory.createScanApiInstance();
        //    _notification = null;
        //    _devicesList = new List<DeviceInfo>();
        //    _noDeviceConnected = new DeviceInfo("", null, SktScanDeviceType.kSktScanDeviceTypeNone);
        //    _scanApiOpen = false;
        //}

        // SetAssignedComPort
        //
        // Tells ScanApi which port is assigned for use by SocketScan.
        // Return TRUE on success, FALSE on failure. 
        SAC_RESULT SetAssignedComPort(string strPortList, int pErrInfo)
        {
            ISktScanObject scanObj;
            //TSktScanObject ScanObj;
            //TSktScanObject pScanObj;
            SAC_RESULT sacResult = SAC_RESULT.SAC_OK;
            //const int COMPORTNAMELEN = 255;
            //char szPortList[COMPORTNAMELEN];

            scanObj = SktClassFactory.createScanObject();
            scanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDataConfirmationMode;
            scanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
            //scanObj.Property.String.Value = ISktScanProperty.propId.kSktScanPropIdConfiguration;
            //scanObj.Property.Byte=(byte)mode;




            //delete[] ScanObj.Property.Value.String.pValue;
            return sacResult;

        }

        void SetComPort(string strPortList)
        {
            ISktScanObject scanObj = SktClassFactory.createScanObject();
            ISktScanObject myScanObj = (ISktScanObject)scanObj;
            //myScanObj.Msg.DeviceGuid = "{EDBE1A21-F62F-414d-9DBE-85EE0F428F39}";
            //myScanObj.Msg.DeviceName = "TestDeviceName";
            scanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdConfiguration;
            scanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            scanObj.Property.String.Value = "SerialPorts=\\\\.\\COM13";
            //SetProperty(_handle,scanObj);


        }


        /**
         * register for notifications in order to receive notifications such as
         * "Device Arrival", "Device Removal", "Decoded Data"...etc...
         * @param notification
         */
        public void SetNotification(ScanApiHelperNotification notification)
        {
            _notification = notification;
        }

        /**
         * specifying a name to display when no device is connected
         * will add a no device connected item in the list with 
         * the name specified, otherwise if there is no device connected
         * the list will be empty.
         */
        public void SetNoDeviceText(String noDeviceText)
        {
            _noDeviceConnected.Name = noDeviceText;
        }

        /**
         * get the list of devices. If there is no device
         * connected and a text has been specified for
         * when there is no device then the list will
         * contain one item which is the no device in the 
         * list
         * @return
         */
        public List<DeviceInfo> GetDevicesList()
        {
            return _devicesList;
        }

        /**
         * check if there is a device connected
         * @return
         */
        public bool IsDeviceConnected()
        {
            bool isDeviceConnected = false;
            if (_devicesList.Count > 0)
                isDeviceConnected = true;
            return isDeviceConnected;
        }

        /**
         * flag to know if ScanAPI is open
         * @return
         */
        public bool IsScanApiOpen()
        {
            return _scanApiOpen;
        }

        /**
         * open ScanAPI and initialize ScanAPI
         * The result of opening ScanAPI is returned in the callback
         * onScanApiInitializeComplete
         */
        public void Open()
        {
            _devicesList.Clear();
            if (_noDeviceConnected.Name.Length > 0)
                _devicesList.Add(_noDeviceConnected);

            Thread thread = new Thread(new ThreadStart(ScanApiInitializationThread));
            thread.Start();
            _scanApiOpen = true;
        }

        /**
         * close ScanAPI. The callback onScanApiTerminated
         * is invoked as soon as ScanAPI is completely closed.
         * If a device is connected, a device removal will be received
         * during the process of closing ScanAPI.
         */
        public void Close()
        {
            PostScanApiAbort(onSetScanApiAbort);
            _scanApiOpen = false;
        }

        public long DoScanAPIReceive()
        {
            long result = SktScanErrors.ESKT_NOERROR;
            bool closeScanApi = false;
            if (_scanApiOpen == true)
            {
                ISktScanObject scanObj;
                result = _scanApi.WaitForScanObject(out scanObj, 1);
                if (SktScanErrors.SKTSUCCESS(result))
                {
                    if (result == SktScanErrors.ESKT_NOERROR)
                    {
                        closeScanApi = HandleScanObject(scanObj);
                        _scanApi.ReleaseScanObject(scanObj);
                    }
                    if (closeScanApi == false)
                        SendNextCommand();
                    else
                    {
                        _scanApi.Close();
                        if (_notification != null)
                            _notification.OnScanApiTerminated();
                    }
                }
                else
                {
                    _scanApi.Close();
                    if (_notification != null)
                    {
                        _notification.OnErrorRetrievingScanObject(result);
                        _notification.OnScanApiTerminated();
                    }
                }
            }
            return result;
        }

        /**
         * remove the pending commands for a specific device
         * or all the pending commands if null is passed as
         * iDevice parameter
         * @param iDevice reference to the device for which
         * the commands must be removed from the list or <b>null</b>
         * if all the commands must be removed.
         */
        public void RemoveCommands(DeviceInfo device)
        {
            ISktScanDevice iDevice = null;
            if (device != null)
                iDevice = device.SktScanDevice;
            // remove all the pending command for this device
            lock (_commandContexts)
            {
                if (iDevice != null)
                {
                    int index = 0;
                    while (index < _commandContexts.Count)
                    {
                        if (_commandContexts[index].ScanDevice == iDevice)
                        {
                            _commandContexts.RemoveAt(index);
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
                else
                {
                    _commandContexts.Clear();
                }
            }
        }

        /**
         * PostGetScanAPIVersion
         * retrieve the ScanAPI Version
         */
        public void PostGetScanAPIVersion(ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdVersion;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;
            CommandContext command = new CommandContext(true, newScanObj, _scanApi, null, callback);
            AddCommand(command);
        }

        /// <summary>
        /// PostGetDataConfirmationMode gets the dataconfirmation feature.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostGetDataConfirmationMode(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDataConfirmationMode;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            CommandContext command = new CommandContext(true, newScanObj, _scanApi, deviceInfo, callback);
            AddCommand(command);
        }

        /**
         * PostSetConfirmationMode
         * Configures ScanAPI so that scanned data must be confirmed by this application before the
         * scanner can be triggered again.
         */
        public void PostSetConfirmationMode(DeviceInfo device, string dataConfirmationMode, ICommandContextCallback callback)
        {
            //ISktScanObject newScanObj = SktClassFactory.createScanObject();
            //newScanObj.Property.ID=ISktScanProperty.propId.kSktScanPropIdDataConfirmationMode;
            //newScanObj.Property.Type=ISktScanProperty.types.kSktScanPropTypeByte;
            //newScanObj.Property.Byte = (byte)dataConfirmationMode;

            //CommandContext command = new CommandContext(false, newScanObj, _scanApi, null, callback);
            //AddCommand(command);
            //return;

            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDataConfirmationMode;
            //newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdDataConfirmationMode);

            if (dataConfirmationMode.Equals("off", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.confirmationMode.kSktScanDataConfirmationModeOff;
            }
            else if (dataConfirmationMode.Equals("device", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.confirmationMode.kSktScanDataConfirmationModeDevice;
            }
            else if (dataConfirmationMode.Equals("scanapi", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.confirmationMode.kSktScanDataConfirmationModeScanAPI;
            }
            else if (dataConfirmationMode.Equals("app", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.confirmationMode.kSktScanDataConfirmationModeApp;
            }
            CommandContext command = new CommandContext(false, newScanObj, _scanApi, device, callback);
            //CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        /**
         * PostSetDataConfirmation
         * acknowledge the decoded data<p>
         * This is only required if the scanner Confirmation Mode is set to kSktScanDataConfirmationModeApp
         */
        public void PostSetDataConfirmation(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {

            ISktScanDevice device = deviceInfo.SktScanDevice;
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDataConfirmationDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeUlong;
            newScanObj.Property.Ulong =
                    SktScan.helper.SKTDATACONFIRMATION(
                            0,
                            ISktScanProperty.values.dataConfirmation.kSktScanDataConfirmationRumbleNone,
                            ISktScanProperty.values.dataConfirmation.kSktScanDataConfirmationBeepGood,
                            ISktScanProperty.values.dataConfirmation.kSktScanDataConfirmationLedGreen);


            CommandContext command = new CommandContext(false, newScanObj, device, null, callback);
            if (_commandContexts.Count == 0)
                AddCommand(command);
            else
            {
                int index = 0;
                CommandContext pendingCommand = (CommandContext)_commandContexts.ElementAt(index);
                if (pendingCommand.Status == CommandContext.statusNotCompleted)
                    _commandContexts.Insert(index + 1, command);
            }

            // try to see if the confirmation can be sent right away
            SendNextCommand();
        }

        /**
         * PostGetBtAddress
         * Creates a TSktScanObject and initializes it to perform a request for the
         * Bluetooth address in the scanner.
         */
        public void PostGetBtAddress(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdBluetoothAddressDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);

        }

        /**
         * PostGetDeviceType
         * Creates a TSktScanObject and initializes it to perform a request for the
         * Device Type of the scanner.
         */
        public void PostGetDeviceType(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDeviceType;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);

        }

        /**
         * DoGetFirmware
         * Creates a TSktScanObject and initializes it to perform a request for the
         * firmware revision in the scanner.
         */
        public void PostGetFirmware(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdVersionDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);

        }
        /**
         * DoStartDecode
         */
        public void PostStartDecode(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdTriggerDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
            newScanObj.Property.Byte = (byte)ISktScanProperty.values.trigger.kSktScanTriggerStart;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(false, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }
        /**
         * DoGetBattery
         * Creates a TSktScanObject and initializes it to perform a request for the
         * battery level in the scanner.
         */
        public void PostGetBattery(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdBatteryLevelDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /**
         * PostGetDecodeAction
         * 
         * Creates a TSktScanObject and initializes it to perform a request for the
         * Decode Action in the scanner.
         * 
         */
        public void PostGetDecodeAction(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdLocalDecodeActionDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);

        }

        /**
         * DoGetCapabilitiesDevice
         * 
         * Creates a TSktScanObject and initializes it to perform a request for the
         * Capabilities Device in the scanner.
         */
        public void PostGetCapabilitiesDevice(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdCapabilitiesDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
            newScanObj.Property.Byte = (byte)ISktScanProperty.values.capabilityGroup.kSktScanCapabilityLocalFunctions;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);

        }

        /**
         * DoGetPostambleDevice
         * 
         * Creates a TSktScanObject and initializes it to perform a request for the
         * Postamble Device in the scanner.
         * 
         */
        public void PostGetPostambleDevice(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdPostambleDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /**
         * PostGetSymbologyInfo
         * 
         * Creates a TSktScanObject and initializes it to perform a request for the
         * Symbology Info in the scanner.
         * 
         */
        public void PostGetSymbologyInfo(DeviceInfo deviceInfo, int symbologyId, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSymbologyDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeSymbology;
            newScanObj.Property.Symbology.Flags = ISktScanSymbology.flags.kSktScanSymbologyFlagStatus;
            newScanObj.Property.Symbology.ID = symbologyId;
            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /**
         * PostGetAllSymbologyInfo
         * 
         * Post a series of get Symbology info in order to retrieve all the
         * Symbology Info of the scanner.
         * The callback would be called each time a Get Symbology request has completed 
         */
        public void PostGetAllSymbologyInfo(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            for (int symbologyId = ISktScanSymbology.id.kSktScanSymbologyNotSpecified + 1;
                symbologyId < ISktScanSymbology.id.kSktScanSymbologyLastSymbologyID; symbologyId++)
            {
                ISktScanObject newScanObj = SktClassFactory.createScanObject();
                newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSymbologyDevice;
                newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeSymbology;
                newScanObj.Property.Symbology.Flags = ISktScanSymbology.flags.kSktScanSymbologyFlagStatus;
                newScanObj.Property.Symbology.ID = symbologyId;
                // add the property and the device to the command context list
                // to send it as soon as it is possible
                CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
                AddCommand(command);
            }
        }

        /**
         * PostSetSymbologyInfo
         * Constructs a request object for setting the Symbology Info in the scanner
         * 
         */
        public void PostSetSymbologyInfo(DeviceInfo deviceInfo, int Symbology, bool Status, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSymbologyDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeSymbology;
            newScanObj.Property.Symbology.Flags = ISktScanSymbology.flags.kSktScanSymbologyFlagStatus;
            newScanObj.Property.Symbology.ID = Symbology;
            if (Status)
                newScanObj.Property.Symbology.Status = ISktScanSymbology.status.kSktScanSymbologyStatusEnable;
            else
                newScanObj.Property.Symbology.Status = ISktScanSymbology.status.kSktScanSymbologyStatusDisable;

            CommandContext command = new CommandContext(false, newScanObj, device, null, callback);
            command.SymbologyId = Symbology;// keep the symbology ID because the Set Complete won't return it
            AddCommand(command);
            return;
        }

        /**
         * PostGetFriendlyName
         * 
         * Creates a TSktScanObject and initializes it to perform a request for the
         * friendly name in the scanner.
         * 
         */

        public void PostGetFriendlyName(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdFriendlyNameDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;
            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /** 
         * PostSetFriendlyName
         * Constructs a request object for setting the Friendly Name in the scanner
         * 
         */
        public void PostSetFriendlyName(DeviceInfo deviceInfo, String friendlyName, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdFriendlyNameDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.String.Value = friendlyName;
            CommandContext command = new CommandContext(false, newScanObj, device, null, callback);
            AddCommand(command);
        }

        /**
         * PostSetDecodeAction
         * 
         * Configure the local decode action of the device
         * 
         * @param device
         * @param decodeVal
         */
        public void PostSetDecodeAction(DeviceInfo device, int decodeVal, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdLocalDecodeActionDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
            newScanObj.Property.Byte = (byte)(decodeVal & 0xffff);

            CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        public void PostSetDecodeAction(DeviceInfo device, string decodeActionValues, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdNotificationsDevice;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdLocalDecodeActionDevice);

            int nCount = 0;
            byte value = 0;
            String[] ppWords = null;
            ppWords = decodeActionValues.Split('+');
            if (ppWords != null)
                nCount = ppWords.Length;
            newScanObj.Property.Byte = ISktScanProperty.values.localDecodeAction.kSktScanLocalDecodeActionNone;
            for (int i = 0; i < nCount; i++)
            {
                if (ppWords[i].Equals("none", StringComparison.OrdinalIgnoreCase))
                {
                    value = ISktScanProperty.values.localDecodeAction.kSktScanLocalDecodeActionNone;
                    break;
                }
                else if (ppWords[i].Equals("beep", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.localDecodeAction.kSktScanLocalDecodeActionBeep;
                }
                else if (ppWords[i].Equals("flash", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.localDecodeAction.kSktScanLocalDecodeActionFlash;
                }
                else if (ppWords[i].Equals("rumble", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.localDecodeAction.kSktScanLocalDecodeActionRumble;
                }
                else
                {
                    //OutputString("Unknown parameter:" + ppWords[i] + ", should be none or beep+flash+rumble or any combination" + CARRIAGE_RETURN);
                    //result = SktScanErrors.ESKT_INVALIDPARAMETER;
                    break;
                }
            }
            newScanObj.Property.Byte = value;
        }

        /// <summary>
        /// This property can be used to get the device preamble. When a preamble is set, it will be added in front of the decoded data. 
        /// The preamble is defined as a string with a length that can contain any value for each character from 0 (0x00) to 255 (0xff). 
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostGetPreambleDevice(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdPreambleDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /// <summary>
        /// This property can be used to set the device preamble. When a preamble is set, it will be added in front of the decoded data. 
        /// The preamble is defined as a string with a length that can contain any value for each character from 0 (0x00) to 255 (0xff).
        /// </summary>
        /// <param name="device"></param>
        /// <param name="prefix"></param>
        /// <param name="callback"></param>
        public void PostSetPreamble(DeviceInfo device, String prefix, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdPreambleDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.String.Value = prefix;

            CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        /**
         * PostSetPostamble
         * 
         * Configure the postamble of the device
         * @param device
         * @param suffix
         */
        public void PostSetPostamble(DeviceInfo device, String suffix, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdPostambleDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.String.Value = suffix;

            CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        public void PostGetSecurityMode(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSecurityModeDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        public void PostSetSecurityMode(DeviceInfo device, string securityMode, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSecurityModeDevice;
            //newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdSecurityModeDevice);

            if (securityMode.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.securityMode.kSktScanSecurityModeNone;
            }
            else if (securityMode.Equals("authentication", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.securityMode.kSktScanSecurityModeAuthentication;
            }
            else if (securityMode.Equals("authenticationEncryption", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.securityMode.kSktScanSecurityModeAuthenticationEncryption;
            }
            CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
            //CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        /// <summary>
        /// Gets the sound configuration of the connected device.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        //public void PostGetSoundConfig(DeviceInfo deviceInfo, int actionTypeId, ICommandContextCallback callback)
        //{
        //    ISktScanDevice device = deviceInfo.SktScanDevice;
        //    // create and initialize the property to send to the device
        //    ISktScanObject newScanObj = SktClassFactory.createScanObject();
        //    newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSoundConfigDevice;
        //    newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
        //    newScanObj.Property.Byte = (byte)actionTypeId;
        //    // add the property and the device to the command context list to send it as soon as it is possible
        //    CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
        //    AddCommand(command);
        //}
        public void PostGetSoundConfig(DeviceInfo device, Byte bAction, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSoundConfigDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
            newScanObj.Property.Byte = bAction;

            //CommandContext command = new CommandContext(true, newScanObj, device.SktScanDevice, null, callback);
            CommandContext command = new CommandContext(true, newScanObj, device.SktScanDevice, device, callback);
            AddCommand(command);

        }


        public void PostSetSoundConfig(DeviceInfo device, string[] soundConfigData, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdSoundConfigDevice;
            //newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdSoundConfigDevice);


            int nNumberOfTones = Convert.ToInt32(soundConfigData[1]);
            int nSize = (nNumberOfTones * 6) + 2 + 2;// +ActionType+NumberOfTones
            short wWord = Convert.ToInt16(soundConfigData[0]);// Action Type

            if (soundConfigData.Count() == ((int)nNumberOfTones * 5))
            {
                byte[] value = new byte[nSize];

                int nIndex = 0;
                if (value != null)
                {
                    // Action Type
                    value[nIndex++] = (byte)(wWord >> 8);
                    value[nIndex++] = (byte)wWord;

                    // Number Of Tones
                    wWord = (short)Convert.ToInt32(soundConfigData[1]);
                    value[nIndex++] = (byte)(wWord >> 8);
                    value[nIndex++] = (byte)wWord;

                    String pToneDetails;
                    short wToneDetails = 0;
                    short wOffset = 2;
                    for (short i = 0; i < wWord; i++)
                    {
                        // frequency
                        if (soundConfigData[wOffset + i].Equals("none", StringComparison.OrdinalIgnoreCase))
                            wToneDetails = ISktScanProperty.values.soundFrequency.kSktScanSoundFrequencyNone;
                        else if (soundConfigData[wOffset + i].Equals("low", StringComparison.OrdinalIgnoreCase))
                            wToneDetails = ISktScanProperty.values.soundFrequency.kSktScanSoundFrequencyLow;
                        else if (soundConfigData[wOffset + i].Equals("medium", StringComparison.OrdinalIgnoreCase))
                            wToneDetails = ISktScanProperty.values.soundFrequency.kSktScanSoundFrequencyMedium;
                        else if (soundConfigData[wOffset + i].Equals("high", StringComparison.OrdinalIgnoreCase))
                            wToneDetails = ISktScanProperty.values.soundFrequency.kSktScanSoundFrequencyHigh;
                        else
                            wToneDetails = 0x1234;// some random value that should produce an error
                        value[nIndex++] = (byte)(wToneDetails >> 8);
                        value[nIndex++] = (byte)wToneDetails;

                        // duration
                        wOffset++;
                        pToneDetails = soundConfigData[wOffset + i];
                        wToneDetails = RetrieveWord(pToneDetails);
                        value[nIndex++] = (byte)(wToneDetails >> 8);
                        value[nIndex++] = (byte)wToneDetails;

                        // pause
                        wOffset++;
                        pToneDetails = soundConfigData[wOffset + i];
                        wToneDetails = RetrieveWord(pToneDetails);
                        value[nIndex++] = (byte)(wToneDetails >> 8);
                        value[nIndex++] = (byte)wToneDetails;
                    }

                    newScanObj.Property.Array.Value = value;
                }
            }
            CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
            //CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        protected short RetrieveWord(String pNumberString)
        {
            return Convert.ToInt16(pNumberString, 16);
        }

        /// <summary>
        /// This functions gets the various timers[Trigger Lock out, Disconnected Off and Connected Off] of connected device.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostGetTimersDevice(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdTimersDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;
            // add the property and the device to the command context list to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /// <summary>
        /// This functions gets the various timers[Trigger Lock out, Disconnected Off and Connected Off] of connected device.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostSetTimersDevice(DeviceInfo device, string[] timersData, ICommandContextCallback callback)
        {
            long result = SktScanErrors.ESKT_NOERROR;
            short[] wValue = new short[1];
            int index = 0;
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdTimersDevice;
            //newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdTimersDevice);

            byte[] deviceTimers = new byte[4 * 2];// = mask+Lockout+disconnected+connected = 4 words
            if (deviceTimers != null)
            {
                result = SktDebug.DBGSKT_EVAL(
                        ReadTimersMask(timersData[0], wValue),
                        "ReadTimersMask(timersData[0],&wValue)");

                if (SktScanErrors.SKTSUCCESS(result))
                {
                    // Mask timer
                    deviceTimers[index++] = (byte)(wValue[0] >> 8);
                    deviceTimers[index++] = (byte)(wValue[0]);

                    // Trigger Lock Out timeout
                    wValue[0] = (short)Convert.ToInt32(timersData[1]);
                    deviceTimers[index++] = (byte)(wValue[0] >> 8);
                    deviceTimers[index++] = (byte)(wValue[0]);

                    // disconnect Power Off timeout
                    wValue[0] = (short)Convert.ToInt32(timersData[2]);
                    deviceTimers[index++] = (byte)(wValue[0] >> 8);
                    deviceTimers[index++] = (byte)(wValue[0]);

                    // Connected Power Off timeout
                    wValue[0] = (short)Convert.ToInt32(timersData[3]);
                    deviceTimers[index++] = (byte)(wValue[0] >> 8);
                    deviceTimers[index++] = (byte)(wValue[0]);

                    newScanObj.Property.Array.Value = deviceTimers;

                    CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
                    AddCommand(command);

                    deviceTimers = null;
                }
            }
            else
            {
                result = SktScanErrors.ESKT_NOTENOUGHMEMORY;
            }
        }
        private long ReadTimersMask(String mask, short[] wValue)
        {
            long result = SktScanErrors.ESKT_NOERROR;
            bool found = false;
            if (wValue == null)
            {
                result = SktScanErrors.ESKT_INVALIDPARAMETER;
            }
            if (SktScanErrors.SKTSUCCESS(result))
            {
                wValue[0] = 0;
                int count = 0;
                String[] words = mask.Split('+');//SplitInWords(mask, "+");
                if (words != null)
                    count = words.Length;
                for (int i = 0; i < count; i++)
                {
                    //if(words[i].CompareTo("connected")==0){
                    if (words[i].Equals("connected", StringComparison.OrdinalIgnoreCase))
                    {
                        wValue[0] += ISktScanProperty.values.timers.kSktScanTimerPowerOffConnected;
                        found = true;
                    }
                    else if (words[i].Equals("disconnected", StringComparison.OrdinalIgnoreCase))
                    {
                        wValue[0] += ISktScanProperty.values.timers.kSktScanTimerPowerOffDisconnected;
                        found = true;
                    }
                    else if (words[i].Equals("lockout", StringComparison.OrdinalIgnoreCase))
                    {
                        wValue[0] += ISktScanProperty.values.timers.kSktScanTimerTriggerAutoLockTimeout;
                        found = true;
                    }
                    else
                    {
                        //OutputString("Invalid parameter: " + words[i] + " should be connected+disconnected+lockout or any combination of them" + CARRIAGE_RETURN);
                        result = SktScanErrors.ESKT_INVALIDPARAMETER;
                        break;
                    }
                }
            }
            if (SktScanErrors.SKTSUCCESS(result))
            {
                if (found == false)
                {
                    //OutputString("Invalid parameter: should be connected+disconnected+lockout or any combination of them" + CARRIAGE_RETURN);
                    result = SktScanErrors.ESKT_INVALIDPARAMETER;
                }
            }
            return result;
        }
        private String[] SplitInWords(String words, String separator)
        {
            // count the number of + in the string
            // allocate an array
            int wordsLength = words.Length;
            int count = 0;
            int index = 0;
            int beginIndex = 0;
            String[] wordsArray = null;

            if (wordsLength > 0)
            {
                int lastIndex = 0;
                for (int i = 0; i < wordsLength; i++)
                {
                    if (separator.IndexOf(words[i]) != -1)
                    {
                        count++;
                        lastIndex = i;
                    }
                }
                // if the last word doesn't finish by
                // separator then add it in the count
                if (lastIndex < (wordsLength - 1))
                    count++;
            }
            if (count > 0)
            {
                wordsArray = new String[count];
                for (int i = 0; i < wordsLength; i++)
                {
                    if (separator.IndexOf(words[i]) != -1)
                    {
                        if (i > beginIndex)// if there is at least one character
                            wordsArray[index++] = words.Substring(beginIndex, i - beginIndex);
                        beginIndex = i + 1;
                    }
                }
                // is there a last word not terminating by a separator
                if (beginIndex < wordsLength)
                {
                    wordsArray[index++] = words.Substring(beginIndex);
                }

            }

            return wordsArray;
        }

        /// <summary>
        /// This function gets the LocalAcknowledgement whether it will be either 'Enable' or 'Disable'.
        /// When it is 'Enable', device will acknowledge the decoded data.
        /// When it is 'Diable', device will not acknowledge decoded data and trigger button is locked until the host acknowledges data or until the trigger lockout time has elapsed.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostGetLocalAcknowledgmentDevice(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdLocalAcknowledgmentDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;
            // add the property and the device to the command context list to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /// <summary>
        /// This function sets the LocalAcknowledgement to either 'Enable' or 'Disable'.
        /// Set it to 'Enable' sothat device will acknowledge the decoded data.
        /// Set it to 'Diable' sothat device will not acknowledge decoded data and trigger button is locked until the host acknowledges data or until the trigger lockout time has elapsed.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostSetLocalAcknowledgmentDevice(DeviceInfo device, string localAcknwledgment, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdLocalAcknowledgmentDevice;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdLocalAcknowledgmentDevice);

            bool bSendCommand = true;
            if (localAcknwledgment.Equals("enable", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.deviceDataAcknowledgment.kSktScanDeviceDataAcknowledgmentOn;
            }
            else if (localAcknwledgment.Equals("disable", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = ISktScanProperty.values.deviceDataAcknowledgment.kSktScanDeviceDataAcknowledgmentOff;
            }
            else if (localAcknwledgment.Equals("wrongpara", StringComparison.OrdinalIgnoreCase))
            {
                newScanObj.Property.Byte = 5;
            }
            else
            {
                //OutputString("Invalid parameter, should be either enable, disable or WrongPara" + CARRIAGE_RETURN);
                bSendCommand = false;
            }
            if (bSendCommand == true)
            {
                CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
                AddCommand(command);
            }
        }

        /// <summary>
        /// Gets the data/information stored in device itself which can be used host. Datastore has 16 storage locations, each identified by a key value of 0 to 15. Each location can store max 64 bytes. 
        /// Currently, this version has support for only for 0th location.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="datastoreIdentifierKey"></param>
        /// <param name="callback"></param>
        public void PostGetDataStoreDevice(DeviceInfo deviceInfo, string datastoreIdentifierKey, ICommandContextCallback callback)
        {
            long result = SktScanErrors.ESKT_NOERROR;
            //byte[] data = new byte[2];
            //data = new byte[] { 0x00, 0x00 };
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDataStoreDevice;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVEGETTYPE(newScanObj.Property.ID);

            result = SktDebug.DBGSKT_EVAL(
                    SetScanObjArray(newScanObj, datastoreIdentifierKey),
                    "SetScanObjArray(scanObj,datastoreIdentifierKey)");

            if (SktScanErrors.SKTSUCCESS(result))
            {
                //newScanObj.Property.Array.Value = data;
                //newScanObj.Property.Array.Value = newScanObj.Property.Array.Value;

                // add the property and the device to the command context list to send it as soon as it is possible
                CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
                AddCommand(command);
            }
        }

        /// <summary>
        /// Sets the data/information stored in device itself which can be used host. Datastore has 16 storage locations, each identified by a key value of 0 to 15. Each location can store max 64 bytes. 
        /// Currently,this version has support for only for 0th location.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="datastoreDetails"></param>
        /// <param name="callback"></param>
        public void PostSetDataStoreDevice(DeviceInfo device, string[] datastoreDetails, ICommandContextCallback callback)
        {
            long result = SktScanErrors.ESKT_NOERROR;

            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdDataStoreDevice;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(newScanObj.Property.ID);

            short wValue = 0;
            int index = 0;
            bool bSend = true;

            int size = Convert.ToInt32(datastoreDetails[1]);
            size += 2 * 2;// add the size of the Key and Length
            byte[] dataStore = new byte[size];
            if (dataStore == null)
            {
                //OutputString("Unable to allocate memory to complete this operation" + CARRIAGE_RETURN);
            }
            else
            {
                // key
                wValue = (short)Convert.ToInt16(datastoreDetails[0]);
                dataStore[index++] = (byte)(wValue >> 8);
                dataStore[index++] = (byte)wValue;

                // Length
                wValue = (short)Convert.ToInt16(datastoreDetails[1]);
                dataStore[index++] = (byte)(wValue >> 8);
                dataStore[index++] = (byte)wValue;

                // if the length is not null
                // then a data field should be specified
                if ((wValue > 0) && (datastoreDetails.Length == 3))
                {
                    if (wValue != datastoreDetails[2].Length / 2)
                    {
                        //OutputString("data length doesn't match with the length specified (" + wValue + "!=" + data._parameters[5].Length / 2 + ")" + CARRIAGE_RETURN);
                        bSend = false;
                    }
                    else
                    {
                        // Data
                        result = SktDebug.DBGSKT_EVAL(
                                ConvertStringToArray(dataStore, index, datastoreDetails[2]),
                                "ConvertStringToArray(dataStore,index,datastoreDetails[2])");
                    }
                }
                else
                {
                    //OutputString("the length is not null and there is no data specified" + CARRIAGE_RETURN);
                    //OutputString("Usage: set hChs kSktScanPropIdDataStoreDevice <Key (0 to 15)> <Length> <Data in Hexa>" + CARRIAGE_RETURN);
                    //OutputString("Usage: set hChs kSktScanPropIdDataStoreDevice 01 16 0102030405060708090A0B0C0D0E0F10" + CARRIAGE_RETURN);
                    bSend = false;
                }
                if (bSend == true)
                {
                    newScanObj.Property.Array.Value = dataStore;
                    CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
                    //CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
                    AddCommand(command);
                }
            }
        }

        private long SetScanObjArray(ISktScanObject scanObj, String value)
        {
            long result = SktScanErrors.ESKT_NOERROR;
            int size = value.Length;

            size /= 2;
            scanObj.Property.Array.Size = size;

            result = SktDebug.Eval(
                    ConvertStringToArray(scanObj.Property.Array.Value, 0, value),
                    "ConvertStringToArray(scanObj.Property.Array.Value,0,value)");

            return result;
        }

        private long ConvertStringToArray(byte[] data, int index, string stringInHexa)
        {
            long result = SktScanErrors.ESKT_NOERROR;
            int size = stringInHexa.Length;
            for (int i = 0; i < size; i++)
            {
                data[index++] = ConvertToByte(stringInHexa.Substring(i, 2));
                i++;
            }

            return result;
        }

        private byte ConvertToByte(String substring)
        {
            byte value = 0;
            for (int i = 0; i < 2; i++)
            {
                value <<= 4;
                if ((substring[i] >= '0') && (substring[i] <= '9'))
                {
                    value += (byte)(substring[i] - '0');
                }
                else if ((substring[i] >= 'A') && (substring[i] <= 'F'))
                {
                    value += (byte)(substring[i] - (char)'A');
                    value += 10;
                }
                else if ((substring[i] >= 'a') && (substring[i] <= 'f'))
                {
                    value += (byte)(substring[i] - 'a');
                    value += 10;
                }
            }
            return value;
        }

        /// <summary>
        /// Gets the attributes of RumbleConfig property. 
        /// Its has 4 RumbleActionTypes:[0 - GoodScan, 1 - GoodScanLocal, 2 - BadScan, 3 - BadScanLocal].
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="actionTypeId"></param>
        /// <param name="callback"></param>
        public void PostGetRumbleConfigDevice(DeviceInfo deviceInfo, int actionTypeId, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdRumbleConfigDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeByte;
            newScanObj.Property.Byte = (byte)actionTypeId;
            // add the property and the device to the command context list to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /// <summary>
        /// Sets the attributes of Rumble config property.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="rumbleConfigData"></param>
        /// <param name="callback"></param>
        public void PostSetRumbleConfigDevice(DeviceInfo device, string[] rumbleConfigData, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdRumbleConfigDevice;
            //newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeString;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdRumbleConfigDevice);

            int nNumberOfRumbles = Convert.ToInt32(rumbleConfigData[1]);
            int nSize = (nNumberOfRumbles * 4) + 2 + 2;// +ActionType+NumberOfRumbles
            short wWord = (short)Convert.ToInt16(rumbleConfigData[0]);// Action Type

            string[] durationPause = rumbleConfigData[2].Split(' ');

            /*((int)nNumberOfRumbles * 2) + 1) ==> 
             * here +2 represents count for ActionType and Number of rumbles i.e. 1+1
             * 
             */

            if (durationPause.Length == (((int)nNumberOfRumbles * 2)))
            {
                byte[] values = new byte[nSize];
                int nIndex = 0;
                if (values != null)
                {
                    // Action Type
                    values[nIndex++] = (byte)(wWord >> 8);
                    values[nIndex++] = (byte)wWord;

                    // Number Of Rumbles
                    wWord = (short)Convert.ToInt32(rumbleConfigData[1]);
                    values[nIndex++] = (byte)(wWord >> 8);
                    values[nIndex++] = (byte)wWord;

                    short wRumbleDetails = 0;
                    short wOffset = 0;
                   
                    for (short i = 0; i < wWord; i++)
                    {
                        wRumbleDetails = RetrieveWord(durationPause[wOffset + i]);
                        values[nIndex++] = (byte)(wRumbleDetails >> 8);
                        values[nIndex++] = (byte)wRumbleDetails;

                        wOffset++;
                        wRumbleDetails = RetrieveWord(durationPause[wOffset + i]);
                        values[nIndex++] = (byte)(wRumbleDetails >> 8);
                        values[nIndex++] = (byte)wRumbleDetails;
                    }

                    newScanObj.Property.Array.Value = values;
                    CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
                    //CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, null, callback);
                    AddCommand(command);
                }
                else
                {
                    //result = SktScanErrors.ESKT_NOTENOUGHMEMORY;
                }
            }
            else
            {
                //OutputString("invalid number of parameters" + CARRIAGE_RETURN);
            }
        }

        /// <summary>
        /// This function gets the notifications that device can send to host.
        /// Supported notifications are either None or combination of ScanPress,PowerPress and PowerRelease.
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="callback"></param>
        public void PostGetNotificationsDevice(DeviceInfo deviceInfo, ICommandContextCallback callback)
        {
            ISktScanDevice device = deviceInfo.SktScanDevice;
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdNotificationsDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            // add the property and the device to the command context list
            // to send it as soon as it is possible
            CommandContext command = new CommandContext(true, newScanObj, device, deviceInfo, callback);
            AddCommand(command);
        }

        /// <summary>
        /// This function sets the notifications that device can send to host.
        /// Supported notifications are either None or combination of ScanPress,PowerPress and PowerRelease.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="notificationsData"></param>
        /// <param name="callback"></param>
        public void PostSetNotificationsDevice(DeviceInfo device, string notificationsData, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdNotificationsDevice;
            newScanObj.Property.Type = (byte)SktScan.helper.SKTRETRIEVESETTYPE(ISktScanProperty.propId.kSktScanPropIdNotificationsDevice);

            int nCount = 0;
            String[] ppWords = notificationsData.Split('+');
            if (ppWords != null)
                nCount = ppWords.Length;
            bool bSend = true;
            int value = 0;
            // scanPress+scanRelease+powerPress+powerRelease+powerState+batteryLevel 
            for (int i = 0; i < nCount; i++)
            {
                if (ppWords[i].Equals("scanpress", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.notifications.kSktScanNotificationsScanButtonPress;
                }
                else if (ppWords[i].Equals("scanrelease", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.notifications.kSktScanNotificationsScanButtonRelease;
                }
                else if (ppWords[i].Equals("powerpress", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.notifications.kSktScanNotificationsPowerButtonPress;
                }
                else if (ppWords[i].Equals("powerrelease", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.notifications.kSktScanNotificationsPowerButtonRelease;
                }
                else if (ppWords[i].Equals("powerstate", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.notifications.kSktScanNotificationsPowerState;
                }
                else if (ppWords[i].Equals("batterylevel", StringComparison.OrdinalIgnoreCase))
                {
                    value |= ISktScanProperty.values.notifications.kSktScanNotificationsBatteryLevelChange;
                }
                else if (ppWords[i].Equals("none", StringComparison.OrdinalIgnoreCase) != true)
                {
                    //OutputString("Unknown parameter " + ppWords[i] + ", should be either " +
                    //       "scanPress+scanRelease+powerPress+powerRelease+powerState+batteryLevel or None" + CARRIAGE_RETURN);

                    bSend = false;
                    break;
                }
            }

            if (bSend == true)
            {
                newScanObj.Property.Ulong = value;
            }

            CommandContext command = new CommandContext(false, newScanObj, device.SktScanDevice, device, callback);
            AddCommand(command);
        }

        /**
         * PostGetStatistics
         * 
         * Retrieve the statistics values from the scanner
         * @param device
         * @param suffix
         */
        public void PostGetStatistics(DeviceInfo device, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdStatisticCountersDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            CommandContext command = new CommandContext(true, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        /// <summary>
        /// PostGetChangeId
        /// retrives a Change ID of the device symbology configuration and the device preamble and postamble.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="callback"></param>
        public void PostGetChangeId(DeviceInfo device, ICommandContextCallback callback)
        {
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdChangeIdDevice;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            CommandContext command = new CommandContext(true, newScanObj, device.SktScanDevice, null, callback);
            AddCommand(command);
        }

        /**
         * PostScanApiAbort
         * 
         * Request ScanAPI to shutdown. If there is some devices connected
         * we will receive Remove event for each of them, and once all the
         * outstanding devices are closed, then ScanAPI will send a 
         * Terminate event upon which we can close this application.
         * If the ScanAPI Abort command failed, then the callback will
         * close ScanAPI
         */
        public void PostScanApiAbort(ICommandContextCallback callback)
        {
            // create and initialize the property to send to the device
            ISktScanObject newScanObj = SktClassFactory.createScanObject();
            newScanObj.Property.ID = ISktScanProperty.propId.kSktScanPropIdAbort;
            newScanObj.Property.Type = ISktScanProperty.types.kSktScanPropTypeNone;

            CommandContext command = new CommandContext(false, newScanObj, _scanApi, null, callback);
            AddCommand(command);
        }

        private void AddCommand(CommandContext newCommand)
        {
            lock (_commandContexts)
            {
                if (newCommand.ScanObject.Property.ID ==
                    ISktScanProperty.propId.kSktScanPropIdAbort)
                {
                    Debug.MSG(Debug.kLevelTrace, "About to Add a ScanAPI Abort command so remove all previous commands");
                    _commandContexts.Clear();
                }
                _commandContexts.Add(newCommand);
                Debug.MSG(Debug.kLevelTrace, "Add a new command to send");
            }
        }



        /**
         * HandleScanObject
         * This method is called each time this application receives a
         * ScanObject from ScanAPI.
         * It returns true is the caller can safely close ScanAPI and
         * terminate its ScanAPI consumer.
         */
        private bool HandleScanObject(ISktScanObject scanObject)
        {
            bool closeScanApi = false;
            switch (scanObject.Msg.ID)
            {
                case ISktScanMsg.kSktScanMsgIdDeviceArrival:
                    HandleDeviceArrival(scanObject);
                    break;
                case ISktScanMsg.kSktScanMsgIdDeviceRemoval:
                    HandleDeviceRemoval(scanObject);
                    break;
                case ISktScanMsg.kSktScanMsgGetComplete:
                case ISktScanMsg.kSktScanMsgSetComplete:
                    DoGetOrSetComplete(scanObject);
                    break;
                case ISktScanMsg.kSktScanMsgIdTerminate:
                    Debug.MSG(Debug.kLevelTrace, "Receive a Terminate event, ask to close ScanAPI");
                    closeScanApi = true;
                    break;
                case ISktScanMsg.kSktScanMsgEvent:
                    HandleEvent(scanObject);
                    break;
            }
            return closeScanApi;
        }

        /**
         * HandleDeviceArrival
         * This method is called each time a device connect to the host.
         * 
         * We create a device info object to hold all the necessary
         * information about this device, including its interface
         * which is used as handle
         */
        private void HandleDeviceArrival(ISktScanObject scanObject)
        {
            String friendlyName = scanObject.Msg.DeviceName;
            String deviceGuid = scanObject.Msg.DeviceGuid;
            long type = scanObject.Msg.DeviceType;
            ISktScanDevice device = SktClassFactory.createDeviceInstance(_scanApi);
            DeviceInfo newDevice = null;
            long result = device.Open(deviceGuid);
            if (SktScanErrors.SKTSUCCESS(result))
            {
                // add the new device into the list
                newDevice = new DeviceInfo(friendlyName, device, type);
                lock (_devicesList)
                {
                    _devicesList.Add(newDevice);
                    _devicesList.Remove(_noDeviceConnected);
                }
            }
            if (_notification != null)
                _notification.OnDeviceArrival(result, newDevice);
        }

        /**
         * HandleDeviceRemoval
         * This method is called each time a device is disconnected from the host.
         * Usually this will be a good opportunity to close the device
         */
        private void HandleDeviceRemoval(ISktScanObject scanObject)
        {
            ISktScanDevice iDevice = scanObject.Msg.DeviceInterface;
            DeviceInfo deviceFound = null;
            lock (_devicesList)
            {
                foreach (DeviceInfo device in _devicesList)
                {
                    if (device.SktScanDevice == iDevice)
                    {
                        deviceFound = device;
                        break;
                    }
                }

                // let's notify whatever UI we might have
                if (deviceFound != null)
                {
                    RemoveCommands(deviceFound);
                    _devicesList.Remove(deviceFound);
                    if (_devicesList.Count == 0)
                    {
                        if (_noDeviceConnected.Name.Length > 0)
                            _devicesList.Add(_noDeviceConnected);
                    }
                    if (_notification != null)
                        _notification.OnDeviceRemoval(deviceFound);
                }
            }
            iDevice.Close();
        }

        /**
         * HandleEvent
         * 
         * This method handles asynchronous events coming from ScanAPI
         * including decoded data
         */
        private void HandleEvent(ISktScanObject scanObject)
        {
            ISktScanEvent Event = scanObject.Msg.Event;
            ISktScanDevice iDevice = scanObject.Msg.DeviceInterface;
            switch (Event.EventID)
            {
                case ISktScanEvent.id.kSktScanEventError:
                    if (_notification != null)
                    {
                        if (Event.DataType == ISktScanEvent.types.kSktScanEventDataTypeString)
                            _notification.OnError(scanObject.Msg.Result, Event.DataString.Value);
                        else
                            _notification.OnError(scanObject.Msg.Result, null);
                    }
                    break;
                case ISktScanEvent.id.kSktScanEventDecodedData:
                    ISktScanDecodedData decodedData = Event.DataDecodedData;
                    DeviceInfo deviceInfo = GetDeviceInfo(iDevice);
                    if (_notification != null)
                    {
                        _notification.OnDecodedData(deviceInfo, decodedData);
                    }

                    // if the Data Confirmation mode is set to App
                    // then confirm Data here
                    if (_dataConfirmationMode ==
                        ISktScanProperty.values.confirmationMode.kSktScanDataConfirmationModeApp)
                    {
                        PostSetDataConfirmation(deviceInfo, null);

                    }
                    break;
                case ISktScanEvent.id.kSktScanEventPower:
                    ISktScanEvent asynchronousEvent = scanObject.Msg.Event;
                    if (asynchronousEvent.DataType == ISktScanEvent.types.kSktScanEventDataTypeUlong)
                    {
                        DisplayPowerState(asynchronousEvent.DataLong);
                    }
                    break;
                case ISktScanEvent.id.kSktScanEventButtons:
                    break;
            }
        }

        /**
         * DoGetOrSetComplete
         * "Get Complete" events arrive asynchonously via code in the timer handler of the Scanner List dialog. Even
         * though they may arrive asynchonously, they only arrive as the result of a successful corresponding "Get"
         * request.
         * 
         * This function examines the get complete event given in the pScanObj arg, and dispatches it to the correct
         * handler depending on the Property ID it contains.
         * 
         * Each property handler must return ESKT_NOERROR if it has successfully performed its processing.
         */
        private long DoGetOrSetComplete(ISktScanObject scanObj)
        {
            long result = SktScanErrors.ESKT_NOERROR;
            bool remove = true;
            bool doCallback = true;
            if (scanObj != null)
            {
                result = scanObj.Msg.Result;
                CommandContext command = (CommandContext)scanObj.Property.Context;
                Debug.MSG(Debug.kLevelTrace, "Complete event received for Context:" + command + "\n");
                if (command != null)
                {
                    if (!SktScanErrors.SKTSUCCESS(result) &&
                        (scanObj.Property.ID != ISktScanProperty.propId.kSktScanPropIdSetPowerOffDevice))
                    {
                        if (command.Retries >= MAX_RETRIES)
                        {
                            remove = true;
                        }
                        else
                        {
                            remove = false;// don't remove the command for a retry
                            doCallback = false;// don't call the callback for a silent retry
                            result = SktScanErrors.ESKT_NOERROR;
                        }
                    }

                    if (doCallback)
                        command.DoCallback(result, scanObj);

                    if (remove == true)
                    {
                        lock (_commandContexts)
                        {
                            Debug.MSG(Debug.kLevelTrace, "Remove command from the list\n");
                            _commandContexts.Remove(command);
                        }
                    }
                    else
                    {
                        command.Status = CommandContext.statusReady;
                    }
                }
                if (SktScanErrors.SKTSUCCESS(result))
                {
                    result = SendNextCommand();
                }
            }
            return result;
        }

        /**
         * sendNextCommand
         * This method checks if there is a command ready to be
         * sent at the top of the list. 
         */
        private long SendNextCommand()
        {
            long result = SktScanErrors.ESKT_NOERROR;

            lock (_commandContexts)
            {
                if (_commandContexts.Count > 0)
                {
                    Debug.MSG(Debug.kLevelTrace, "There are some commands to send\n");
                    CommandContext command = _commandContexts.First();
                    Debug.MSG(Debug.kLevelTrace, "And this one has status=" + command.Status + " for command: " +
                         command.ScanObject.Property.ID);
                    if (command.Status == CommandContext.statusReady)
                    {
                        result = command.DoGetOrSetProperty();
                        if (!SktScanErrors.SKTSUCCESS(result))
                        {
                            _commandContexts.Remove(command);
                            // case where the command is not supported by the device
                            // we can ignore it
                            if (result == SktScanErrors.ESKT_NOTSUPPORTED)
                            {
                                Debug.MSG(Debug.kLevelWarning, "Remove an unsupported command\n");
                            }
                            // case where the device handle is invalid (propably disconnected)
                            // we can ignore it
                            else if (result == SktScanErrors.ESKT_INVALIDHANDLE)
                            {
                                Debug.MSG(Debug.kLevelWarning, "Remove a command with an invalid handle\n");
                            }
                        }
                    }
                }
            }
            return result;
        }

        /**
         * retrieve the deviceInfo object matching to its ISktScanDevice interface
         * @param iDevice ScanAPI device interface
         * @return a deviceInfo object if it finds a matching device interface null
         * otherwise
         */
        private DeviceInfo GetDeviceInfo(ISktScanDevice iDevice)
        {
            DeviceInfo deviceInfo = null;
            lock (_devicesList)
            {
                foreach (DeviceInfo device in _devicesList)
                {
                    if (device.SktScanDevice == iDevice)
                    {
                        deviceInfo = device;
                        break;
                    }
                }
            }
            return deviceInfo;
        }

        private void ScanApiInitializationThread()
        {
            long result = SktScanErrors.ESKT_NOERROR;
            ScanApiHelper thisApp = (ScanApiHelper)this;
            result = thisApp._scanApi.Open(null);

            if (thisApp._notification != null)
                thisApp._notification.OnScanApiInitializeComplete(result);
        }

        /// <summary>
        /// This method calls OnScanApiTerminated if the set ScanAPI abort
        /// failed. If the set ScanAPI abort, the onScanApiTerminated is called
        /// upon reception of the Terminate event from ScanAPI
        /// </summary>
        /// <param name="target"></param>
        /// <param name="result"></param>
        /// <param name="scanObj"></param>
        public void onSetScanApiAbort(long result, ISktScanObject scanObj)
        {
            if (!SktScanErrors.SKTSUCCESS(result))
            {
                if (_notification != null)
                    _notification.OnScanApiTerminated();
            }
        }

        public string DisplayPowerState(long ulPowerState)
        {
            string powerState = string.Empty;
            int nState = SktScan.helper.SKTPOWER_GETSTATE((int)ulPowerState);
            if ((nState & ISktScanProperty.values.powerStates.kSktScanPowerStatusOnBattery) ==
                ISktScanProperty.values.powerStates.kSktScanPowerStatusOnBattery)
            {
                powerState = "On Battery ";
            }
            if ((nState & ISktScanProperty.values.powerStates.kSktScanPowerStatusOnCradle) ==
                ISktScanProperty.values.powerStates.kSktScanPowerStatusOnCradle)
            {
                powerState = "On Cradle ";
            }
            if ((nState & ISktScanProperty.values.powerStates.kSktScanPowerStatusOnAc) ==
                ISktScanProperty.values.powerStates.kSktScanPowerStatusOnAc)
            {
                powerState = "On AC ";
            }
            if (nState == ISktScanProperty.values.powerStates.kSktScanPowerStatusUnknown)
            {
                powerState = "Unknown state ";
            }

            return powerState;
        }
    }
}
