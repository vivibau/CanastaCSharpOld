using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canasta
{
    public partial class Canasta : Form
    {
        const double originalW = 1300;
        const double originalH = 700;
        string m_server;
        int m_interval;
        public static Game m_game;
        double m_ratioX;
        double m_ratioY;

        private GroupBox m_groupBoxPlayersSection;
        private Panel m_panelPlayersSection;
        private GroupBox[] m_groupBoxPlayers;
        private Panel[] m_panelPlayers;
        private GroupBox m_groupBoxStack;
        private GroupBox m_groupBoxRarePiece;
        private GroupBox m_groupBoxPot;
        private Panel m_panelDisplayed;
        private Panel m_panelBoard;
        private Button m_buttonBoardLeft;
        private Button m_buttonBoardRight;

        public Canasta()
        {
            InitializeComponent();
            string optionsFile = Directory.GetCurrentDirectory() + "\\config.ini";
            using (System.IO.StreamReader file = new System.IO.StreamReader(optionsFile))
            {
                m_server = file.ReadLine();
                m_interval = Convert.ToInt16(file.ReadLine());
            }
            toolStripStatusLabel1.Text = this.Size.Width + "x" + this.Size.Height;

            string data = "";
            data += (char)2;  // number of players
            data += (char)1;  // player name length
            data += "A";      // player name
            data += (char)0;  // player order
            data += (char)1;  // player team id
            data += "123456789012345";  // player board
            data += (char)1;  // and again...
            data += "B";
            data += (char)1;
            data += (char)2;
            data += "12345678901234";
            data += (char)18; // rare piece

            m_game = new Game(data);
            m_game.evGameCreated += new Game.GameCreated(this.onGameStarted);

        }

        private void onGameStarted(object sender, EventArgs e)
        {
//            this.Controls.Clear();
            m_groupBoxPlayersSection = new GroupBox();
//            m_groupBoxPlayersSection.SuspendLayout();
            m_groupBoxPlayersSection.Location = new Point(0, 31);
            m_groupBoxPlayersSection.Size = new Size(420, 596);
            this.Controls.Add(m_groupBoxPlayersSection);
//            m_groupBoxPlayers.ResumeLayout();
//            m_groupBoxPlayers.PerformLayout();

            m_panelPlayersSection = new Panel();
            m_panelPlayersSection.AutoScroll = true;
            m_panelPlayersSection.Location = new Point(6, 21);
            m_panelPlayersSection.Size = new Size(408, 569);
            m_groupBoxPlayersSection.Controls.Add(m_panelPlayersSection);

            m_groupBoxPlayers = new GroupBox[m_game.getNumberOfPlayers()];
            m_panelPlayers = new Panel[m_game.getNumberOfPlayers()];
            for (int i = 0; i < m_game.getNumberOfPlayers(); i++)
            {
                m_groupBoxPlayers[i] = new GroupBox();
                m_groupBoxPlayers[i].Location = new Point(3, 3 + i * 186);
                m_groupBoxPlayers[i].Size = new Size(381, 184);
                m_groupBoxPlayers[i].Text = i.ToString();
                m_panelPlayersSection.Controls.Add(m_groupBoxPlayers[i]);
            }

            m_groupBoxStack = new GroupBox();
            m_groupBoxStack.Location = new Point(464, 31);
            m_groupBoxStack.Size = new Size(252, 151);
            m_groupBoxStack.Text = "Piese de luat: ";
            this.Controls.Add(m_groupBoxStack);

            m_groupBoxRarePiece = new GroupBox();
            m_groupBoxRarePiece.Location = new Point(151, 20);
            m_groupBoxRarePiece.Size = new Size(95, 125);
            m_groupBoxRarePiece.Text = "Piatra rara";
            m_groupBoxStack.Controls.Add(m_groupBoxRarePiece);

            m_groupBoxPot = new GroupBox();
            m_groupBoxPot.Location = new Point(523, 202);
            m_groupBoxPot.Size = new Size(143, 169);
            m_groupBoxPot.Text = "Pot: ";
            this.Controls.Add(m_groupBoxPot);

            m_panelDisplayed = new Panel();
            m_panelDisplayed.AutoScroll = true;
            m_panelDisplayed.Location = new Point(743, 88);
            m_panelDisplayed.Size = new Size(457, 270);
            this.Controls.Add(m_panelDisplayed);

            m_panelBoard = new Panel();
            m_panelBoard.AutoScroll = true;
            m_panelBoard.Location = new Point(464, 444);
            m_panelBoard.Size = new Size(623, 177);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Canasta));
            m_panelBoard.BackgroundImage = ((Image)(resources.GetObject("m_panelBoard.BackgroundImage")));
            m_panelBoard.BackgroundImageLayout = ImageLayout.Stretch; 
            this.Controls.Add(m_panelBoard);

            m_buttonBoardLeft = new Button();
            m_buttonBoardLeft.Location = new Point(435, 444);
            m_buttonBoardLeft.Size = new Size(23, 177);
            m_buttonBoardLeft.Text = "<";
            this.Controls.Add(m_buttonBoardLeft);

            m_buttonBoardRight = new Button();
            m_buttonBoardRight.Location = new Point(1093, 444);
            m_buttonBoardRight.Size = new Size(23, 177);
            m_buttonBoardRight.Text = ">";
            this.Controls.Add(m_buttonBoardRight);
        }

        private void onResizeForm(object sender, EventArgs e)
        {
            //Resize of the form shall be made only when the form is not minimized
            if (WindowState != FormWindowState.Minimized)
            {
                toolStripStatusLabel1.Text = this.Size.Width + "x" + this.Size.Height;
                m_ratioX = (double)this.Size.Width / (double)originalW;
                m_ratioY = (double)this.Size.Height / (double)originalH;

//                resizeControl(groupBox1, 880, 30, 390, 505, m_ratioX, m_ratioY);
            }
        }

        private void resizeControl(Control control,
                                   int originalX,
                                   int originalY,
                                   int originalW,
                                   int originalH,
                                   double ratioX,
                                   double ratioY)
        {
            control.Location = new Point((int)Math.Truncate(originalX * ratioX), (int)Math.Truncate(originalY * ratioY));
            control.Size = new Size((int)Math.Truncate(originalW * ratioX), (int)Math.Truncate(originalH * ratioY));
        }

        private void iesireToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void creareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newGame = new NewGame(m_server, m_interval, ref m_game);
            newGame.ShowDialog();
        }

        private void alaturareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form joinGame = new JoinGame(m_server, m_interval, ref m_game);
            joinGame.ShowDialog();
        }

        private void onFormClosing(object sender, EventArgs e)
        {
//            this.deactivateTimer();
        }

        private void optiuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form options = new Options(m_server, m_interval);
            options.ShowDialog();

            string optionsFile = Directory.GetCurrentDirectory() + "\\config.ini";
            using (System.IO.StreamReader file = new System.IO.StreamReader(optionsFile))
            {
                m_server = file.ReadLine();
                m_interval = Convert.ToInt16(file.ReadLine());
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_game.generateEvent();
        }

    }
}
