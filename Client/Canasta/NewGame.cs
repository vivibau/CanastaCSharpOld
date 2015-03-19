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
        int m_numberOfPlayers;
        int m_numberOfTeams;

        List<Player> m_players;
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
            m_interval = interval;
            timer1.Interval = m_interval;
            m_players = new List<Player>();
            
            m_teams = new ListBox[MAX_TEAMS];
            m_addTeam = new Button[MAX_TEAMS];
            m_removeTeam = new Button[MAX_TEAMS];
            m_upTeam = new Button[MAX_TEAMS];
            m_downTeam = new Button[MAX_TEAMS];

            this.Size = new Size(720, 530);

            for (int i = 0; i < MAX_TEAMS; i++)
            {
                m_teams[i] = new ListBox();
                m_teams[i].FormattingEnabled = true;
                m_teams[i].HorizontalScrollbar = true;
                m_teams[i].ItemHeight = 16;
                m_teams[i].Location = new Point(750, 45 + i * 106);
                m_teams[i].Size = new Size(260, 100);
                Controls.Add(m_teams[i]);
                m_teams[i].Visible = false;

                m_addTeam[i] = new Button();
                m_addTeam[i].Location = new Point(669, 69 + i * 106);
                m_addTeam[i].Size = new Size(75, 23);
                m_addTeam[i].Text = ">>";
                m_addTeam[i].UseVisualStyleBackColor = true;
                m_addTeam[i].Click += new System.EventHandler(buttonAddTeamClick);
                Controls.Add(m_addTeam[i]);
                m_addTeam[i].Visible = false;

                m_removeTeam[i] = new Button();
                m_removeTeam[i].Location = new Point(669, 97 + i * 106);
                m_removeTeam[i].Size = new Size(75, 23);
                m_removeTeam[i].Text = "<<";
                m_removeTeam[i].UseVisualStyleBackColor = true;
                m_removeTeam[i].Click += new System.EventHandler(buttonRemoveTeamClick);
                Controls.Add(m_removeTeam[i]);
                m_removeTeam[i].Visible = false;

                m_upTeam[i] = new Button();
                m_upTeam[i].Location = new Point(1016, 53 + i * 106);
                m_upTeam[i].Size = new Size(40, 39);
                m_upTeam[i].Text = "˄";
                m_upTeam[i].UseVisualStyleBackColor = true;
                m_upTeam[i].Click += new System.EventHandler(buttonUpTeamClick);
                Controls.Add(m_upTeam[i]);
                m_upTeam[i].Visible = false;

                m_downTeam[i] = new Button();
                m_downTeam[i].Location = new Point(1016, 97 + i * 106);
                m_downTeam[i].Size = new Size(40, 39);
                m_downTeam[i].Text = "˅";
                m_downTeam[i].UseVisualStyleBackColor = true;
                m_downTeam[i].Click += new System.EventHandler(buttonDownTeamClick);
                Controls.Add(m_downTeam[i]);
                m_downTeam[i].Visible = false;
            }
            comboBox2.SelectedIndex = 2;
            populateTeamOptions(null, null);
            comboBox1.SelectedIndex = 1;
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
            data += (char)Convert.ToInt16(comboBox2.SelectedItem.ToString());
            data += (char)0;

            Request request = new Request(m_server, m_gameName, m_playerName, 1, data);

            byte[] buffer = new byte[1024];
            buffer = request.send();

            if (buffer[0] == 0)
            {
                // Game created, continue with Join functionality
                m_gameName = textBox1.Text;
                this.Text = "Canasta - Joc: " + m_gameName;
                timer1.Enabled = false;
                button1.Enabled = false;
                button1.Text = "Start joc";

                label3.Text = "Mesaje:";
                listBox1.Visible = false;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button3.Visible = true;
                m_creareJoc = false;

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;

                timer1.Enabled = true;
            }
            else
            {
                m_gameName = "";
                m_playerName = ""; 
                MessageBox.Show("Jocul exista deja!");
            }
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
                int curPos = 1;
                m_players.Clear();
                for (int i = 0; i < buffer[0]; i++)
                {
                    string name = "";
                    for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                        name += (char)buffer[j];
                    curPos += buffer[curPos] + 2;
                    m_players.Add(new Player(name, (int)buffer[curPos - 1]));

                    if (!searchName(name))
                        listBox2.Items.Add(name);
                }
                updateDeletedPlayers();

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

        private bool searchName(string name)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
                if (listBox2.Items.IndexOf(name) != -1)
                    return true;
            for (int j = 0; j < m_numberOfTeams; j++)
                for (int i = 0; i < m_teams[j].Items.Count; i++)
                    if (m_teams[j].Items.IndexOf(name) != -1)
                        return true;
            return false;
        }

        private void updateDeletedPlayers()
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                bool exists = false;
                for (int j = 0; j < m_players.Count; j++)
                    if (m_players[j].Name == listBox2.Items[i].ToString())
                        exists = true;
                if (exists == false)
                    listBox2.Items.RemoveAt(i);
            }
        }

        public void deactivateTimer()
        {
            timer1.Enabled = false;
        }

        private void buttonAddTeamClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null)
                return;

            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_addTeam[i] == (sender as Button))
                {
                    if (m_teams[i].Items.Count == m_numberOfPlayers / m_numberOfTeams)
                        return;
                    m_teams[i].Items.Add(listBox2.SelectedItem.ToString());
                    listBox2.Items.Remove(listBox2.SelectedItem);
                    return;
                }
        }

        private void buttonRemoveTeamClick(object sender, EventArgs e)
        {
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_removeTeam[i] == (sender as Button))
                {
                    if (m_teams[i].SelectedItem == null)
                        return;
                    listBox2.Items.Add(m_teams[i].SelectedItem.ToString());
                    m_teams[i].Items.Remove(m_teams[i].SelectedItem);
                    return;
                }
        }

        private void buttonUpTeamClick(object sender, EventArgs e)
        {
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_upTeam[i] == (sender as Button))
                {
                    if (m_teams[i].SelectedItem == null)
                        return;
                    if (m_teams[i].SelectedIndex == 0)
                        return;
                    int newIndex = m_teams[i].SelectedIndex - 1;
                    object selected = m_teams[i].SelectedItem;
                    m_teams[i].Items.Remove(selected);
                    m_teams[i].Items.Insert(newIndex, selected);
                    m_teams[i].SetSelected(newIndex, true);
                    return;
                }
        }

        private void buttonDownTeamClick(object sender, EventArgs e)
        {
            for (int i = 0; i < MAX_TEAMS; i++)
                if (m_downTeam[i] == (sender as Button))
                {
                    if (m_teams[i].SelectedItem == null)
                        return;
                    if (m_teams[i].SelectedIndex == m_teams[i].Items.Count - 1)
                        return;
                    int newIndex = m_teams[i].SelectedIndex + 1;
                    object selected = m_teams[i].SelectedItem;
                    m_teams[i].Items.Remove(selected);
                    m_teams[i].Items.Insert(newIndex, selected);
                    m_teams[i].SetSelected(newIndex, true);
                    return;
                }
        }

        private void populateTeamOptions(object sender, EventArgs e)
        {
            m_numberOfPlayers = Convert.ToInt16(comboBox2.SelectedItem.ToString());
            comboBox1.Items.Clear();
            comboBox1.Items.Add("-");
            for (int i = 2; i < m_numberOfPlayers; i++)
                if (m_numberOfPlayers % i == 0)
                    comboBox1.Items.Add(i);
            comboBox1.SelectedIndex = 0;
        }

        private void showTeams(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "-")
                m_numberOfTeams = 0;
            else
                m_numberOfTeams = Convert.ToInt16(comboBox1.SelectedItem.ToString());

            if (comboBox1.SelectedItem.ToString() == "-")
                this.Size = new Size(720, 530);
            else
                this.Size = new Size(1106, 530);

            int selectedNumberOfTeams = 0;
            if (comboBox1.SelectedItem.ToString() != "-")
                selectedNumberOfTeams = Convert.ToInt16(comboBox1.SelectedItem.ToString());

            for (int i = selectedNumberOfTeams; i < MAX_TEAMS; i++)
            {
                m_teams[i].Visible = false;
                m_addTeam[i].Visible = false;
                m_removeTeam[i].Visible = false;
                m_upTeam[i].Visible = false;
                m_downTeam[i].Visible = false;
            }

            for (int i = selectedNumberOfTeams; i < MAX_TEAMS; i++)
            {
                m_teams[i].Visible = false;
                m_addTeam[i].Visible = false;
                m_removeTeam[i].Visible = false;
                m_upTeam[i].Visible = false;
                m_downTeam[i].Visible = false;
            }

            for (int i = 0; i < selectedNumberOfTeams; i++)
            {
                m_teams[i].Visible = true;
                m_addTeam[i].Visible = true;
                m_removeTeam[i].Visible = true;
                m_upTeam[i].Visible = true;
                m_downTeam[i].Visible = true;
            }
        }

        private void textBox4_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
