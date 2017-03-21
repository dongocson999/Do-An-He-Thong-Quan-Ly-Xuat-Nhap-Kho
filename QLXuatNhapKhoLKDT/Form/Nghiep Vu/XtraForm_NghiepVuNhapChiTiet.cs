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
        public List<ClassChiTietLinhKien> DanhSachChiTietLinhKien;

        public List<string> DanhSachLinhKienXoa;

        //Khai báo biến lưu giá trị true-false cho biết đang thêm mới linh kiện
        bool TaoMoi;

        //Các biến lưu thông tin phiếu nhập
        string sophieu = "";
        string lydo = "";
        string mancc = "";
        string makho = "";
        DateTime ngaylap = new DateTime();


        //Biến dùng để phân biệt thêm mới chi tiết và xem chi tiết
        bool ThemPN; //= true - đang thêm phiếu nhập             = false - đang xem phiếu nhập


        public XtraForm_NghiepVuNhapChiTiet(string sophieu, bool k)
        {
            InitializeComponent();
            NghiepVuNhap = new ClassNghiepVuNhap();
            this.sophieu = "";
            this.lydo = "";
            this.mancc = "";
            this.makho = "";
            this.ngaylap = new DateTime();

            ThemPN = k;

        }

        public XtraForm_NghiepVuNhapChiTiet(string sophieu, string mancc, string makho, string lydo, DateTime ngaylap, bool k)
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

            ThemPN = k;
        }

        //Load dữ liệu cho form chi tiet phiếu nhập
        private void LoadData(string sophieu)
        {
            if (ThemPN == false)
            {
                NghiepVuNhap.LayDuLieuChiTietPhieuNhap(sophieu,this);
            }
            gridControl_DanhMucChiTietPhieuNhap.DataSource = DanhSachChiTietLinhKien;
        }


        private void XtraForm_NghiepVuNhapChiTiet_Load(object sender, EventArgs e)
        {

            //Khởi tạo đối tượng lưu danh sách linh kiện
            DanhSachChiTietLinhKien = new List<ClassChiTietLinhKien>();

            DanhSachLinhKienXoa = new List<string>();

            //Reset tất cả chức năng về trạng thái ban đầu
            ResetForm();

            if (ThemPN == false)
            {
                //Load dữ liệu ra form
                LoadData(sophieu);
            }
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
            comboTimLinhKien.Text = "";
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
            comboTimLinhKien.Focus();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (TaoMoi == true)
            {
                ClassChiTietLinhKien ChiTiet = new ClassChiTietLinhKien();
                ChiTiet.STT = DanhSachChiTietLinhKien.Count() + 1;
                var chuoi = comboTimLinhKien.SelectedValue.ToString().Split('-');
                string malk = chuoi[0];
                ChiTiet.MaLK = malk;
                ChiTiet.SoLuong = int.Parse(numer_SoLuong.Value.ToString());
                ChiTiet.DonGia = int.Parse(txtDonGia.Text);
                ChiTiet.ChietKhau = float.Parse(txtChietKhau.Text);
                ChiTiet.ThanhTien = (int.Parse(numer_SoLuong.Value.ToString()) * int.Parse(txtDonGia.Text)) - ((int.Parse(numer_SoLuong.Value.ToString()) * int.Parse(txtDonGia.Text)) * float.Parse(txtChietKhau.Text));
                ChiTiet.QuyCach = chuoi[2];

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


        private void btHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                if (ThemPN == false)
                {
                    //Lưu thông tin phiếu nhập
                    NghiepVuNhap.ThemPhieuNhap(sophieu, lydo, FormMain.UserId, mancc, makho, ngaylap);

                    //Lưu thông tin chi tiết của phiếu nhập trên
                    foreach (var item in DanhSachChiTietLinhKien)
                    {
                        NghiepVuNhap.Them_CT_PhieuNhap(sophieu, item.MaLK, item.SoLuong, item.DonGia, item.ChietKhau.ToString());
                    }
                }
                else
                {
                    //Lưu thông tin chi tiết của phiếu nhập trên
                    foreach (var item in DanhSachChiTietLinhKien)
                    {
                        var ob = NghiepVuNhap.TimKiemLinhKienTheoSoPhieuNhap(item.MaLK,sophieu);
                        if(ob != null)
                        {
                            NghiepVuNhap.Sua_ChiTiet_PhieuNhap(sophieu,item.MaLK,item.SoLuong,item.DonGia,item.ChietKhau.ToString());
                        }
                        else
                        {
                            NghiepVuNhap.Them_CT_PhieuNhap(sophieu, item.MaLK, item.SoLuong, item.DonGia, item.ChietKhau.ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}