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
using System.IO;

namespace QLXuatNhapKhoLKDT.Form.He_Thong
{
    public partial class XtraForm_QLNguoiDung : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_QLNguoiDung()
        {
            InitializeComponent();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        

        private void btThoat_Click(object sender, EventArgs e)
        {

        }

        private void btThoat_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "Tắt Form Quản Lý Người Dùng !!";
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "--";
        }

        private void btTaoMoi_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "Tạo Mới Thông Tin Người Dùng !!";
        }

        private void btChinhSua_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "Chỉnh Sửa Thông Tin Người Dùng !!";
        }

        private void btLuu_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "Lưu Thông Tin Sau Các Thao Tác Chỉnh Sửa !!";
        }

        private void btXoa_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "Xóa Thông Tin 1 Người Dùng Trong Danh Mục !!";
        }

        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            lblGhiChu.Text = "Thêm Nhóm Quyền !!";
        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btChonHinh_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            open.ShowDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    string CorrectFileName = Path.GetFileName(open.FileName);
                    File.Copy(open.FileName, path + "\\Images\\NhanVien\\" + CorrectFileName);
                }

            }
        }
    }
}