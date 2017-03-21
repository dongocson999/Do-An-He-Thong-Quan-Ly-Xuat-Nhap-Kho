using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace QLXuatNhapKhoLKDT.Form.Danh_Muc
{
    public partial class XtraForm_DanhMucKhachHang : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public XtraForm_DanhMucKhachHang()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtraForm_LoaiKhachHang form = new XtraForm_LoaiKhachHang();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }
    }
}