using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    public class OrderHeader
    {
        //Fields
        //private string orderStateString = "Unknown";

        public List<OrderItem> _orderItems = new List<OrderItem>();


        //Properties 
        public int OrderID { get; set; } //Will probably need to edit set for this ID 

        public DateTime DateTime { get; set; }

        public int StateID { get; set; }

        public string OrderState {
            get; set;
        }

        public decimal Total { get; set; }


       public int OrderLineNum {get; set; } 


        //ctor 
        public OrderHeader(int OrderID, DateTime dateAndTime, int StateID) 
        {
            this.OrderID = OrderID;
            this.DateTime = dateAndTime;
            this.StateID = StateID; 
        }

        //Methods 
        public void AddOrderItem(int stockItemID, string description, decimal price, int quantity) 
        {
            //Ea. time in the UI we add a new item, this item needs to be added to the _orderItems list! 
            //Method takes in the OrderItem class properties retrieved from database and creates a new order item.

            //Increases the quatity number of an existing added order item, otherwise adds a new order item
            var item = _orderItems.FirstOrDefault(i => i.StockItemId == stockItemID);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new OrderItem(description, this.OrderID, price, quantity, stockItemID); 
                _orderItems.Add(item);
            }
        }

        public void Complete() { 

        }

        public void Reject()
        {

        }

        //For each orderHeader there is a stateID attached to it. Based on the StateID, we can set the string State by creating the appropriate order state class 
        //public void setState()
        //{
            
        //    if (StateID == 1)
        //    {
        //        OrderState = "New";
        //    }
        //    else if (StateID == 2)
        //    {
        //        OrderState = "Pending";
        //    }
        //    else if (StateID == 3) {
        //        OrderState = "Rejected";
        //    }
        //    else if (StateID == 4) {
        //        OrderState = "Completed";
        //    }
        //}


        public void setState(int OrderStateID)
        {
            if (OrderStateID == 1)
            {
                OrderState = "New";
            }
            else if (OrderStateID == 2)
            {
                OrderState = "Pending";
            }
            else if (OrderStateID == 3)
            {
                OrderState = "Completed";
            }
            else if (OrderStateID == 4)
            {
                OrderState = "Rejected";
            }
        }



        public void Submit()
        {

        }
    }
}
