using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class Formation : IComparable<Formation>
    {
        List<int> m_pieces;
        int m_size;

        public List<int> Pieces
        {
            get { return m_pieces; }
        }

        public Formation()
        {
            m_size = 0;
            m_pieces = new List<int>();
        }

        public bool exists(int piece)
        {
            return (m_pieces[0] / 8 == 14 ? 2 : m_pieces[0] / 8) == (piece / 8 == 14 ? 2 : piece / 8);
        }

        public void addPiece(int piece)
        {
            m_pieces.Add(piece);
            m_pieces.Sort();
            m_size += 53;
        }

        public int getSize()
        {
            return m_size;
        }

        public int getNumberOfPieces()
        {
            return m_pieces.Count;
        }

        public int CompareTo(Formation formation)
        {
            int value1 = m_pieces[0] / 8 == 14 ? 2 : m_pieces[0] / 8;
            int value2 = formation.Pieces[0] / 8 == 14 ? 2 : formation.Pieces[0] / 8;

            return value1.CompareTo(value2);
//            if (value1 > value2)
//                return 1;
//            return -1;
        }
    }
}
