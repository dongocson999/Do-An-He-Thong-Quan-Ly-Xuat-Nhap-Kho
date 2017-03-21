using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLXuatNhapKhoLKDT.Form.He_Thong;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.HeThong
{
    class ClassThongTinTaiKhoan
    {
        //Khai báo đối tượng ket nối CSDL
        ConnectToDatabase Data;

        public ClassThongTinTaiKhoan()
        {
            Data = new ConnectToDatabase();
        }

        //Load dữ liệu lên form thong tin tai khoan
        public string LoadData(XtraForm_ThongTinTaiKhoan frm)
        {
            NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.MANV == FormMain.UserId);
            frm.txtMaNV.Text = FormMain.UserId;
            frm.txtHoTen.Text = nv.TENNV;
            frm.txtNgaySinh.Text = nv.NGAYSINH.ToString();
            bool gt = (bool)nv.GIOITINH;
            if (gt == true)
                frm.txtGioiTinh.Text = "Nam";
            else
                frm.txtGioiTinh.Text = "Nữ";
            frm.txtCMND.Text = nv.CMND_CANCUOC;
            frm.txtDienThoai.Text = nv.DIENTHOAI;
            frm.txtDiaChiTamTru.Text = nv.DIACHI_TAMTRU;
            frm.txtDiaChiThuongTru.Text = nv.DIACHI_THUONGTRU;
            frm.txtusername.Text = nv.USERNAME;
            frm.txtpassword.Text = nv.PASSWORD;
            string hinhanh = nv.HINHANH;
            return hinhanh;
        }

        //Hàm cập nhật thông tin username và password của user
        public void SuaThongTinDangNhap(string ma, string username, string password)
        {
            try
            {
                if (username != "" && password != "")
                {
                    NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.MANV == ma);
                    if (nv != null)
                    {
                        nv.USERNAME = username;
                        nv.PASSWORD = password;

                        DialogResult thongbao = MessageBox.Show("Bạn có muốn thay đồi thông tin Username và Password không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Đồi thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Mã user hiện tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Hàm kiểm tra thông tin khi sửa
        private bool KiemTra(string user, string pass)
        {
            bool kt = true;
            string kq = "Thông Báo: ";
            if (user == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập username.";
            }
            else
            {
                if (user.Length > 15)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập username dưới 15 ký tự.";
                }
            }
            if (pass == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập password.";
            }
            else
            {
                if (pass.Length > 15)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập password dưới 15 ký tự.";
                }
            }
            if (kt == false)
            {
                MessageBox.Show(kq,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return kt;
        }
    }
}
