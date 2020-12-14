using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgTwoProject.Data;
using ProgTwoProject.Data_Access_Layer; 

namespace ProgTwoProject.Controllers
{
    public class OrderController
    {

        OrdersRepository _ordersRepo = new OrdersRepository();

        StockItemsRepository _stockItemsRepo = new StockItemsRepository();

        OrderItem _orderItem;

        OrderHeader orderHeader = null; 

        //ctor
        public OrderController()
        {

        }


        //methods 

        // GetOrderHeaders() : IEnumerable<OrderHeader> -> DONE. 
        public IEnumerable<OrderHeader> GetOrderHeaders() {
            return _ordersRepo.GetOrderHeaders(); //GetOrderHeaders() returns the IEnumerable<OrderHeader> collection
        }


        // CreateNewOrderHeader() : OrderHeader -> DONE
        public OrderHeader CreateNewOrderHeader() {
            return _ordersRepo.InsertOrderHeader(); //InsertOrderHeader() returns an OrderHeader
        }


        // UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity) : OrderHeader -> DONE 
        public OrderHeader UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity)
        {
            //Method inserts/updates the new order item into the orderItems table 

            StockItem selectedStockItem = _stockItemsRepo.GetStockItem(stockItemId); //Retrieves info abt the selected stock item from the db 

            orderHeader = _ordersRepo.GetOrderHeader(orderHeaderId); //Retrieve info abt the selected orderHeader from the db 

            orderHeader.AddOrderItem(selectedStockItem.Id, selectedStockItem.Name, selectedStockItem.Price, quantity); // Adds an order item to the orderHeader + incr quantity.  

            _orderItem = new OrderItem(selectedStockItem.Name, orderHeaderId, selectedStockItem.Price, quantity, selectedStockItem.Id); //Creates an orderItem using the retrieve stock item info 

            _ordersRepo.UpsertOrderItem(_orderItem); // Adds the new order item to the order items table in db THERE'S SOMETHING WRONG HERE.

            //P.S no need to update orderHeader because no additional info needs to be added 

            return orderHeader; 
        }



        // SubmitOrder(int orderHeaderId) : OrderHeader
        public OrderHeader SubmitOrder(int orderHeaderId)
        {

            orderHeader = _ordersRepo.GetOrderHeader(orderHeaderId); // Retrieves the orderHeader from the db, incl. all the orderItems 

            orderHeader.StateID = 2; // Change the instantiated orderHeader object's OrderStateID

            _ordersRepo.UpdateOrderState(orderHeader); // Updates the db with the orderHeader object's details 

            return orderHeader; // Returns the orderHeader object  
        }



        // ProcessOrder(int orderHeaderId) : OrderHeader
        public OrderHeader ProcessOrder(int orderHeaderId)
        {

            StockItem stockItem; 
            orderHeader = _ordersRepo.GetOrderHeader(orderHeaderId); //Instantiates orderHeader object with info from db 
            orderHeader._orderItems = _ordersRepo.GetOrderItemsByOrderHeaderID(orderHeaderId); // Feeds all the associated orderItems into the orderHeader 

            bool allowOrder = true;
            orderHeader.StateID = 3; // Completed

            //Allow Instock to update ONLY IF the instock value is larger than the orderItem quantity
            //for each order item's , find it's stock item via stockItemId, then compare the orderItem quantity to the stockItem InStock
            foreach (OrderItem orderItem in orderHeader._orderItems) {
                stockItem = _stockItemsRepo.GetStockItem(orderItem.StockItemId); //Retrieves stockItem via Id 

                if (orderItem.Quantity > stockItem.InStock)
                {
                    allowOrder = false;
                }
            }

            if (allowOrder)
            {
                _stockItemsRepo.UpdateStockItemAmount(orderHeader); // Method reduces the stockItem Instock for each orderItem ONLY IF allowOrder == true 
            }
            else {
                orderHeader.StateID = 4; // Rejected ONLY IF allowOrder == false 
            }
            

            _ordersRepo.UpdateOrderState(orderHeader); //Updates OrderStateID on the db accordingly NO MATTER WHAT. 

            return orderHeader; 
        }



        // DeleteOrderHeaderAndOrderItems(int orderHeaderId) -> DONE
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId) {
            _ordersRepo.DeleteOrderHeaderAndOrderItems(orderHeaderId); 
        }


        //DeleteOrderItem(int orderHeaderId, int stockItemId) -> DONE
        public void DeleteOrderItem(int orderHeaderId, int stockItemId) {
            _ordersRepo.DeleteOrderItem(orderHeaderId, stockItemId); 
        }


        // GetStockItems() : IEnumerable<StockItem>
        public IEnumerable<StockItem> GetStockItems() {
            return _stockItemsRepo.GetStockItems(); 
        }


    }
}
