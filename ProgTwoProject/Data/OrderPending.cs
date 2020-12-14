using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    class OrderPending : OrderState
    {

        //Properties
        //Inherits State


        public OrderPending(OrderHeader orderHeader)
        {
            this._orderHeader = orderHeader;
        }
        //Methods
        //Inherits Constructor
        //Inherits Complete()
        //Inherits Reject()
        //Inherits Submit()
    }
}
