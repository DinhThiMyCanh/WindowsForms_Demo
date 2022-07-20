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
    public partial class frmNhanVien : Form
    {
        SqlConnection cn;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder builder;
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            string st = @"Data Source=CANH-DHQN\SQLEXPRESS;Initial Catalog=QLNV;Integrated Security=True";
            // cn = new SqlConnection(st);
            cn = new SqlConnection();
            cn.ConnectionString = st;
            ds = new DataSet();
            loadPB();
            loadData();
            builder = new SqlCommandBuilder(da);

        }
        //Đổ dư liệu lên Combobox Phòng ban
        public void loadPB()
        {
            string sql = "select * from PhongBan";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "PhongBan");
            cboPhongBan.DataSource = ds.Tables["PhongBan"];
            cboPhongBan.DisplayMember = "tenPB";
            cboPhongBan.ValueMember = "MaPB";

        }
        //Đổ dư liệu lên DataGridview Nhan viên
        public void loadData()
        {
            string sql = "select * from NhanVien";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "NhanVien");
            data.DataSource = ds.Tables["NhanVien"];
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string sql = "select B.TenPB, count(A.MaPB) as SoLuongNV from NhanVien as A, PhongBan as B " +
                "where A.MaPB=B.MaPB group by B.TenPB";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "ThongKe");
            data.DataSource = ds.Tables["ThongKe"];
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from NhanVien where tennv Like N'%{0}'",txtTimKiem.Text);
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "TimKiem");
            data.DataSource = ds.Tables["TimKiem"];
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtMaNV.Focus();


            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ktraMaNV(txtMaNV.Text) == 0)
            {
                DataTable dt = ds.Tables["NhanVien"];
                DataRow r = dt.NewRow();
                r[0] = txtMaNV.Text;
                r[1] = txtTenNV.Text;
                r[2] = dtpNS.Value.ToString();
                r[3] = rdNam.Checked == true ? true : false;
                r[4] = txtSoDT.Text;
                r[5] = cboPhongBan.SelectedValue.ToString();
                r[6] = "";
                dt.Rows.Add(r);

                //Cập nhật dữ liệu xuống Database
                da.Update(ds, "NhanVien");
            }
            else
                MessageBox.Show("Bị trùng mã nhân viên");
            

        }
        //Phương thức kiểm tra mã nhân viên bị trùng
        public int ktraMaNV(string ma)
        {
            string sql = string.Format("select count(*) from NhanVien where maNV ='{0}'",txtMaNV.Text);
            cn.Open();
            SqlCommand cmd = new SqlCommand(sql, cn);
            int sl = (int) cmd.ExecuteScalar();
            cn.Close();
            return sl;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
                int i = data.CurrentCell.RowIndex;
                DataTable dt = ds.Tables["NhanVien"];
                DataRow r = dt.Rows[i];
                r[0] = txtMaNV.Text;
                r[1] = txtTenNV.Text;
                r[2] = dtpNS.Value.ToString();
                r[3] = rdNam.Checked == true ? true : false;
                r[4] = txtSoDT.Text;
                r[5] = cboPhongBan.SelectedValue.ToString();
                r[6] = "";
                
                //Cập nhật dữ liệu xuống Database
                da.Update(ds, "NhanVien");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rd = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rd ==DialogResult.Yes)
            {
                int i = data.CurrentCell.RowIndex;//Trả về dòng đang được chọn trên DataGridview
                DataTable dt = ds.Tables["NhanVien"];
                dt.Rows[i].Delete();

                //Cập nhật dữ liệu xuống Database
                da.Update(ds, "NhanVien");
            }    
           
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
