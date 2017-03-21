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
    public partial class XtraForm_LoaiKhachHang : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Khai báo đối tượng tương tác CSDL Loại khách hàng
        ClassLoaiKhachHang LoaiKhach;

        //Tạo biến lưu chức năng đang tạo mới
        bool TaoMoi;


        public XtraForm_LoaiKhachHang()
        {
            InitializeComponent();
        }

        private void XtraForm_LoaiKhachHang_Load(object sender, EventArgs e)
        {
            LoaiKhach = new ClassLoaiKhachHang();

            //Gọi hàm load dữ liệu lên form
            LoadData();

            //Reset form về trạng thái ban đầu
            ResetForm();
        }

        //Load dữ liệu lên form loại khách hàng
        private void LoadData()
        {
            LoaiKhach.LoadData(this);
        }

        //Hàm reset form về trạng thái ban đầu
        private void ResetForm()
        {
            //Gán biến tạo mới 
            TaoMoi = false;

            //Bật tắt chức năng button ban đầu
            btTaoMoi.Enabled = true;
            btSua.Enabled = false;
            btXoa.Enabled = false;
            btThoat.Enabled = true;
            btLuu.Enabled = false;

            //Xóa rỗng các textbox
            txtMaLoai.Text = "";
            txtTenLoai.Text = "";
            txtGhiChu.Text = "";
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            //Gán biến tạo mới
            TaoMoi = true;

            //Bật tắt chức năng tương ứng
            btTaoMoi.Enabled = false;
            btSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = true;
            btThoat.Enabled = false;

            //Load dữ liệu mã tự động lên textbox mã
            txtMaLoai.Text = LoaiKhach.CapMaTuDong();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaLoai.Text;
                string ten = txtTenLoai.Text;
                string ghichu = txtGhiChu.Text;
                bool kt;
                if (TaoMoi == true)
                {
                    kt = LoaiKhach.ThemLKH(ma,ten,ghichu);
                }
                else
                {
                    kt = LoaiKhach.SuaLKH(ma,ten,ghichu);
                }

                if (kt == true)
                {
                    //Load dữ liệu sau khi thêm - sửa
                    LoadData();

                    //Resetform về trạng thái ban đầu
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            //Gán biến tạo mới
            TaoMoi = false;

            //Bật tắt chức năng button tương ứng
            btTaoMoi.Enabled = false;
            btSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = true;
            btThoat.Enabled = false;

            //Load dữ liệu ra textbox
            txtMaLoai.Text = gridView1.GetFocusedRowCellValue("MALOAIKH").ToString();
            txtTenLoai.Text = gridView1.GetFocusedRowCellValue("TENLOAIKHACHHANG").ToString();
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (TaoMoi == false)
            {
                btSua.Enabled = true;
                btXoa.Enabled = true;
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaLoai.Text;
                bool kt;
                kt = LoaiKhach.XoaLKH(ma);

                if (kt == true)
                {
                    //Load lại dữ liệu sau khi xóa thành công
                    LoadData();

                    //reset form về trạng thái ban đầu
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult thongbao = MessageBox.Show("Bạn có muốn thoát khỏi form loại khách hàng.","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (thongbao == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}