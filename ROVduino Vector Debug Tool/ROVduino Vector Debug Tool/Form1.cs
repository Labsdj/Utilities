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

namespace ROVduino_Vector_Debug_Tool
{
    public partial class Form1 : Form
    {
        public class UdpState
        {
            public IPEndPoint e;
            public UdpClient u;
        }
        public Form1()
        {
            InitializeComponent();
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 514);
            UdpClient UdpClient = new UdpClient(RemoteIpEndPoint);
            UdpState UdpState = new UdpState();
            UdpState.e = RemoteIpEndPoint;
            UdpState.u = UdpClient;
            UdpClient.BeginReceive(new AsyncCallback(receiveUDP), UdpState);
        }
        #region UDP recieve
        public void receiveUDP(IAsyncResult ar)
        {
            UdpState UpdState = new UdpState();
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);
            char[] delimiterChar = { ' ' };
            string[] strValues = receiveString.Split(delimiterChar);
            updateGUIELEMENT(flc, strValues[0]);
            updateGUIELEMENT(frc, strValues[1]);
            updateGUIELEMENT(brc, strValues[2]);
            updateGUIELEMENT(blc, strValues[3]);
            updateGUIELEMENT(vlc, strValues[4]);
            updateGUIELEMENT(vrc, strValues[5]);

            Console.WriteLine("Received: {0}", receiveString);
            UpdState.u = u;
            UpdState.e = e;
            u.BeginReceive(new AsyncCallback(receiveUDP), UpdState);
        }
        #endregion
        #region GUI Stuff
        public void updateGUIELEMENT(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new MethodInvoker(delegate { ctrl.Text = text; }));
            }
        }
        #endregion
        #region UDP Send
        static void sendUDP(string ip, int port, string data)
        {
            UdpClient client = new UdpClient();
            byte[] packet = Encoding.ASCII.GetBytes(data);
            try
            {
                client.Send(packet, packet.Length, ip, port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            client.Close();
        }
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            string val = allnew.Text;
            string toSEND = val + " " + val + " " + val + " " + val+ " " + val + " " + val;
            sendUDP(ipadr.Text, Convert.ToInt32(portadr.Text), toSEND);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void updateMOTORSbutton_Click(object sender, EventArgs e)
        {
            string fl = fln.Text;
            string fr = frn.Text;
            string br = brn.Text;
            string bl = bln.Text;
            string vl = vln.Text;
            string vr = vrn.Text;
            string toSEND = fl + " " + fr + " " + br + " " + bl + " " + vl + " " + vr;
            sendUDP(ipadr.Text, Convert.ToInt32(portadr.Text), toSEND);
        }
    }
}
