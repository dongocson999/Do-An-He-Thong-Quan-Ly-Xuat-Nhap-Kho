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
            
        }

        private void btXoa_Click(object sender, EventArgs e)
        {

        }
       
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var lk = gridView1.GetFocusedRowCellValue("MA_LK").ToString();
            
        }
    }
}