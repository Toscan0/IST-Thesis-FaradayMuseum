using System;
using System.Collections;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(ParserData))]
public class BLEManager : MonoBehaviour
{
    public static Action<bool> OnBluetoothConnected;

    [SerializeField]
    [Tooltip("To enable or disable the BLE conection.")]
    private bool connectToBLE;

    //Bluetooth icons 
    [SerializeField]
    private GameObject bluetoothSearching;
    [SerializeField]
    private GameObject bluetoothConnected;
    [SerializeField]
    private GameObject bluetoothDisconnected;

    [SerializeField]
    private TargetManager targetManager;

    // Arduino device name
    private string deviceName;
    // UART service UUID 
    private string UUID;
    // UUID_RX -> recive from arduino
    private string UUID_RX;
    // UUID_TX -> to write on arduino
    private string UUID_TX;

    //if the app is connect to the BLE
    private bool BLEconnected = false;

    private ParserData parserData;
    private BluetoothHelper bluetoothHelper;
    

    public Text UITEXT;

    private void Awake()
    {
        parserData = gameObject.GetComponent<ParserData>();

        MyTrackableEventHandler.OnTrackingObj += ObjTracking;
    }

    #region BLE_EVENTS
    private void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {
        Debug.Log("Found " + devices.Count);

        if (devices.Count == 0)
        {
            //Debug.Log("11111111111111111111111111");
            UpdateBluetoothIcons(true, false, false);
            BLEConnected(false);

            helper.ScanNearbyDevices();
            return;
        }

        try
        {
            helper.setDeviceName(deviceName);
            helper.Connect();

            Debug.Log("Connecting...");
        }
        catch (Exception ex)
        {
            //Debug.Log("222222222222222222222222222222222");

            UpdateBluetoothIcons(false, false, true);
            BLEConnected(false);

            Debug.LogError("OnScanEnded Exception founded: " + ex);
        }
    }

    private void OnConnected(BluetoothHelper helper)
    {
        Debug.Log("Connected");
        UpdateBluetoothIcons(false, true, false);
        BLEConnected(true);

        bluetoothHelper.StartListening();
    }

    private void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("Connection failed... Scanning aguen...");

        UpdateBluetoothIcons(true, false, false);
        BLEConnected(false);

        helper.ScanNearbyDevices();
    }

    private void OnDataReceived(BluetoothHelper helper)
    {
        string data = helper.Read();
        Debug.Log("TEST " + data);

        if (data != null && data != "")
        {
            parserData.Parser(data);

            UITEXT.text = data;
        }
    }
    #endregion


    #region BLE_ICONS
    private void InitBluetoothIcons()
    {
        if (connectToBLE)
        {
            UpdateBluetoothIcons(true, false, false);
            BLEConnected(false);
        }
        else
        {
            UpdateBluetoothIcons(false, false, false);
        }
    }

    private void UpdateBluetoothIcons(bool searching, bool connected, bool disconnected)
    {
        bluetoothSearching.SetActive(searching);
        bluetoothConnected.SetActive(connected);
        bluetoothDisconnected.SetActive(disconnected);
    }
    #endregion


    #region BLE_CONNECTION
    private void TryToConnect()
    {
        Debug.Log("App started Search for BLE connection: " + connectToBLE);

        if (connectToBLE == true)
        {
            try
            {
                BluetoothHelper.BLE = true;  //use Bluetooth Low Energy Technology
                bluetoothHelper = BluetoothHelper.GetInstance("TEST");

                // Every msg received needs to end in \n ==> needs to match BLE mdoule script
                bluetoothHelper.setTerminatorBasedStream("\n");

                Debug.Log("Device name: " + bluetoothHelper.getDeviceName());

                bluetoothHelper.OnScanEnded += OnScanEnded;

                bluetoothHelper.OnConnected += OnConnected;

                bluetoothHelper.OnConnectionFailed += OnConnectionFailed;

                bluetoothHelper.OnDataReceived += OnDataReceived;

                BluetoothHelperCharacteristic txC = new BluetoothHelperCharacteristic(UUID_TX);
                txC.setService(UUID);

                BluetoothHelperCharacteristic rxC = new BluetoothHelperCharacteristic(UUID_RX);
                rxC.setService(UUID);

                bluetoothHelper.setRxCharacteristic(rxC);
                bluetoothHelper.setTxCharacteristic(txC);

                // To subscribe explicitly - not necessary for now
                //bluetoothHelper.Subscribe(rxC);

                bluetoothHelper.ScanNearbyDevices();
            }
            catch (Exception ex)
            {
                //Debug.Log("3333333333333333333333333333333333");

                UpdateBluetoothIcons(false, false, true);
                BLEConnected(false);

                Debug.LogError("Connection Try exception founded: " + ex);
            }
        }
    }

    private void BLEConnected(bool isConnected)
    {
        BLEconnected = isConnected;

        OnBluetoothConnected?.Invoke(isConnected);
    }

    private void ConnectBLE()
    {
        DefineBLEDevice();
        TryToConnect();
    }

    private void DisconectBLE()
    {
        BLEDisconnectFromEvents();

        if (bluetoothHelper != null)
        {
            bluetoothHelper.Disconnect();
        }
    }

    private void BLEDisconnectFromEvents()
    {
        bluetoothHelper.OnScanEnded -= OnScanEnded;
        bluetoothHelper.OnConnected -= OnConnected;
        bluetoothHelper.OnConnectionFailed -= OnConnectionFailed;
        bluetoothHelper.OnDataReceived -= OnDataReceived;
    }
    #endregion


    #region BLE_API
    public void ConnectedByUser()
    {
        //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
        DefineBLEDevice();
        TryToConnect();

        UpdateBluetoothIcons(true, false, false);
        BLEConnected(false);
    }

    public void DisconnectByUser()
    {
        //Debug.Log("======================================================");
        UpdateBluetoothIcons(false, false, true);
        BLEConnected(false);

        BLEDisconnectFromEvents();

        if (bluetoothHelper != null)
        {
            bluetoothHelper.Disconnect();
        }
    }
    #endregion


    private void ObjTracking(bool isTracking)
    {
        if (isTracking)
        {
            InitBluetoothIcons();
            ConnectBLE();
        }
        else
        {
            UpdateBluetoothIcons(false, false, false);

            DisconectBLE();
        }
    }

    private void DefineBLEDevice()
    {
        if (targetManager.GetTargetID() == "CR")
        {
            deviceName = "ASTRA_K_LED_BLE";
            UUID = "6E400001-B5A3-F393-E0A9-E50E24DCCA9E";
            UUID_RX = "6E400002-B5A3-F393-E0A9-E50E24DCCA9E";
            UUID_TX = "6E400003-B5A3-F393-E0A9-E50E24DCCA9E";
        }
        else
        {
            Debug.LogError("Excpetion founded in DefineBLEDevice, targetID: " + targetManager.GetTargetID() + " don't match");
        }

    }

    void OnDestroy()
    {
        DisconectBLE();

        MyTrackableEventHandler.OnTrackingObj += ObjTracking;   
    }
}