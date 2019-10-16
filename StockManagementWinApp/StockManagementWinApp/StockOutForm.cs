using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManagementWinApp.BLL;
using StockManagementWinApp.Model;


namespace StockManagementWinApp
{
    public partial class StockOutForm : Form
    {
        private Item item;
        private Stock stock;

        public StockOutForm()
        {
            InitializeComponent();
            item = new Item();
            stock = new Stock();
        }

        StockOutManager _stockOutManager = new StockOutManager();


        private void StockOutForm_Load(object sender, EventArgs e)
        {
            companyComboBox.DataSource = _stockOutManager.LoadCompany();
            categoryComboBox.Enabled = false;
            itemComboBox.Enabled = false;

        }

        private void displayDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.displayDataGridView.Rows[e.RowIndex].Cells["sl"].Value = (e.RowIndex + 1).ToString();

        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (companyComboBox.SelectedValue.ToString() != null)
            {
                item.CompanyId = Convert.ToInt32(companyComboBox.SelectedValue.ToString());
                categoryComboBox.DataSource = _stockOutManager.LoadCategory(item);
                categoryComboBox.Enabled = true;
                itemComboBox.Enabled = false;
            }

        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedValue.ToString() != null)
            {
                item.CategoryId = Convert.ToInt32(categoryComboBox.SelectedValue.ToString());
                itemComboBox.DataSource = _stockOutManager.LoadItem(item);
                itemComboBox.Enabled = true;
            }
        }

        private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemComboBox.SelectedValue.ToString() != null)
            {
                item.Id = Convert.ToInt32(itemComboBox.SelectedValue.ToString());
                reorderLevelTextBox.Text = _stockOutManager.LoadReorderLevel(item).ToString();
                availableQuantityTextBox.Text = _stockOutManager.LoadAvailableQuantity(item).ToString();
                reorderLevelTextBox.Enabled = false;
                availableQuantityTextBox.Enabled = false;
            }
        }

        private void SellButton_Click(object sender, EventArgs e)
        {
            if (itemComboBox.SelectedValue.ToString() != null)
            {
                try
                {
                    stock.StockStatus = "Sell";
                    stock.ItemID = Convert.ToInt32(itemComboBox.SelectedValue.ToString());
                    stock.Quantity = Convert.ToInt32(stockOutQuantityTextBox.Text);

                    int isExecuted;
                    isExecuted = _stockOutManager.SellItem(stock);

                    if (isExecuted > 0)
                    {
                        MessageBox.Show(" Sucessfully Sold!!");
                    }
                    else
                    {
                        MessageBox.Show("Failed!");
                    }
                }

                catch (Exception exception)
                {
                    MessageBox.Show("Exception!");
                }

                stockOutQuantityTextBox.Clear();
                availableQuantityTextBox.Text = _stockOutManager.LoadAvailableQuantity(item).ToString();
            }
        }

        private void LostButton_Click(object sender, EventArgs e)
        {
            if (itemComboBox.SelectedValue.ToString() != null)
            {
                try
                {
                    stock.StockStatus = "Lost";
                    stock.ItemID = Convert.ToInt32(itemComboBox.SelectedValue.ToString());
                    stock.Quantity = Convert.ToInt32(stockOutQuantityTextBox.Text);

                    int isExecuted;
                    isExecuted = _stockOutManager.LostItem(stock);

                    if (isExecuted > 0)
                    {
                        MessageBox.Show(" Lost Item!!");
                    }
                    else
                    {
                        MessageBox.Show("Failed!");
                    }
                }

                catch (Exception exception)
                {
                    MessageBox.Show("Exception!");
                }

                stockOutQuantityTextBox.Clear();
                availableQuantityTextBox.Text = _stockOutManager.LoadAvailableQuantity(item).ToString();
            }
        }

        private void DamageButton_Click(object sender, EventArgs e)
        {
            if (itemComboBox.SelectedValue.ToString() != null)
            {
                try
                {
                    stock.StockStatus = "Damaged";
                  //  stock.StockDate = DateTime.MinValue.ToString();
                    stock.ItemID = Convert.ToInt32(itemComboBox.SelectedValue.ToString());
                    stock.Quantity = Convert.ToInt32(stockOutQuantityTextBox.Text);

                    int isExecuted;
                    isExecuted = _stockOutManager.DamagedItem(stock);

                    if (isExecuted > 0)
                    {
                        MessageBox.Show(" Damged Item!!");
                    }
                    else
                    {
                        MessageBox.Show("Failed!");
                    }
                }

                catch (Exception exception)
                {
                    MessageBox.Show("Exception!");
                }

                stockOutQuantityTextBox.Clear();
                availableQuantityTextBox.Text = _stockOutManager.LoadAvailableQuantity(item).ToString();
            }

            else

            {
                displayDataGridView.DataSource = _stockOutManager.ShowSellItem(stock);
            }
        }
    }
}
