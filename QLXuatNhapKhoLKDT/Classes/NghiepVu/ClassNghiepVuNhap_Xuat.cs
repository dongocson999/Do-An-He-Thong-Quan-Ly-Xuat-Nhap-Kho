using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.NghiepVu
{
    class ClassNghiepVuNhap_Xuat
    {
        //Khai báo biến kết nối CSDL
        ConnectToDatabase Data;

        public ClassNghiepVuNhap_Xuat()
        {
            Data = new ConnectToDatabase();


            //Gọi hàm load dữ liệu vào danh sách để thực hiện tìm kiếm và thêm mới khi cần
            LoadDataToListPhieuNhap();
            LoadDataToListLinhKien();
        }


        //Tạo biến lưu danh mục dùng để tìm kiếm CSDL PHIEUNHAP bao gồm: <Số Phiếu> - <Ngày Lập>
        public List<string> LoadDataToListPhieuNhap()
        {
            List<string> DanhSachPhieuNhap = new List<string>(); //Lưu dưới dạng "Số_Phiếu - Ngày_Lập"
            DanhSachPhieuNhap.Clear();
            
            var dulieu = Data.Database().LAY_DANHMUC_PHIEUNHAP_QUANLY().ToList();
            foreach (var item in dulieu)
            {
                DanhSachPhieuNhap.Add(item.SOPHIEUNHAP + " - " + item.NGAYNHAP.ToString());
            }

            return DanhSachPhieuNhap;
        }

        //Tạo danh sách lưu danh mục linh kiện tìm kiếm theo <Mã LK> - <Tên LK> - <Đơn Vị Tính>
        public List<string> LoadDataToListLinhKien()
        {
            List<string> DanhSachLinhKien = new List<string>();//Lưu dưới dạng "Mã_LK - Tên_LK - QuyCach"
            DanhSachLinhKien.Clear();

            var dulieu = Data.Database().LAY_DANHMUC_LINHKIEN().ToList();
            foreach (var item in dulieu)
            {
                DANHMUCQUYCACH quycach = Data.Database().DANHMUCQUYCACHes.SingleOrDefault(a => a.MAQC == item.MAQC);
                DanhSachLinhKien.Add(item.MA_LK + " - " + item.TEN_LK + " - " + quycach.TENQUYCACH);
            }

            return DanhSachLinhKien;
        }
    }
}
