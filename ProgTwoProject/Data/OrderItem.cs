using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    public class OrderItem
    {
        
        //Properties
        public string Description { get; set; }

        public OrderHeader OrderHeader { get; set; }

        public int OrderHeaderId {
            get;  /*{ return OrderHeader.OrderID; }*/
            set; /* { } */
        }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int StockItemId { get; set; }

        public decimal Total { get; set; }

        //ctor 
        public OrderItem(string Description, int OrderHeaderId, decimal Price, int Quantity, int StockItemId)
        {
            this.Description = Description;
            this.OrderHeaderId = OrderHeaderId;
            this.Price = Price;
            this.Quantity = Quantity;
            this.StockItemId = StockItemId; 
        }
    }
}
