using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLXuatNhapKhoLKDT.Form.Nghiep_Vu;

namespace QLXuatNhapKhoLKDT.Classes.NghiepVu
{
    class ClassNghiepVuNhap
    {

        //Tạo đối tượng kết nối CSDL
        ConnectToDatabase Data;

        //Tạo đối tượng nghiệp vụ nhập - xuất hỗ trợ việc nhập
        ClassNghiepVuNhap_Xuat Nhap_Xuat;

        //Tạo danh sách phiếu nhập dùng để truy xuất tìm kiếm
        private List<string> DanhSachPhieuNhap = new List<string>();

        //Tạo danh sách linh kiện dùng để truy xuất tìm kiếm
        private List<string> DanhSachLinhKien = new List<string>();

        public ClassNghiepVuNhap()
        {
            //Khởi tạo đối tượng kết nối CSDL
            Data = new ConnectToDatabase();

            //Khởi tạo đối tượng lưu trữ các dữ liệu liên quan việc nhập xuất
            Nhap_Xuat = new ClassNghiepVuNhap_Xuat();

            //Load dữ liệu hỗ trợ việc tìm kiếm
            LoadDataListPhieuNhap();
            LoadDataListLinhKien();
        }

        //Hàm load dữ liệu lên DanhSachPhieuNhap
        public void LoadDataListPhieuNhap()
        {
            DanhSachPhieuNhap = Nhap_Xuat.LoadDataToListPhieuNhap();
        }

        //Hàm load dữ liệu lên DanhSachLinhKien
        public void LoadDataListLinhKien()
        {
            DanhSachLinhKien = Nhap_Xuat.LoadDataToListLinhKien();
        }


        //Hàm tìm kiếm dữ liệu trả về chuỗi tìm thấy
        public string TimKiem_LinhKien(string chuoi)
        {
            foreach (var item in DanhSachLinhKien)
            {
                if (item.Contains(chuoi) == true)
                {
                    return item;
                }
            }
            return chuoi;
        }


        //Hàm lấy số phiếu nhập khi thêm 1 phiếu nhập mới
        public string CapMaSoPhieuNhap()
        {
            string kq = Data.CapMaTuDong("PN");
            return kq;
        }

        //Hàm cập nhập lại số phiếu nhập sau khi đã thêm mới
        private void CapNhatSoPhieuNhap()
        {
            Data.CapNhatMaTuDong("PN");
        }


        //Hàm trả về dữ liệu là danh sách phiếu nhập cho form Nghiệp Vụ Phiếu Nhập
        public object LayDuLieuPhieuNhap()
        {
            if (FormMain.UserId == "NQ001")
            {
                var dulieu = Data.Database().LAY_DANHMUC_PHIEUNHAP_QUANLY().ToList();
                return dulieu;
            }
            else
            {
                var dulieu = Data.Database().LAY_DANHMUC_PHIEUNHAP_NV_THUONG().ToList();
                return dulieu;
            }
        }

        //Hàm trả về danh mục chi tiết phiếu nhập đã chọn
        public void LayDuLieuChiTietPhieuNhap(string sophieu, XtraForm_NghiepVuNhapChiTiet frm)
        {
            var dulieu = Data.Database().LAY_DANHMUC_CHITIET_PHIEUNHAP(sophieu).ToList();
            int stt = 1;
            foreach (var item in dulieu)
            {
                ClassChiTietLinhKien ct = new ClassChiTietLinhKien();
                ct.STT = stt++;
                ct.MaLK = item.MA_LK;
                ct.TenLK = item.TEN_LK;
                ct.QuyCach = item.TENQUYCACH;
                ct.SoLuong = (int)item.SOLUONGNHAP;
                ct.DonGia = (int)item.DONGIANHAP;
                ct.ChietKhau = (float)item.CHIETKHAU;
                ct.ThanhTien = ((int)item.SOLUONGNHAP *(int)item.DONGIANHAP) - ((int)item.SOLUONGNHAP * (int)item.DONGIANHAP * (float)item.CHIETKHAU);

                frm.DanhSachChiTietLinhKien.Add(ct);
            }
        }

        //Hàm load dữ liệu chi tiết phiếu nhập của phiếu nhập đã chọn
        public void LoadDataChiTietPhieuNhap(XtraForm_NghiepVuNhapKho frm, string sophieu)
        {
            var dulieu = Data.Database().LAY_DANHMUC_CHITIET_PHIEUNHAP(sophieu).ToList();
            frm.gridControl_ChiTietPhieuNhap.DataSource = dulieu;
        }

