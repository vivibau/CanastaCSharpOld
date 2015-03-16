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
    public partial class Canasta : Form
    {
        string m_server;
        int m_interval;

        public Canasta()
        {
            InitializeComponent();
            string optionsFile = Directory.GetCurrentDirectory() + "\\config.ini";
            using (System.IO.StreamReader file = new System.IO.StreamReader(optionsFile))
            {
                m_server = file.ReadLine();
                m_interval = Convert.ToInt16(file.ReadLine());
            }
        }

        private void iesireToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void creareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newGame = new NewGame(m_server, m_interval);
            newGame.ShowDialog();
        }

        private void alaturareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form joinGame = new JoinGame(m_server, m_interval);
            joinGame.ShowDialog();
        }

        private void onFormClosing(object sender, EventArgs e)
        {
//            this.deactivateTimer();
        }

        private void optiuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form options = new Options(m_server, m_interval);
            options.ShowDialog();

            string optionsFile = Directory.GetCurrentDirectory() + "\\config.ini";
            using (System.IO.StreamReader file = new System.IO.StreamReader(optionsFile))
            {
                m_server = file.ReadLine();
                m_interval = Convert.ToInt16(file.ReadLine());
            }
        }

    }
}
