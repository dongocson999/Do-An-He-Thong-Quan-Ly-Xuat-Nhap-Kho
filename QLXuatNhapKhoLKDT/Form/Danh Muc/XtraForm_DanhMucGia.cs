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
using QLXuatNhapKhoLKDT.Classes.DanhMuc;

namespace QLXuatNhapKhoLKDT.Form.Danh_Muc
{
    public partial class XtraForm_DanhMucGia : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Khai báo đối tượng tương tác CSDL Danh Mục Giá
        ClassDanhMucGia Gia;

        //Tạo biến cho biết đang chỉnh sửa
        bool ChinhSua;

        public XtraForm_DanhMucGia()
        {
            InitializeComponent();
        }

        private void XtraForm_DanhMucGia_Load(object sender, EventArgs e)
        {
            Gia = new ClassDanhMucGia();

            //Load dữ liệu ra form
            LoadData();

            //Reset form về trạng thái ban đầu
            ResetForm();
        }

        //Load dữ liệu ra form
        private void LoadData()
        {
            Gia.LoadData(this);
        }

        //Hàm reset form về trạng thái ban đầu
        private void ResetForm()
        {
            //Gán biến chinh sửa
            ChinhSua = false;

            //Bật tắt chức năng button tương ứng
            btChinhSua.Enabled = false;
            btLuu.Enabled = false;
            btThoat.Enabled = true;

            //Tắt các textbox không được chỉnh sửa
            txtMaLK.Enabled = false;
            txtTenLK.Enabled = false;
            txtQuyCach.Enabled = false;
            txtGiaNhap.Enabled = false;
            txtChietKhau.Enabled = false;
            txtChenhLech.Enabled = false;
            txtChenhLechSauChietKhau.Enabled = false;
        }

        private void btChinhSua_Click(object sender, EventArgs e)
        {
            //Gán biến chỉnh sửa
            ChinhSua = true;

            //bật tắt chức năng button tương ứng
            btChinhSua.Enabled = false;
            btLuu.Enabled = true;
            btThoat.Enabled = false;

            //Để forcus tại textbox giá xuất bán
            txtGiaXuatBan.Focus();

            //Load dữ liệu lên textbox từ gridcontrol
            txtMaLK.Text = gridView1.GetFocusedRowCellValue("MA_LK").ToString();
            txtTenLK.Text = gridView1.GetFocusedRowCellValue("TEN_LK").ToString();
            txtQuyCach.Text = gridView1.GetFocusedRowCellValue("TENQUYCACH").ToString();
            txtGiaNhap.Text = gridView1.GetFocusedRowCellValue("DONGIANHAP").ToString();
            txtChietKhau.Text = gridView1.GetFocusedRowCellValue("CHIETKHAU").ToString();
            txtGiaXuatBan.Text = gridView1.GetFocusedRowCellValue("GIAXUATBAN").ToString();
            txtChenhLech.Text = gridView1.GetFocusedRowCellValue("CHENHLECH").ToString();
            txtChenhLechSauChietKhau.Text = gridView1.GetFocusedRowCellValue("CHENHLECHSAUCHIETKHAU").ToString();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (ChinhSua == false)
            {
                btChinhSua.Enabled = true;
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string malk = txtMaLK.Text;
                int giaban = int.Parse(txtGiaXuatBan.Text);
                bool kt = false;
                if (ChinhSua == true)
                {
                    kt = Gia.SuaGiaXuatBan(malk, giaban);
                }

                if (kt == true)
                {
                    //Load dữ liệu lại sau khi sửa xong
                    LoadData();

                    //Reset form sau khi sửa xong
                    ResetForm();

                    //Gán biến chỉnh sửa
                    ChinhSua = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}