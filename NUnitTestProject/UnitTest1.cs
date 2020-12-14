using NUnit.Framework;
using ProgTwoProject.Controllers; 


namespace NUnitTestProject
{
    public class Tests
    {

        OrderController orderController;
        [SetUp]
        public void Setup()
        {
            //orderController = new OrderController();
            orderController = new OrderController();
        }

        [Test]
        public void Test1()
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