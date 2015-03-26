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
        Player[] m_players;
        int m_rarePiece;
        int m_state;
        int m_currentPlayer;

        public event GameCreated evGameCreated;
        public EventArgs e = null;
        public delegate void GameCreated(Game g, EventArgs e);

        public Game(string data)
        {
            int pos = 1;
            Utils u = new Utils();
            m_gameName = u.readNextString(data, ref pos);

            int numberOfPlayers = u.readNextInt(data, ref pos);
            m_players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
                m_players[i] = new Player(u.readNextString(data, ref pos));

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
            return m_players.Length;
        }

        public void generateEvent()
        {
            evGameCreated(this, e);
        }
    }
}
