using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.NhomQuyen
{
    class NhomQuyen
    {
        //Khởi tạo kết nối database
        ConnectToDatabase Connect; 
        

        //Hàm khởi tạo đối tượng lưu CSDL
        public NhomQuyen()
        {
            Connect = new ConnectToDatabase();
        }

        //Hàm trả về dữ liệu của bảng NHOMQUYEN đưa lên form nhóm quyền
        public object DanhMucNhomQuyen()
        {
            var dulieu = Connect.Database().LAY_DANHMUC_NHOMQUYEN();
            return dulieu;
        }

        //Hàm lấy mã tự động cho danh mục nhóm quyền khi thêm mới
        public string MaNhomQuyen()
        {
            string kq = Connect.CapMaTuDong("NQ");
            return kq;
        }

        //Hàm cập nhật lại mã cho danh mục nhóm quyền sau khi thêm mới
        public void CapNhatMaNhomQuyen()
        {
            Connect.CapNhatMaTuDong("NQ");
        }


        //Hàm thêm dữ liệu vào CSDL
        public void ThemNhomQuyen(string Ten, string GhiChu)
        {
            try
            {
                NHOMQUYEN NQ = new NHOMQUYEN();
                NQ.MANHOMQUYEN = MaNhomQuyen();
                NQ.TENNHOMQUYEN = Ten;
                NQ.GHICHU = GhiChu;

                DialogResult thongbao = MessageBox.Show("Bạn có muốn thêm nhóm quyền này ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if (thongbao == DialogResult.Yes)
                {
                    Connect.Database().NHOMQUYENs.InsertOnSubmit(NQ);
                    Connect.Database().SubmitChanges();

                    //Cập nhật lại mã tự động tăng cho nhóm quyền
                    CapNhatMaNhomQuyen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Hàm sửa dữ liệu 1 nhóm quyền
        public void SuaNhomQuyen(string Ma, string Ten, string GhiChu)
        {
            try
            {
                NHOMQUYEN NQ = Connect.Database().NHOMQUYENs.SingleOrDefault(a => a.MANHOMQUYEN == Ma);
                if (NQ != null)
                {
                    NQ.TENNHOMQUYEN = Ten;
                    NQ.GHICHU = GhiChu;

                    DialogResult thongbao = MessageBox.Show("Bạn có muốn sửa lại thông tin nhóm quyền này ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (thongbao == DialogResult.Yes)
                    {
                        Connect.Database().SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Hàm xóa dữ liệu 1 nhóm quyền
        public void XoaNhomQuyen(string Ma)
        {
            try
            {
                NHOMQUYEN NQ = Connect.Database().NHOMQUYENs.SingleOrDefault(a => a.MANHOMQUYEN == Ma);
                if (NQ != null)
                {
                    DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa nhóm quyền này ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (thongbao == DialogResult.Yes)
                    {
                        Connect.Database().NHOMQUYENs.DeleteOnSubmit(NQ);
                        Connect.Database().SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
