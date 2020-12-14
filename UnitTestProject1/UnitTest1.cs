using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProgTwoProject.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        OrderController _orderController; 

        [TestInitialize]
        public void Init() {
            _orderController = new OrderController(); 
        }

        [TestMethod]
        public void GetStockItemsTest()
        {
            int actual=0;
            var stockItems = _orderController.GetStockItems();
            foreach (var stockItem in stockItems) {
                actual++; 
            }

            int expected = 9;

            Assert.AreEqual(actual, expected); 
        }
    }
}
