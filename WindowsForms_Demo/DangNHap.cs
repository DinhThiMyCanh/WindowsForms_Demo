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
    public partial class frmDangNHap : Form
    {
        public frmDangNHap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTK.Text == "Admin" && txtMK.Text == "123456")
            {
                MessageBox.Show("Bạn đăng nhập thành công");
                //frmControlCB f = new frmControlCB();
                //f.ShowDialog();
                this.Hide();
                string st = "admin";
                frmMenu f = new frmMenu(st);
                f.ShowDialog();
            }    
                
        }
    }
}
