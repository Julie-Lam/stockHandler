using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Page
    {
        OrdersRepository _ordersRepo = new OrdersRepository();
        StockItemsRepository _stockRepo = new StockItemsRepository();
        OrderController _orderController = new OrderController(); 
        OrderHeader _orderHeader; 

        public OrderDetails(OrderHeader orderHeader)
        {
            InitializeComponent();
            _orderHeader = orderHeader;
            //On page load, displays the selected order's OrderId, DateTime, Total and OrderState. 
            uiOrderId.Text = _orderHeader.OrderID.ToString();
            uiDateTime.Text = _orderHeader.DateTime.ToString();
            uiTotal.Text = _orderHeader.Total.ToString(); // Displays the selected orderHeader's Total. 
            uiOrderState.Text = _orderHeader.OrderState;  

            var orderItems = _ordersRepo.GetOrderItemsByOrderHeaderID(orderHeader.OrderID); // Retrieves all the orderItems for this orderHeader

            // Calc Total amount for EA. orderItem's price*quantity to add into datagrid 
            foreach (OrderItem orderItem in orderItems) {
                orderItem.Total += (orderItem.Price * orderItem.Quantity); 
            }

            dgOrderItems.ItemsSource = orderItems; 
        }

        //Process Btn Click --> Navigates back to Orders View 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                int orderHeaderID = _orderHeader.OrderID; // Targets this orderheader's OrderID. 

                _orderController.ProcessOrder(orderHeaderID); //Updates Instock amount and Status 

                NavigationService.Navigate(new Orders(_orderController)); // Navigate back to Orders View 
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }

        }

        //Return Btn Click 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                NavigationService.Navigate(new Orders(_orderController));
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }
        }


        //Delete Btn Click 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try {
                _orderController.DeleteOrderHeaderAndOrderItems(_orderHeader.OrderID);
                NavigationService.Navigate(new Orders(_orderController));
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }

        }
    }
}
