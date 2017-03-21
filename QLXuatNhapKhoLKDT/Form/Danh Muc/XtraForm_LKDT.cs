using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLXuatNhapKhoLKDT.Form.Danh_Muc
{
    public partial class XtraForm_LKDT : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_LKDT()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            XtraForm_LoaiLKDT form = new XtraForm_LoaiLKDT();
            this.Enabled = false;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                this.Enabled = true;
            }
        }
    }
}