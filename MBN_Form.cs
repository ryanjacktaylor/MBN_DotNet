using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;

using MbnApi;  //Reference COM - 'Definition: UCM Extension API for MBN Type Library'
using System.Reflection;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace MBN_DotNet
{
    public partial class MBN_Form : Form
    {
        static IMbnInterface[] radios;
        static uint scanRequestId;
        static string selectedInterfaceId;
        static MBN_PROVIDER[] lastScannedNetworks;

        //MBNAPI stuff
        private IMbnInterfaceManager m_MbnInterfaceManager;
        private IMbnConnectionManager m_MbnConnectionManager;
        private DeviceServicesEventsSink m_DeviceServicesEventsSink;
        private IMbnInterface m_MbnInterface;
        private IMbnConnection m_MbnConnection;
        private IMbnDeviceServicesContext m_MbnDeviceServicesContext;
        private IMbnDeviceServicesManager m_MbnDeviceServicesManager;
        private ConnectionEventsSink m_ConnectionEventsSink;
        private RegistrationEventsSink m_RegistrationEventsSink;
        private InterfaceEventsSink m_InterfaceEventsSink;

        //Connection Event Delegates
        private OnConnectCompleteHandler m_OnConnectCompleteEventDelegate;
        private OnDisconnectCompleteHandler m_OnDisconnectCompleteEventDelegate;

        //Device Service Events Delegates
        private OnOpenCommandSessionCompleteHandler m_OnOpenCommandSessionCompleteEventDelegate;
        private OnQueryCommandCompleteHandler m_OnQueryCommandCompleteEventDelegate;
        private OnSetCommandCompleteHandler m_OnSetCommandCompleteEventDelegate;
        private OnCloseCommandSessionCompleteHandler m_OnCloseCommandSessionEventDelegate;

        //Registration Events Delegates
        private OnRegisterModeAvailableHandler m_OnRegisterModeAvailableHandlerEventDelegate;
        private OnRegisterStateChangeHandler m_OnRegisterStateChangeHandlerEventDelegate;
        private OnPacketServiceStateChangeHandler m_OnPacketServiceStateChangeHandlerEventDelegate;
        private OnSetRegisterModeCompleteHandler m_OnSetRegisterModeCompleteHandlerEventDelegate;

        //Interface Event Delegates
        OnEmergencyModeChangeHandler OnEmergencyModeChangeHandlerEventDelegate;
        OnHomeProviderAvailableHandler OnHomeProviderAvailableHandlerEventDelegate;
        OnInterfaceCapabilityAvailableHandler OnInterfaceCapabilityAvailableHandlerEventDelegate;
        OnPreferredProvidersChangeHandler OnPreferredProvidersChangeHandlerEventDelegate;
        OnReadyStateChangeHandler OnReadyStateChangeHandlerEventDelegate;
        OnScanNetworkCompleteHandler OnScanNetworkCompleteHandlerEventDelegate;
        OnSetPreferredProvidersCompleteHandler OnSetPreferredProvidersCompleteHandlerEventDelegate;
        OnSubscriberInformationChangeHandler OnSubscriberInformationChangeHandlerEventDelegate;

        public MBN_Form()
        {
            InitializeComponent();
        }

        private void MBN_Form_Load(object sender, EventArgs e)
        {
            //SHow the network column headers
            lvNetwork.View = View.Details;

            //Initialize all the managers
            InitializeManagers();

            // Get first interface ID
            radios = (IMbnInterface[])m_MbnInterfaceManager.GetInterfaces();

            //Populate the combo box with the device
            cmbRadio.Items.Clear();
            foreach (IMbnInterface radio in radios)
            {
                MBN_INTERFACE_CAPS radioCap = radio.GetInterfaceCapability();
                cmbRadio.Items.Add(radioCap.manufacturer + " - " + radioCap.model);
            }

            cmbLocalNetwork.Items.Clear();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Wwanpp) || (nic.NetworkInterfaceType == NetworkInterfaceType.Wwanpp2))
                {
                    cmbLocalNetwork.Items.Add(nic.Description);
                }
            }

        }

        private void cmbRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedInterfaceId = radios[cmbRadio.SelectedIndex].InterfaceID;
            InitializeEventHandlers();
            uint age = 0;
            lvNetwork.Items.Clear();
            try
            {
                lastScannedNetworks = (MBN_PROVIDER[])m_MbnInterface.GetVisibleProviders(out age);
                PopulateNetworks(lastScannedNetworks);
                lblLastScan.Text = DateTime.Now.AddSeconds(-1 * age).ToShortTimeString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Networks not found.  Please scan to find networks.");
                lblLastScan.Text = "Unknown";
            }

            //Fill In the registration stuff
            IMbnRegistration registrationInterface = m_MbnInterface as IMbnRegistration;
            updateRegistration(registrationInterface);

            //Fill in the IMSI
            try
            {
                string IMSI = m_MbnInterfaceManager.GetInterface(selectedInterfaceId).GetSubscriberInformation().SubscriberID;
                lblIMSI.Text = IMSI;
            } catch (Exception ex)
            {
                lblIMSI.Text = "Unknown";
            }

            //Check if connected
            try
            {
                IMbnConnection connection = ((IMbnConnection[])m_MbnConnectionManager.GetConnections())[0];
                MBN_ACTIVATION_STATE activationState;
                string profileName;
                connection.GetConnectionState(out activationState, out profileName);
                if (activationState == MBN_ACTIVATION_STATE.MBN_ACTIVATION_STATE_ACTIVATED)
                {
                    btnPcConnect.Enabled = false;
                }
                else
                {
                    btnPcDisconnect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                btnPcDisconnect.Enabled = false;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnScan.Enabled = false;
            lvNetwork.Items.Clear();
            m_MbnInterface.ScanNetwork(out scanRequestId);
            lblLastScan.Text = "Scanning...";
            LogMessage("Started Scanning...");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IMbnRegistration registrationInterface = m_MbnInterfaceManager.GetInterface(selectedInterfaceId) as IMbnRegistration;

            ListViewItem item = lvNetwork.SelectedItems[0];
            uint requestId;
            registrationInterface.SetRegisterMode(MBN_REGISTER_MODE.MBN_REGISTER_MODE_MANUAL, item.SubItems[1].Text, UInt32.Parse(item.SubItems[2].Text), out requestId);


        }

        private void PopulateNetworks(MBN_PROVIDER[] networks)
        {
            foreach (MBN_PROVIDER network in networks)
            {
                string providerState = "Unknown";
                if (network.providerState > 8)
                {
                    providerState = Enum.GetName(typeof(MBN_PROVIDER_STATE), network.providerState - 8).Replace("MBN_PROVIDER_STATE_", "");
                }
                else
                {
                    providerState = Enum.GetName(typeof(MBN_PROVIDER_STATE), network.providerState).Replace("MBN_PROVIDER_STATE_", "");
                }
                ListViewItem lvItemArray = new ListViewItem(new[] {
                            network.providerName,
                            network.providerID,
                            network.dataClass.ToString(),
                            providerState});
                lvNetwork.Items.Add(lvItemArray);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //If there is a deregister function, I haven't found it.  So instead, we'll manually set it to a network that I don't think exists.
            IMbnRegistration registrationInterface = m_MbnInterfaceManager.GetInterface(selectedInterfaceId) as IMbnRegistration;
            uint requestId;
            registrationInterface.SetRegisterMode(MBN_REGISTER_MODE.MBN_REGISTER_MODE_MANUAL, "123456", 1, out requestId);
        }

        private void updateRegistration(IMbnRegistration registrationInterface)
        {
            try
            {
                lblRegMode.Text = Enum.GetName(typeof(MBN_REGISTER_MODE), registrationInterface.GetRegisterMode()).Replace("MBN_REGISTER_MODE_", "");
                lblRegState.Text = Enum.GetName(typeof(MBN_REGISTER_STATE), registrationInterface.GetRegisterState()).Replace("MBN_REGISTER_STATE_", "");
                lblRegName.Text = registrationInterface.GetProviderName();
                lblRegId.Text = registrationInterface.GetProviderID();

                //Update the list as well
                uint age;
                for (int i = 0; i < lastScannedNetworks.Length; i++)
                {
                    if (lastScannedNetworks[i].providerID.Equals(registrationInterface.GetProviderID()))
                    {
                        lastScannedNetworks[i].providerState = (uint)registrationInterface.GetRegisterState();
                    }
                }
                PopulateNetworks(lastScannedNetworks);

                //Check if manual or automatic
                chkRegMode.Checked = registrationInterface.GetRegisterMode().Equals(MBN_REGISTER_MODE.MBN_REGISTER_MODE_AUTOMATIC);
                grpScan.Enabled = !chkRegMode.Checked;
            }
            catch (Exception ex)
            {
                //whatever
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void connectToPc()
        {
            try
            {
                LogMessage("Connecting to PC...");
                string imsi = m_MbnInterfaceManager.GetInterface(selectedInterfaceId).GetSubscriberInformation().SubscriberID;

                IMbnConnection connection = ((IMbnConnection[])m_MbnConnectionManager.GetConnections())[0];
                string profileXml = String.Format(
@"<MBNProfile xmlns='http://www.microsoft.com/networking/WWAN/profile/v1'>
    <Name>tempProfile</Name>
    <IsDefault>false</IsDefault>
    <SubscriberID>{0}</SubscriberID>
    <Context>
        <AccessString>ID{1}</AccessString>
    </Context>
</MBNProfile>", imsi, txtAPN.Text);
                uint requestId;
                //connection
                connection.Connect(MBN_CONNECTION_MODE.MBN_CONNECTION_MODE_TMP_PROFILE, profileXml, out requestId);
            }
            catch (Exception ex)
            {
                LogMessage("Connect failed - " + ex.ToString());
                LogMessage("Connect failed - " + ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectToPc();
        }

        private void pingNetwork()
        {
            LogMessage("Pinging Google.com...");
            //Find the network adapter that corresponds to this adapter
            NetworkInterface wwanInterface = null;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Description.Equals(cmbLocalNetwork.Text))
                {
                    wwanInterface = nic;
                    break;
                }
            }

            if (wwanInterface != null)
            {
                foreach (UnicastIPAddressInformation ip in wwanInterface.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        string output = ip.Address.ToString();
                        var proc = new Process();
                        proc.StartInfo.FileName = @"ping";
                        proc.StartInfo.Arguments = string.Format(@"google.com -S {0}", output);
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.RedirectStandardOutput = true;
                        proc.Start();
                        string pingOut = proc.StandardOutput.ReadToEnd();

                        proc.WaitForExit();
                        var exitCode = proc.ExitCode;
                        proc.Close();

                        //Parse the output
                        if (pingOut.Contains("Received = 0"))
                        {
                            //Ping failed.  Send the email
                            LogMessage("Ping failed.");
                        }
                        else
                        {
                            //Log that all is good
                            LogMessage("Ping successful");
                        }
                    }
                }
            }
        }
        private void Ping_Click(object sender, EventArgs e)
        {
            pingNetwork();
        }

        private void disconnectFromPc()
        {
            try
            {
                LogMessage("Disconnecting from PC...");
                IMbnConnection connection = ((IMbnConnection[])m_MbnConnectionManager.GetConnections())[0];
                uint requestId;
                connection.Disconnect(out requestId);
            }
            catch (Exception ex)
            {
                LogMessage("Disconnect failed - " + ex.ToString());
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            disconnectFromPc();
        }

        private void LogMessage(string message)
        {
            string timeStamp = DateTime.Now.ToString("MM/dd h:mm:ss tt") + ":  ";
            this.Invoke((MethodInvoker)delegate ()
            {
                txtLog.Text += timeStamp + message + "\n";

                //Scroll to end
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            });

            //Also write to a log file
            DateTime dateTime = DateTime.Now.Date;
            string logFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            logFilePath += "\\LN940_" + dateTime.ToString("ddMMyyyy") + ".log";
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Dispose();
            }
            using (TextWriter tw = new StreamWriter(logFilePath, true))
            {
                tw.WriteLine(timeStamp + message);
            }
        }

        // Initialize the MBN interfaces
        public void InitializeManagers()
        {
            try
            {
                // Get MbnInterfaceManager
                if (m_MbnInterfaceManager == null)
                {
                    m_MbnInterfaceManager = (IMbnInterfaceManager)new MbnInterfaceManager();
                }

                // Get MbnConnectionManager
                if (m_MbnConnectionManager == null)
                {
                    m_MbnConnectionManager = (IMbnConnectionManager)new MbnConnectionManager();
                }

                // Get MbnDeviceServicesManager
                if (m_MbnDeviceServicesManager == null)
                {
                    m_MbnDeviceServicesManager = (IMbnDeviceServicesManager)new MbnDeviceServicesManager();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(ParseExceptionCode(e));
            }
        }

        private IConnectionPoint GetMbnConnectionEventsConnectionPoint()
        {
            IConnectionPointContainer connectionPointContainer = m_MbnConnectionManager as IConnectionPointContainer;

            Guid iid_IMbnConnectionEvents = typeof(IMbnConnectionEvents).GetTypeInfo().GUID;

            IConnectionPoint connectionPoint;
            connectionPointContainer.FindConnectionPoint(ref iid_IMbnConnectionEvents, out connectionPoint);

            return connectionPoint;
        }

        private IConnectionPoint GetRegisterEventsConnectionPoint()
        {
            IConnectionPointContainer connectionPointContainer = m_MbnInterfaceManager as IConnectionPointContainer;

            Guid iid_IMbnRegistrationEvents = typeof(IMbnRegistrationEvents).GetTypeInfo().GUID;

            IConnectionPoint connectionPoint;
            connectionPointContainer.FindConnectionPoint(ref iid_IMbnRegistrationEvents, out connectionPoint);

            return connectionPoint;
        }

        private IConnectionPoint GetInterfaceEventsConnectionPoint()
        {
            IConnectionPointContainer connectionPointContainer = m_MbnInterfaceManager as IConnectionPointContainer;

            Guid iid_IMbnInterfaceEvents = typeof(IMbnInterfaceEvents).GetTypeInfo().GUID;

            IConnectionPoint connectionPoint;
            connectionPointContainer.FindConnectionPoint(ref iid_IMbnInterfaceEvents, out connectionPoint);

            return connectionPoint;
        }

        private IConnectionPoint GetMbnDeviceServicesEventsConnectionPoint()
        {
            IConnectionPointContainer connectionPointContainer = m_MbnDeviceServicesManager as IConnectionPointContainer;

            Guid iid_IMbnDeviceServicesEvents = typeof(IMbnDeviceServicesEvents).GetTypeInfo().GUID;

            IConnectionPoint connectionPoint;
            connectionPointContainer.FindConnectionPoint(ref iid_IMbnDeviceServicesEvents, out connectionPoint);

            return connectionPoint;
        }

        private void InitializeEventHandlers()
        {
            try
            {
                // Get the interface
                m_MbnInterface = m_MbnInterfaceManager.GetInterface(selectedInterfaceId);
                m_MbnConnection = m_MbnInterface.GetConnection();
                m_MbnDeviceServicesContext = m_MbnDeviceServicesManager.GetDeviceServicesContext(m_MbnInterface.InterfaceID);

                // Register for IMbnConnectionEvents
                if (m_ConnectionEventsSink == null)
                {
                    m_OnConnectCompleteEventDelegate = new OnConnectCompleteHandler(ProcessOnConnectComplete);
                    m_OnDisconnectCompleteEventDelegate = new OnDisconnectCompleteHandler(ProcessOnDisconnectComplete);
                    m_ConnectionEventsSink = new ConnectionEventsSink(m_OnConnectCompleteEventDelegate,
                                                                      m_OnDisconnectCompleteEventDelegate,
                                                                      GetMbnConnectionEventsConnectionPoint());
                }

                // Register for IMbnDeviceServicesEvents
                if (m_DeviceServicesEventsSink == null)
                {
                    m_OnOpenCommandSessionCompleteEventDelegate = new OnOpenCommandSessionCompleteHandler(ProcessOnOpenCommandSessionComplete);
                    m_OnQueryCommandCompleteEventDelegate = new OnQueryCommandCompleteHandler(ProcessOnQueryCommandComplete);
                    m_OnSetCommandCompleteEventDelegate = new OnSetCommandCompleteHandler(ProcessOnSetCommandComplete);
                    m_OnCloseCommandSessionEventDelegate = new OnCloseCommandSessionCompleteHandler(ProcessOnCloseCommandSessionComplete);
                    m_DeviceServicesEventsSink = new DeviceServicesEventsSink(
                                                    m_OnOpenCommandSessionCompleteEventDelegate,
                                                    m_OnQueryCommandCompleteEventDelegate,
                                                    m_OnCloseCommandSessionEventDelegate,
                                                    m_OnSetCommandCompleteEventDelegate,
                                                    GetMbnDeviceServicesEventsConnectionPoint());
                }

                //Register for IMbnRegistrationEvents
                if (m_RegistrationEventsSink == null)
                {
                    m_OnRegisterModeAvailableHandlerEventDelegate = new OnRegisterModeAvailableHandler(ProcessOnRegisterModeAvailable);
                    m_OnRegisterStateChangeHandlerEventDelegate = new OnRegisterStateChangeHandler(ProcessOnRegisterStateChange);
                    m_OnPacketServiceStateChangeHandlerEventDelegate = new OnPacketServiceStateChangeHandler(ProcessOnPacketServiceStateChange);
                    m_OnSetRegisterModeCompleteHandlerEventDelegate = new OnSetRegisterModeCompleteHandler(ProcessOnSetRegisterModeComplete);
                    m_RegistrationEventsSink = new RegistrationEventsSink(
                                                    m_OnRegisterModeAvailableHandlerEventDelegate,
                                                    m_OnRegisterStateChangeHandlerEventDelegate,
                                                    m_OnPacketServiceStateChangeHandlerEventDelegate,
                                                    m_OnSetRegisterModeCompleteHandlerEventDelegate,
                                                    GetRegisterEventsConnectionPoint());
                }

                if (m_InterfaceEventsSink == null)
                {
                    OnEmergencyModeChangeHandlerEventDelegate = new OnEmergencyModeChangeHandler(ProcessOnEmergencyModeChange);
                    OnHomeProviderAvailableHandlerEventDelegate = new OnHomeProviderAvailableHandler(ProcessOnHomeProviderAvailable);
                    OnInterfaceCapabilityAvailableHandlerEventDelegate = new OnInterfaceCapabilityAvailableHandler(ProcessOnInterfaceCapabilityAvailable);
                    OnPreferredProvidersChangeHandlerEventDelegate = new OnPreferredProvidersChangeHandler(ProcessOnPreferredProvidersChange);
                    OnReadyStateChangeHandlerEventDelegate = new OnReadyStateChangeHandler(ProcessOnReadyStateChange);
                    OnScanNetworkCompleteHandlerEventDelegate = new OnScanNetworkCompleteHandler(ProcessOnScanNetworkComplete);
                    OnSetPreferredProvidersCompleteHandlerEventDelegate = new OnSetPreferredProvidersCompleteHandler(ProcessOnSetPreferredProvidersComplete);
                    OnSubscriberInformationChangeHandlerEventDelegate = new OnSubscriberInformationChangeHandler(ProcessOnSubscriberInformationChange);
                    m_InterfaceEventsSink = new InterfaceEventsSink(
                                                OnEmergencyModeChangeHandlerEventDelegate,
                                                OnHomeProviderAvailableHandlerEventDelegate,
                                                OnInterfaceCapabilityAvailableHandlerEventDelegate,
                                                OnPreferredProvidersChangeHandlerEventDelegate,
                                                OnReadyStateChangeHandlerEventDelegate,
                                                OnScanNetworkCompleteHandlerEventDelegate,
                                                OnSetPreferredProvidersCompleteHandlerEventDelegate,
                                                OnSubscriberInformationChangeHandlerEventDelegate,
                                                GetInterfaceEventsConnectionPoint());

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ParseExceptionCode(e));
            }
        }

        private void ProcessOnEmergencyModeChange(IMbnInterface newInterface) { LogMessage("ProcessOnEmergencyModeChange"); }

        private void ProcessOnHomeProviderAvailable(IMbnInterface newInterface) { LogMessage("ProcessOnHomeProviderAvailable");  }

        private void ProcessOnInterfaceCapabilityAvailable(IMbnInterface newInterface) { LogMessage("ProcessOnInterfaceCapabilityAvailable"); }

        private void ProcessOnPreferredProvidersChange(IMbnInterface newInterface) { LogMessage("ProcessOnPreferredProvidersChange"); }

        private void ProcessOnReadyStateChange(IMbnInterface newInterface) { LogMessage("ProcessOnReadyStateChange"); }

        private void ProcessOnScanNetworkComplete(IMbnInterface newInterface, uint requestID, int status) {
            LogMessage("Scanning Complete");
            uint age;
            try
            {
                MBN_PROVIDER[] lastScannedNetworks = (MBN_PROVIDER[])newInterface.GetVisibleProviders(out age);
                this.Invoke((MethodInvoker)delegate ()
                {
                    PopulateNetworks(lastScannedNetworks);
                    lblLastScan.Text = DateTime.Now.AddSeconds(-1 * age).ToShortTimeString();
                    btnScan.Enabled = true;
                });
            } catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    LogMessage("No Networks found");
                    lblLastScan.Text = DateTime.Now.ToShortTimeString();
                });
            }
            
        }

        private void ProcessOnSetPreferredProvidersComplete(IMbnInterface newInterface, uint requestID, int status) { LogMessage("ProcessOnSetPreferredProvidersComplete"); }

        private void ProcessOnSubscriberInformationChange(IMbnInterface newInterface) { LogMessage("ProcessOnSubscriberInformationChange"); }

        private void ProcessOnSetRegisterModeComplete(IMbnRegistration newInterface, uint requestID, int status)
        {
            MBN_REGISTER_STATE state = newInterface.GetRegisterState();
            LogMessage("ProcessOnSetRegisterModeComplete.  State: " + state.ToString());
            if (state == MBN_REGISTER_STATE.MBN_REGISTER_STATE_ROAMING || 
                state == MBN_REGISTER_STATE.MBN_REGISTER_STATE_HOME || 
                state == MBN_REGISTER_STATE.MBN_REGISTER_STATE_PARTNER)
            {
                LogMessage("Registered to " + newInterface.GetProviderName());
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                updateRegistration(newInterface);
            });
        }

        private void ProcessOnPacketServiceStateChange(IMbnRegistration newInterface)
        {
            LogMessage("ProcessOnPacketServiceStateChange");
        }

        private void ProcessOnRegisterStateChange(IMbnRegistration newInterface)
        {
            MBN_REGISTER_STATE state = newInterface.GetRegisterState();
            LogMessage("ProcessOnRegisterStateChange.  State: " + state.ToString() + "  Name: " + newInterface.GetProviderName());
            this.Invoke((MethodInvoker)delegate ()
            {
                updateRegistration(newInterface);
            });
        }

        private void ProcessOnRegisterModeAvailable(IMbnRegistration newInterface)
        {
            LogMessage("ProcessOnRegisterModeAvailable");
        }

        // Parse the common exception codes
        private string ParseExceptionCode(Exception error)
        {
            string returnStr = "";

            switch (error.HResult)
            {
                case -2147023728: // 0x80070490: ERROR_NOT_FOUND
                    returnStr += "\"The given mobile broadband interface or subscriber ID is not present\", error code: 0x80070490";
                    returnStr += "\nPlease plug in required mobile broadband device before continuing with sample.";
                    break;
                case -2147023834: // 0x80070426: ERROR_SERVICE_NOT_ACTIVE
                    returnStr += "\"The service has not been started\", error code: 0x80070426";
                    returnStr += "\nPlease start the wwansvc before continuing with sample.";
                    break;
                case -2147024875: // 0x80070015: ERROR_NOT_READY
                    returnStr += "\"The device is not ready\", error code: 0x80070015";
                    break;
                case -2147024841: // 0x80070037: ERROR_DEV_NOT_EXIST
                    returnStr += " \"The specified mobile broadband device is no longer available\", error code: 0x80070037";
                    break;
                case -2147023690: // 0x800704b6: ERROR_BAD_PROFILE
                    returnStr += "\"The network connection profile is corrupted\", error code: 0x800704b6";
                    break;
                case -2147221164: // 0x80040154: REGDB_E_CLASSNOTREG
                    returnStr += "\"Class not registered, this might be because of WwanSvc is not supported on particular SKU\", error code: 0x80040154";
                    break;
                default:
                    returnStr += "Unexpected exception occured: " + error.ToString();
                    break;
            }
            return returnStr;
        }

        private void ProcessOnConnectComplete(IMbnConnection connection, uint requestId, int status)
        {
            MBN_ACTIVATION_STATE activationState;
            string profileName;
            connection.GetConnectionState(out activationState, out profileName);
            LogMessage("ProcessOnConnectComplete - " + profileName + " - " + activationState);
            if (activationState == MBN_ACTIVATION_STATE.MBN_ACTIVATION_STATE_ACTIVATED)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    btnPcConnect.Enabled = false;
                    btnPcDisconnect.Enabled = true;
                });
            }
        }

        private void ProcessOnDisconnectComplete(IMbnConnection connection, uint requestId, int status)
        {
            LogMessage("Disconnected from PC");
            this.Invoke((MethodInvoker)delegate ()
            {
                btnPcConnect.Enabled = true;
                btnPcDisconnect.Enabled = false;
            });
        }

        private void ProcessOnOpenCommandSessionComplete(IMbnDeviceService deviceService, int status, uint requestId)
        {
            LogMessage("ProcessOnOpenCommandSessionComplete");
        }

        private void ProcessOnQueryCommandComplete(IMbnDeviceService deviceService, uint responseId, byte[] deviceServiceData, int status, uint requestId)
        {
            LogMessage("ProcessOnQueryCommandComplete");
        }

        private void ProcessOnSetCommandComplete(IMbnDeviceService deviceService, uint responseID, byte[] deviceServiceData, int status, uint requestID)
        {
            LogMessage("ProcessOnSetCommandComplete");
        }

        private void ProcessOnCloseCommandSessionComplete(IMbnDeviceService deviceService, int status, uint requestId)
        {
            LogMessage("ProcessOnCloseCommandSessionComplete");
        }

        private void chkRegMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegMode.Checked)
            {
                IMbnRegistration registrationInterface = m_MbnInterfaceManager.GetInterface(selectedInterfaceId) as IMbnRegistration;
                uint requestId;
                registrationInterface.SetRegisterMode(MBN_REGISTER_MODE.MBN_REGISTER_MODE_AUTOMATIC, null, 60, out requestId);

                //Disable scan
                grpScan.Enabled = false;
            } else
            {
                //Enable scan
                grpScan.Enabled = true;
            }
        }
    }
}
