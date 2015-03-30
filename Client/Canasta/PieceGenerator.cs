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
            for (int i = 0; i < 106; i++)
                x.Add(i + 8);

            Random rnd = new Random();

            for (int i = 0; i < 106; i++)
            {
                int r = rnd.Next(x.Count() - 1);
                m_pieces.Add(x[r]);
                x.Remove(x[r]);
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
