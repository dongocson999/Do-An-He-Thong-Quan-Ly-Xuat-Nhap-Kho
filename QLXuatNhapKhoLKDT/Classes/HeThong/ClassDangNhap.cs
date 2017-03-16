using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.HeThong
{
    class ClassDangNhap
    {
        //Khởi tạo đối tượng kết nối CSDL
        ConnectToDatabase Data;


        //Biến lưu tên user vừa mới đăng nhập
        static public string TenUser = "";

        //Biến lưu chức vụ của user vừa mới đăng nhập
        static public string ChucVu = "";

        public ClassDangNhap()
        {
            Data = new ConnectToDatabase();
        }

        //Hàm tìm mã nhân viên vừa đăng nhập --> Nếu đúng trả về Mã nhân viên --> nếu sai trả về ""
        public bool TimMaNhanVien(string username, string password)
        {
            if (KT(username, password) == true)
            {
                NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.USERNAME == username && a.PASSWORD == password);
                if (nv != null)
                {
                    FormMain.UserId = nv.MANV;
                    TenUser = nv.TENNV;
                    NHOMQUYEN NQ = Data.Database().NHOMQUYENs.SingleOrDefault(a => a.MANHOMQUYEN == nv.MANHOMQUYEN);
                    ChucVu = NQ.TENNHOMQUYEN;
                    return true;
                }
                else
                {
                    MessageBox.Show("Nhập sai Username hoặc Password !!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
                return false;
        }

        //Hàm kiểm tra thông tin người dùng có nhập username và pass chưa
        private bool KT(string username, string password)
        {
            bool KT = true;
            string kq = "Bạn chưa nhập: ";
            if (username == "")
            {
                KT = false;
                kq += "\n- Username.";
            }
            if (password == "")
            {
                KT = false;
                kq += "\n- Password.";
            }
            if (KT == false)
            {
                MessageBox.Show(kq,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return KT;
        }
    }
}
