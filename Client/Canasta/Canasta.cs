using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canasta
{
    public partial class Canasta : Form
    {
        public Canasta()
        {
            InitializeComponent();
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
            Form newGame = new NewGame();
            newGame.ShowDialog();
        }

        private void alaturareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form joinGame = new JoinGame();
            joinGame.ShowDialog();
        }

        private void onFormClosing(object sender, EventArgs e)
        {
//            this.deactivateTimer();
        }

    }
}
