using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canasta
{
    public partial class JoinGame : Form
    {
        string m_gameName = "";
        string m_server;
        int m_interval;

        public JoinGame(string server, int interval)
        {
            InitializeComponent();
            m_server = server;
            m_interval = interval;
            timer1.Interval = m_interval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] addressList = Dns.GetHostAddresses(m_server);
            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(addressList[0].ToString());
            System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3291);
            soc.Connect(remoteEP);

            byte[] byData = new byte[1 + 1 + m_gameName.Length + 1 + textBox2.Text.Length + 1 + 1];

            int curPos = 0;
            byData[curPos] = (byte)1;   //gameType

            curPos++;
            byData[curPos] = (byte)m_gameName.Length; // gameName length

            curPos++;
            for (int i = curPos; i < curPos + m_gameName.Length; i++)
                byData[i] = (byte)m_gameName[i - curPos]; // gameName

            curPos += m_gameName.Length;
            byData[curPos] = (byte)textBox2.Text.Length; // playerName length

            curPos++;
            for (int i = curPos; i < curPos + textBox2.Text.Length; i++)
                byData[i] = (byte)textBox2.Text[i - curPos]; // playerName

            curPos += textBox2.Text.Length;
            byData[curPos] = (byte)8;   // operationType

            curPos++;
            byData[curPos] = (byte)0;    // data length

            soc.Send(byData);
            byte[] buffer = new byte[1024];
            int iRx = soc.Receive(buffer);
            soc.Disconnect(true);
            if (buffer[0] == 0)
                toolStripStatusLabel1.Text = "Alaturare reusita. Asteapta sa inceapa jocul...";
            else
                toolStripStatusLabel1.Text = "Eroare!";
        }

        private void activateJoin(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                m_gameName = listBox1.SelectedItem.ToString();
            if (textBox2.Text == "" || m_gameName == "") button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void askstatus(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1.Text != "Alaturare reusita. Asteapta sa inceapa jocul...")
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress[] addressList = Dns.GetHostAddresses(m_server);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(addressList[0].ToString());
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3291);
                soc.Connect(remoteEP);
                byte[] byData = new byte[5] { (byte)1, (byte)0, (byte)0, (byte)5, (byte)0 }; // ask game list
                soc.Send(byData);

                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                soc.Disconnect(true);

                if (buffer[0] != listBox1.Items.Count)
                {
                    listBox1.Items.Clear();

                    int curPos = 1;
                    for (int i = 0; i < buffer[0]; i++)
                    {
                        int size = buffer[curPos];
                        string name = "";
                        for (int j = curPos + 1; j < curPos + 1 + size; j++)
                        {
                            name += (char)buffer[j];
                        }
                        curPos += size + 1;
                        listBox1.Items.Add(name);
                    }
                }
            }
/*            else
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.189.140");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3291);
                soc.Connect(remoteEP);


                byte[] byData = new byte[1 + 1 + textBox1.Text.Length + 1 + textBox2.Text.Length + 1 + 1];

                int curPos = 0;
                byData[curPos] = (byte)1;   //gameType

                curPos++;
                byData[curPos] = (byte)textBox1.Text.Length; // gameName length

                curPos++;
                for (int i = curPos; i < curPos + textBox1.Text.Length; i++)
                    byData[i] = (byte)textBox1.Text[i - curPos]; // gameName

                curPos += textBox1.Text.Length;
                byData[curPos] = (byte)0; // playerName length

                curPos++;
                byData[curPos] = (byte)6;   // operationType

                curPos++;
                byData[curPos] = (byte)0;    // data length


                //                byte[] byData = new byte[5] { (byte)1, (byte)0, (byte)0, (byte)5, (byte)0 };
                soc.Send(byData);

                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                soc.Disconnect(true);

                if (buffer[0] != listBox1.Items.Count)
                {
                    listBox1.Items.Clear();

                    curPos = 1;
                    for (int i = 0; i < buffer[0]; i++)
                    {
                        string name = "";
                        for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                            name += (char)buffer[j];
                        curPos += buffer[curPos] + 1;
                        if (!listBox1.Items.Contains(name))
                            listBox2.Items.Add(name);
                        listBox1.Items.Add(name);
                    }
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }

    }
}
