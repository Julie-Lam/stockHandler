using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProgTwoProject.Controllers; 

namespace UnitTestProjectMS
{


    [TestClass]
    public class UnitTest1
    {

        OrderController orderController;

        [TestInitialize]
        public void Init() {
            orderController = new OrderController(); 
        }

        [TestMethod]
        public void GetStockItemsTest()
        {
            int actual = 0;
            var stockItems = orderController.GetStockItems();
            foreach (var stockItem in stockItems)
            {
                actual++;
            }

            int expected = 9;

            Assert.AreEqual(actual, expected);
        }
    }
}
