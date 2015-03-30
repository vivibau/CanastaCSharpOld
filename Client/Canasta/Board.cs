using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class Board
    {
        List<Table> m_tables;
        List<Formation> m_formations;

        public List<Table> Tables
        {
            get { return m_tables; }
        }

        public Board()
        {
            m_tables = new List<Table>();
            m_formations = new List<Formation>();
        }

        public void addPiece(int piece)
        {
            bool exists = false;
            foreach (Formation f in m_formations)
                if (f.exists(piece))
                {
                    f.addPiece(piece);
                    exists = true;
                    break;
                }

            if (exists == false)
            {
                Formation f = new Formation();
                f.addPiece(piece);
                m_formations.Add(f);
            }

            arrangePieces();
        }

        private void arrangePieces()
        {
            m_tables.Clear();
            m_tables.Add(new Table());
            Table t = m_tables[m_tables.Count - 1];

            foreach (Formation f in m_formations)
                if (t.addFormation(f) == false)
                {
                    m_tables.Add(new Table());
                    t = m_tables[m_tables.Count - 1];
                    t.addFormation(f);
                }
        }
    }
}
