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

        //Tạo biến tạo mới
        bool TaoMoi = false;

        public XtraForm_NghiepVuNhapKho()
        {
            InitializeComponent();
            
        }

        private void XtraForm_NghiepVuNhapKho_Load(object sender, EventArgs e)
        {
            NghiepVuNhap = new ClassNghiepVuNhap();

            //Gọi hàm resetform trả về form ban đầu khi mới load form
            ResetForm();

            //Loaddata lên form phiếu nhập
            LoadData();
        }

        //Load dữ liệu lên form phiếu nhập
        public void LoadData()
        {
            var dulieu = NghiepVuNhap.LayDuLieuPhieuNhap();
            gridControl_DanhMucPhieuNhap.DataSource = dulieu;

            NghiepVuNhap.LoadDataToCombo(this);
        }

        //ResetForm
        private void ResetForm()
        {
            //Ẩn Hiện chức năng ban đầu
            btTaoMoi.Enabled = true;
            btChinhSua.Enabled = false;
            btLuu.Enabled = false;
            btXuatExcel.Enabled = true;
            btChiTietPhieuNhap.Enabled = false;
            btXem.Enabled = false;

            //Xóa rỗng các textbox
            comboKhoHang.Text = "";
            comboNCC.Text = "";
            txtMaNVLapPhieu.Enabled = false;
            txtMaNVLapPhieu.Text = "";
            txtLyDoNhap.Text = "";

            //Gán text ban đầu cho button TaoMoi
            btTaoMoi.Text = "Tạo Mới";

            //Gán biến tạo mới
            TaoMoi = false;

            txtSoPhieuNhap.Enabled = false;
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            //Gán biến tạo mới 
            TaoMoi = true;

            //Bật tắt các button khác
            btTaoMoi.Enabled = false;
            btTaoMoi.Text = "Đang Tạo";
            btChinhSua.Enabled = false;
            btLuu.Enabled = false;
            btXuatExcel.Enabled = false;
            btChiTietPhieuNhap.Enabled = true;

            //Load dữ liệu Mã Nhân Viên lên textboxMaNV
            txtMaNVLapPhieu.Text = FormMain.UserId;

            //Đặt focus tại textbox nhà cung cấp
            comboNCC.Focus();

            //Load dữ liệu cho textbox số phiếu nhập
            txtSoPhieuNhap.Text = NghiepVuNhap.CapMaSoPhieuNhap();
        }


        private void btChiTietPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                //Lấy Số phiếu nhập được cấp tự động từ hệ thống
                string sophieu = gridView1.GetFocusedRowCellValue("SOPHIEUNHAP").ToString();

                //Gọi form chi tiết phiếu nhập và đưa dữ liệu tìm được qua
                XtraForm_NghiepVuNhapChiTiet frm = new XtraForm_NghiepVuNhapChiTiet(sophieu, false);
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btChinhSua_Click(object sender, EventArgs e)
        {
            //Gán biến tạo mới
            TaoMoi = false;

            //Bật tắt chức năng tương ứng
            btTaoMoi.Enabled = false;
            btLuu.Enabled = true;
            btChinhSua.Enabled = false;
            btXuatExcel.Enabled = false;
            btChiTietPhieuNhap.Enabled = false;

            //Load dữ liệu lên textbox từ gridcontrol
            txtSoPhieuNhap.Text = gridView1.GetFocusedRowCellValue("SOPHIEUNHAP").ToString();
            comboNCC.SelectedValue = gridView1.GetFocusedRowCellValue("MANCC").ToString();
            comboKhoHang.SelectedValue = gridView1.GetFocusedRowCellValue("MAKHOHANG").ToString();
            txtMaNVLapPhieu.Text = FormMain.UserId;
            dateTimePicker1.Text = gridView1.GetFocusedRowCellValue("NGAYNHAP").ToString();
            txtLyDoNhap.Text = gridView1.GetFocusedRowCellValue("LYDONHAP").ToString();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (TaoMoi == false)
            {
                btChinhSua.Enabled = true;
                btXem.Enabled = true;
            }
        }

        private void btXem_Click(object sender, EventArgs e)
        {
            string sophieunhap = gridView1.GetFocusedRowCellValue("SOPHIEUNHAP").ToString();
            NghiepVuNhap.LoadDataChiTietPhieuNhap(this,sophieunhap);
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Lấy giá trị vừa tìm được trên form Nghiệp Vụ Nhập để đưa qua form Chi Tiết phiếu nhập
                string mancc = comboNCC.SelectedValue.ToString();

                string makho = comboKhoHang.SelectedValue.ToString();

                string lydo = txtLyDoNhap.Text;

                DateTime ngaylap = DateTime.Parse(dateTimePicker1.Text);

                //Lấy Số phiếu nhập được cấp tự động từ hệ thống
                string sophieu = txtSoPhieuNhap.Text;

                if (TaoMoi == true)
                {
                    //Gọi form chi tiết phiếu nhập và đưa dữ liệu tìm được qua
                    XtraForm_NghiepVuNhapChiTiet frm = new XtraForm_NghiepVuNhapChiTiet(sophieu, mancc, makho, lydo, ngaylap, true);
                    frm.MdiParent = this.MdiParent;
                    frm.Show();

                    //Load lại dữ liệu sau khi thêm
                    LoadData();

                    ResetForm();
                }
                else
                {
                    NghiepVuNhap.SuaPhieuNhap(sophieu,ngaylap,lydo,mancc,makho);

                    //Load lại dữ liệu sau khi thêm
                    LoadData();

                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}