using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.DanhMuc
{
    class ClassLoaiKhachHang
    {
        //Khai báo đối tượng kết nối CSDL
        ConnectToDatabase Data;

        public ClassLoaiKhachHang()
        {
            Data = new ConnectToDatabase();
        }

        //Load dữ liệu ra form Loại khách hàng
        public void LoadData(XtraForm_LoaiKhachHang frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_LOAIKHACHHANG().ToList();
            frm.gridControl_DanhMucLoaiKhachHang.DataSource = dulieu;
        }

        //Hàm cấp mã tự động cho đối tượng loại khách hàng
        public string CapMaTuDong()
        {
            string kq = Data.CapMaTuDong("LH");
            return kq;
        }

        //Hàm cập nhật lại mã loại khách hàng sau khi đã cấp thành công
        public void CapNhatMaTuDong()
        {
            Data.CapNhatMaTuDong("LH");
        }

        //Hàm thêm 1 dòng dữ liệu vào trong CSDL loai khách hàng
        public bool ThemLKH(string ma, string ten, string ghichu)
        {
            try
            {
                if (KiemTra(ten, ghichu) == true)
                {
                    LOAIKHACHHANG lkh = new LOAIKHACHHANG();
                    lkh.MALOAIKH = ma;
                    lkh.TENLOAIKHACHHANG = ten;
                    lkh.GHICHU = ghichu;

                    DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm vào CSDL.","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                    if (thongbao == DialogResult.Yes)
                    {
                        Data.Database().LOAIKHACHHANGs.InsertOnSubmit(lkh);
                        Data.Database().SubmitChanges();

                        MessageBox.Show("Thêm thành công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        //Gọi hàm cập nhật lại mã tự dộng sau khi đã cấp thành công
                        CapNhatMaTuDong();

                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Hàm sửa 1 dòng dữ liệu trong CSDL loại khách hàng
        public bool SuaLKH(string ma, string ten, string ghichu)
        {
            try
            {
                LOAIKHACHHANG lkh = Data.Database().LOAIKHACHHANGs.SingleOrDefault(a => a.MALOAIKH == ma);
                if (lkh != null)
                {
                    if (KiemTra(ten, ghichu) == true)
                    {
                        lkh.TENLOAIKHACHHANG = ten;
                        lkh.GHICHU = ghichu;

                        DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa dữ liệu loại khách hàng này ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Sửa thành công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Hàm xóa 1 dòng dữ liệu trong CSDL loại khách hàng
        public bool XoaLKH(string ma)
        {
            try
            {
                LOAIKHACHHANG lkh = Data.Database().LOAIKHACHHANGs.SingleOrDefault(a => a.MALOAIKH == ma);
                if (lkh != null)
                {
                    DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa dữ liệu loại khách hàng này ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if (thongbao == DialogResult.Yes)
                    {
                        var kh = Data.Database().KHACHHANGs.Where(a => a.MALOAIKH == ma).ToList();
                        if (kh.Count() > 1)
                        {
                            MessageBox.Show("Loại khách hàng này đang được hệ thống sử dụng và không được xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else
                        {
                            Data.Database().LOAIKHACHHANGs.DeleteOnSubmit(lkh);
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Xóa thành công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                            return true;
                        }
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Hàm kiểm tra dữ liệu nhập vào
        private bool KiemTra(string ten, string ghichu)
        {
            bool kt = true;
            string kq = "Thông Báo: ";
            if (ten == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập tên loại khách hàng";
            }
            else
            {
                if (ten.Length > 20)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập tên loại dưới 20 ký tự.";
                }
            }
            if (ghichu.Length > 30)
            {
                kt = false;
                kq += "\n - Vui lòng nhập ghi chú dưới 30 ký tự.";
            }
            if (kt == false)
            {
                MessageBox.Show(kq);
            }
            return kt;
        }
    }
}
