using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            string user = txtTK.Text.Trim();
            string pp = txtMK.Text.Trim();
            string sql = string.Format("Select * from DangNhap where TenDangNhap =={0} and MatKhau=={1}",user, pp);
           // if (KetNoi.Ktra(sql)!=null)
            {
                MessageBox.Show("Bạn đăng nhập thành công");
                this.Hide();
                string st = "admin";
                frmMenu f = new frmMenu(st);
                f.ShowDialog();
            }  
            //else
            //    MessageBox.Show("Bạn đăng nhập không thành công");

        }
    }
}
