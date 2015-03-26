using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class SizeLocation
    {
        string m_name;
        System.Drawing.Point m_location;
        System.Drawing.Size m_size;

        public SizeLocation(string name, System.Drawing.Point location, System.Drawing.Size size)
        {
            m_name = name;
            m_location = location;
            m_size = size;
        }

        public System.Drawing.Point OriginalLocation
        {
            get { return m_location; }
            set { m_location = value; }
        }

        public System.Drawing.Size OriginalSize
        {
            get { return m_size; }
            set { m_size = value; }
        }

        public string Name
        {
            get { return m_name; }
        }
    }
}
