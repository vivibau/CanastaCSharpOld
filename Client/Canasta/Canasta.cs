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
        const double originalW = 1250;
        const double originalH = 700;
        string m_server;
        int m_interval;
        public static Game m_game;
        public static string m_playerName;
        double m_ratioX;
        double m_ratioY;

        List<SizeLocation> m_sizeLocation;

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

        private Button[] m_buttonBoard;

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


            PieceGenerator p = new PieceGenerator();

            string data = "";
            data += (char)1;  // game type
            data += (char)1;  // game name length
            data += "A";
            data += (char)2;  // number of players
            data += (char)1;  // player name length
            data += "B";      // player name
            data += (char)1;  // player team id
            data += (char)0;  // player order
            data += (char)1;  // score length
            data += "0";      // score

            data += (char)14; // board length
            for (int i = 0; i < 14; i++)
                data += (char)p.getNext();

            data += (char)0;  // displayed length
            data += (char)0;  // displayed2 length

            data += (char)1;  // player name length
            data += "C";      // player name
            data += (char)1;  // player team id
            data += (char)0;  // player order
            data += (char)1;  // score length
            data += "0";      // score

            data += (char)14; // board length
            for (int i = 0; i < 14; i++)
                data += (char)p.getNext();

            data += (char)0;  // displayed length
            data += (char)0;  // displayed2 length

            data += (char)0;  // game state
            data += (char)0;  // current player
            data += (char)18; // rare piece
                

            m_game = new Game(data);
            m_game.evGameCreated += new Game.GameCreated(this.onGameStarted);
            
            m_sizeLocation = new List<SizeLocation>();
            m_sizeLocation.Clear();
        }

        private void onGameStarted(object sender, EventArgs e)
        {
            m_groupBoxPlayersSection = new GroupBox();
            m_groupBoxPlayersSection.Location = new Point(0, 31);
            m_groupBoxPlayersSection.Size = new Size(420, 596);
            m_groupBoxPlayersSection.BackColor = SystemColors.ControlLight;
            m_groupBoxPlayersSection.Name = "m_groupBoxPlayersSection";
            m_sizeLocation.Add(new SizeLocation(m_groupBoxPlayersSection.Name, new Point(0, 31), new Size(420, 596)));
            this.Controls.Add(m_groupBoxPlayersSection);

            m_panelPlayersSection = new Panel();
            m_panelPlayersSection.AutoScroll = true;
            m_panelPlayersSection.Location = new Point(6, 21);
            m_panelPlayersSection.Size = new Size(408, 569);
            m_panelPlayersSection.Name = "m_panelPlayersSection";
            m_sizeLocation.Add(new SizeLocation(m_panelPlayersSection.Name, new Point(6, 21), new Size(408, 569)));
            m_groupBoxPlayersSection.Controls.Add(m_panelPlayersSection);

            m_groupBoxPlayers = new GroupBox[m_game.getNumberOfPlayers()];
            m_panelPlayers = new Panel[m_game.getNumberOfPlayers()];
//            for (int i = 0; i < m_game.getNumberOfPlayers(); i++)
            int idx = 0;
            foreach (Player p in m_game.getPlayers())
                if (p.Name != m_playerName)
                {
                    m_groupBoxPlayers[idx] = new GroupBox();
                    m_groupBoxPlayers[idx].Location = new Point(3, 3 + idx * 186);
                    m_groupBoxPlayers[idx].Size = new Size(381, 184);
                    m_groupBoxPlayers[idx].Text = p.Name;
                    m_groupBoxPlayers[idx].BackColor = SystemColors.ControlDark;
                    m_groupBoxPlayers[idx].Name = "m_groupBoxPlayers" + p.Name;
                    m_sizeLocation.Add(new SizeLocation(m_groupBoxPlayers[idx].Name, new Point(3, 3 + idx * 186), new Size(381, 184)));
                    m_panelPlayersSection.Controls.Add(m_groupBoxPlayers[idx]);

                    m_panelPlayers[idx] = new Panel();
                    m_panelPlayers[idx].AutoScroll = true;
                    m_panelPlayers[idx].Location = new Point(7, 22);
                    m_panelPlayers[idx].Size = new Size(368, 156);
                    m_panelPlayers[idx].Name = "m_panelPlayers" + p.Name;
                    m_sizeLocation.Add(new SizeLocation(m_panelPlayers[idx].Name, new Point(7, 22), new Size(368, 156)));
                    m_groupBoxPlayers[idx].Controls.Add(m_panelPlayers[idx]);
                }

            m_groupBoxStack = new GroupBox();
            m_groupBoxStack.Location = new Point(464, 31);
            m_groupBoxStack.Size = new Size(252, 151);
            m_groupBoxStack.Text = "Piese de luat: ";
            m_groupBoxStack.BackColor = SystemColors.ControlDark;
            m_groupBoxStack.Name = "m_groupBoxStack";
            m_sizeLocation.Add(new SizeLocation(m_groupBoxStack.Name, new Point(464, 31), new Size(252, 151)));
            this.Controls.Add(m_groupBoxStack);

            m_groupBoxRarePiece = new GroupBox();
            m_groupBoxRarePiece.Location = new Point(151, 20);
            m_groupBoxRarePiece.Size = new Size(95, 125);
            m_groupBoxRarePiece.Text = "Piatra rara";
            m_groupBoxRarePiece.BackColor = SystemColors.ActiveCaption;
            m_groupBoxRarePiece.Name = "m_groupBoxRarePiece";
            m_sizeLocation.Add(new SizeLocation(m_groupBoxRarePiece.Name, new Point(151, 20), new Size(95, 125)));
            m_groupBoxStack.Controls.Add(m_groupBoxRarePiece);

            m_groupBoxPot = new GroupBox();
            m_groupBoxPot.Location = new Point(523, 202);
            m_groupBoxPot.Size = new Size(143, 169);
            m_groupBoxPot.Text = "Pot: ";
            m_groupBoxPot.BackColor = SystemColors.ControlDark;
            m_groupBoxPot.Name = "m_groupBoxPot";
            m_sizeLocation.Add(new SizeLocation(m_groupBoxPot.Name, new Point(523, 202), new Size(143, 169)));
            this.Controls.Add(m_groupBoxPot);

            m_panelDisplayed = new Panel();
            m_panelDisplayed.AutoScroll = true;
            m_panelDisplayed.Location = new Point(743, 88);
            m_panelDisplayed.Size = new Size(457, 270);
            m_panelDisplayed.BackColor = SystemColors.ControlLight;
            m_panelDisplayed.Name = "m_panelDisplayed";
            m_sizeLocation.Add(new SizeLocation(m_panelDisplayed.Name, new Point(743, 88), new Size(457, 270)));
            this.Controls.Add(m_panelDisplayed);

            m_panelBoard = new Panel();
            m_panelBoard.AutoScroll = true;
            m_panelBoard.Location = new Point(464, 444);
            m_panelBoard.Size = new Size(623, 177);
//            m_panelBoard.BackgroundImage = Properties.Resources.Board;
//            m_panelBoard.BackgroundImageLayout = ImageLayout.Stretch;
            m_panelBoard.BackColor = SystemColors.ControlLight;
            m_panelBoard.Name = "m_panelBoard";
            m_sizeLocation.Add(new SizeLocation(m_panelBoard.Name, new Point(464, 444), new Size(623, 177)));
            this.Controls.Add(m_panelBoard);

            m_buttonBoardLeft = new Button();
            m_buttonBoardLeft.Location = new Point(435, 444);
            m_buttonBoardLeft.Size = new Size(23, 177);
            m_buttonBoardLeft.Text = "<";
            m_buttonBoardLeft.Name = "m_buttonBoardLeft";
            m_sizeLocation.Add(new SizeLocation(m_buttonBoardLeft.Name, new Point(435, 444), new Size(23, 177)));
            this.Controls.Add(m_buttonBoardLeft);

            m_buttonBoardRight = new Button();
            m_buttonBoardRight.Location = new Point(1093, 444);
            m_buttonBoardRight.Size = new Size(23, 177);
            m_buttonBoardRight.Text = ">";
            m_buttonBoardRight.Name = "m_buttonBoardRight";
            m_sizeLocation.Add(new SizeLocation(m_buttonBoardRight.Name, new Point(1093, 444), new Size(23, 177)));
            this.Controls.Add(m_buttonBoardRight);

            Board board = new Board();
//            string pieces = m_game.getPlayer("B").Board;
            string pieces = m_game.getPlayer(m_playerName).Board;
            for (int i = 0; i < pieces.Length; i++)
                board.addPiece(pieces[i]);

            List<Table> tables = board.Tables;
            Table table = tables[0];
            Row up = table.Up;
            List<Formation> formations = up.Formations;
            int x = up.Spacing / 2;
            int buttonIndex = 0;
            m_buttonBoard = new Button[table.getNumberOfPieces()];
            foreach (Formation f in formations)
            {
                List<int> tmp = f.Pieces;
                tmp.Sort();
                foreach (int i in tmp)
                {
                    m_buttonBoard[buttonIndex] = new Button();
                    m_buttonBoard[buttonIndex].Location = new Point(x, 10);
                    m_buttonBoard[buttonIndex].Size = new Size(53, 78);
//                    m_buttonBoard[buttonIndex].BackgroundImage = Properties.Resources.Piece;
//                    m_buttonBoard[buttonIndex].BackgroundImageLayout = ImageLayout.Stretch;
                    m_buttonBoard[buttonIndex].Text = (i/8).ToString();
                    m_buttonBoard[buttonIndex].Font = new System.Drawing.Font("Microsoft Sans Serif", 12.14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    m_panelBoard.Controls.Add(m_buttonBoard[buttonIndex]);
                    x += 52;
                    buttonIndex++;
                }
                x += up.Spacing;
            }
            Row down = table.Down;
            formations = down.Formations;
            x = down.Spacing / 2;
            foreach (Formation f in formations)
            {
                List<int> tmp = f.Pieces;
                tmp.Sort();
                foreach (int i in tmp)
                {
                    m_buttonBoard[buttonIndex] = new Button();
                    m_buttonBoard[buttonIndex].Location = new Point(x, 90);
                    m_buttonBoard[buttonIndex].Size = new Size(53, 78);
//                    m_buttonBoard[buttonIndex].BackgroundImage = Properties.Resources.Piece;
//                    m_buttonBoard[buttonIndex].BackgroundImageLayout = ImageLayout.Stretch;
                    m_buttonBoard[buttonIndex].Text = (i/8).ToString();
                    m_buttonBoard[buttonIndex].Font = new System.Drawing.Font("Microsoft Sans Serif", 12.14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    m_panelBoard.Controls.Add(m_buttonBoard[buttonIndex]);
                    x += 52;
                    buttonIndex++;
                }
                x += down.Spacing;
            }
        }

        private void onResizeForm(object sender, EventArgs e)
        {
            //Resize of the form shall be made only when the form is not minimized
            if (WindowState == FormWindowState.Minimized) return;

            m_ratioX = (double)this.Size.Width / (double)originalW;
            m_ratioY = (double)this.Size.Height / (double)originalH;
            toolStripStatusLabel1.Text = this.Size.Width + "x" + this.Size.Height;
            foreach (SizeLocation item in m_sizeLocation)
            {
                
//                resizeControl(item.Control, item.OriginalLocation.X, item.OriginalLocation.Y, item.OriginalSize.Width, item.OriginalSize.Height, m_ratioX, m_ratioY);
                resizeControl(this.Controls.Find(item.Name, true)[0], item.OriginalLocation.X, item.OriginalLocation.Y, item.OriginalSize.Width, item.OriginalSize.Height, m_ratioX, m_ratioY);
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
            Form newGame = new NewGame(m_server, m_interval, ref m_game, ref m_playerName);
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

        private void askStatus(object sender, EventArgs e)
        {
//            Request request = new Request(m_server, m_game.Name, m_playerName, 4, "");
//            byte[] buffer = new byte[1024];
//            buffer = request.send();

            /*
            if (m_creareJoc)
            {
                // populate list of games
                listBox1.Items.Clear();

                int curPos = 3;
                for (int i = 0; i < buffer[2]; i++)
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
                int curPos = 3;
                if (buffer[1] != 2) // game state != in progress
                {
                    m_players.Clear();
                    for (int i = 0; i < buffer[2]; i++)
                    {
                        string name = "";
                        for (int j = curPos + 1; j < curPos + 1 + buffer[curPos]; j++)
                            name += (char)buffer[j];
                        curPos += buffer[curPos] + 3;
                        m_players.Add(new Player(name, (int)buffer[curPos - 2], (int)buffer[curPos - 1], ""));

                        if (!searchName(name))
                            listBox2.Items.Add(name);
                    }
                    updateDeletedPlayers();
                    curPos++;
                }

                // updates
                for (int i = 0; i < buffer[curPos - 1]; i++)
                {
                    Update update = new Update(buffer, ref curPos);
                    if (update.Operation == 9) // op type = broadcast
                    {
                        textBox3.AppendText("[" + update.Name + "]: ");
                        textBox3.AppendText(update.Data);
                        textBox3.AppendText("\n");
                    }
                    if (update.Operation == 13) // op type = get board
                        if (update.Name == m_playerName)
                        {
                            //                            m_game = new Game(m_playerName, update.Data);
                            Canasta.m_game = new Game(update.Data);
                            Close();
                        }
                }
                if (m_players.Count == m_numberOfPlayers)
                    button1.Enabled = true;
            }
             */
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_game.generateEvent();
        }

        private void addPieceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
