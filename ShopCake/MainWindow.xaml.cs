using ShopCake.Views;
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

namespace ShopCake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           
            _frame.Children.Clear();
            _frame.Children.Add(new ProductsMenuView());
        }
       
        private void MenuCakesMenu_MouseUp(object sender, RoutedEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new ProductsMenuView());
        }

        private void MenuNew_MouseUp(object sender, RoutedEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new CreateNewView());
        }

        private void MenuReport_MouseUp(object sender, RoutedEventArgs e)
        {

        }

        private void Cart(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new OderView());
        }
    }
}
