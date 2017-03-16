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
using QLXuatNhapKhoLKDT.Classes.NhomQuyen;

namespace QLXuatNhapKhoLKDT.Form.He_Thong
{
    public partial class XtraForm_PhanQuyen : DevExpress.XtraEditors.XtraForm
    {
        //Khai báo biến Nhóm Quyền
        NhomQuyen NQ = new NhomQuyen();

        //Khai báo biến Chức Năng Nhóm Quyền
        ChucNangNhomQuyen CN = new ChucNangNhomQuyen();


        //Biến tạo mới nhóm quyền
        bool TaoMoi;

        //Biến chỉnh sửa chức năng và biến lưu Mã Nhóm Quyền Đang Sửa hiện tại và Mã Chức Năng đang chọn hiện tại
        bool ChinhSuaChucNang;
        string MaNhomQuyen_curr = "";
        string SttChucNang_curr = "";

        public XtraForm_PhanQuyen()
        {
            InitializeComponent();
            btChinhSua.Enabled = false;
            btLuu.Enabled = false;
            btXoa.Enabled = false;
            txtMaNhomQuyen.Enabled = false;
            LoadData();
            ResetForm();
            ResetForm_ChinhSua();
        }

        //-------------------------------------------------------------------------------------------------
        //Load dữ liệu lên gridcontrol nhóm quyền 
        private void LoadData()
        {
            gridControl1.DataSource = NQ.DanhMucNhomQuyen();
        }

        //Load dữ liệu lên gridcontrol chức năng nhóm quyền và chức năng còn lại chưa cấp cho nhóm quyền
        private void LoadData_ChucNang(string maNQ)
        {
            //Load dữ liệu ra GridControl chức năng của nhóm quyền hiện tại đang được chọn chỉnh sửa
            var chucnang_curr = CN.LayChucNangCuaNhomQuyen(maNQ);
            gridControl_ChucNangHienTai.DataSource = chucnang_curr;

            //Load dữ liệu ra GridControl chức năng chưa cấp của nhóm quyền hiện tại đang được chọn chỉnh sửa
            var chucnang_conlai = CN.LayChucNangChuaCapChoNhomQuyen(maNQ);
            gridControl_ChucNangConLai.DataSource = chucnang_conlai;
        }
    

        //----------------------------------------------------------------------------------------------
        //Reset form thêm, xóa, sửa nhóm quyền
        private void ResetForm()
        {
            TaoMoi = true;
            btHuy.Enabled = true;
            btTaoMoi.Enabled = true;
            btChinhSua.Enabled = true;
            btLuu.Enabled = false;
            btXoa.Enabled = true;

            txtGhiChu.Text = "";
            txtMaNhomQuyen.Text = "";
            txtTenNhomQuyen.Text = "";
            btTaoMoi.Text = "Tạo Mới";
        }

        //Reset form chỉnh sửa chức năng của nhóm quyền
        private void ResetForm_ChinhSua()
        {
            btChinhSuaChucNang.Visible = true;
            btHoanThanh.Visible = false;
            ChinhSuaChucNang = false;
            MaNhomQuyen_curr = "";
            lblNhomQuyen_curr.Text = "<Chưa Chọn>";
            btBoRa.Enabled = false;
            btThemVao.Enabled = false;
            btHuyChinhSuaChucNang.Enabled = false;
        }

        //Bật tắt button Thêm Vào và Bỏ Ra
        private void Button_ThemVao_BoRa(bool k)
        {
            btThemVao.Enabled = k;
            btBoRa.Enabled = !k;
        }



        private void btThoat_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Tắt Form Phân Quyền !!";
        }

