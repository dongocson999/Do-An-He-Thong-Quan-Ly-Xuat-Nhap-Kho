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
using QLXuatNhapKhoLKDT.Form.He_Thong;
using QLXuatNhapKhoLKDT.Classes.HeThong;

namespace QLXuatNhapKhoLKDT.Form.He_Thong
{
    public partial class XtraForm_QLNguoiDung : DevExpress.XtraEditors.XtraForm
    {
        //Khai báo đối tượng tương tác nhân viên
        ClassQuanLyNguoiDung QuanLyNguoiDung;

        //Tạo biến cho biết đang tạo mới hay chỉnh sửa
        bool TaoMoi = false;

        public XtraForm_QLNguoiDung()
        {
            InitializeComponent();
            QuanLyNguoiDung = new ClassQuanLyNguoiDung();
        }


        private void XtraForm_QLNguoiDung_Load(object sender, EventArgs e)
        {
            //Reset form ban đầu
            ResetForm();

            //Load dữ liệu lên gridView1
            LoadData();

            //Load dữ liệu lên combobox Nhóm quyền
            LoadDataComboboxNhomQuyen();
        }


        //Hàm load dữ liệu cho combobox Nhóm Quyền
        private void LoadDataComboboxNhomQuyen()
        {
            QuanLyNguoiDung.LoadComboMaQuyen(this);
        }

        //Hàm load dữ liệu lên form gridcontrol
        private void LoadData()
        {
            QuanLyNguoiDung.LoadData(this);
        }

        //Hàm resetform đưa về trạng thái ban đầu
        private void ResetForm()
        {
            //Bật tắt button trên form ban đầu
            btTaoMoi.Enabled = true;
            btChinhSua.Enabled = false;
            btLuu.Enabled = false;
            btXoa.Enabled = false;

            //Xóa rỗng textbox
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            DatetimeNgaySinh.Text = "";
            txtDiaChiTamTru.Text = "";
            txtDiaChiThuongTru.Text = "";
            txtDienThoai.Text = "";
            comboGioiTinh.SelectedIndex = 0;
            btChonHinh.Text = "Chọn Hình Đại Diện";
            txtUsername.Text = "";
            txtPassword.Text = "";
            comboNhomQuyen.Text = "";

            //Gán giá trị cho biến tạo mới
            TaoMoi = false;

            //Tắt textbox mã nhân viên vì được cấp tự động
            txtMaNV.Enabled = false;
        }


        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult thongbao = MessageBox.Show("Bạn có muốn thoại form quản lý nhân viên ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (thongbao == DialogResult.Yes)
            {
                this.Close();
            }
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


        //Tạo biến lưu thao tác chọn hình
        OpenFileDialog open;
        private void btChonHinh_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            open.ShowDialog();
        }

