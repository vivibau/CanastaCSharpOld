using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Canasta
{
    class Player : IComparable<Player>
    {
        string m_name;
        int m_teamId;
        int m_order;
        int m_score;

        string m_board;
        string m_displayed;
        string m_displayed2;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int TeamId
        {
            get { return m_teamId; }
            set { m_teamId = value; }
        }

        public int Order
        {
            get { return m_order; }
            set { m_order = value; }
        }

        public Player(string name, int teamId, int order, string board)
        {
            m_name = name;
            m_teamId = teamId;
            m_order = order;
            m_board = board;
        }

        public Player(string fullData)
        {
            Utils u = new Utils();
            int pos = 0;
            m_name = u.readNextString(fullData, ref pos);
            m_teamId = u.readNextInt(fullData, ref pos);
            m_order = u.readNextInt(fullData, ref pos);
            string score = u.readNextString(fullData, ref pos);
            m_score = Convert.ToInt32(score);
            m_board = u.readNextString(fullData, ref pos);
            m_displayed = u.readNextString(fullData, ref pos);
            m_displayed2 = u.readNextString(fullData, ref pos);
        }

        public int CompareTo(Player comparePlayer)
        {
            // A null value means that this object is greater. 
            if (comparePlayer == null)
                return 1;
            else
                return this.m_order.CompareTo(comparePlayer.m_order);
        }

        private void sortBoard()
        {
            char[] tmp = m_board.ToCharArray();
            Array.Sort(tmp);
            m_board = new string(tmp);
        }
    }
}
