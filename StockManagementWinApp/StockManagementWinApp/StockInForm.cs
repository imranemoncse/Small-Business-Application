using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManagementWinApp.Model;
using System.Configuration;
using StockManagementWinApp.BLL;

namespace StockManagementWinApp
{
    public partial class StockInForm : Form
    {

        private Item item;
        private Stock stock;
        public StockInForm()
        {
            InitializeComponent();

            item = new Item();
            stock = new Stock();
            errorLabel.Text = "";
        }


        StockInManager _stockInManager = new StockInManager();


        private void StockInForm_Load(object sender, EventArgs e)
        {

            itemDataGridView.Rows.Clear();
            companyComboBox.DataSource = _stockInManager.SelectCompany();
            categoryComboBox.Enabled = false;
            itemComboBox.Enabled = false;

        }

  

        private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (itemComboBox.SelectedValue.ToString() != null)
            {
                item.Id = Convert.ToInt32(itemComboBox.SelectedValue.ToString());
                
                stock.ItemID = item.Id;
                reorderTextBox.Text = _stockInManager.LoadReorderLevel(item);
                availableQtyTextBox.Text = _stockInManager.LoadAvailableQty(stock);


                itemDataGridView.DataSource = _stockInManager.ShowGridItem(stock);

            }

        }

        private void companyComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (companyComboBox.SelectedValue.ToString() != null)
            {
                item.CompanyId = Convert.ToInt32(companyComboBox.SelectedValue.ToString());
                categoryComboBox.DataSource = _stockInManager.SelectCategory(item);
                categoryComboBox.Enabled = true;
                itemComboBox.Enabled = false;
            }
        }

        private void categoryComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedValue.ToString() != null)
            {
                item.CategoryId = Convert.ToInt32(categoryComboBox.SelectedValue.ToString());
                itemComboBox.DataSource = _stockInManager.SelectItem(item);
                itemComboBox.Enabled = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt32(companyComboBox.SelectedValue) < 1)
                {
                    errorLabel.Text = "Please Select a Company!";
                    errorLabel.ForeColor = Color.Red;
                    return;
                }
                if (Convert.ToInt32(categoryComboBox.SelectedValue) < 1)
                {
                    errorLabel.Text = "Please Select a Category!";
                    errorLabel.ForeColor = Color.Red;
                    return;
                }
                if (Convert.ToInt32(itemComboBox.SelectedValue) < 1)
                {
                    errorLabel.Text = "Please Select a Item!";
                    errorLabel.ForeColor = Color.Red;
                    return;
                }
                if (stockInTextBox.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Stock Quantity.");
                    errorLabel.ForeColor = Color.Red;
                    return;
                }
                if (Convert.ToInt32(stockInTextBox.Text) < 0)
                {
                    errorLabel.Text = "Invalied Input in StockIn Quantity!";
                    errorLabel.ForeColor = Color.Red;
                    return;
                }

                else
                {
                    
                    stock.CompanyId = Convert.ToInt32(companyComboBox.SelectedValue);
                    stock.CategoryId = Convert.ToInt32(categoryComboBox.SelectedValue);
                    stock.ItemID = Convert.ToInt32(itemComboBox.SelectedValue);
                    stock.Quantity = Convert.ToInt32(stockInTextBox.Text);
                    

                }

                int isExecuted;

                    if (SaveButton.Text == "Save")
                    {
                        
                        
                        isExecuted = _stockInManager.Insert(stock);
                        SaveLabel.Text = "Item Stocked In!";
                        itemDataGridView.DataSource = _stockInManager.ShowGridItem(stock);
                        availableQtyTextBox.Text = _stockInManager.LoadAvailableQty(stock);


                }
                    else
                    {
                        stock.StockId = Convert.ToInt32(idTextBox.Text);
                        

                    if ((Convert.ToInt32(availableQtyTextBox.Text)) <= (Convert.ToInt32(prevQtyTextBox.Text) - Convert.ToInt32(stockInTextBox.Text)))
                    {
                        isExecuted = _stockInManager.Update(stock);
                        SaveLabel.Text = "Item Updated!";
                        itemDataGridView.DataSource = _stockInManager.ShowGridItem(stock);
                        availableQtyTextBox.Text = _stockInManager.LoadAvailableQty(stock);

                    }
                    else
                    {
                        MessageBox.Show("Invalid amount! StockIn Quantity can't be less than avaiable quantity");
                        return;

                    }

                   
                }
                SaveButton.Text = "Save";
                idTextBox.Text = "";

            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            itemDataGridView.Focus();

        }



        private void itemDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (itemDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                itemDataGridView.CurrentRow.Selected = true;

                stockInTextBox.Text = itemDataGridView.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                prevQtyTextBox.Text = itemDataGridView.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                SaveButton.Text = "Update";
                
                idTextBox.Text = itemDataGridView.Rows[e.RowIndex].Cells["StockId"].Value.ToString();
                
            }
        }

        private void itemDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.itemDataGridView.Rows[e.RowIndex].Cells["SL"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
