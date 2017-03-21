using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLXuatNhapKhoLKDT.Form.He_Thong;

namespace QLXuatNhapKhoLKDT.Classes.HeThong
{
    class ClassDoiMatKhau
    {
        //Tạo đối tượng kết nối CSDL
        ConnectToDatabase Data;

        public ClassDoiMatKhau()
        {
            Data = new ConnectToDatabase();
        }

        //Load dữ liệu username ra label
        public string UserName()
        {
            NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.MANV == FormMain.UserId);
            return nv.USERNAME;
        }

        //Hàm xử lý đổi mật khẩu
        public bool DoiMatKhau(string username, string matkhaucu, string matkhaumoi_1, string matkhaumoi_2)
        {
            try
            {
                NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.USERNAME == username && a.MANV == FormMain.UserId);
                if (nv != null)
                {
                    if (nv.PASSWORD.Trim() == matkhaucu.Trim())
                    {
                        if (matkhaumoi_1 == matkhaumoi_2)
                        {
                            nv.PASSWORD = matkhaumoi_2;

                            DialogResult thongbao = MessageBox.Show("Bạn có muốn đổi mật khẩu ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                            if (thongbao == DialogResult.Yes)
                            {
                                Data.Database().SubmitChanges();

                                MessageBox.Show("Đổi mật khẩu thành công. Quay về trang đăng nhập.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cữ không đúng.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Không đúng thông tin User.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
