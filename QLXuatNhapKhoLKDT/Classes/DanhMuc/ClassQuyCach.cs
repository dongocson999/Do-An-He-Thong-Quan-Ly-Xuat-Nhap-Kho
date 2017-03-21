using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLXuatNhapKhoLKDT.Form.Danh_Muc;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.DanhMuc
{
    class ClassQuyCach
    {
        //Khai báo đối tượng kết nối CSDL
        ConnectToDatabase Data;


        public ClassQuyCach()
        {
            Data = new ConnectToDatabase();
        }

        //Load dữ liệu ra form QuyCach
        public void LoadData(XtraForm_QuyCach frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_QUYCACH().ToList();
            frm.gridControl_DanhMucQuyCach.DataSource = dulieu;
        }

        //Hàm trả về chuỗi mã tự động cấp bởi hệ thống
        public string CapMaTuDong()
        {
            string kq = Data.CapMaTuDong("QC");
            return kq;
        }

        //Hàm cấp nhật ký số sau khi cấp thành công 1 mã
        public void CapNhatMaTuDong()
        {
            Data.CapNhatMaTuDong("QC");
        }

        //Hàm thêm 1 dòng dữ liệu quy cách vào CSDL
        public void ThemQC(string ma, string ten, string ghichu)
        {
            try
            {
                if (KiemTra(ten, ghichu) == true)
                {
                    DANHMUCQUYCACH qc = new DANHMUCQUYCACH();
                    qc.MAQC = ma;
                    qc.TENQUYCACH = ten;
                    qc.GHICHU = ghichu;

                    DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm quy cách này vào CSDL.","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                    if (thongbao == DialogResult.Yes)
                    {
                        Data.Database().DANHMUCQUYCACHes.InsertOnSubmit(qc);
                        Data.Database().SubmitChanges();

                        MessageBox.Show("Thêm vào thành công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        //Cập nhập lại mã tự động sai khi thêm thành công
                        CapNhatMaTuDong();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Hàm chỉnh sửa 1 dòng dữ liệu
        public void SuaQC(string ma, string ten, string ghichu)
        {
            try
            {
                DANHMUCQUYCACH qc = Data.Database().DANHMUCQUYCACHes.SingleOrDefault(a => a.MAQC == ma);
                if (qc != null)
                {
                    if (KiemTra(ten, ghichu) == true)
                    {
                        qc.TENQUYCACH = ten;
                        qc.GHICHU = ghichu;

                        DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa quy cách này vào CSDL.", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();

                            MessageBox.Show("Sửa vào thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Hàm xóa 1 dòng dữ liệu ra khỏi CSDL
        public void XoaQC(string ma)
        {
            try
            {
                DANHMUCQUYCACH qc = Data.Database().DANHMUCQUYCACHes.SingleOrDefault(a => a.MAQC == ma);
                if (qc != null)
                {
                    DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa quy cách đã chọn không?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if (thongbao == DialogResult.Yes)
                    {
                        var lk = Data.Database().LINHKIENs.Where(a => a.MAQC == ma).ToList();
                        if (lk.Count() > 0)
                        {
                            MessageBox.Show("Bạn không được xóa vì quy cách này đã được sử dụng trong hệ thống.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Data.Database().DANHMUCQUYCACHes.DeleteOnSubmit(qc);

                            MessageBox.Show("Xóa thành công.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Hàm kiễm tra dữ liệu nhập vào
        private bool KiemTra(string ten, string ghichu)
        {
            bool kt = true;
            string kq = "Thông Báo: ";
            if (ten == "")
            {
                kt = false;
                kq += "\n - Vui lòng nhập tên quy cách.";
            }
            else
            {
                if (ten.Length > 10)
                {
                    kt = false;
                    kq += "\n - Vui lòng nhập tên quy cách dưới 10 ký tự.";
                }
            }
            if (ghichu.Length > 20)
            {
                kt = false;
                kq += "\n - Vui lòng nhập ghi chú dưới 20 ký tự.";
            }
            if (kt == false)
            {
                MessageBox.Show(kq);
            }
            return kt;
        }
    }
}
