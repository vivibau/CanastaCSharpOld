using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class PieceGenerator
    {
        List<int> m_pieces;
        
        public PieceGenerator()
        {
            m_pieces = new List<int>();
            List<int> x = new List<int>();
            for (int i = 0; i < 107; i++)
                x.Add(i + 8);

            Random rnd = new Random();
            for (int i = 0; i < 107; i++)
            {
                int r = rnd.Next(x.Count() - 1);
                m_pieces.Add(x[r]);
                x.Remove(x[r]);
            }

            // for some reason the last piece is 106 + 8 = 113
            // but the rest are random
            // so make the series one element bigger and remove the last element (now 114)
            m_pieces.Remove(m_pieces[m_pieces.Count() - 1]);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"log.txt"))
            {
                for (int i = 0; i < m_pieces.Count(); i++)
                {
                    file.WriteLine(m_pieces[i] + " ");
                }
            }
        }

        public int getNext()
        {
            int result = m_pieces[m_pieces.Count() - 1];
            m_pieces.Remove(result);
            return result;
        }
    }
}
