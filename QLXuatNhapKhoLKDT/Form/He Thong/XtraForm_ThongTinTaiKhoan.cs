using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using QLXuatNhapKhoLKDT.Classes.HeThong;

namespace QLXuatNhapKhoLKDT.Form.He_Thong
{
    public partial class XtraForm_ThongTinTaiKhoan : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Khai báo đối tượng tương tác thông tin tài khoản
        ClassThongTinTaiKhoan TaiKhoan;

        public XtraForm_ThongTinTaiKhoan()
        {
            InitializeComponent();
        }

        private void XtraForm_ThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            TaiKhoan = new ClassThongTinTaiKhoan();
            LoadData();

            ResetForm();
        }

        //Hàm reset form về trạng thái ban đầu
        private void ResetForm()
        {
            //Bật tắt button chức năng
            btSua.Enabled = true;
            btLuu.Enabled = false;
            btHuy.Enabled = true;

            //Tắt textbox Mã NV
            txtMaNV.Enabled = false;
            txtHoTen.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtNgaySinh.Enabled = false;
            txtDienThoai.Enabled = false;
            txtCMND.Enabled = false;
            txtDiaChiTamTru.Enabled = false;
            txtDiaChiThuongTru.Enabled = false;
            txtusername.Enabled = false;
            txtpassword.Enabled = false;
        }

        //Hàm load dữ liệu lên form thông tin tài khoản
        public void LoadData()
        {
            string kq = TaiKhoan.LoadData(this);
            if (kq != "--")
            {
                string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                imageHinhNV.Image = Image.FromFile(path + "\\Images\\NhanVien\\" + kq);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            //Bật tắt chức năng 
            btSua.Enabled = false;
            btLuu.Enabled = true;
            btHuy.Enabled = false;

            //Bật textbox username và password
            txtusername.Enabled = true;
            txtpassword.Enabled = true;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            //Gọi hàm lưu thông tin vừa sửa
            string ma = txtMaNV.Text;
            string username = txtusername.Text;
            string pass = txtpassword.Text;
            TaiKhoan.SuaThongTinDangNhap(ma,username,pass);

            //Reset form lại trạng thái ban đầu
            ResetForm();

            //Load lại dữ liệu ra form
            LoadData();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}