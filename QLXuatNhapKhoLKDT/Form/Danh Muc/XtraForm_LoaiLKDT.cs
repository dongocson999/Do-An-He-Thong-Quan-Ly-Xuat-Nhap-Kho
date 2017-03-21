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
    public partial class XtraForm_LoaiLKDT : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Khai báo đối tượng tương tác loại linh kiện
        ClassLoaiLinhKien LoaiLinhKien;

        //Tạo biến lưu hành động đang Thêm hoặc Sửa
        bool TaoMoi = false;

        public XtraForm_LoaiLKDT()
        {
            InitializeComponent();
        }

        private void XtraForm_LoaiLKDT_Load(object sender, EventArgs e)
        {
            //Khởi tạo đối tượng tương tác loại linh kiện
            LoaiLinhKien = new ClassLoaiLinhKien();

            //Gọi hàm load dữ liệu cho loại linh kiện gridcontrol
            LoadData();

            //Gọi hàm resetform đưa về trạng thái ban đầu
            ResetForm();
        }

        //Load dữ liệu lên GridControl
        private void LoadData()
        {
            LoaiLinhKien.LoadData(this);
        }

        //Hàm resetform đưa về mặc định ban đầu
        private void ResetForm()
        {
            //Bật tắt chức năng ban đầu 
            btThem.Enabled = true;
            btSua.Enabled = false;
            btLuu.Enabled = false;
            btXoa.Enabled = false;

            //Xóa dữ liệu trên textbox
            txtMaLoai.Text = "";
            txtTenLoai.Text = "";
            txtGhiChu.Text = "";

            //Tắt textbox Mã loại vì được cấp tự động - không nhập
            txtMaLoai.Enabled = false;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            //Gán giá trị cho biết đang thêm mới 
            TaoMoi = true;

            //Tắt các chức năng liên quan
            btThem.Enabled = false;
            btLuu.Enabled = true;
            btSua.Enabled = false;
            btXoa.Enabled = false;

            //Xóa trống các textbox
            txtTenLoai.Text = "";
            txtMaLoai.Text = "";
            txtGhiChu.Text = "";

            //Gán mã tự động cho textbox Mã Loại
            txtMaLoai.Text = LoaiLinhKien.CapMaTuDong();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                bool kt = false;
                string ma = txtMaLoai.Text;
                string ten = txtTenLoai.Text;
                string ghichu = txtGhiChu.Text;
                if (TaoMoi == true)
                {
                    kt = LoaiLinhKien.ThemLoai(ma,ten,ghichu);
                }
                else
                {
                    kt =LoaiLinhKien.SuaLoai(ma,ten,ghichu);
                }

                if (kt == true)
                {
                    //Tắt chức năng tạo mới 
                    TaoMoi = false;

                    //resetform
                    ResetForm();

                    //Load lại dữ liệu sau khi thêm 
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            //Tắt chức năng tạo mới
            TaoMoi = false;

            //Tắt các chức năng liên quan
            btThem.Enabled = false;
            btLuu.Enabled = true;
            btSua.Enabled = false;
            btXoa.Enabled = false;

            //Load dữ liệu từ dòng đã chọn trong GridView lên textbox
            txtMaLoai.Text = gridView1.GetFocusedRowCellValue("MALOAI").ToString();
            txtTenLoai.Text = gridView1.GetFocusedRowCellValue("TENLOAI").ToString();
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaLoai.Text;
                LoaiLinhKien.XoaLoai(ma);

                //ResetForm ve ban đầu
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (TaoMoi == false)
            {
                btSua.Enabled = true;
                btXoa.Enabled = true;
            }
        }
    }
}