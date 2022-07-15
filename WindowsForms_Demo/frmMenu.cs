using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_Demo
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }
        public frmMenu(string phanquyen)
        {
            InitializeComponent();
            quảnLýToolStripMenuItem.Enabled = true;
        }
        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangNHap f = new frmDangNHap();
            f.ShowDialog();
            this.Hide();

        }

        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmControlCB f = new frmControlCB();
            f.MdiParent = this;
            f.Show();
        }
    }
}
