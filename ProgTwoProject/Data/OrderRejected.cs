using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    class OrderRejected : OrderState
    {

        //Properties
        //Inherits State


        public OrderRejected(OrderHeader orderHeader)
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
