using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ROVduino_udpTOOL
{
    public partial class Form1 : Form
    {
        errorHANDLER logging = new errorHANDLER();
        public class UdpState
        {
            public IPEndPoint e;
            public UdpClient u;
        }
        public void receiveUDP(IAsyncResult ar)
        {
            //UDP State Setup
            UdpState UpdState = new UdpState();
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            //Receive Buffer Array
            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);
            //char[] delimiterChar = { ' ' };
            //string[] strValues = receiveString.Split(delimiterChar);

            Invoke(new MethodInvoker(delegate { logging.displayERROR(false, false, true, receiveString, Color.Red); dataSTREAM.SelectionStart = dataSTREAM.Text.Length; dataSTREAM.ScrollToCaret(); }));
            //Invoke(new MethodInvoker(delegate { dataSTREAM.AppendText(receiveString); dataSTREAM.SelectionStart = dataSTREAM.Text.Length; dataSTREAM.ScrollToCaret(); }));
            UpdState.u = u;
            UpdState.e = e;
            u.BeginReceive(new AsyncCallback(receiveUDP), UpdState);
        }
        public Form1()
        {
            InitializeComponent();
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, Properties.Settings.Default.portadr);
            UdpClient UdpClient = new UdpClient(RemoteIpEndPoint);
            UdpState UdpState = new UdpState();
            UdpState.e = RemoteIpEndPoint;
            UdpState.u = UdpClient;
            UdpClient.BeginReceive(new AsyncCallback(receiveUDP), UdpState);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            update.Start();
            logging.initializeLOGGING();
            updateintervaltxt.Text = Properties.Settings.Default.looptime.ToString();
            broadcastSET.Checked = Properties.Settings.Default.BROADCAST;
            p2pSET.Checked = Properties.Settings.Default.P2P;
            portTXT.Text = Properties.Settings.Default.portadr.ToString();
            remoteIPtxt.Text = Properties.Settings.Default.ipadr.ToString();
            autosendENABLED.Checked = Properties.Settings.Default.autosend;
        }
        #region udp sending
        public void sendBROADCAST(int port, string data)
        {
            //Create Remote Endpoint
            UdpClient client = new UdpClient();
            //Generate Packet
            byte[] packet = Encoding.ASCII.GetBytes(data);
            //Send Packet
            IPEndPoint groupEp = new IPEndPoint(IPAddress.Broadcast, port);
            client.Connect(groupEp);
            try
            {
                client.Send(packet, packet.Length);
            }
            catch (Exception)
            {

            }
            //Close the Connection
            client.Close();

        }
        public void sendP2P(string ip, int port, string data)
        {
            //Create Remote Endpoint
            UdpClient client = new UdpClient();
            //Generate Packet
            byte[] packet = Encoding.ASCII.GetBytes(data);
            //Send Packet
            try
            {
                client.Send(packet, packet.Length, ip, port);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ROVduino Error");
            }
            //Close the Connection
            client.Close();
        }
        #endregion
        #region TIMER
        private void update_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.autosend)
            {
                if (Properties.Settings.Default.P2P)
                {
                    sendP2P(Properties.Settings.Default.ipadr, Properties.Settings.Default.portadr, newDATA.Text);
                }
                if (Properties.Settings.Default.BROADCAST)
                {
                    sendBROADCAST(Properties.Settings.Default.portadr, newDATA.Text);
                }
            }

            dataSTREAM.Text = logging.getERRORS();
            adrtext.Text = getLOCALIP();
            label4.Text = getLOCALHOSTNAME();
        }
        #endregion
        #region Get Local Data
        public string getLOCALIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string localIP = "";
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        public string getLOCALHOSTNAME()
        {
            String HostName = Dns.GetHostName();
            return HostName;
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.looptime = Convert.ToInt16(updateintervaltxt.Text);
            Properties.Settings.Default.BROADCAST = broadcastSET.Checked;
            Properties.Settings.Default.P2P = p2pSET.Checked;
            Properties.Settings.Default.portadr = Convert.ToInt16(portTXT.Text);
            Properties.Settings.Default.ipadr = remoteIPtxt.Text;
            Properties.Settings.Default.autosend = autosendENABLED.Checked;
            tabControl1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            if (Properties.Settings.Default.autosend)
            {
                if (Properties.Settings.Default.P2P)
                {
                    sendP2P(Properties.Settings.Default.ipadr, Properties.Settings.Default.portadr, newDATA.Text);
                }
                if (Properties.Settings.Default.BROADCAST)
                {
                    sendBROADCAST(Properties.Settings.Default.portadr, newDATA.Text);
                }
            }

            dataSTREAM.Text = logging.getERRORS();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            logging.openLOGFILE();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            logging.saveLOGFILE();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            logging.saveLOGFILEas();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            logging.clearLOG();
        }
    }
}