        private void btTaoMoi_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Tạo Thêm Nhóm Quyền Mới !!";
        }

        private void btChinhSua_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Chỉnh Sửa Thông Tin Nhóm Quyền !!";
        }

        private void groupControl3_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "--";
        }

        private void btLuu_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Lưu Thông Tin Sau Khi Đã Chỉnh Sửa Nhóm Quyền !!";
        }

        private void btXoa_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Xóa Thông Tin Nhóm Quyền !!";
        }

        private void simpleButton7_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Thêm Chức Năng Mới Vào Nhóm Quyền !!";
        }

        private void simpleButton8_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Chỉnh Sửa Chức Năng Trong Nhóm Quyền !!";
        }

        private void groupControl5_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "--";
        }

        private void simpleButton6_MouseHover(object sender, EventArgs e)
        {
            lblGhiChuChucNang.Text = "Xóa Chức Năng Trong Nhóm Quyền !!";
        }


        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            btTaoMoi.Enabled = false;
            btLuu.Enabled = true;
            btTaoMoi.Text = "Đang Tạo";
            TaoMoi = true;
            txtMaNhomQuyen.Text = NQ.MaNhomQuyen();

        }

        

        private void btLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaNhomQuyen.Text;
            string ten = txtTenNhomQuyen.Text;
            string ghichu = txtGhiChu.Text;

            if (TaoMoi == true)
            {
                NQ.ThemNhomQuyen(ten,ghichu);
            }
            else
            {
                NQ.SuaNhomQuyen(ma,ten,ghichu);
            }
            LoadData();
            ResetForm();
        }


        private void btHuy_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btChinhSua_Click(object sender, EventArgs e)
        {
            //Bật tắt các chức năng liên quan
            TaoMoi = false;
            btTaoMoi.Enabled = false;
            btLuu.Enabled = true;
            btXoa.Enabled = true;
            btChinhSua.Enabled = false;

            //Load dữ liệu từ GridControl lên textbox
            txtMaNhomQuyen.Text = gridView1.GetFocusedRowCellValue("MANHOMQUYEN").ToString();
            txtTenNhomQuyen.Text = gridView1.GetFocusedRowCellValue("TENNHOMQUYEN").ToString();
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }

        private void XtraForm_PhanQuyen_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = NQ.DanhMucNhomQuyen();
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
            string ma = txtMaNhomQuyen.Text;
            try
            {
                NQ.XoaNhomQuyen(ma);

                ResetForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (btChinhSuaChucNang.Visible == false)
            {
                Button_ThemVao_BoRa(false);
                SttChucNang_curr = gridView2.GetFocusedRowCellValue("STT").ToString();
            }
        }

        private void gridView3_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (btChinhSuaChucNang.Visible == false)
            {
                Button_ThemVao_BoRa(true);
                SttChucNang_curr = gridView3.GetFocusedRowCellValue("STT").ToString();
            }
        }

        private void btChinhSuaChucNang_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("MANHOMQUYEN").ToString() != "")
            {
                //Ẩn button Chỉnh Sửa - Hiện button hoàn thành - Bật button hủy 
                btChinhSuaChucNang.Visible = false;
                btHoanThanh.Visible = true;
                btHuyChinhSuaChucNang.Enabled = true;

                //Bật chức năng chỉnh sửa
                ChinhSuaChucNang = true;

                //Lưu mã nhóm quyền đang chọn
                MaNhomQuyen_curr = gridView1.GetFocusedRowCellValue("MANHOMQUYEN").ToString();
                lblNhomQuyen_curr.Text = gridView1.GetFocusedRowCellValue("TENNHOMQUYEN").ToString();

                //Load dữ liệu lên 2 GridControl Chức Năng Hiện Có của Nhóm Quyền và Chức Năng Chưa Cấp của nhóm quyền
                LoadData_ChucNang(MaNhomQuyen_curr);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhóm quyền trước khi chỉnh sửa chức năng nhóm quyền !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btHoanThanh_Click(object sender, EventArgs e)
        {
            //Thông báo hoàn thành
            MessageBox.Show("Đã lưu lại các chức năng hệ thống của nhóm quyền: " + lblNhomQuyen_curr.Text,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

            //Reset form chỉnh sửa về ban đầu
            ResetForm_ChinhSua();

            //Trả lại 2 bảng dữ liệu trống ban đầu của GridControl Chức Năng Hiện Hành và Chức Năng Chưa Cấp
            LoadData_ChucNang("");
        }

        private void btHuyChinhSuaChucNang_Click(object sender, EventArgs e)
        {
            //Reset form chỉnh sửa về ban đầu
            ResetForm_ChinhSua();

            //Trả lại 2 bảng dữ liệu trống ban đầu của GridControl Chức Năng Hiện Hành và Chức Năng Chưa Cấp
            LoadData_ChucNang("");
        }


        //Hàm chức năng thêm vào nhóm quyền 1 chức năng đã chọn từ GridControl chức năng chưa cấp
        private void btThemVao_Click(object sender, EventArgs e)
        {
            //Gọi hàm ThemVao từ đối tượng Chức Năng "CN" tạo từ class ChucNangNhomQuyen
            //Dùng để thêm 1 chức năng vào nhóm quyền
            CN.ThemVao(MaNhomQuyen_curr,SttChucNang_curr);
            
            //Load lại dữ liệu ra GridControl Chức Năng Đã Cấp và Chức Năng Chưa Cấp
            LoadData_ChucNang(MaNhomQuyen_curr);

            //Trả lại biến SttChucNang_curr về giá trị ban đầu
            SttChucNang_curr = "";
        }

        //Gọi hàm chức năng bỏ ra (xóa) khỏi nhóm quyền 1 chức năng đã chọn từ GridControl chức năng đã cấp
        private void btBoRa_Click(object sender, EventArgs e)
        {
            //Gọi hàm BoRa từ đối tượng Chức Năng "CN" tạo từ class ChucNangNhomQuyen
            //Dùng để xóa 1 chức năng ra khỏi nhóm quyền
            CN.BoRa(MaNhomQuyen_curr,SttChucNang_curr);

            //Load lại dữ liệu ra GridControl Chức Năng Đã Cấp và Chức Năng Chưa Cấp
            LoadData_ChucNang(MaNhomQuyen_curr);

            //Trả lại biến SttChucNang_curr về giá trị ban đầu
            SttChucNang_curr = "";
        }
    }
}