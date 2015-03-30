using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class Table
    {
        Row m_up;
        Row m_down;

        public Row Up
        {
            get { return m_up; }
        }

        public Row Down
        {
            get { return m_down; }
        }

        public Table()
        {
            m_up = new Row();
            m_down = new Row();
        }

        public bool addFormation(Formation f)
        {
            if (m_up.Size > m_down.Size)
                return m_down.addFormation(f);
            return m_up.addFormation(f);
        }

        public int getNumberOfPieces()
        {
            return m_up.getNumberOfPieces() + m_down.getNumberOfPieces();
        }
    }
}
