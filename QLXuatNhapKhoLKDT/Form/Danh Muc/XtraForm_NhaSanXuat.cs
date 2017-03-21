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
    public partial class XtraForm_NhaSanXuat : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Khai báo đối tượng tương tác Danh Mục Nhà Sản Xuất
        ClassNhaSanXuat NhaSanXuat;

        //Tạo biến chức năng tạo mới
        bool TaoMoi;

        public XtraForm_NhaSanXuat()
        {
            InitializeComponent();
        }

        private void XtraForm_NhaSanXuat_Load(object sender, EventArgs e)
        {
            NhaSanXuat = new ClassNhaSanXuat();

            //Load dữ liệu danh muc nha san xuat ra form
            LoadData();

            //Resetform về trạng thái ban đầu
            ResetForm();
        }

        //Hàm load dữ liệu danh mục nhà sản xuất lên form
        private void LoadData()
        {
            NhaSanXuat.LoadData(this);
        }

        //hàm reset form về trạng thái ban đầu
        private void ResetForm()
        {
            //Bật tắt chức năng khi mới load form
            btTaoMoi.Enabled = true;
            btSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = false;
            btThoat.Enabled = true;

            //Xóa tất cả dữ liệu trên textbox
            txtMaNSX.Text = "";
            txtTenNSX.Text = "";
            txtQuocGia.Text = "";

            //Gán biến tạo mới ban đầu
            TaoMoi = false;
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            //Gán biến tạo mới
            TaoMoi = true;

            //Bật tắt chức năng liên quan
            btTaoMoi.Enabled = false;
            btSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = true;
            btThoat.Enabled = false;

            //Load dữ liệu mã nhà sản xuất mới ra textbox mã
            txtMaNSX.Text = NhaSanXuat.CapMaTuDong();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaNSX.Text;
                string ten = txtTenNSX.Text;
                string quocgia = txtQuocGia.Text;
                bool kt;
                if (TaoMoi == true)
                {
                    kt = NhaSanXuat.ThemNSX(ma, ten, quocgia);
                }
                else
                {
                    kt = NhaSanXuat.SuaNSX(ma,ten,quocgia);
                }

                if (kt == true)
                {
                    //Load lại dữ liệu ra form sau khi thao tác thêm - sửa
                    LoadData();

                    //Reset form về trạng thái ban đầu
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
            //Gán biến tạo mới = false vì đang sửa 
            TaoMoi = false;

            //Bật tắt chức năng button tương ứng
            btTaoMoi.Enabled = false;
            btSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = false;
            btThoat.Enabled = false;

            //load dữ liệu từ gridcontrol lên textbox
            txtMaNSX.Text = gridView1.GetFocusedRowCellValue("MANSX").ToString();
            txtTenNSX.Text = gridView1.GetFocusedRowCellValue("TENNSX").ToString();
            txtQuocGia.Text = gridView1.GetFocusedRowCellValue("QUOCGIA").ToString();
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
                string ma = txtMaNSX.Text;
                NhaSanXuat.XoaNSX(ma);

                //Load lại dữ liệu ra form sau khi xóa
                LoadData();

                //reset form về trạng thái ban đầu
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult thongbao = MessageBox.Show("Bạn có muốn thoát khỏi form nhà sàn xuất.","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (thongbao == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}