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
using QLXuatNhapKhoLKDT.Classes.NghiepVu;

namespace QLXuatNhapKhoLKDT.Form.Nghiep_Vu
{
    public partial class XtraForm_NghiepVuNhapKho : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Khởi tạo đối tượng nghiệp vụ nhập phiếu 
        ClassNghiepVuNhap NghiepVuNhap;

        public XtraForm_NghiepVuNhapKho()
        {
            InitializeComponent();
            NghiepVuNhap = new ClassNghiepVuNhap();
        }

        private void XtraForm_NghiepVuNhapKho_Load(object sender, EventArgs e)
        {
            //Gọi hàm resetform trả về form ban đầu khi mới load form
            ResetForm();
        }

        //ResetForm
        private void ResetForm()
        {
            //Ẩn Hiện chức năng ban đầu
            btTaoMoi.Enabled = true;
            btChinhSua.Enabled = false;
            btLuu.Enabled = false;
            btXoa.Enabled = false;
            btXuatExcel.Enabled = true;
            btChiTietPhieuNhap.Enabled = false;

            //Xóa rỗng các textbox
            txtTimNhaCungCap.Text = "";
            txtTimKhoHang.Text = "";
            txtMaNVLapPhieu.Enabled = false;
            txtMaNVLapPhieu.Text = "";
            txtLyDoNhap.Text = "";

            //Gán text ban đầu cho button TaoMoi
            btTaoMoi.Text = "Tạo Mới";

            //Gán thuộc tính đầu tiên cho MaNCC và MaKho bằng rỗng
            mancc = "";
            makho = "";
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            //Bật tắt các button khác
            btTaoMoi.Enabled = false;
            btTaoMoi.Text = "Đang Tạo";
            btChinhSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = true;
            btXuatExcel.Enabled = false;
            btChiTietPhieuNhap.Enabled = true;

            //Load dữ liệu Mã Nhân Viên lên textboxMaNV
            txtMaNVLapPhieu.Text = FormMain.UserId;

            //Đặt focus tại textbox nhà cung cấp
            txtTimNhaCungCap.Focus();
        }


        //Tạo 2 biến lưu MãNCC và MãKho
        string mancc = "";
        string makho = "";

        private void btChiTietPhieuNhap_Click(object sender, EventArgs e)
        {
            //Lấy giá trị vừa tìm được trên form Nghiệp Vụ Nhập để đưa qua form Chi Tiết phiếu nhập
            var ncc = txtTimNhaCungCap.Text.Split('-');
            mancc = ncc[0].ToString().Trim();

            var kho = txtTimKhoHang.Text.Split('-');
            makho = kho[0].ToString().Trim();

            string lydo = txtLyDoNhap.Text;

            DateTime ngaylap = DateTime.Parse(dateTimePicker1.Text);

            //Lấy Số phiếu nhập được cấp tự động từ hệ thống
            string sophieu = NghiepVuNhap.LaySoPhieuNhap();

            //Gọi form chi tiết phiếu nhập và đưa dữ liệu tìm được qua
            XtraForm_NghiepVuNhapChiTiet frm = new XtraForm_NghiepVuNhapChiTiet();
            frm = new XtraForm_NghiepVuNhapChiTiet();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}