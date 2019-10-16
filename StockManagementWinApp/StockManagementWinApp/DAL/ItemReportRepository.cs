using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagementWinApp.Model;

namespace StockManagementWinApp.DAL
{
    public class ItemReportRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["StockManagementConnectionString"].ConnectionString;
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private string commandString;

        public DataTable LoadComapny()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            //
            commandString = @"select distinct CompanyId, CompanyName from [dbo].[ItemsView]";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            //
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            DataRow row = dataTable.NewRow();
            row[0] = -1;
            row[1] = "Please Select";
            dataTable.Rows.InsertAt(row, 0);

            sqlConnection.Close();
            return dataTable;
        }

        public DataTable LoadCategory(Item item)
        {


            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            //
            commandString = @"select distinct CategoryId, CategoryName from [dbo].[ItemsView] where CompanyId='" + item.CompanyId + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);

            //
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            DataRow row = dataTable.NewRow();
            row[0] = -1;
            row[1] = "Please Select";
            dataTable.Rows.InsertAt(row, 0);

            sqlConnection.Close();
            return dataTable;
        }

    }
}