        //Hàm load dữ liệu cho combobox Kho Hang
        public void LoadDataToCombo(XtraForm_NghiepVuNhapKho frm)
        {
            var dulieu1 = Data.Database().LAY_DANHMUC_KHO().ToList();
            frm.comboKhoHang.DataSource = dulieu1;
            frm.comboKhoHang.DisplayMember = "TENKHOHANG";
            frm.comboKhoHang.ValueMember = "MAKHOHANG";

            var dulieu2 = Data.Database().LAY_DANHMUC_NHACUNGCAP().ToList();
            frm.comboNCC.DataSource = dulieu2;
            frm.comboNCC.DisplayMember = "TENNCC";
            frm.comboNCC.ValueMember = "MANCC";
        }

        //Hàm tìm kiếm mã linh kiện theo số phiếu nhập
        public object TimKiemLinhKienTheoSoPhieuNhap(string malk, string sophieu)
        {
            var ob = Data.Database().CT_PHIEUNHAPs.SingleOrDefault(a => a.MA_LK == malk && a.SOPHIEUNHAP == sophieu);
            return ob;
        }

        //Thêm 1 dòng dữ liệu mới vào CSDL PHIEUNHAP
        public void ThemPhieuNhap(string sophieu, string lydo, string manv, string mancc, string makho, DateTime ngaylap)
        {
            try
            {
                if (KiemTra(lydo, mancc, makho) == true)
                {
                    //Thêm 1 dòng dữ liệu phiếu nhập vào CSDL
                    Data.Database().THEM_PHIEUNHAP(sophieu, ngaylap, lydo, manv, mancc, makho);

                    //Cập nhật lại số phiếu nhập sau khi đã thêm phiếu nhập
                    CapNhatSoPhieuNhap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Thêm 1 dòng dữ liệu vào CSDL Chi Tiết Phiếu Nhập
        public void Them_CT_PhieuNhap(string sophieu, string malk, int soluong, int dongia, string chietkhau)
        {
            try
            {
                if (KiemTra_ChiTiet_PhieuNhap(soluong,dongia,chietkhau) == true)
                {
                    //Thêm 1 dòng dữ liệu vào trong CSDL chi tiết phiếu nhập
                    Data.Database().THEM_CHITIET_PHIEUNHAP(sophieu,malk,soluong,dongia,float.Parse(chietkhau));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Xóa 1 dòng dữ liệu trong CSDL PHIEUNHAP <ĐK: chưa lập chi tiết cho phiếu này>
        public void XoaPhieuNhap(string sophieunhap)
        {
            try
            {
                if (sophieunhap != "")
                {
                    PHIEUNHAP pn = Data.Database().PHIEUNHAPs.SingleOrDefault(a => a.SOPHIEUNHAP == sophieunhap);
                    if (pn != null)
                    {
                        DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa phiếu nhập này không ?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                        if (thongbao == DialogResult.Yes)
                        {
                            var ctpn = Data.Database().CT_PHIEUNHAPs.Where(a => a.SOPHIEUNHAP == sophieunhap).ToList();
                            if (ctpn.Count() == 0)
                            {
                                Data.Database().XOA_PHIEUNHAP(sophieunhap);

                                MessageBox.Show("Xóa phiếu nhập thành công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Phiếu nhập này hiện đã có chi tiết phiếu nên không được quyền xóa!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 phiếu nhập cần xóa!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Xóa 1 dòng dữ liệu trong CSDL Chi Tiết Phiếu Nhập
        public void Xoa_ChiTiet_PhieuNhap(string sophieunhap, string malk)
        {
            try
            {
                if (sophieunhap != "" && malk != "")
                {
                    CT_PHIEUNHAP ct = Data.Database().CT_PHIEUNHAPs.SingleOrDefault(a => a.SOPHIEUNHAP == sophieunhap && a.MA_LK == malk);
                    if (ct != null)
                    {
                        DialogResult thongbao = MessageBox.Show("Bạn có muốn xóa chi tiết đang chọn này ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().XOA_CHITIET_PHIEUNHAP(ct.SOPHIEUNHAP, ct.MA_LK);

                            MessageBox.Show("Xóa thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng để chỉnh sửa chi tiết","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Chỉnh sủa 1 dòng dữ liệu trong CSDL PHIEUNHAP
        public void SuaPhieuNhap(string sophieunhap, DateTime ngaylap, string lydo, string mancc, string makho)
        {
            try
            {
                if (sophieunhap != "")
                {
                    PHIEUNHAP PN = Data.Database().PHIEUNHAPs.SingleOrDefault(a => a.SOPHIEUNHAP == sophieunhap);
                    if (PN != null)
                    {
                        PN.NGAYNHAP = ngaylap;
                        PN.LYDONHAP = lydo;
                        PN.MANCC = mancc;
                        PN.MAKHOHANG = makho;

                        DialogResult thongbao = MessageBox.Show("Bạn có chắc muốn sửa phiếu nhập này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (thongbao == DialogResult.Yes)
                        {
                            Data.Database().SubmitChanges();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng phiếu nhập để bắt đầu chỉnh sửa !","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Chỉnh Sửa 1 dòng dữ liệu trong CSDL Chi Tiết Phiếu Nhập
        public void Sua_ChiTiet_PhieuNhap(string sophieunhap, string malk, int soluong, int dongia, string chietkhau)
        {
            try
            {
                if (sophieunhap != "" && malk != "")
                {
                    CT_PHIEUNHAP ct = Data.Database().CT_PHIEUNHAPs.SingleOrDefault(a => a.SOPHIEUNHAP == sophieunhap && a.MA_LK == malk);
                    if (ct != null)
                    {
                        ct.SOLUONGNHAP = soluong;
                        ct.DONGIANHAP = dongia;
                        ct.CHIETKHAU = float.Parse(chietkhau);

                        Data.Database().SubmitChanges();

                        MessageBox.Show("Đã sửa thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm được chi tiết phiếu mà bạn đã chọn trong CSDL","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng chi tiết phiếu nhập để bắt đầu chỉnh sửa !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Kiểm tra dữ liệu khi thêm phiếu nhập
        private bool KiemTra(string lydo, string mancc, string makho)
        {
            bool kt = true;
            string kq = "Thông báo: ";
            if (lydo.Length > 21)
            {
                kt = false;
                kq += "\n - Vui lòng nhập lý do tóm tắt (ít hơn 20 ký tự).";
            }
            if (mancc == "")
            {
                kt = false;
                kq += "\n - Vui lòng chọn nhà cung cấp.";
            }
            if (makho == "")
            {
                kt = false;
                kq += "\n - Vui lòng chọn kho lưu trữ.";
            }
            if (kt == false)
            {
                MessageBox.Show(kq, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return kt;
        }

        //Hàm kiểm tra 1 chuỗi là số : đúng -> true, sai -> false
        private bool IsNumber(string chuoi)
        {
            foreach (char c in chuoi)
            {
                if (Char.IsLetter(c) == true)
                    return false;
            }
            return true;
        }

        //Hàm kiệm tra 1 chuỗi là số thực: đúng -> true; sai -> false
        private bool IsFloat(string chuoi)
        {
            float a;
            bool kt = float.TryParse(chuoi, out a);
            return kt;
        }


        //Kiểm tra dữ liệu khi thêm chi tiết phiếu nhập
        private bool KiemTra_ChiTiet_PhieuNhap(int soluong, int dongia, string chietkhau)
        {
            bool kt = true;
            string kq = "Thông báo: ";

            if (IsNumber(soluong.ToString()) == false)
            {
                kt = false;
                kq += "\n - Không được nhập ký tự ở số lượng.";
            }
            else
            {
                if (soluong < 0)
                {
                    kt = false;
                    kq += "\n - Số lượng không được <= 0";
                }
            }

            if (IsNumber(dongia.ToString()) == false)
            {
                kt = false;
                kq += "\n - Không được nhập ký tự ở đơn giá.";
            }
            else
            {
                if (soluong < 0)
                {
                    kt = false;
                    kq += "\n - Đơn giá không được <= 0";
                }
            }

            if (IsFloat(chietkhau.ToString()) == false)
            {
                kt = false;
                kq += "\n - Không được nhập chữ ở chiết khấu.";
            }
            else
            {
                if (float.Parse(chietkhau) < 0)
                {
                    kt = false;
                    kq += "\n - Chiết khấu không được phép âm.";
                }
            }

            if (kt == false)
            {
                MessageBox.Show(kq, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return kt;
        }
    }
}
