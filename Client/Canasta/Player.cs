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

        public int CompareTo(Player comparePlayer)
        {
            // A null value means that this object is greater. 
            if (comparePlayer == null)
                return 1;
            else
                return this.m_order.CompareTo(comparePlayer.m_order);
        }
    }
}
