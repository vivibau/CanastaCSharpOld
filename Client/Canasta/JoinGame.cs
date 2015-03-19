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
        const int MAX_TEAMS = 4;

        string m_gameName = "";
        string m_playerName = "";
        List<Player> m_players;
        ListBox[] m_teams;
        string m_server;
        int m_interval;
        bool m_joined = false;

        public JoinGame(string server, int interval)
        {
            InitializeComponent();
            m_server = server;
            m_interval = interval;
            timer1.Interval = m_interval;
            listBox2.Enabled = false;
            m_players = new List<Player>();
            m_teams = new ListBox[MAX_TEAMS];

            for (int i = 0; i < MAX_TEAMS; i++)
            {
                m_teams[i] = new ListBox();
                m_teams[i].FormattingEnabled = true;
                m_teams[i].HorizontalScrollbar = true;
                m_teams[i].ItemHeight = 16;
                m_teams[i].Location = new Point(633, 36 + i * 106);
                m_teams[i].Size = new Size(260, 100);
                Controls.Add(m_teams[i]);
                m_teams[i].Enabled = false;
            }

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
                m_joined = true;
            }
            else
                MessageBox.Show("Eroare! Jucatorul cu acest nume exista deja sau jocul e deja complet.");

            listBox1.Visible = false;
            label3.Text = "Mesaje:";
            textBox3.Visible = true;
            textBox4.Visible = true;
            button3.Visible = true;
            button1.Enabled = false;
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
                int curPos = 1;
                m_players.Clear();
                listBox2.Items.Clear();
                for (int i = 0; i < MAX_TEAMS; i++)
                    m_teams[i].Items.Clear();

                bool sss = false;
                for (int i = 0; i < buffer[0]; i++)
                {
                    string name = "";
                    for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                        name += (char)buffer[j];
                    curPos += buffer[curPos] + 3;
                    m_players.Add(new Player(name, (int)buffer[curPos - 2], (int)buffer[curPos - 1]));
                    if ((int)buffer[curPos - 2] < 100) sss = true;
/*
                    if ((int)buffer[curPos - 2] == 100) // id of the team when the player does not have a team
                        listBox2.Items.Add(name);
                    else
                        m_teams[(int)buffer[curPos - 2]].Items.Add(name);
 */
                }

                if (sss)
                {
                    int x = 0;
                }
                m_players.Sort();
                for (int i = 0; i < m_players.Count; i++)
                    if (m_players[i].TeamId == 100) // id of the team when the player does not have a team
                        listBox2.Items.Add(m_players[i].Name);
                    else
                        m_teams[m_players[i].TeamId].Items.Add(m_players[i].Name);

                // updates
                curPos++;
                for (int i = 0; i < buffer[curPos - 1]; i++)
                {
                    string name = "";
                    for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                        name += (char)buffer[j];
                    curPos += buffer[curPos] + 1;
                    if (buffer[curPos] == 9)
                    {
                        curPos++;
                        string message = "";
                        for (int k = curPos + 1; k < curPos + 1 + buffer[curPos]; k++)
                            message += (char)buffer[k];
                        curPos += buffer[curPos] + 1;
                        textBox3.AppendText("[" + name + "]: ");
                        textBox3.AppendText(message);
                        textBox3.AppendText("\n");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (m_joined)
            {
                Request request = new Request(m_server, m_gameName, m_playerName, 11, "");
                byte[] buffer = new byte[1024];
                buffer = request.send();
            }

            Close();
        }

        private void textBox4_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox4.Text != "")
            {
                Request request = new Request(m_server, m_gameName, m_playerName, 9, textBox4.Text);
                byte[] buffer = new byte[1024];
                buffer = request.send();
                /*
                                textBox3.AppendText("[" + m_playerName + "]: ");
                                textBox3.AppendText(textBox4.Text);
                                textBox3.AppendText("\n");*/
                textBox4.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                Request request = new Request(m_server, m_gameName, m_playerName, 9, textBox4.Text);
                byte[] buffer = new byte[1024];
                buffer = request.send();
                /*
                                textBox3.AppendText("[" + m_playerName + "]: ");
                                textBox3.AppendText(textBox4.Text);
                                textBox3.AppendText("\n");*/
                textBox4.Text = "";
            }
        }

    }
}
