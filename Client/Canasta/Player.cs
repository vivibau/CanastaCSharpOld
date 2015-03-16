using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canasta
{
    class Player
    {
        string m_name;
        int m_teamId;

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

        public Player(string name, int teamId)
        {
            m_name = name;
            m_teamId = teamId;
        }
    }
}