        private void btThemNhomQuyen_Click(object sender, EventArgs e)
        {
            XtraForm_PhanQuyen frm = new XtraForm_PhanQuyen();
            this.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (TaoMoi == false)
            {
                btChinhSua.Enabled = true;
                btXoa.Enabled = true;
                macanxoa = gridView1.GetFocusedRowCellValue("MANV").ToString();

                //Load dữ liệu xem trên form
                //Load dữ liệu từ GridControl lên textbox
                lblMaNV.Text = gridView1.GetFocusedRowCellValue("MANV").ToString();
                lblHoTen.Text = gridView1.GetFocusedRowCellValue("TENNV").ToString();
                lblNgaySinh.Text = gridView1.GetFocusedRowCellValue("NGAYSINH").ToString();
                bool gioitinh = (bool)gridView1.GetFocusedRowCellValue("GIOITINH");
                if (gioitinh == true)
                    lblGioiTinh.Text = "Nam";
                else
                    lblGioiTinh.Text = "Nữ";
                lblDiaChi.Text = gridView1.GetFocusedRowCellValue("DIACHI_THUONGTRU").ToString();
                lblCMND.Text = gridView1.GetFocusedRowCellValue("CMND_CANCUOC").ToString();
                lblDienThoai.Text = gridView1.GetFocusedRowCellValue("DIENTHOAI").ToString();
                string hinhanh = gridView1.GetFocusedRowCellValue("HINHANH").ToString().Trim();
                if (hinhanh != "--")
                {
                    string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    imageHinhNV.Image = Image.FromFile(path + "\\Images\\NhanVien\\" + hinhanh);
                    //imageHinhNV.Size = 
                }
                else
                {
                    imageHinhNV.Image = null;
                }
                string p = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                lblUsername.Text = gridView1.GetFocusedRowCellValue("USERNAME").ToString();
                lblPassword.Text = gridView1.GetFocusedRowCellValue("PASSWORD").ToString();
                lblPhanQuyen.Text = lblUsername.Text = gridView1.GetFocusedRowCellValue("MANHOMQUYEN").ToString();
            }
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {          
            ResetForm();

            //Gán biến tạo mới
            TaoMoi = true;

            //Tất button lien
            btTaoMoi.Enabled = false;
            btChinhSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = true;

            //Gán mã nhân viên tự động ra textbox
            txtMaNV.Text = QuanLyNguoiDung.CapMaNguoiDung();

            open = null;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaNV.Text;
                string ten = txtTenNV.Text;
                DateTime ngaysinh = Convert.ToDateTime(DatetimeNgaySinh.Text.ToString());
                bool gioitinh;
                if (comboGioiTinh.SelectedItem.ToString().Trim() == "Nam")
                {
                    gioitinh = true;
                }
                else
                {
                    gioitinh = false;
                }
                //MessageBox.Show(comboGioiTinh.SelectedItem.ToString());
                string dienthoai = txtDienThoai.Text;
                string diachitam = txtDiaChiTamTru.Text;
                string diachithuong = txtDiaChiThuongTru.Text;
                string username = txtUsername.Text;
                string pass = txtPassword.Text;
                string maquyen = comboNhomQuyen.SelectedValue.ToString();
                string cmnd = txtCMND.Text;
                if (TaoMoi == true)
                {
                    if (QuanLyNguoiDung.ThemNV(ma, ten, ngaysinh, gioitinh, cmnd, diachitam, diachithuong, dienthoai, open, username, pass, maquyen) == true)
                    {

                        //Load lại dữ liệu ra form sau khi thêm thành công
                        LoadData();

                        //resetform về ban đầu
                        ResetForm();

                        //Gán biến tạo mới
                        TaoMoi = false;
                    }
                }
                else
                {
                    if (QuanLyNguoiDung.SuaNV(ma, ten, ngaysinh, gioitinh, cmnd, diachitam, diachithuong, dienthoai, open, username, pass, maquyen) == true)
                    {
                        //Load lại dữ liệu ra form sau khi thêm thành công
                        LoadData();

                        //resetform về ban đầu
                        ResetForm();

                        //Gán biến tạo mới
                        TaoMoi = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btChinhSua_Click(object sender, EventArgs e)
        {
            open = null;

            //Gán giá trị cho biến tạo mới 
            TaoMoi = false;

            //Bật tất button liên quan
            btTaoMoi.Enabled = false;
            btChinhSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = true;

            //Load dữ liệu từ GridControl lên textbox
            txtMaNV.Text = gridView1.GetFocusedRowCellValue("MANV").ToString();
            txtTenNV.Text = gridView1.GetFocusedRowCellValue("TENNV").ToString();
            DatetimeNgaySinh.Text =  gridView1.GetFocusedRowCellValue("NGAYSINH").ToString();
            bool gioitinh =  (bool)gridView1.GetFocusedRowCellValue("GIOITINH");
            if (gioitinh == true)
                comboGioiTinh.SelectedItem = "Nam";
            else
                comboGioiTinh.SelectedItem = "Nữ";
            txtDiaChiTamTru.Text = gridView1.GetFocusedRowCellValue("DIACHI_TAMTRU").ToString();
            txtDiaChiThuongTru.Text = gridView1.GetFocusedRowCellValue("DIACHI_THUONGTRU").ToString();
            txtDienThoai.Text = gridView1.GetFocusedRowCellValue("DIENTHOAI").ToString();
            txtCMND.Text = gridView1.GetFocusedRowCellValue("CMND_CANCUOC").ToString();
            string hinhanh = gridView1.GetFocusedRowCellValue("HINHANH").ToString().Trim();
            if (hinhanh != "--")
            {
                string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                imageHinhDaiDien.Image = Image.FromFile(path + "\\Images\\NhanVien\\" + hinhanh);
            }
            else
            {
                imageHinhDaiDien.Image = null;
            }
            txtUsername.Text = gridView1.GetFocusedRowCellValue("USERNAME").ToString().Trim();
            txtPassword.Text = gridView1.GetFocusedRowCellValue("PASSWORD").ToString().Trim();
        }


        string macanxoa = "";
        private void btXoa_Click(object sender, EventArgs e)
        {
            QuanLyNguoiDung.XoaNV(macanxoa);

            //Load lại dữ liệu ra form 
            LoadData();

            //Reset form về trạng thái ban đầu
            ResetForm();
        }

        private void comboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboGioiTinh.SelectedItem.ToString().Trim());
        }
    }
}