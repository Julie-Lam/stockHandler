using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgTwoProject.Data; 

namespace ProgTwoProject.Data_Access_Layer
{
    public class StockItemsRepository
    {

        private string _connString;

        public StockItemsRepository()
        {
            _connString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
        }


        //Methods 

        //GetStockItems() : IEnumerable<StockItem> - Refed in OrderController
        public IEnumerable<StockItem> GetStockItems() 
        {
            var stockItems = new List<StockItem>();

            SqlConnection connection = new SqlConnection(_connString);
            SqlCommand command = new SqlCommand("sp_SelectStockItems", connection);

            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    StockItem stockItem = new StockItem();

                    stockItem.Id = dataReader.GetInt32(0);
                    stockItem.Name = dataReader.GetString(1);
                    stockItem.Price = dataReader.GetDecimal(2);
                    stockItem.InStock = dataReader.GetInt32(3);

                    stockItems.Add(stockItem);
                }

                dataReader.Close();
                connection.Close();
            }
            return stockItems;

        }


        //GetStockItem(int id) : StockItem
        public StockItem GetStockItem(int id) {
            StockItem stockItem = null; 
            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();
            
            SqlCommand command = new SqlCommand("sp_SelectStockItemById @id", connection);
            
            command.Parameters.AddWithValue("@id", id);
            
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    stockItem = new StockItem();
                    stockItem.Id = dataReader.GetInt32(0);
                    stockItem.Name = dataReader.GetString(1);
                    stockItem.Price = dataReader.GetDecimal(2);
                    stockItem.InStock = dataReader.GetInt32(3); 
                }
                dataReader.Close();
                connection.Close(); 
            }

            return stockItem; 
        }


        //UpdateStockItemAmount(OrderHeader order) 
        public void UpdateStockItemAmount(OrderHeader orderHeader) {
            StockItem stockItem = null;

            SqlConnection connection = new SqlConnection(_connString);
            connection.Open(); 
            SqlCommand command = new SqlCommand("sp_UpdateStockItemAmount @id, @amount", connection);

            foreach (var orderItem in orderHeader._orderItems) { 
            command.Parameters.Add(new SqlParameter("@id", orderItem.StockItemId));
            command.Parameters.Add(new SqlParameter("@amount", -orderItem.Quantity)); //quantity of stockItem decreases as an order item is added
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            }

            connection.Close();
        }
    }
}
