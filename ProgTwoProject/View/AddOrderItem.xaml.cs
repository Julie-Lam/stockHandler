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
    /// Interaction logic for AddOrderItem.xaml
    /// </summary>
    public partial class AddOrderItem : Page
    {
        private StockItemsRepository _stockItemsRepo;
        private OrdersRepository _ordersRepo;

        OrderController _orderController = new OrderController();

        OrderHeader _orderHeader = null; 


        public AddOrderItem(OrderHeader orderHeader) //--> DONE
        {
            //On load, this page must load all the stock items 
            _stockItemsRepo = new StockItemsRepository();
            IEnumerable<StockItem> stockItems = new List<StockItem>(); 

            stockItems = _stockItemsRepo.GetStockItems(); // Retrieves all stockItems from db (Id, Name, Price, InStock)
            InitializeComponent();

            dgStockItems.ItemsSource = stockItems; // Populates the datagrid 

            //Instantiates the orderHeader info so the Add New button event can access the info 
            _orderHeader = orderHeader; // Retrieves orderHeader object from previous page, positioned as a global value.

        }

        //Click Add New on an order Item 
        private void Button_Click(object sender, RoutedEventArgs e) // --> DONE
        {
            try {
                int quantity = int.Parse(uiQuantity.Text); //Targets quantity in the textbox 

                StockItem selectedStockItem = (StockItem)dgStockItems.SelectedItem; //Targets the selected stock item

                OrderItem newOrderItem = new OrderItem(selectedStockItem.Name, _orderHeader.OrderID, selectedStockItem.Price, int.Parse(uiQuantity.Text), selectedStockItem.Id); // Instantiates a new order item from the selected stock item

                _orderController.UpsertOrderItem(newOrderItem.OrderHeaderId, newOrderItem.StockItemId, quantity); //Adds the new orderitem to the orderItems table in the db 

                _orderHeader.Total = _orderHeader.Total + (
                    selectedStockItem.Price * quantity);  // Update the orderHeader total 

                NavigationService.Navigate(new AddOrder(_orderHeader)); // Navigates back to the AddOrder View 
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error Encountered: {error}");
            }
        }




        


    }
}
