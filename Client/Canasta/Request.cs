using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Canasta
{
    class Request
    {
        string m_gameName;
        string m_playerName;
        string m_data;
        int m_operation;
        string m_server;

        public Request(string server, string gameName, string playerName, int operation, string data)
        {
            m_gameName = gameName;
            m_playerName = playerName;
            m_data = data;
            m_operation = operation;
            m_server = server;
        }

        private Byte[] data()
        {
            byte[] byData = new byte[1 + 1 + m_gameName.Length + 1 + m_playerName.Length + 1 + 1 + m_data.Length];

            int curPos = 0;
            byData[curPos] = (byte)1;   //gameType
            curPos++;

            byData[curPos] = (byte)m_gameName.Length;
            curPos++;

            for (int i = curPos; i < curPos + m_gameName.Length; i++)
                byData[i] = (byte)m_gameName[i - curPos];
            curPos += m_gameName.Length;

            byData[curPos] = (byte)m_playerName.Length;
            curPos++;

            for (int i = curPos; i < curPos + m_playerName.Length; i++)
                byData[i] = (byte)m_playerName[i - curPos];
            curPos += m_playerName.Length;

            byData[curPos] = (byte)m_operation;
            curPos++;

            byData[curPos] = (byte)m_data.Length;
            curPos++;

            for (int i = curPos; i < curPos + m_data.Length; i++)
                byData[i] = (byte)m_data[i - curPos];

            return byData;
        }

        public byte[] send()
        {
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] addressList = Dns.GetHostAddresses(m_server);
            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(addressList[0].ToString());
            System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3291);
            soc.Connect(remoteEP);

            soc.Send(data());
            byte[] buffer = new byte[1024];
            int iRx = soc.Receive(buffer);
            soc.Disconnect(true);

            return buffer;
        }
    }
}
