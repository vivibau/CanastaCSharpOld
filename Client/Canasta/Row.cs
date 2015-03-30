using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class Row
    {
        int m_size;
        int m_spacing;
        List<Formation> m_formations;

        public Row()
        {
            m_size = 0;
            m_spacing = 0;
            m_formations = new List<Formation>();
        }

        public int Size
        {
            get { return m_size; }
        }

        public int Spacing
        {
            get { return m_spacing; }
        }

        public List<Formation> Formations
        {
            get { return m_formations; }
        }

        public bool addFormation(Formation f)
        {
            if (600 - m_size < f.getSize())
                return false;

            m_formations.Add(f);
            m_size += f.getSize();

            int totalSize = 0;
            foreach (Formation tmp in m_formations)
                totalSize += tmp.getSize();
            m_spacing = (600 - totalSize) / m_formations.Count;
            return true;
        }

        public int getNumberOfPieces()
        {
            int result = 0;
            foreach (Formation f in m_formations)
                result += f.getNumberOfPieces();
            return result;
        }
    }
}
