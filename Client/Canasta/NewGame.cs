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
    public partial class NewGame : Form
    {
        string m_currentGame;
        bool m_creareJoc;

        public NewGame()
        {
            InitializeComponent();
            m_creareJoc = true;
            m_currentGame = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("217.122.151.181");
            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.189.140");
            System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3291);
            soc.Connect(remoteEP);

            byte[] byData = new byte[1 + 1 + textBox1.Text.Length + 1 + textBox2.Text.Length + 1 + 1 + 1 + 1];

            int curPos = 0;
            byData[curPos] = (byte)1;   //gameType

            curPos++;
            byData[curPos] = (byte)textBox1.Text.Length; // gameName length

            curPos++;
            for (int i = curPos; i < curPos + textBox1.Text.Length; i++)
                byData[i] = (byte)textBox1.Text[i - curPos]; // gameName

            curPos += textBox1.Text.Length;
            byData[curPos] = (byte)textBox2.Text.Length; // playerName length

            curPos++;
            for (int i = curPos; i < curPos + textBox2.Text.Length; i++)
                byData[i] = (byte)textBox2.Text[i - curPos]; // playerName

            curPos += textBox2.Text.Length;
            byData[curPos] = (byte)1;   // operationType

            curPos++;
            byData[curPos] = (byte)2;    // data length

            curPos++;
            byData[curPos] = (byte)numericUpDown1.Value;    // number of players

            curPos++;
            byData[curPos] = (byte)0;    // number of teams

            soc.Send(byData);
            byte[] buffer = new byte[1024];
            int iRx = soc.Receive(buffer);
            soc.Disconnect(true);
            if (buffer[0] == 0)
                toolStripStatusLabel1.Text = "Joc creat cu succes.";
            else
                toolStripStatusLabel1.Text = "Eroare! Jocul exista deja.";

            // Game created, continue with Join functionality
            m_currentGame = textBox1.Text;
            this.Text = "Canasta - Joc: " + m_currentGame;
//            timer1.Stop();
            timer1.Enabled = false;
            button1.Enabled = false;
            button1.Text = "Start joc";

            label3.Text = "Ordinea de joc:";
            listBox1.Items.Clear();
            m_creareJoc = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            numericUpDown1.Enabled = false;

            timer1.Enabled = true;
        }

        private void checkFields(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void askstatus(object sender, EventArgs e)
        {
            if (m_creareJoc)
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("217.122.151.181");
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.189.140");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3291);
                soc.Connect(remoteEP);
                byte[] byData = new byte[5] { (byte)1, (byte)0, (byte)0, (byte)5, (byte)0 }; // ask game list
                soc.Send(byData);

                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                soc.Disconnect(true);

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
            else
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

                soc.Send(byData);

                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                soc.Disconnect(true);

                if (buffer[0] != listBox1.Items.Count)
                {
//                    listBox1.Items.Clear();

                    curPos = 1;
                    for (int i = 0; i < buffer[0]; i++)
                    {
                        string name = "";
                        for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                            name += (char)buffer[j];
                        curPos += buffer[curPos] + 1;
                        ListBox tmpListBox = new ListBox();
                        tmpListBox.Items.Add(name);

                        if (!listBox1.Items.Contains(tmpListBox.Items[0]))
                        {
                            listBox2.Items.Add(name);
                            listBox1.Items.Add(name);
                        }
                    }
                }
            }
        }

        public void deactivateTimer()
        {
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            listBox3.Items.Add(listBox2.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            listBox4.Items.Add(listBox2.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem == null) return;
            listBox2.Items.Add(listBox3.SelectedItem);
            listBox3.Items.Remove(listBox3.SelectedItem);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem == null) return;
            listBox2.Items.Add(listBox4.SelectedItem);
            listBox4.Items.Remove(listBox4.SelectedItem);
        }

        private void button12_Click(object sender, EventArgs e)
        {
        }

    }
}
