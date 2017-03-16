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
    public partial class XtraForm_DangNhap : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Tạo đối tượng kiểm tra việc đăng nhập vào hệ thống
        ClassDangNhap DangNhap;


        public XtraForm_DangNhap()
        {
            InitializeComponent();
            
        }

        private void XtraForm_DangNhap_Load(object sender, EventArgs e)
        {
            DangNhap = new ClassDangNhap();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text;
            string pass = txtMatKhau.Text;
            bool KT = DangNhap.TimMaNhanVien(username,pass);
            if (KT == true)
            {
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        
    }
}