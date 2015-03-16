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
        string m_playerName = "";
        string m_server;
        int m_interval;
        bool m_joined = false;

        public JoinGame(string server, int interval)
        {
            InitializeComponent();
            m_server = server;
            m_interval = interval;
            timer1.Interval = m_interval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_gameName = listBox1.SelectedItem.ToString();
            m_playerName = textBox2.Text;
            Request request = new Request(m_server, m_gameName, m_playerName, 3, "");

            byte[] buffer = new byte[1024];
            buffer = request.send();

            if (buffer[0] == 0)
            {
                toolStripStatusLabel1.Text = "Alaturare reusita. Asteapta sa inceapa jocul...";
                m_joined = true;
            }
            else
                toolStripStatusLabel1.Text = "Eroare!";

            listBox1.Enabled = false;
            listBox2.Enabled = false;
        }

        private void activateJoin(object sender, EventArgs e)
        {
            if (m_joined == false)
                button1.Enabled = (listBox1.SelectedItem != null && textBox2.Text != "");
        }

        private void askStatus(object sender, EventArgs e)
        {
            Request request = new Request(m_server, m_gameName, m_playerName, 4, "");

            byte[] buffer = new byte[1024];
            buffer = request.send();

            if (m_joined == false)
            {
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
            else
            {
                if (buffer[0] != listBox1.Items.Count)
                {
                    listBox1.Items.Clear();

                    int curPos = 1;
                    for (int i = 0; i < buffer[0]; i++)
                    {
                        string name = "";
                        for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                            name += (char)buffer[j];
                        curPos += buffer[curPos] + 2;
                        if (!listBox1.Items.Contains(name))
                            listBox2.Items.Add(name);
                        listBox1.Items.Add(name);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }

    }
}
