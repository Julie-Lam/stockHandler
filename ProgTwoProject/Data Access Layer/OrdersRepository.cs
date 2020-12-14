using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    public class OrdersRepository
    {
        //Fields

        private string _connString;


        //ctor 
        public OrdersRepository() {
            // Getting connection string 
            _connString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString; 
        }


        //methods 

        // InsertOrderHeader() : OrderHeader - *Refed in OrderController DEEFINITELY WORKS 
        public OrderHeader InsertOrderHeader() {

            OrderHeader orderHeader = null;

            SqlConnection connection = new SqlConnection(_connString);
            connection.Open(); 

            //Sql command is adds an OrderStateId of 1 (i.e. "New") and today's date to the OrderHeaders table.
            using (var command = new SqlCommand("sp_InsertOrderHeader", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Selects the id value from the OrderHeaders table 
                int id = Convert.ToInt32(command.ExecuteScalar());

                //Uses the id to target the newly inserted table row 
                var newCommand = new SqlCommand("SELECT * FROM OrderHeaders WHERE Id = " + id, connection); 

                //Then we use the reader to read and select the table's date value
                using (SqlDataReader reader = newCommand.ExecuteReader())
                {
                    reader.Read();
                    var dateTime = reader.GetDateTime(2);
                    orderHeader = new OrderHeader(id, dateTime, 1);
                    reader.Close();
                }
            }

            return orderHeader; 

        }




        // GetOrderHeader(int id) : OrderHeader 
        public OrderHeader GetOrderHeader(int id) {

            OrderHeader orderHeader = null;

            //OrderHeader class properties 
            int OrderState;
            DateTime dateAndTime;

            //OrderItem class properties 
            int StockItemId;
            string Description;
            decimal Price;
            int Quantity;

            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();

            //Sql command takes in para of id and selects the OrderHeader row
            using (var command = new SqlCommand("sp_SelectOrderHeaderById @id", connection))
            {
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", id));

                //We use the reader to instantiate a new orderHeader object
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        OrderState = dataReader.GetInt32(1);
                        dateAndTime = dataReader.GetDateTime(2);

                        orderHeader = new OrderHeader(id, dateAndTime, OrderState);
                    }
                    dataReader.Close();
                }
            }

            var newCommand = new SqlCommand("select * from OrderItems where OrderHeaderId = " + id, connection);
            SqlDataReader newDataReader = newCommand.ExecuteReader();

            if (newDataReader.HasRows)
            {
                while (newDataReader.Read())
                {
                    StockItemId = newDataReader.GetInt32(1);
                    Description = newDataReader.GetString(2);
                    Price = newDataReader.GetDecimal(3);
                    Quantity = newDataReader.GetInt32(4);

                    orderHeader.AddOrderItem(StockItemId, Description, Price, Quantity);
                }
                newDataReader.Close();
            }

            connection.Close();
            return orderHeader;
        }


        //	GetOrderHeaders() : IEnumerable<OrderHeader> - *Refed in OrderController 
        public IEnumerable<OrderHeader> GetOrderHeaders() {

            var orders = new List<OrderHeader>();

            SqlConnection connection = new SqlConnection(_connString);
            //SqlCommand command = new SqlCommand("sp_SelectOrderHeaders", connection);
            SqlCommand command = new SqlCommand("select * from OrderHeaders", connection); 

            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows) {
                while (dataReader.Read()) {
                    OrderHeader orderHeader = new OrderHeader(dataReader.GetInt32(0), dataReader.GetDateTime(2), dataReader.GetInt32(1)); 

                    orders.Add(orderHeader); 
                }

                dataReader.Close();
                connection.Close();
            }
            return orders; 
        }


        // UpsertOrderItem(OrderItem orderItem)
        public void UpsertOrderItem(OrderItem orderItem) {
            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();
            using (var command = new SqlCommand("sp_UpsertOrderItem @orderHeaderId, @stockItemId, @description, @price, @quantity", connection))
            {
                command.Parameters.Add(new SqlParameter("@orderHeaderId", orderItem.OrderHeaderId));
                command.Parameters.Add(new SqlParameter("@stockItemId", orderItem.StockItemId));
                command.Parameters.Add(new SqlParameter("@description", orderItem.Description));
                command.Parameters.Add(new SqlParameter("@price", orderItem.Price));
                command.Parameters.Add(new SqlParameter("@quantity", orderItem.Quantity));
                command.ExecuteNonQuery();
            }

        }


        // UpdateOrderState(OrderHeader order) 
        public void UpdateOrderState(OrderHeader orderHeader) {
            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();

            SqlCommand command = new SqlCommand("sp_UpdateOrderState @orderHeaderId, @stateId", connection);
            command.Parameters.Add(new SqlParameter("@stateId", orderHeader.StateID));
            command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeader.OrderID));
            command.ExecuteNonQuery();

            connection.Close();
        }



        //	DeleteOrderHeaderAndOrderItems(int orderHeaderId) - *Refed in OrderController
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderID) {
            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();

            SqlCommand command = new SqlCommand("sp_DeleteOrderHeaderAndOrderItems @orderHeaderId", connection);
            command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderID));
            command.ExecuteNonQuery();
            connection.Close();
        }

        // DeleteOrderItem(int orderHeaderId,int stockItemId) - *Refed in OrderController
        public void DeleteOrderItem(int orderHeaderID, int stockItemID) {
            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();

            SqlCommand command = new SqlCommand("sp_DeleteOrderItem @orderHeaderId, @stockItemId", connection);
            command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderID));
            command.Parameters.Add(new SqlParameter("@stockItemId", stockItemID));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<OrderItem> GetOrderItemsByOrderHeaderID(int orderHeaderID) {
            var orderItems = new List<OrderItem>(); 
            OrderItem orderItem = null;

            SqlConnection connection = new SqlConnection(_connString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * from OrderItems WHERE OrderHeaderId = " + orderHeaderID, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    orderItem = new OrderItem(dataReader.GetString(2), dataReader.GetInt32(0), dataReader.GetDecimal(3), dataReader.GetInt32(4), dataReader.GetInt32(1));
                    orderItems.Add(orderItem); 
                }

                dataReader.Close();
                connection.Close();
            }

            return orderItems; 
        }

    }
}
