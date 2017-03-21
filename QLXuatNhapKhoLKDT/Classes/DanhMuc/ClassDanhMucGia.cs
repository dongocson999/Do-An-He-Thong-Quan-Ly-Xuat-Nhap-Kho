using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;

namespace QLXuatNhapKhoLKDT.Classes.DanhMuc
{
    class ClassDanhMucGia
    {
        //Khai báo đối tượng kết nối CSDL
        ConnectToDatabase Data;

        public ClassDanhMucGia()
        {
            Data = new ConnectToDatabase();
        }

        //Load dữ liệu ra form danh mục giá
        public void LoadData(XtraForm_DanhMucGia frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_GIA().ToList();
            frm.gridControl_DanhMucGia.DataSource = dulieu;
        }

        //Hàm sửa giá xuất bán sản phẩm
        public bool SuaGiaXuatBan(string malk, int giaxuatban)
        {
            try
            {
                if (KiemTra(malk, giaxuatban) == true)
                {
                    LINHKIEN lk = Data.Database().LINHKIENs.SingleOrDefault(a => a.MA_LK == malk);
                    if (lk != null)
                    {
                        lk.DONGIA = giaxuatban;

                        DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa giá bán này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Sửa thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        //Hàm kiểm tra 1 chuồi là số hay không
        private bool IsNumber(string chuoi)
        {
            foreach (char c in chuoi)
            {
                if (Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        //Hàm kiểm tra dữ liệu nhập vào
        private bool KiemTra(string malk, int giaban)
        {
            bool kt = true;
            string kq = "Thông báo: ";
            if (IsNumber(giaban.ToString()) == false)
            {
                kt = false;
                kq += "\n - Vui lòng nhập giá bán là số nguyên không âm từ 1 đến dưới 1 tỷ.";
            }
            else
            { 
                if (giaban <= 0)
                {
                    kt = false;
                    kq += "\n - Giá bán không được <= 0.";
                }
                else
                {
                    if (giaban.ToString().Length > 9)
                    {
                        kt = false;
                        kq += "\n - Giá bán phải từ 1 đến dưới 1 tỷ đồng.";
                    }
                    else
                    {
                        int gianhap = (int)Data.Database().LAY_DONGIA_NHAP(malk);
                        if(giaban < gianhap)
                        {
                            kt = false;
                            kq += "\n - Vui lòng nhập giá bán lớn hơn giá nhập.";
                        }
                    }
                }
            }
            if (kt == false)
            {
                MessageBox.Show(kq);
            }
            return kt;
        }

    }
}
