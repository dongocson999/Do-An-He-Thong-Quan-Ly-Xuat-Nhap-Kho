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
using QLXuatNhapKhoLKDT.Classes.DanhMuc;


namespace QLXuatNhapKhoLKDT.Form.Danh_Muc
{
    public partial class XtraForm_LKDT : DevExpress.XtraEditors.XtraForm
    {
        ConnectToDatabase Data;
        ClassLinhKien clk; //Khai báo biến kiểu ClassLinhKien
        public XtraForm_LKDT()
        {
            InitializeComponent();
            clk = new ClassLinhKien(); //Khởi tạo biến lấy dữ liệu của class Linh kiện
            Data = new ConnectToDatabase();
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

        private void XtraForm_LKDT_Load(object sender, EventArgs e)
        {
            clk.LoadData(this); //Load dữ liệu lên from
        }
        

        private void btLuu_Click(object sender, EventArgs e)
        {
            clk.ThemLK(comboBoxEdit2.SelectedItem.ToString(), comboBoxEdit3.SelectedItem.ToString(), textEdit1.Text,dateTimePicker1.Value, textEdit4.Text, Int32.Parse(textEdit2.Text), false, pictureEdit1.Text, comboBoxEdit1.SelectedItem.ToString());
            clk.LoadData(this);
        }

        private void btChinhSua_Click(object sender, EventArgs e)
        {
            clk.SuaLK(comboBoxEdit2.SelectedItem.ToString(), comboBoxEdit3.SelectedItem.ToString(), textEdit1.Text, dateTimePicker1.Value, textEdit4.Text, Int32.Parse(textEdit2.Text), false, pictureEdit1.Text, comboBoxEdit1.SelectedItem.ToString());
            clk.LoadData(this);
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            clk.XoaLK();
            clk.LoadData(this);
        }
       
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            comboBoxEdit1.Text = gridView1.GetFocusedRowCellValue("MAQC").ToString();
            comboBoxEdit2.Text = gridView1.GetFocusedRowCellValue("MANSX").ToString();
            comboBoxEdit3.Text = gridView1.GetFocusedRowCellValue("MALOAI").ToString();
            textEdit1.Text = gridView1.GetFocusedRowCellValue("TEN_LK").ToString();
            dateTimePicker1.Text = gridView1.GetFocusedRowCellValue("THOIGIAN_BH").ToString();
            textEdit2.Text = gridView1.GetFocusedRowCellValue("TRONGLUONG").ToString();
            textEdit3.Text = gridView1.GetFocusedRowCellValue("HINHANH").ToString();
            textEdit4.Text = gridView1.GetFocusedRowCellValue("TINHNANG").ToString();
            checkEdit1.Text= gridView1.GetFocusedRowCellValue("DUOCPHEPDOITRA").ToString();
        }

        private void btTaoMoi_Click(object sender, EventArgs e)
        {
            comboBoxEdit1.SelectedIndex = -1;
            comboBoxEdit2.SelectedIndex = -1;
            comboBoxEdit3.SelectedIndex = -1;
            textEdit1.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString();
            textEdit2.Text = "";
            textEdit3.Text = "";
            textEdit4.Text = "";
            checkEdit1.Text = "";
        }
    }
}