using StockManagementWinApp.BLL;
using StockManagementWinApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagementWinApp
{
    public partial class StockReportForm : Form
    {
        private Stock stock;
        public StockReportForm()
        {
            InitializeComponent();

            stock = new Stock();
        }

        StockReportManager _stockReportManager = new StockReportManager();
        private void StockReportForm_Load(object sender, EventArgs e)
        {
            stockReportDataGridView.Rows.Clear();
            soldRadioButton.Checked = true;
        }

        private void soldRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            stock.StockStatus = "Sold";
            stockReportDataGridView.Columns[3].HeaderText = "Sold Quantity";
        }

        private void damageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            stock.StockStatus = "Damaged";
            stockReportDataGridView.Columns[3].HeaderText = "Damaged Quantity";
            //stockReportDataGridView.DataSource = _stockReportManager.Search(stock);
        }

        private void lostRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            stock.StockStatus = "Lost";
            stockReportDataGridView.Columns[3].HeaderText = "Lost Quantity";
            //stockReportDataGridView.DataSource = _stockReportManager.Search(stock);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            stock.FromStockDate = fromDateTimePicker.Value.ToShortDateString();
            stock.ToStockDate = toDateTimePicker.Value.ToShortDateString();
            bool flag = _stockReportManager.SearchExists(stock);
            if (flag)
            {
                stockReportDataGridView.DataSource = _stockReportManager.Search(stock);
            }
            else
            {
                MessageBox.Show("Data Not Found!","Result");
                stockReportDataGridView.DataSource = _stockReportManager.Search(stock);
            }
            
        }

        private void stockReportDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            stockReportDataGridView.Rows[e.RowIndex].Cells["SL"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
