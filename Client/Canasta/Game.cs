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
            // format:
            // - number of players
            // - for each player: name (length + name), order, 14 pieces (15 if order = 0)
            // - rare piece
            m_players = new Player[data[0]];
            int pos = 1;
            for (int i = 0; i < data[0]; i++)  // 0 to number of players
            {
                int playerNameLength = data[pos];
                string playerName = "";
                pos++;
                for (int j = 0; j < playerNameLength; j++)
                {
                    playerName += data[pos];
                    pos++;
                }
                int playerOrder = data[pos];
                pos++;
                int teamId = data[pos];
                pos++;
                string playerBoard = "";
                for (int j = 0; j < (playerOrder == 0 ? 15 : 14); j++)
                {
                    playerBoard += data[pos];
                    pos++;
                }
                m_players[i] = new Player(playerName, playerOrder, teamId, playerBoard);
            }
            m_rarePiece = data[pos];
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
