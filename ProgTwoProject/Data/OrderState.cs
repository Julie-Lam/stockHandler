using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    public abstract class OrderState
    {
        //Fields
        public OrderHeader _orderHeader;


        //Properties 
        public string State {
            get;
            set;   
        }


        //ctor
        public OrderState()
        {

        }
 
    }
}
