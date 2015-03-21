using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canasta
{
    public class Game
    {
        string m_board;
        string m_displayed;
        string m_displayed2;
        string m_playerName;
        int m_order;

        public Game(string name, string board)
        {
            m_playerName = name;
//            m_order = order;
            m_board = board;
        }

        public string Board
        {
            get { return m_board; }
            set { m_board = value; }
        }
    }
}
