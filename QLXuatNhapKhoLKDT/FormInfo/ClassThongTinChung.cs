using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLXuatNhapKhoLKDT.FormInfo
{
    class ClassThongTinChung
    {

        //Khởi tạo đối tượng kết nối CSDL
        ConnectToDatabase Connect;

        public ClassThongTinChung()
        {
            Connect = new ConnectToDatabase();
        }


        //Load dữ liệu thông tin doanh số - nhập  - xuất
        public object LoadData_Nhap_Xuat()
        {
            var dulieu = Connect.Database().LAY_TONGHOPDOANHTHU();
            return dulieu;
        }

        //Load dữ liệu thông tin thu - chi - công nợ
        public object LoadData_Thu_Chi_CongNo()
        {
            var dulieu = Connect.Database().LAY_THU_CHI_CONGNO();
            return dulieu;
        }

        //Load dữ liệu thông tin chung chức năng hệ thống
        public object LoadData_ChucNangChungHeThong()
        {
            var dulieu = Connect.Database().LAY_THONGTINCHUNG_CHUCNANG_HETHONG();
            return dulieu;
        }
    }
}
