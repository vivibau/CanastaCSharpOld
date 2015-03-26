using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canasta
{
    class Utils
    {
        public int readNextInt(string source, ref int pos)
        {
            int result;
            result = source[pos];
            pos++;
            return result;
        }

        public string readNextString(string source, ref int pos)
        {
            string result = "";
            int originalPos = pos;
            pos++;
            for (; pos < (char)source[originalPos]; pos++)
                result += (char)source[pos];
            return result;
        }

        public string readString(string source)
        {
            string result = "";
            int originalPos = 0;
            for (int i = 0; i < (char)source[originalPos]; i++)
                result += (char)source[i];
            return result;
        }
    }
}
