using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Canasta
{
    public class Game
    {
        string m_gameName;
        List<Player> m_players;
        int m_rarePiece;
        int m_state;
        int m_currentPlayer;

        public event GameCreated evGameCreated;
        public EventArgs e = null;
        public delegate void GameCreated(Game g, EventArgs e);

        public Game(string data)
        {
            updateData(data);
        }

        public void updateData(string data)
        {
            int pos = 1;
            Utils u = new Utils();
            m_gameName = u.readNextString(data, ref pos);

            int numberOfPlayers = u.readNextInt(data, ref pos);
            m_players = new List<Player>();
            m_players.Clear();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                string playerName = u.readNextString(data, ref pos);
                int playerTeamId = u.readNextInt(data, ref pos);
                int playerOrder = u.readNextInt(data, ref pos);
                string tmpscore = u.readNextString(data, ref pos);
                int playerScore = Convert.ToInt32(tmpscore);
                string playerBoard = u.readNextString(data, ref pos);
                string playerDisplayed = u.readNextString(data, ref pos);
                string playerDisplayed2 = u.readNextString(data, ref pos);

                m_players.Add(new Player(playerName, playerTeamId, playerOrder, playerScore, playerBoard, playerDisplayed, playerDisplayed2));
            }
            m_players.Sort();

            m_state = u.readNextInt(data, ref pos);
            m_currentPlayer = u.readNextInt(data, ref pos);
            m_rarePiece = u.readNextInt(data, ref pos);

        }

        public string Name
        {
            get { return m_gameName; }
        }

        public int getNumberOfPlayers()
        {
            return m_players.Count;
        }

        public void generateEvent()
        {
            evGameCreated(this, e);
        }

        public Player getPlayer(string name)
        {
            foreach (Player p in m_players)
                if (p.Name == name)
                    return p;

            return null;
        }

        public List<Player> getPlayers()
        {
            return m_players;
        }
    }
}
