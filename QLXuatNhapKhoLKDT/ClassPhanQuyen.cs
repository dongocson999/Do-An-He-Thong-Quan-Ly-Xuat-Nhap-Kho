using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT
{
    class ClassPhanQuyen
    {
        //Tạo kết nối CSDL
        ConnectToDatabase Data;

        //Tạo danh sách lưu các chức năng của user
        List<string> chucnang = new List<string>();


        public ClassPhanQuyen()
        {
            Data = new ConnectToDatabase();
        }


        //Lấy mã nhóm quyền của user vừa đăng nhập
        private string MaNhomQuyen(string MaNV)
        {
            NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.MANV == FormMain.UserId);
            return nv.MANHOMQUYEN;
        }


        //Lấy danh sách chức năng của user hiện tại
        List<string> DS_ChucNang = new List<string>();
        private void LayDanhSachChucNang()
        {
            string MaQuyen = MaNhomQuyen(FormMain.UserId);
            var ds = Data.Database().LAY_CHUCNANGHETHONG_PHANQUYEN(MaQuyen).ToList();

            foreach (var item in ds)
            {
                DS_ChucNang.Add(item.NAME);
            }
        }


        //Lấy danh sách các pages của các chức năng
        List<string> DS_Page = new List<string>();
        private void LayDanhSachPage()
        {
            string MaQuyen = MaNhomQuyen(FormMain.UserId);
            var ds = Data.Database().LAY_PAGES_PHANQUYEN(MaQuyen).ToList();

            foreach (var item in ds)
            {
                DS_Page.Add(item.NAME);
            }
        }


        //Lấy danh sách các Categories của các pages
        List<string> DS_Category = new List<string>();
        private void LayDanhSachCategory()
        {
            string MaQuyen = MaNhomQuyen(FormMain.UserId);
            var ds = Data.Database().LAY_CATEGORIES_PHANQUYEN(MaQuyen).ToList();

            foreach (var item in ds)
            {
                DS_Category.Add(item.NAME);
            }
        }

        public void BatCategories(FormMain frm)
        {
            
        }


        //Tắt tất cả chức năng hệ thống khi mới đăng nhập
        public void TatChucNangHeThong(FormMain frm)
        {
            frm.ribbonPageGroup_HeThong.Visible = false;
            frm.ribbonPageGroup_PhanQuyen.Visible = false;
            frm.ribbonPageGroup_QuanLyTaiKhoan.Visible = false;
            frm.ribbonPageGroup_ThongTinChung.Visible = false;
            frm.ribbonPageGroup_HangHoaLinhKien.Visible = false;
            frm.ribbonPageGroup_Kho.Visible = false;
            //frm.ribbonPageGroup_NhanVien.Visible = false;
            frm.ribbonPageGroup_KhuyenMai.Visible = false;
            frm.ribbonPageGroup_Gia.Visible = false;
            frm.ribbonPageGroup_NhaCungCap.Visible = false;
            frm.ribbonPageGroup_KhachHang.Visible = false;
            frm.ribbonPageGroup_NhapXuat.Visible = false;
            frm.ribbonPageGroup_NhapXuatKhac.Visible = false;
            frm.ribbonPageGroup_NghiepVuKhac.Visible = false;
            frm.ribbonPageGroup_ThongKe.Visible = false;
            frm.ribbonPageGroup_NghiepVuThuChi.Visible = false;
            frm.ribbonPageGroup_ThongKeThuChi.Visible = false;
            frm.ribbonPageGroup_DanhMucBaoHanh.Visible = false;
            frm.ribbonPageGroup_ThongKeTinhTrangBaoHanh.Visible = false;
            frm.ribbonPageGroup_BCNhapXuatLinhKien.Visible = false;
            frm.ribbonPageGroup_BCDoanhSoNhapXuatLinhKien.Visible = false;
            frm.ribbonPageGroup_BCThuChiTongHop.Visible = false;
            frm.ribbonPageGroup_BCCongNoTongHop.Visible = false;
            frm.ribbonPageGroup_CaiDatChucNang.Visible = false;
            frm.ribbonPageGroup_GuiMail.Visible = false;
            frm.ribbonPageGroup_QuanLyFile.Visible = false;
            frm.ribbonPageGroup_PhienBanHeThong.Visible = false;
            frm.ribbonPageGroup_CaiDatRiengTu.Visible = false;
            frm.ribbonPage_HeThong.Visible = false;
            frm.ribbonPage_TrongKho.Visible = false;
            frm.ribbonPage_DoiTac.Visible = false;
            frm.ribbonPage_Nhap_Xuat.Visible = false;
            frm.ribbonPage_Thu_Chi.Visible = false;
            frm.ribbonPage_BaoHanh.Visible = false;
            frm.ribbonPage_BCNhapXuat.Visible = false;
            frm.ribbonPage_BcThuChi.Visible = false;
            frm.ribbonPageTienIch.Visible = false;
            frm.ribbonPageCategory_HeThong.Visible = false;
            frm.ribbonPageCategory_DanhMuc.Visible = false;
            frm.ribbonPageCategory_NghiepVu.Visible = false;
            frm.ribbonPageCategory_BaoHanh.Visible = false;
            frm.ribbonPageCategory_BaoCao.Visible = false;
            frm.ribbonPageCategory_CongCu.Visible = false;

        }


        public void ThucHienPhanQuyen(FormMain frm)
        {

            //Gọi hàm lấy chức nang phân quyền hệ thống
            LayDanhSachChucNang();

            //Gọi hàm lấy danh sách pages
            LayDanhSachPage();

            //Gọi hàm lấy danh sách categories
            LayDanhSachCategory();

            //So sánh và bật categories
            foreach (var item in DS_Category)
            {
                if (frm.ribbonPageCategory_HeThong.Name == item.Trim())
                    frm.ribbonPageCategory_HeThong.Visible = true;
                if (frm.ribbonPageCategory_DanhMuc.Name == item.Trim())
                    frm.ribbonPageCategory_DanhMuc.Visible = true;
                if (frm.ribbonPageCategory_NghiepVu.Name == item.Trim())
                    frm.ribbonPageCategory_NghiepVu.Visible = true;
                if (frm.ribbonPageCategory_BaoHanh.Name == item.Trim())
                    frm.ribbonPageCategory_BaoHanh.Visible = true;
                if (frm.ribbonPageCategory_BaoCao.Name == item.Trim())
                    frm.ribbonPageCategory_BaoCao.Visible = true;
                if (frm.ribbonPageCategory_CongCu.Name == item.Trim())
                    frm.ribbonPageCategory_CongCu.Visible = true;
            }

            //So sánh và bật pages
            foreach (var item in DS_Page)
            {
                if (frm.ribbonPage_HeThong.Name == item.Trim())
                    frm.ribbonPage_HeThong.Visible = true;
                if (frm.ribbonPage_TrongKho.Name == item.Trim())
                    frm.ribbonPage_TrongKho.Visible = true;
                if (frm.ribbonPage_DoiTac.Name == item.Trim())
                    frm.ribbonPage_DoiTac.Visible = true;
                if (frm.ribbonPage_Nhap_Xuat.Name == item.Trim())
                    frm.ribbonPage_Nhap_Xuat.Visible = true;
                if (frm.ribbonPage_Thu_Chi.Name == item.Trim())
                    frm.ribbonPage_Thu_Chi.Visible = true;
                if (frm.ribbonPage_BaoHanh.Name == item.Trim())
                    frm.ribbonPage_BaoHanh.Visible = true;
                if (frm.ribbonPage_BCNhapXuat.Name == item.Trim())
                    frm.ribbonPage_BCNhapXuat.Visible = true;
                if (frm.ribbonPage_BcThuChi.Name == item.Trim())
                    frm.ribbonPage_BcThuChi.Visible = true;
                if (frm.ribbonPageTienIch.Name == item.Trim())
                    frm.ribbonPageTienIch.Visible = true;
            }

            //So sánh và bật chức năng
            foreach (var item in DS_ChucNang)
            {
                if (frm.ribbonPageGroup_HeThong.Name == item.Trim())
                    frm.ribbonPageGroup_HeThong.Visible = true;
                if (frm.ribbonPageGroup_PhanQuyen.Name == item.Trim())
                    frm.ribbonPageGroup_PhanQuyen.Visible = true;
                if (frm.ribbonPageGroup_QuanLyTaiKhoan.Name == item.Trim())
                    frm.ribbonPageGroup_QuanLyTaiKhoan.Visible = true;
                if (frm.ribbonPageGroup_ThongTinChung.Name == item.Trim())
                    frm.ribbonPageGroup_ThongTinChung.Visible = true;
                if (frm.ribbonPageGroup_HangHoaLinhKien.Name == item.Trim())
                    frm.ribbonPageGroup_HangHoaLinhKien.Visible = true;
                if (frm.ribbonPageGroup_Kho.Name == item.Trim())
                    frm.ribbonPageGroup_Kho.Visible = true;
                /*if (frm.ribbonPageGroup_NhanVien.Name == item.Trim())
                    frm.ribbonPageGroup_NhanVien.Visible = true;*/
                if (frm.ribbonPageGroup_KhuyenMai.Name == item.Trim())
                    frm.ribbonPageGroup_KhuyenMai.Visible = true;
                if (frm.ribbonPageGroup_Gia.Name == item.Trim())
                    frm.ribbonPageGroup_Gia.Visible = true;
                if (frm.ribbonPageGroup_NhaCungCap.Name == item.Trim())
                    frm.ribbonPageGroup_NhaCungCap.Visible = true;
                if (frm.ribbonPageGroup_KhachHang.Name == item.Trim())
                    frm.ribbonPageGroup_KhachHang.Visible = true;
                if (frm.ribbonPageGroup_NhapXuat.Name == item.Trim())
                    frm.ribbonPageGroup_NhapXuat.Visible = true;
                if (frm.ribbonPageGroup_NhapXuatKhac.Name == item.Trim())
                    frm.ribbonPageGroup_NhapXuatKhac.Visible = true;
                if (frm.ribbonPageGroup_NghiepVuKhac.Name == item.Trim())
                    frm.ribbonPageGroup_NghiepVuKhac.Visible = true;
                if (frm.ribbonPageGroup_ThongKe.Name == item.Trim())
                    frm.ribbonPageGroup_ThongKe.Visible = true;
                if (frm.ribbonPageGroup_NghiepVuThuChi.Name == item.Trim())
                    frm.ribbonPageGroup_NghiepVuThuChi.Visible = true;
                if (frm.ribbonPageGroup_ThongKeThuChi.Name == item.Trim())
                    frm.ribbonPageGroup_ThongKeThuChi.Visible = true;
                if (frm.ribbonPageGroup_DanhMucBaoHanh.Name == item.Trim())
                    frm.ribbonPageGroup_DanhMucBaoHanh.Visible = true;
                if (frm.ribbonPageGroup_ThongKeTinhTrangBaoHanh.Name == item.Trim())
                    frm.ribbonPageGroup_ThongKeTinhTrangBaoHanh.Visible = true;
                if (frm.ribbonPageGroup_BCNhapXuatLinhKien.Name == item.Trim())
                    frm.ribbonPageGroup_BCNhapXuatLinhKien.Visible = true;
                if (frm.ribbonPageGroup_BCDoanhSoNhapXuatLinhKien.Name == item.Trim())
                    frm.ribbonPageGroup_BCDoanhSoNhapXuatLinhKien.Visible = true;
                if (frm.ribbonPageGroup_BCThuChiTongHop.Name == item.Trim())
                    frm.ribbonPageGroup_BCThuChiTongHop.Visible = true;
                if (frm.ribbonPageGroup_BCCongNoTongHop.Name == item.Trim())
                    frm.ribbonPageGroup_BCCongNoTongHop.Visible = true;
                if (frm.ribbonPageGroup_CaiDatChucNang.Name == item.Trim())
                    frm.ribbonPageGroup_CaiDatChucNang.Visible = true;
                if (frm.ribbonPageGroup_GuiMail.Name == item.Trim())
                    frm.ribbonPageGroup_GuiMail.Visible = true;
                if (frm.ribbonPageGroup_QuanLyFile.Name == item.Trim())
                    frm.ribbonPageGroup_QuanLyFile.Visible = true;
                if (frm.ribbonPageGroup_PhienBanHeThong.Name == item.Trim())
                    frm.ribbonPageGroup_PhienBanHeThong.Visible = true;
                if (frm.ribbonPageGroup_CaiDatRiengTu.Name == item.Trim())
                    frm.ribbonPageGroup_CaiDatRiengTu.Visible = true;
            }

        }
    }
}
