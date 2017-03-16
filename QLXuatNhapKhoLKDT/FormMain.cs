using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLXuatNhapKhoLKDT.Form.He_Thong;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;
using QLXuatNhapKhoLKDT.Form.Nghiep_Vu;
using QLXuatNhapKhoLKDT.Classes.HeThong;

namespace QLXuatNhapKhoLKDT
{
    public partial class FormMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Biến Lưu User Đang Đăng Nhập Hiện Tại
        static public string UserId = "";

        //Khởi tạo biến phân quyền hệ thống
        ClassPhanQuyen PhanQuyen;


        public FormMain()
        {
            InitializeComponent();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            //Hiển thị ngày tháng trên giao diện form chính
            lblDate.Text = DateTime.Now.ToShortDateString();

            //Ẩn Form chính khi chưa đăng nhập
            this.Visible = false;

            //Load form login hệ thống -- đăng nhập
            XtraForm_DangNhap form = new XtraForm_DangNhap();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
                this.Close();
            if (form.DialogResult == DialogResult.OK)
            {
                this.Visible = true;

                //Khởi tạo đối tượng phân quyền
                PhanQuyen = new ClassPhanQuyen();

                //Gọi hàm tắt tất cả chức năng hệ thông khi chưa phân quyền
                PhanQuyen.TatChucNangHeThong(this);

                //Gọi hàm load form info
                LoadForm_Info();

                //Load info thông tin user và chức vụ phía dưới trang chủ
                lbl_TenNV.Text = "Nhân Viên: " + ClassDangNhap.TenUser;
                lbl_ChucVu.Text = "Chức Vụ: " + ClassDangNhap.ChucVu;

                //Phân quyền sau khi đăng nhập vào hệ thống
                PhanQuyen.ThucHienPhanQuyen(this);
            }
        }

        //Hàm kiểm tra form con có đang Active hay không ------------------------------------------
        private bool CheckExistForm(XtraForm form)
        {
            foreach (var child in this.MdiChildren)
            {
                if (child.Name == form.Name)
                {
                    child.Activate();
                    return true;
                }
            }
            return false;
        }


        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_QLNguoiDung form_QL_ND = new XtraForm_QLNguoiDung();
            if (CheckExistForm(form_QL_ND)) return;
            form_QL_ND = new XtraForm_QLNguoiDung();
            form_QL_ND.MdiParent = this;
            form_QL_ND.Show();
            
        }

       

        private void barButtonItem_MenuPupop_HeThong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_HeThong.ShowPopup(new Point(500,300));
        }

        private void barButtonItem_MenuPupop_Thoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem_MenuPupop_DanhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_DanhMuc.ShowPopup(new Point(500,300));
        }

        private void barButtonItem_MenuPupop_NghiepVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_NghiepVu.ShowPopup(new Point(500,300));
        }

        private void barButtonItem_MenuPopup_QuayLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_NghiepVu.HidePopup();
        }

        private void barButtonItem109_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem_MenuPupop_BaoHanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_BaoHanh.ShowPopup(new Point(500,300));
        }

        private void barButtonItem_MenuPupop_BaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_BaoCao.ShowPopup(new Point(500,300));
        }

        private void barButtonItem_MenuPupop_CongCu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radialMenu_CongCu.ShowPopup(new Point(500, 300));
        }

        private void barButtonItem_MenuPopup_UserInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_QLNguoiDung form_QL_ND = new XtraForm_QLNguoiDung();
            if (CheckExistForm(form_QL_ND)) return;
            form_QL_ND = new XtraForm_QLNguoiDung();
            form_QL_ND.MdiParent = this;
            form_QL_ND.Show();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_PhanQuyen form = new XtraForm_PhanQuyen();
            if (CheckExistForm(form)) return;
            form = new XtraForm_PhanQuyen();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem_MenuPopup_RightsGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_PhanQuyen form = new XtraForm_PhanQuyen();
            if (CheckExistForm(form)) return;
            form = new XtraForm_PhanQuyen();
            form.MdiParent = this;
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToShortTimeString();
        }

        

        //---------------Load Form XtraForm_Info ---- Thông Tin Chung
        private void LoadForm_Info()
        {
            XtraForm_Info form = new XtraForm_Info();
            if (CheckExistForm(form)) return;
            form = new XtraForm_Info();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_LKDT form = new XtraForm_LKDT();
            if (CheckExistForm(form)) return;
            form = new XtraForm_LKDT();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadForm_Info();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_QuyCach form = new XtraForm_QuyCach();
            if (CheckExistForm(form)) return;
            form = new XtraForm_QuyCach();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem87_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ẩn Form chính khi chưa đăng nhập
            this.Visible = false;

            //Load form login hệ thống -- đăng nhập
            XtraForm_DangNhap form = new XtraForm_DangNhap();
            form.ShowDialog();

            //Trả giá trị của UserID về rỗng
            UserId = "";
        }

        private void barButtonItem89_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_DoiMatKhau form = new XtraForm_DoiMatKhau();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }

        private void btThongTinTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_ThongTinTaiKhoan form = new XtraForm_ThongTinTaiKhoan();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_LoaiLKDT form = new XtraForm_LoaiLKDT();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_NhaSanXuat form = new XtraForm_NhaSanXuat();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_DanhMucKho form = new XtraForm_DanhMucKho();
            if (CheckExistForm(form)) return;
            form = new XtraForm_DanhMucKho();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_DanhMucGia form = new XtraForm_DanhMucGia();
            if (CheckExistForm(form)) return;
            form = new XtraForm_DanhMucGia();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_DanhMucNhaCungCap form = new XtraForm_DanhMucNhaCungCap();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_DanhMucKhachHang form = new XtraForm_DanhMucKhachHang();
            if (CheckExistForm(form)) return;
            form = new XtraForm_DanhMucKhachHang();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_LoaiKhachHang form = new XtraForm_LoaiKhachHang();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_NghiepVuNhapKho form = new XtraForm_NghiepVuNhapKho();
            if (CheckExistForm(form)) return;
            form = new XtraForm_NghiepVuNhapKho();
            form.MdiParent = this;
            form.Show();
        }
    }
}
