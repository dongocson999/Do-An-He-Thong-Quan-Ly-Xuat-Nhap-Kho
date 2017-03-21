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
using QLXuatNhapKhoLKDT.Classes.DanhMuc;

namespace QLXuatNhapKhoLKDT.Form.Danh_Muc
{
    public partial class XtraForm_QuyCach : DevExpress.XtraEditors.XtraForm
    {
        //Khai báo đối tượng tương tác Danh Mục Quy Cách
        ClassQuyCach QuyCach;

        //Tạo biến tạo mới
        bool TaoMoi;

        public XtraForm_QuyCach()
        {
            InitializeComponent();
        }

        private void XtraForm_QuyCach_Load(object sender, EventArgs e)
        {
            QuyCach = new ClassQuyCach();

            //Gọi hàm load dữ liệu lên form quy cách
            LoadData();

            //Resetform về trạng thái ban đầu
            ResetForm();
        }

        //Load dữ liệu lên form 
        private void LoadData()
        {
            QuyCach.LoadData(this);
        }

        //Hàm resetform về trạng thái ban đầu
        private void ResetForm()
        {
            //Tắt textbox Mã Quy Cách
            txtMaQuyCach.Enabled = false;

            //Bật tắt button chức năng tương ứng
            btTaoMoi.Enabled = true;
            btChinhSua.Enabled = false;
            btXoa.Enabled = false;
            btLuu.Enabled = false;
            btThoat.Enabled = true;

            //Xóa trắng textbox
            txtMaQuyCach.Text = "";
            txTenQuyCach.Text = "";
            txtGhiChuQuyCach.Text = "";

            //Biến tạo mới
            TaoMoi = false;
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            //Reset form 
            ResetForm();

            //Gán biến tạo mới
            TaoMoi = true;

            //Gán dữ liệu Mã Quy Cách
            txtMaQuyCach.Text = QuyCach.CapMaTuDong();

            //Bật tắt chức năng liên quan
            btTaoMoi.Enabled = false;
            btChinhSua.Enabled = false;
            btXoa.Enabled = false;
            btThoat.Enabled = false;
            btLuu.Enabled = true;

            txTenQuyCach.Focus();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaQuyCach.Text;
                string ten = txTenQuyCach.Text;
                string ghichu = txtGhiChuQuyCach.Text;
                if (TaoMoi == true)
                {
                    QuyCach.ThemQC(ma,ten,ghichu);
                }
                else
                {
                    QuyCach.SuaQC(ma,ten,ghichu);
                }

                //Load lại dữ liệu ra form
                LoadData();

                //Reset form ve trạng thái ban đầu
                ResetForm();
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
            btChinhSua.Enabled = false;
            btLuu.Enabled = true;
            btXoa.Enabled = false;
            btThoat.Enabled = false;

            //Load dữ liệu lên form
            txtMaQuyCach.Text = gridView1.GetFocusedRowCellValue("MAQC").ToString();
            txTenQuyCach.Text = gridView1.GetFocusedRowCellValue("TENQUYCACH").ToString();
            txtGhiChuQuyCach.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (TaoMoi == false)
            {
                btChinhSua.Enabled = true;
                btXoa.Enabled = true;
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaQuyCach.Text;
                QuyCach.XoaQC(ma);

                //Load lại dữ liệu ra form sau khi xóa
                LoadData();

                //Reset form về trạng thái ban đầu
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
    }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}