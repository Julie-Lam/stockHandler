using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProgTwoProject.Data;
using ProgTwoProject.Data_Access_Layer;
using ProgTwoProject.Controllers; 

namespace ProgTwoProject.View
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {

        private OrdersRepository _ordersRepo = new OrdersRepository();
        private StockItemsRepository stockRepo;
        IEnumerable<OrderHeader> orderHeaders = new List<OrderHeader>(); 
        OrderHeader _orderHeader;
        List<OrderItem> orderItems; 

        private OrderController _orderController = new OrderController(); 

        public Orders(OrderController orderController) 
        {
            //On start-up, we want to load all orders using a method to retrieve all orderHeaders

            _orderController = orderController; 
            
            InitializeComponent();

            orderHeaders = _orderController.GetOrderHeaders(); // Retrieves orderHeaders from db (Id, OrderStateId, DateTime)
            
            foreach (OrderHeader order in orderHeaders) {
                order.setState(order.StateID); //Translates the OrderStateId to OrderState 
                orderItems = _ordersRepo.GetOrderItemsByOrderHeaderID(order.OrderID);  // Gets all orderItems for a orderHeader ID
                order._orderItems = orderItems; // Add it to each OrderHeader orderItems List
                order.OrderLineNum = order._orderItems.Count(); 
                //Sets the total of each OrderHeader 
                foreach (OrderItem orderItem in order._orderItems) {
                    order.Total = order.Total + (orderItem.Price * orderItem.Quantity); 
                }
            }
            
            dgOrders.ItemsSource = orderHeaders; // Populates the datagrid 
        }

        //Add Order Btn Clicked -> Navigates to Add Order Page. 
        private void button_Click(object sender, RoutedEventArgs e) // --> DONE 
        {
            try {

                OrderHeader orderHeader = _orderController.CreateNewOrderHeader(); // Creates a new orderHeader in db, populates with default Id, OrderStateId and DateTime. Retrieves the orderHeader from db.   
                NavigationService.Navigate(new AddOrder(orderHeader)); // Navigates the info abt the new orderHeader to the next page 
            }
            catch (Exception error) {
                MessageBox.Show($"Error Encountered: {error}"); 
            }

        }


        //View Details Btn clicked -> Navigates to OrderDetails
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                OrderHeader selectedOrder = (OrderHeader)dgOrders.SelectedItem;  //Retrieves selectedOrder's details 
                NavigationService.Navigate(new OrderDetails(selectedOrder));   // Navigates to OrderDetails view - needs info on OrderId, DateTime, Total, StateID 
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }

        }
    }
}
