using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;


namespace QLXuatNhapKhoLKDT.Classes.DanhMuc
{
    class ClassLinhKien
    {
        ConnectToDatabase Data; //Khai báo biến kết nối CSDL
        public ClassLinhKien()
        {
            Data = new ConnectToDatabase(); // Khởi tạo biến kết nối CSDL
        }
        public void LoadData(XtraForm_LKDT frm)
        {
            var lk = Data.Database().LINHKIENs.ToList(); // Khai báo biến lấy dữ liệu từ bảng linh kiện
            frm.gridControl1.DataSource = lk; // Đổ dữ liệu lên gridcontrol1
        }
        public void ThemLK(string mansx, string maloai, string tenlk,DateTime thoigianbh, string tinhnang, float trongluong, bool duocphepdoitra, string hinhanh, string maquycach)
        {
            try
            {

                LINHKIEN lk = new LINHKIEN(); //Khai báo biến lk
                lk.MA_LK = Data.CapMaTuDong("LK");
                lk.MANSX = mansx;
                lk.MALOAI = maloai;
                lk.TEN_LK = tenlk;
                lk.THOIGIAN_BH=thoigianbh;
                lk.TINHNANG = tinhnang;
                lk.TRONGLUONG = trongluong;
                lk.DUOCPHEPDOITRA = duocphepdoitra;
                lk.HINHANH = hinhanh;
                lk.MAQC = maquycach;
                DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm linh kiện này ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (thongbao == DialogResult.Yes)
                {
                    Data.Database().LINHKIENs.InsertOnSubmit(lk);
                    Data.Database().SubmitChanges();
                    Data.CapNhatMaTuDong("LK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        string malk;
        public void SuaLK(string mansx, string maloai, string tenlk, DateTime thoigianbh, string tinhnang, float trongluong, bool duocphepdoitra, string hinhanh, string maquycach)
        {
            try
            {

                LINHKIEN lk = Data.Database().LINHKIENs.SingleOrDefault(a => a.MA_LK == malk);//Khai báo biến lk
                lk.MANSX = mansx;
                lk.MALOAI = maloai;
                lk.TEN_LK = tenlk;
                lk.THOIGIAN_BH = thoigianbh;
                lk.TINHNANG = tinhnang;
                lk.TRONGLUONG = trongluong;
                lk.DUOCPHEPDOITRA = duocphepdoitra;
                lk.HINHANH = hinhanh;
                lk.MAQC = maquycach;
                DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa linh kiện này ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (thongbao == DialogResult.Yes)
                {
                    
                    Data.Database().SubmitChanges();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void XoaLK()
        {
            try
            {

                LINHKIEN lk = Data.Database().LINHKIENs.SingleOrDefault(a => a.MA_LK == malk);//Khai báo biến lk
                DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa linh kiện này ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (thongbao == DialogResult.Yes)
                {
                    Data.Database().LINHKIENs.DeleteOnSubmit(lk);
                    Data.Database().SubmitChanges();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
