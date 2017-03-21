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
using QLXuatNhapKhoLKDT.FormInfo;

namespace QLXuatNhapKhoLKDT
{
    public partial class XtraForm_Info : DevExpress.XtraEditors.XtraForm
    {
        //Tạo đối tượng truy xuất xử lý ThongTinChung
        ClassThongTinChung ThongTinChung = new ClassThongTinChung();


        public XtraForm_Info()
        {
            InitializeComponent();
        }


        //Load dữ liệu lên form doanh số nhập xuất
        private void LoadData_DoanhSo()
        {
            var dl = ThongTinChung.LoadData_Nhap_Xuat();
            gridControl_DoanhSo_TongTriGia.DataSource = dl;
        }


        //Load dữ liệu lên form thu - chi - công nợ
        private void LoadData_ThuChi()
        {
            var dl = ThongTinChung.LoadData_Thu_Chi_CongNo();
            gridControl_ThuChiCongNo.DataSource = dl;
        }


        //Load dữ liệu lên form Chức năng chung hệ thống
        private void LoadData_ChucNangChung()
        {
            var dl = ThongTinChung.LoadData_ChucNangChungHeThong();
            gridControl_ChucNangChungHeThong.DataSource = dl;
        }

        private void XtraForm_Info_Load(object sender, EventArgs e)
        {
            LoadData_DoanhSo();
            LoadData_ThuChi();
            LoadData_ChucNangChung();
        }
    }
}