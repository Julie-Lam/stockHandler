using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    class OrderNew : OrderState
    {
        //Fields
            //Inherits _orderHeader

        //Properties
        //Inherits State


        //ctor
        public OrderNew(OrderHeader orderHeader)
        {
            this._orderHeader = orderHeader; //
        } 

        //Methods
            //Inherits Complete()
            //Inherits Reject()
            //Inherits Submit()

    }
}
