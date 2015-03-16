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
        const int MAX_TEAMS = 4;

        string m_gameName;
        string m_playerName;
        bool m_creareJoc;
        string m_server;
        int m_interval;
        decimal m_numberOfPlayers;

//        List<Player> m_players;
        ListBox[] m_teams;
        Button[] m_addTeam;
        Button[] m_removeTeam;
        Button[] m_upTeam;
        Button[] m_downTeam;

        public NewGame(string server, int interval)
        {
            InitializeComponent();
            m_creareJoc = true;
            m_gameName = "";
            m_playerName = "";
            m_server = server;
            m_numberOfPlayers = numericUpDown1.Value;
            populateTeamOptions(null, null);
            m_interval = interval;
            timer1.Interval = m_interval;
            
            m_teams = new ListBox[MAX_TEAMS];
            m_addTeam = new Button[MAX_TEAMS];
            m_removeTeam = new Button[MAX_TEAMS];
            m_upTeam = new Button[MAX_TEAMS];
            m_downTeam = new Button[MAX_TEAMS];

            for (int i = 0; i < MAX_TEAMS; i++)
            {
                m_teams[i] = new ListBox();
                m_teams[i].FormattingEnabled = true;
                m_teams[i].HorizontalScrollbar = true;
                m_teams[i].ItemHeight = 16;
                m_teams[i].Location = new Point(750, 45 + i * 106);
                m_teams[i].Size = new Size(260, 100);
                Controls.Add(m_teams[i]);

                m_addTeam[i] = new Button();
                m_addTeam[i].Location = new Point(669, 69 + i * 106);
                m_addTeam[i].Size = new Size(75, 23);
                m_addTeam[i].Text = ">>";
                m_addTeam[i].UseVisualStyleBackColor = true;
                m_addTeam[i].Click += new System.EventHandler(buttonAddTeamClick);
                Controls.Add(m_addTeam[i]);

                m_removeTeam[i] = new Button();
                m_removeTeam[i].Location = new Point(669, 97 + i * 106);
                m_removeTeam[i].Size = new Size(75, 23);
                m_removeTeam[i].Text = "<<";
                m_removeTeam[i].UseVisualStyleBackColor = true;
                m_removeTeam[i].Click += new System.EventHandler(buttonRemoveTeamClick);
                Controls.Add(m_removeTeam[i]);

                m_upTeam[i] = new Button();
                m_upTeam[i].Location = new Point(1016, 53 + i * 106);
                m_upTeam[i].Size = new Size(40, 39);
                m_upTeam[i].Text = "˄";
                m_upTeam[i].UseVisualStyleBackColor = true;
                m_upTeam[i].Click += new System.EventHandler(buttonUpTeamClick);
                Controls.Add(m_upTeam[i]);

                m_downTeam[i] = new Button();
                m_downTeam[i].Location = new Point(1016, 97 + i * 106);
                m_downTeam[i].Size = new Size(40, 39);
                m_downTeam[i].Text = "˅";
                m_downTeam[i].UseVisualStyleBackColor = true;
                m_downTeam[i].Click += new System.EventHandler(buttonDownTeamClick);
                Controls.Add(m_downTeam[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_gameName = textBox1.Text;
            m_playerName = textBox2.Text;

            string data = "";
            data += (char)numericUpDown1.Value;
            data += (char)0;

            Request request = new Request(m_server, m_gameName, m_playerName, 1, data);

            byte[] buffer = new byte[1024];
            buffer = request.send();
            if (buffer[0] == 0)
                toolStripStatusLabel1.Text = "Joc creat cu succes.";
            else
                toolStripStatusLabel1.Text = "Eroare! Jocul exista deja.";

            // Game created, continue with Join functionality
            m_gameName = textBox1.Text;
            this.Text = "Canasta - Joc: " + m_gameName;
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

        private void askStatus(object sender, EventArgs e)
        {
            Request request = new Request(m_server, m_gameName, m_playerName, 4, "");

            byte[] buffer = new byte[1024];
            buffer = request.send();

            if (m_creareJoc)
            {
                // populate list of games
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
                // populate list of players
                if (buffer[0] != listBox1.Items.Count)
                {
                    int curPos = 1;
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

        private void buttonAddTeamClick(object sender, EventArgs e)
        {
            int team = -1;
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_addTeam[i] == (sender as Button))
                {
                    team = i;
                    // do stuff
                    return;
                }
        }

        private void buttonRemoveTeamClick(object sender, EventArgs e)
        {
            int team = -1;
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_removeTeam[i] == (sender as Button))
                {
                    team = i;
                    // do stuff
                    // check if team not full
                    return;
                }
        }

        private void buttonUpTeamClick(object sender, EventArgs e)
        {
            int team = -1;
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_upTeam[i] == (sender as Button))
                {
                    team = i;
                    // do stuff
                    return;
                }
        }

        private void buttonDownTeamClick(object sender, EventArgs e)
        {
            int team = -1;
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_downTeam[i] == (sender as Button))
                {
                    team = i;
                    // do stuff
                    return;
                }
        }

        private void populateTeamOptions(object sender, EventArgs e)
        {
            m_numberOfPlayers = numericUpDown1.Value;
            comboBox1.Items.Clear();
            comboBox1.Items.Add(0);
            for (int i = 2; i < m_numberOfPlayers; i++)
                if (m_numberOfPlayers % i == 0)
                    comboBox1.Items.Add(i);
        }
    }
}
