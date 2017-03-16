using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace QLXuatNhapKhoLKDT
{
    class ConnectToDatabase
    {
        //Khai báo đối tượng linq
        DB_QL_XuatNhapKho_LKDTDataContext Data;

        public ConnectToDatabase()
        {
            Data = new DB_QL_XuatNhapKho_LKDTDataContext();
        }



        public DB_QL_XuatNhapKho_LKDTDataContext Database()
        {
            return Data;
        }



        //Hàm cấp mã tự động
        public string CapMaTuDong(string kytu)
        {
            string kq = kytu;
            int kyso = (int) Data.LAYMATUDONG(kytu);
            int dem = 5 - kq.Length - kyso.ToString().Length;
            for (int i = 1; i <= dem; i++)
            {
                kq += "0";
            }
            kq += kyso.ToString();
            return kq;
        }

        //Hàm cập nhật lại mã tự động sau khi cấp
        public void CapNhatMaTuDong(string kytu)
        {
            Data.CAPNHATMATUDONG(kytu);
        }

    }
}
