using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLXuatNhapKhoLKDT.Classes.HeThong;

namespace QLXuatNhapKhoLKDT.Form.He_Thong
{
    public partial class XtraForm_DoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        //Khai báo đối tượng tương tác đổi mật khẩu
        ClassDoiMatKhau QLMatKhau;

        public XtraForm_DoiMatKhau()
        {
            InitializeComponent();
        }

        FormMain frm;

        public XtraForm_DoiMatKhau(FormMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            txtusername.Enabled = false;
            txtmatkhaucu.Focus();
        }

        private void XtraForm_DoiMatKhau_Load(object sender, EventArgs e)
        {
            QLMatKhau = new ClassDoiMatKhau();

            LoadDataUserName();
        }

        //Load dữ liệu lên textbox username
        public void LoadDataUserName()
        {
            txtusername.Text = QLMatKhau.UserName();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string matkhaucu = txtmatkhaucu.Text;
            string matkhaumoi_1 = txtmatkhaumoi_1.Text;
            string matkhaumoi_2 = txtmatkhaumoi_2.Text;

            if (QLMatKhau.DoiMatKhau(username, matkhaucu, matkhaumoi_1, matkhaumoi_2) == true)
            {
                //Ẩn Form chính khi chưa đăng nhập
                frm.Visible = false;

                //Trả giá trị của UserID về rỗng
                FormMain.UserId = "";

                //Load form login hệ thống -- đăng nhập
                XtraForm_DangNhap form = new XtraForm_DangNhap();
                form.ShowDialog();

                frm.Visible = true;

                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}