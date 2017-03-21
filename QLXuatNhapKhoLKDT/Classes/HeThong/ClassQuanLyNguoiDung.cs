using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLXuatNhapKhoLKDT.Form.He_Thong;
using System.Windows.Forms;
using System.IO;

namespace QLXuatNhapKhoLKDT.Classes.HeThong
{
    class ClassQuanLyNguoiDung
    {
        //Khai báo đối tượng kết nối CSDL
        ConnectToDatabase Data;

        public ClassQuanLyNguoiDung()
        {
            Data = new ConnectToDatabase();
        }

        //Hàm cấp mã tự động cho Người dùng (Nhân Viên)
        public string CapMaNguoiDung()
        {
            string kq = Data.CapMaTuDong("NV");
            return kq;
        }

        //Load dữ liệu cho combobox Mã Quyền
        public void LoadComboMaQuyen(XtraForm_QLNguoiDung frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_NHOMQUYEN().ToList();
            frm.comboNhomQuyen.DataSource = dulieu;
            frm.comboNhomQuyen.DisplayMember = "TENNHOMQUYEN";
            frm.comboNhomQuyen.ValueMember = "MANHOMQUYEN";
        }

        //Load dữ liệu lên Gridcontrol
        public void LoadData(XtraForm_QLNguoiDung frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_NHANVIEN().ToList();
            frm.gridControl_DanhMucNhanVien.DataSource = dulieu;
        }

        //Thêm 1 nhan viên vào CSDL
        public bool ThemNV(string ma, string ten, DateTime ngaysinh, bool gioitinh, string cmnd, string diachitam, string diachithuongtru, string dienthoai, OpenFileDialog open, string username, string password, string maquyen)
        {
            try
            {
                if (KiemTra(ten, ngaysinh, gioitinh, cmnd, diachitam, diachithuongtru, dienthoai, username, password, maquyen) == true)
                {
                    NHANVIEN nv = new NHANVIEN();
                    nv.MANV = ma;
                    nv.TENNV = ten;
                    nv.NGAYSINH = ngaysinh;
                    nv.CMND_CANCUOC = cmnd;
                    nv.DIACHI_TAMTRU = diachitam;
                    nv.DIACHI_THUONGTRU = diachithuongtru;
                    nv.DIENTHOAI = dienthoai;
                    if (open.FileName != null)
                    {
                        nv.HINHANH = Path.GetFileName(open.FileName);
                    }
                    else
                        nv.HINHANH = "--";
                    nv.USERNAME = username;
                    nv.PASSWORD = password;
                    nv.MANHOMQUYEN = maquyen;

                    DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm nhân viên này vào hệ thống!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (thongbao == DialogResult.Yes)
                    {
                        Data.Database().NHANVIENs.InsertOnSubmit(nv);
                        Data.Database().SubmitChanges();



                        //Cập nhậ lại mã nhân viên sau khi đã thêm thành công
                        Data.CapNhatMaTuDong("NV");


                        //Lưu hình ảnh vào folder trong project
                        if (open.CheckFileExists)
                        {
                            string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                            string CorrectFileName = Path.GetFileName(open.FileName);
                            File.Copy(open.FileName, path + "\\Images\\NhanVien\\" + CorrectFileName);
                        }

                        MessageBox.Show("Lưu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Hàm sửa dữ liệu của 1 nhân viên đã chọn từ Gridcontrol
        public bool SuaNV(string ma, string ten, DateTime ngaysinh, bool gioitinh, string cmnd, string diachitam, string diachithuongtru, string dienthoai, OpenFileDialog open, string username, string password, string maquyen)
        {
            try
            {
                if (KiemTra(ten, ngaysinh, gioitinh, cmnd, diachitam, diachithuongtru, dienthoai, username, password, maquyen) == true)
                {
                    NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.MANV == ma);
                    if (nv != null)
                    {
                        nv.MANV = ma;
                        nv.TENNV = ten;
                        nv.NGAYSINH = ngaysinh;
                        nv.CMND_CANCUOC = cmnd;
                        nv.DIACHI_TAMTRU = diachitam;
                        nv.DIACHI_THUONGTRU = diachithuongtru;
                        nv.DIENTHOAI = dienthoai;
                        if (open != null)
                        {
                            nv.HINHANH = Path.GetFileName(open.FileName);
                        }
                        nv.USERNAME = username;
                        nv.PASSWORD = password;
                        nv.MANHOMQUYEN = maquyen;
                        nv.GIOITINH = gioitinh;

                        DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm nhân viên này vào hệ thống!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();

                            //Cập nhậ lại mã nhân viên sau khi đã thêm thành công
                            Data.CapNhatMaTuDong("NV");


                            //Lưu hình ảnh vào folder trong project
                            if (open != null)
                            {
                                string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                                string CorrectFileName = Path.GetFileName(open.FileName);
                                File.Copy(open.FileName, path + "\\Images\\NhanVien\\" + CorrectFileName);
                            }

                            MessageBox.Show("Lưu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên bạn đã chọn trong hệ thống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Xóa 1 dòng dữ liệu nhân viên ra khỏi hệ thống
        public void XoaNV(string ma)
        {
            try
            {
                NHANVIEN nv = Data.Database().NHANVIENs.SingleOrDefault(a => a.MANV == ma);
                if (nv != null)
                {
                    if (nv.NHOMQUYEN == null)
                    {
                        DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa nhân viên này ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().NHANVIENs.DeleteOnSubmit(nv);
                            Data.Database().SubmitChanges();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng xóa nhóm quyền của nhân viên này trước khi thực hiện xóa.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên bạn đã chọn.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Hàm kiểm tra dữ liệu hợp lệ
        private bool KiemTra(string ten, DateTime ngaysinh, bool gioitinh, string cmnd, string diachitam, string diachithuongtru, string dienthoai, string username, string password, string maquyen)
        {
            bool kt = true;
            string kq = "Thông Báo: ";
            if (ten == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập họ tên nhân viên.";
            }
            else
            {
                if (ten.Length > 30)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập họ tên dưới 30 ký tự.";
                }
            }
            if (DateTime.Now.Year - ngaysinh.Year < 15)
            {
                kt = false;
                kq += "\n - Chỉ nhập những ngày sinh trước năm " + (DateTime.Now.Year - 18).ToString();
            }
            if (cmnd == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập CMND/Căn Cước.";
            }
            else
            {
                if (cmnd.Length > 12)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập lại CMND/Căn Cước tối đa 12 ký tự.";
                }
            }
            if (diachithuongtru == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập địa chỉ thường trú.";
            }
            else
            {
                if (diachithuongtru.Length > 50)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập địa chỉ dưới 50 ký tự.";
                }
            }
            if (dienthoai == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập điện thoại liên lạc.";
            }
            else
            {
                if (dienthoai.Length > 11)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập lại điện thoại dưới 11 ký tự.";
                }
            }
            if (username == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập username để đăng nhập hệ thống.";
            }
            else
            {
                if (username.Length > 15)
                {
                    kt = false;
                    kq += "\n - Chỉ được nhập username dưới 15 ký tự.";
                }
            }
            if (password == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập password.";
            }
            else
            {
                if (password.Length > 15)
                {
                    kt = false;
                    kq += "\n - Chỉ được nhập pass dưới 15 ký tự.";
                }
            }
            if (maquyen == "")
            {
                kt = false;
                kq += "\n - Vui lòng chọn phân quyền user.";
            }
            if (kt == false)
            {
                MessageBox.Show(kq,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return kt;
        }
    }
}
