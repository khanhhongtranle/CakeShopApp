using ShopCake.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ShopCake.Views
{
    /// <summary>
    /// Interaction logic for OderView.xaml
    /// </summary>
    public partial class OderView : UserControl
    {
        private Order order;
        private ObservableCollection<OrderCake> OrderCakeList;

        public OderView()
        {
            InitializeComponent();
            order = ApplicationContext.Order;
            OrderCakeList = new ObservableCollection<OrderCake>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var o_c in order.List_Order)
            {
                OrderCakeList.Add(o_c);
            }
            dataListview.ItemsSource = OrderCakeList;
            _quanlity.Content = order.getNumberItems();
            _total.Content = order.Total;
            DateTime localDate = DateTime.Now;
            _date.Content = localDate;
        }

        private void Order_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult resultComfirm = MessageBox.Show("Are you sure to complete this order?", "Notification", MessageBoxButton.OKCancel);
            if (resultComfirm == MessageBoxResult.OK)
            {
                //save this order
                DBHelper dBHelper = ApplicationContext.DBHelper;
                order.insert();
                MessageBoxResult successComfirm = MessageBox.Show("This order has completed yet.", "Notification");
                OrderCakeList.Clear();
                _quanlity.Content = "";
                _total.Content = "";
            }
            else
            {
                //to do nothing
            }
        }


        private void buttonDeleteProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var img = (Image)e.OriginalSource;
            var cake = (OrderCake) img.DataContext;
            order.deleteOrderItem(cake.Id);
            OrderCakeList.Remove(cake);
            dataListview.ItemsSource = OrderCakeList;
            _quanlity.Content = order.getNumberItems();
            _total.Content = order.Total;
        }

    }
}
