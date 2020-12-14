using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTwoProject.Data
{
    public class StockItem
    {
        //Properties 
        public int Id { get; set; }

        public int InStock { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }


        //ctor
        public StockItem()
        {

        }
        
    }
}
