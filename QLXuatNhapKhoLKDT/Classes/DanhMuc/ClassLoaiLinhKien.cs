using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;

namespace QLXuatNhapKhoLKDT.Classes.DanhMuc
{
    class ClassLoaiLinhKien
    {
        //Khai báo đối tượng kết nối CSDL
        ConnectToDatabase Data;

        public ClassLoaiLinhKien()
        {
            Data = new ConnectToDatabase();
        }

        //Hàm trả về mã loại linh kiện được cấp tự động
        public string CapMaTuDong()
        {
            string kq = Data.CapMaTuDong("LA");
            return kq;
        }

        //Load dữ liệu Danh Mục Loại linh kiện
        public void LoadData(XtraForm_LoaiLKDT frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_LOAILINHKIEN().ToList();
            frm.gridControl_DanhMucLoaiLinhKien.DataSource = dulieu;
        }

        //Thêm 1 loại linh kiện vào CSDL
        public bool ThemLoai(string maloai, string tenloai, string ghichu)
        {
            if (KiemTra(tenloai, ghichu) == true)
            {
                LOAILINHKIEN loai = new LOAILINHKIEN();
                loai.MALOAI = maloai;
                loai.TENLOAI = tenloai;
                loai.GHICHU = ghichu;

                DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm loại linh kiện này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (thongbao == DialogResult.Yes)
                {
                    Data.Database().LOAILINHKIENs.InsertOnSubmit(loai);
                    Data.Database().SubmitChanges();

                    //Cập nhật lại mã tự động cho loại linh kiện
                    Data.CapNhatMaTuDong("LA");

                    MessageBox.Show("Thêm thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        //Sửa thông tin 1 loại linh kiện đã chọn
        public bool SuaLoai(string maloai, string tenloai, string ghichu)
        {
            if (KiemTra(tenloai, ghichu) == true)
            {
                LOAILINHKIEN loai = Data.Database().LOAILINHKIENs.SingleOrDefault(a => a.MALOAI == maloai);
                if (loai != null)
                {
                    loai.TENLOAI = tenloai;
                    loai.GHICHU = ghichu;

                    DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa loại linh kiện này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (thongbao == DialogResult.Yes)
                    {
                        Data.Database().SubmitChanges();

                        MessageBox.Show("Sửa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại linh kiện vừa chọn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Hàm xóa thông tin 1 loại linh kiện đã chọn <ĐK: không có tham gia vào các quan hệ>
        public void XoaLoai(string ma)
        {
            if (ma != "")
            {
                LOAILINHKIEN loai = Data.Database().LOAILINHKIENs.SingleOrDefault(a => a.MALOAI == ma);
                if (loai != null)
                {
                    LINHKIEN lk = Data.Database().LINHKIENs.SingleOrDefault(a => a.MALOAI == ma);
                    if (lk != null)
                    {
                        MessageBox.Show("Không được xóa vì loại linh kiện này đang được sử dụng trong hệ thống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa loại linh kiện này không.","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().LOAILINHKIENs.DeleteOnSubmit(loai);
                            Data.Database().SubmitChanges(); 
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại linh kiện mà bạn đã chọn trong CSDL.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        //Kiểm tra thông tin khi thêm loai linh kiện
        private bool KiemTra(string tenloai, string ghichu)
        {
            bool kt = true;
            string kq = "Thông Báo: ";
            if (tenloai == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập tên loại linh kiện.";
            }
            else
            {
                if (tenloai.Length > 10)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập tên loại linh kiện dưới 10 ký tự.";
                }
            }
            if (ghichu.Length > 50)
            {
                kt = false;
                kq += "\n - Vui lòng nhập ghi chú dưới 50 ký tự.";
            }

            if (kt == false)
            {
                MessageBox.Show(kq,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return kt;
        }
    }
}
