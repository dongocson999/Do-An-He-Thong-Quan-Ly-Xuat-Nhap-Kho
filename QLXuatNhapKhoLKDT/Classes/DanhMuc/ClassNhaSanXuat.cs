using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.DanhMuc
{
    class ClassNhaSanXuat
    {
        //Khai báo đối tượng liên kết CSDL
        ConnectToDatabase Data;

        public ClassNhaSanXuat()
        {
            Data = new ConnectToDatabase();
        }


        //Load dữ liệu ra form nhà sản xuất
        public void LoadData(XtraForm_NhaSanXuat frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_NHASANXUAT().ToList();
            frm.gridControl_DanhMucNhaSanXuat.DataSource = dulieu;
        }

        //Hàm cấp mã tự động cho bảng nhà sản xuất
        public string CapMaTuDong()
        {
            string kq = Data.CapMaTuDong("SX");
            return kq;
        }

        //Hàm cập nhật lại mã nhà sản xuất sau khi đã cấp tự động
        public void CapNhatMaTuDong()
        {
            Data.CapNhatMaTuDong("SX");
        }

        //Hàm thêm 1 dòng dữ liệu vào Danh Mục Nhà Sản Xuất
        public bool ThemNSX(string ma, string ten, string quocgia)
        {
            try
            {
                if (KiemTra(ten, quocgia) == true)
                {
                    NHASANXUAT sx = new NHASANXUAT();
                    sx.MANSX = ma;
                    sx.TENNSX = ten;
                    sx.QUOCGIA = quocgia;

                    DialogResult thongbao = MessageBox.Show("Bạn có muôn thêm nhà sản xuất này vào CSDL","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                    if (thongbao == DialogResult.Yes)
                    {
                        Data.Database().NHASANXUATs.InsertOnSubmit(sx);
                        Data.Database().SubmitChanges();

                        MessageBox.Show("Thêm vào thành công.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        //Gọi hàm cập nhật lại mã nhà sản xuất sau khi đã thêm thành công
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

        //hàm sửa dữ liệu của 1 nhà sản xuất
        public bool SuaNSX(string ma, string ten, string quocgia)
        {
            try
            {
                NHASANXUAT sx = Data.Database().NHASANXUATs.SingleOrDefault(a => a.MANSX == ma);
                if (sx != null)
                {
                    if (KiemTra(ten, quocgia) == true)
                    {
                        sx.TENNSX = ten;
                        sx.QUOCGIA = quocgia;

                        DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa thông tin nhà sản xuất này.","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Sửa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        //Hàm xóa 1 dòng dữ liệu trong csdl Nhà Sản Xuất
        public void XoaNSX(string ma)
        {
            try
            {
                NHASANXUAT sx = Data.Database().NHASANXUATs.SingleOrDefault(a => a.MANSX == ma);
                if (sx != null)
                {
                    DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa nhà sản xuất đã chọn không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (thongbao == DialogResult.Yes)
                    {
                        var lk = Data.Database().LINHKIENs.Where(a => a.MANSX == ma).ToList();
                        if (lk.Count() > 1)
                        {
                            MessageBox.Show("Mã nhà sản xuất này đang được hệ thống sử dụng và không được xóa.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Data.Database().NHASANXUATs.DeleteOnSubmit(sx);
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Xóa thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Hàm kiểm tra dữ liệu Danh Mục Nhà Sản Xuất
        private bool KiemTra(string ten, string quocgia)
        {
            bool kt = true;
            string kq = "Thông Báo: ";
            if (ten == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập tên Nhà Sản Xuất.";
            }
            else
            {
                if (ten.Length > 20)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập tên nhà sản xuất dưới 20 ký tự.";
                }
            }

            if (quocgia == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập quốc gia sản xuất.";
            }
            else
            {
                if (quocgia.Length > 15)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập quốc giá dưới 15 ký tự";
                }
            }
            if (kt = false)
            {
                MessageBox.Show(kq);
            }
            return kt;
        }
    }
}
