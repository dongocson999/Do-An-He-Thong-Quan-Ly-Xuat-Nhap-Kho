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
using QLXuatNhapKhoLKDT.Classes.DanhMuc;

namespace QLXuatNhapKhoLKDT.Form.Danh_Muc
{
    public partial class XtraForm_LoaiLKDT : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        ConnectToDatabase Data;
        ClassLoaiLinhKien loailk;
        public XtraForm_LoaiLKDT()
        {
            InitializeComponent();
            Data = new ConnectToDatabase();
            loailk = new ClassLoaiLinhKien();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
            loailk.ThemLoai(textEdit1.Text,textEdit2.Text,textEdit3.Text);
            loailk.LoadData(this);
        }

        private void XtraForm_LoaiLKDT_Load(object sender, EventArgs e)
        {
            loailk.LoadData(this);
            textEdit1.Enabled = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string malk = textEdit1.Text;
            textEdit1.Enabled = false;
            loailk.SuaLoai(textEdit1.Text,textEdit2.Text, textEdit3.Text);
            loailk.LoadData(this);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            textEdit2.Text = gridView1.GetFocusedRowCellValue("TENLOAI").ToString();
            textEdit3.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            loailk.XoaLoai(textEdit1.Text);
            loailk.LoadData(this);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            textEdit1.Enabled = false;
            textEdit2.Text = "";
            textEdit3.Text = "";
        }
    }
}