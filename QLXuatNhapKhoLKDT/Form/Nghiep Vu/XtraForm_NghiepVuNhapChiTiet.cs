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
using QLXuatNhapKhoLKDT.Classes.NghiepVu;

namespace QLXuatNhapKhoLKDT.Form.Nghiep_Vu
{
    public partial class XtraForm_NghiepVuNhapChiTiet : DevExpress.XtraEditors.XtraForm
    {
        //Khai báo đối tượng lưu những thao tác nghiệp vụ
        ClassNghiepVuNhap NghiepVuNhap;

        //Khai báo đối tượng lưu chi tiết linh kiện
        List<ClassChiTietLinhKien> DanhSachChiTietLinhKien;

        //Khai báo biến lưu giá trị true-false cho biết đang thêm mới linh kiện
        bool TaoMoi;

        //Các biến lưu thông tin phiếu nhập
        string sophieu = "";
        string lydo = "";
        string mancc = "";
        string makho = "";
        DateTime ngaylap = new DateTime();



        public XtraForm_NghiepVuNhapChiTiet()
        {
            InitializeComponent();
            NghiepVuNhap = new ClassNghiepVuNhap();
        }

        public XtraForm_NghiepVuNhapChiTiet(string sophieu, string lydo, string mancc, string makho, DateTime ngaylap)
        {
            InitializeComponent();
            NghiepVuNhap = new ClassNghiepVuNhap();
            this.sophieu = sophieu;
            this.lydo = lydo;
            this.mancc = mancc;
            this.makho = makho;
            this.ngaylap = ngaylap;

            txtChietKhau.Text = "0";
            txtDonGia.Text = "0";
        }

        private void XtraForm_NghiepVuNhapChiTiet_Load(object sender, EventArgs e)
        {

            //Khởi tạo đối tượng lưu danh sách linh kiện
            DanhSachChiTietLinhKien = new List<ClassChiTietLinhKien>();

            //Reset tất cả chức năng về trạng thái ban đầu
            ResetForm();
        }

        //Hàm load dữ liệu từ DanhSachChiTietLinhKien len gridcontrol
        public void LoadDataToGridControl_CT_LinhKien()
        {
            gridControl_DanhMucChiTietPhieuNhap.DataSource = DanhSachChiTietLinhKien;
        }


        //Hàm resetform về ban đầu
        private void ResetForm()
        {
            //Bật tắt chức năng khi mới load form
            btTaoMoi.Enabled = true;
            btChinhSua.Enabled = true;
            btXoa.Enabled = true;
            btLuu.Enabled = false;
            btHoanThanh.Enabled = false;

            //Reset giá trị của textbox về rỗng
            txtTimLinhKien.Text = "";
            numer_SoLuong.Value = 0;
            txtDonGia.Text = "";
            txtChietKhau.Text = "";
            txtThanhTien.Text = "";

            //Các giá trị ban đầu
            TaoMoi = true;

            //Gán thuộc tính text cho button TaoMoi
            btTaoMoi.Text = "Tạo Mới";
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            //Khởi tạo giá trị tạo mới
            TaoMoi = true;

            //Reset form về trạng thái ban đầu
            ResetForm();

            //Bật tắt một số button
            btTaoMoi.Enabled = false;
            btTaoMoi.Text = "Đang Thêm";
            btChinhSua.Enabled = false;
            btXoa.Enabled = false;
            btHoanThanh.Enabled = false;
            btLuu.Enabled = true;

            //Để focus ở tìm linh kiện
            txtTimLinhKien.Focus();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (TaoMoi == true)
            {
                ClassChiTietLinhKien ChiTiet = new ClassChiTietLinhKien();
                ChiTiet.STT = DanhSachChiTietLinhKien.Count() + 1;
                var chuoi = txtTimLinhKien.Text.Split('-');
                string malk = chuoi[0];
                ChiTiet.MaLK = malk;
                ChiTiet.SoLuong = int.Parse(numer_SoLuong.Value.ToString());
                ChiTiet.DonGia = int.Parse(txtDonGia.Text);
                ChiTiet.ChietKhau = float.Parse(txtChietKhau.Text);
                ChiTiet.ThanhTien = (int.Parse(numer_SoLuong.Value.ToString()) * int.Parse(txtDonGia.Text)) - ((int.Parse(numer_SoLuong.Value.ToString()) * int.Parse(txtDonGia.Text)) * float.Parse(txtChietKhau.Text));

                //Đưa 1 chi tiết vào danh sách chi tiết linh kiện
                DanhSachChiTietLinhKien.Add(ChiTiet);

                //Load lại dữ liệu ra GridControl
                LoadDataToGridControl_CT_LinhKien();
            }
            else
            {
            }
        }

        private void txtChietKhau_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtThanhTien.Text = (Int32.Parse(numer_SoLuong.Value.ToString()) * int.Parse(txtDonGia.Text) * float.Parse(txtChietKhau.Text)).ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtTimLinhKiem_EditValueChanged(object sender, EventArgs e)
        {
            string chuoi = txtTimLinhKien.Text;
            txtTimLinhKien.Text = NghiepVuNhap.TimKiem_LinhKien(chuoi);
        }

        private void btHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                //Lưu thông tin phiếu nhập
                NghiepVuNhap.ThemPhieuNhap(sophieu,lydo,FormMain.UserId,mancc,makho,ngaylap);

                //Lưu thông tin chi tiết của phiếu nhập trên
                foreach (var item in DanhSachChiTietLinhKien)
                {
                    NghiepVuNhap.Them_CT_PhieuNhap(sophieu, item.MaLK, item.SoLuong, item.DonGia, item.ChietKhau);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}