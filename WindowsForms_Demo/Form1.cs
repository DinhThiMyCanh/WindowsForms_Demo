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
    public partial class frmControlCB : Form
    {
        
        public frmControlCB()
        {
            InitializeComponent();
        }
        //Cho phép xem thông tin sv
        private void btnXemThongTin_Click(object sender, EventArgs e)
        {
            string ht = txtHoTen.Text;
            string ns = dtpNgaySinh.Text;
            string gt = rdNam.Checked == true ? "Nam" : "Nữ";
            string nn = "";
            if (chkTiengAnh.Checked == true)
                nn = "Tiếng Anh" +"\n";
            if (chkTiengTrung.Checked == true)
                nn = nn + "Tiếng Trung";
            string qq = cboQueQuan.Text;
            string dsmh = "";
            foreach (var item in lstDSMH.SelectedItems)
                dsmh += item + "\n";
            MessageBox.Show(ht + "\n" + ns + "\n" + gt + "\n" + nn + "\n" + qq + "\n" + dsmh);
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtHoTen.Focus();
            dtpNgaySinh.Value = DateTime.Now;
            if (rdNam.Checked == false)
                rdNam.Checked = true;
            if (chkTiengAnh.Checked == true)
                chkTiengAnh.Checked = false;
            if (chkTiengTrung.Checked == true)
                chkTiengTrung.Checked = false;
            cboQueQuan.SelectedIndex = 0;
            lstDSMH.ClearSelected();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmControlCB_Load(object sender, EventArgs e)
        {
            cboQueQuan.Items.Add("Quảng Nam");
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "Tập tin ảnh|*.bmp;*.png;*.jpg|File tùy ý (*.*)|*.*";
            if (oFile.ShowDialog(this)==DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(oFile.FileName);
            }    
        }

        
    }
}
