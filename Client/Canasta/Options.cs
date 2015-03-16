using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canasta
{
    public partial class Options : Form
    {
        string m_server;
        int m_interval;

        public Options(string server, int interval)
        {
            InitializeComponent();
            m_server = server;
            m_interval = interval;
            textBox1.Text = m_server;
            numericUpDown1.Value = m_interval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string optionsFile = Directory.GetCurrentDirectory() + "\\config.ini";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(optionsFile))
            {
                file.WriteLine(textBox1.Text);
                file.WriteLine(numericUpDown1.Value);
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
