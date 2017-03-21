using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXuatNhapKhoLKDT.Classes.NhomQuyen
{
    class ChucNangNhomQuyen
    {
        //Tạo biến kết nối CSDL
        ConnectToDatabase Data;

        //Tạo biến lưu cho biết đang chỉnh sửa
        bool ChinhSua = false;


        public ChucNangNhomQuyen()
        {
            Data = new ConnectToDatabase();
        }

        //Lấy danh mục chức năng của 1 nhóm quyền
        public object LayChucNangCuaNhomQuyen(string manhomquyen)
        {
            var dulieu = Data.Database().LAY_CHUCNANG_CUA_NHOMQUYEN(manhomquyen).ToList();
            return dulieu;
        }

        //Lấy danh mục chức năng chưa cấp cho nhóm quyền
        public object LayChucNangChuaCapChoNhomQuyen(string manhomquyen)
        {
            var dulieu = Data.Database().LAY_CHUCNANG_CHUACAP_CHO_NHOMQUYEN(manhomquyen).ToList();
            return dulieu;
        }

        //Thêm 1 chức năng mới vào nhóm quyền đã chọn
        public void ThemVao(string manhomquyen, string sttchucnangchuaco)
        {
            if (sttchucnangchuaco.ToString() != "")
            {
                Data.Database().THEM_CHUCNANGMOI_VAO_NHOMQUYEN(manhomquyen,int.Parse(sttchucnangchuaco));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức năng mới muốn thêm vào !","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        //Xóa 1 chức năng của nhóm quyền được chọn
        public void BoRa(string manhomquyen, string sttchucnangdaco)
        {
            if (sttchucnangdaco.ToString() != "")
            {
                Data.Database().XOA_CHUCNANG_CUA_NHOMQUYEN(manhomquyen,int.Parse(sttchucnangdaco));
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức hiện hành của nhóm quyền muốn bỏ ra !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
