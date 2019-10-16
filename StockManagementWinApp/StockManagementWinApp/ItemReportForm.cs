using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManagementWinApp.BLL;
using StockManagementWinApp.Model;

namespace StockManagementWinApp
{
    public partial class ItemReportForm : Form
    {
        private Item item;
        public ItemReportForm()
        {
            InitializeComponent();
            item=new Item();
        }

        ItemReportManager _itemReportManager = new ItemReportManager();
        private void ItemReportForm_Load(object sender, EventArgs e)
        {
            companyComboBox.DataSource = _itemReportManager.LoadCompany();
            categoryComboBox.Enabled = false;

        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (companyComboBox.SelectedValue.ToString() != null)
            {
                item.CompanyId = Convert.ToInt32(companyComboBox.SelectedValue.ToString());
                categoryComboBox.DataSource = _itemReportManager.LoadCategory(item);
                categoryComboBox.Enabled = true;
             
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.displayDataGridView.Rows[e.RowIndex].Cells["sl"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
