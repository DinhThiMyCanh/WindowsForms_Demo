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
    public partial class frmNhanVien : Form
    {
        
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            DataAccess.moKetNoi();
            loadPB();
            loadData();
            DataAccess.dongKetNoi();

        }
        //Đổ dư liệu lên Combobox Phòng ban
        public void loadPB()
        {
            string sql = "select * from PHONGBAN";
            cboPhongBan.DataSource = DataAccess.getData(sql);
            cboPhongBan.DisplayMember = "TenPB";
            cboPhongBan.ValueMember = "MaPB";


        }
        //Đổ dư liệu lên DataGridview Nhan viên
        public void loadData()
        {
            string sql = "select * from NhanVien";
            data.DataSource = DataAccess.getData(sql);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string sql = "Select B.tenPB, count(A.MaNV) from NhanVien as A, PhongBan as B " +
                "where A.MaPB =B.MaPB group by B.TenPB";
            data.DataSource = DataAccess.getData(sql);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from NhanVien where TenNV Like N'%{0}'", txtTimKiem.Text);
            data.DataSource = DataAccess.getData(sql);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtMaNV.Focus();


            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "insert into NhanVien(MaNV,TenNV,NgaySinh,GioiTinh,SoDT,MaPB,Picture) " +
                "values( @MaNV , @TenNV , @NS , @gt , @sodt , @MaPB , @Picture )";
            string[] name = { "@MaNV", "@TenNV", "@NS", "@gt", "@sodt", "@MaPB", "@Picture" };
            bool gt = rdNam.Checked == true ? true : false;
            object[] value = { txtMaNV.Text, txtTenNV.Text, dtpNS.Value, gt, txtSoDT.Text, cboPhongBan.SelectedValue, "" };
            if (DataAccess.updateData(sql, name, value) > 0)
            {
                MessageBox.Show("Thêm thành công");
                  loadData();
            }
            else
                MessageBox.Show("Thêm không thành công");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql = string.Format("update NhanVien set MaNV = @MaNV , TenNV = @TenNV ,ngaySinh = @ns , gioiTinh = @gt ," +
                " SoDT= @SoDT , MaPB = @MaPB , Picture = @Picture where MaNV ='{0}'",txtMaNV.Text);
            string[] name = { "@MaNV", "@TenNV", "@NS", "@gt", "@sodt", "@MaPB", "@Picture" };
            bool gt = rdNam.Checked == true ? true : false;
            object[] value = { txtMaNV.Text, txtTenNV.Text, dtpNS.Value, gt, txtSoDT.Text, cboPhongBan.SelectedValue, "" };
            if (DataAccess.updateData(sql, name, value) > 0)
            {
                MessageBox.Show("Sửa thành công");
                loadData();
            }
            else
                MessageBox.Show("Sửa không thành công");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from NhanVien where MaNV ='{0}'",txtMaNV.Text);
            if (DataAccess.updateData(sql) > 0)
            {
                MessageBox.Show("Xóa thành công");
                loadData();
            }
            else
                MessageBox.Show("Xóa không thành công");
        }

        private void data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             int i = data.CurrentCell.RowIndex;//Trả về dòng đang được chọn trên DataGridview
            txtMaNV.Text = data.Rows[i].Cells[0].Value.ToString();
            txtTenNV.Text = data.Rows[i].Cells[1].Value.ToString();
            dtpNS.Text = data.Rows[i].Cells[2].Value.ToString();
            string gt = data.Rows[i].Cells[3].Value.ToString();
            if (gt == "True")
            {
                rdNam.Checked = true;
            }
            else
                rdNu.Checked = true;
            txtSoDT.Text = data.Rows[i].Cells[4].Value.ToString();
            cboPhongBan.SelectedValue = data.Rows[i].Cells[5].Value.ToString();
        }
    }


       
}
