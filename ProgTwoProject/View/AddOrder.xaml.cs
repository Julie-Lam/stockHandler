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
using ProgTwoProject.Controllers;
using ProgTwoProject.Data;
using ProgTwoProject.Data_Access_Layer; 

namespace ProgTwoProject.View
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        //private OrdersRepository ordersRepo;

        private StockItemsRepository stockItemsRepository; 

        private OrderHeader _orderHeader;
        private OrderController _orderController = new OrderController(); 

        public AddOrder(OrderHeader orderHeader) // --> DONE
        {
            // On start up, this page loads the orderHeader info from prev page and uses it to populate the UI field. 
            _orderHeader = orderHeader; //Retrieves orderHeader object from previous page, positioned as a global value. 

            _orderHeader.setState(_orderHeader.StateID); 
            /*_orderHeader.setState();*/ //Converts OrderStateID to OrderState
            InitializeComponent();

            uiOrderID.Text = _orderHeader.OrderID.ToString();
            uiDateAndTime.Text = _orderHeader.DateTime.ToString();
            uiTotal.Text = _orderHeader.Total.ToString();
            uiOrderState.Text = _orderHeader.OrderState; 

        }

        //Add Btn Click --> Navigate to AddOrderItem Page
        private void AddButton_Click(object sender, RoutedEventArgs e) // --> DONE
        {
            try {
                NavigationService.Navigate(new AddOrderItem(_orderHeader)); //Navigates the info abt the current orderHeader to the next page 
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }
        }

        //Submit Btn Click --> DONE
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                _orderController.SubmitOrder(_orderHeader.OrderID); // Updates the orderHeader's orderStateId in the db)  

                NavigationService.Navigate(new Orders(_orderController)); // Navigates back to the Orders View 
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }
        }


        //Delete Btn Click --> TODO
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                //Navigate back to Orders View

                _orderController.DeleteOrderHeaderAndOrderItems(_orderHeader.OrderID); //delete orderHeader

                NavigationService.Navigate(new Orders(_orderController));
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }
        }
    }
}
