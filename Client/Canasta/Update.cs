using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canasta
{
    class Update
    {
        string  m_name;
        int     m_operation;
        string  m_data;

        public Update(byte[] buffer, ref int pos)
        {
            m_name = "";
            for (int i = pos + 1; i < pos + 1 + buffer[pos]; i++)
                m_name += (char)buffer[i];
            pos += buffer[pos] + 1;
            m_operation = buffer[pos];
            if (m_operation == 9) // op type = broadcast
            {
                pos++;
                m_data = "";
                for (int i = pos + 1; i < pos + 1 + buffer[pos]; i++)
                    m_data += (char)buffer[i];
            }
            if (m_operation == 13) // op type = get board
            {
                pos++;
                m_data = "";
                for (int i = pos + 1; i < pos + 1 + buffer[pos]; i++)
                    m_data += (char)buffer[i];
            }
        }

        public string Name
        {
            get { return m_name; }
        }

        public int Operation
        {
            get { return m_operation; }
        }

        public string Data
        {
            get { return m_data; }
        }
    }
}
