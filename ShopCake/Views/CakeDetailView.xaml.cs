using ShopCake.Models;
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

namespace ShopCake.Views
{
    /// <summary>
    /// Interaction logic for CakeDetailView.xaml
    /// </summary>
    public partial class CakeDetailView : UserControl
    {
        private DBHelper dBHelper;
        private Cake cake;
        private string kind;
        private int quantity = 1;

        public CakeDetailView(Cake item)
        {
            InitializeComponent();
            this.cake = item;
            this.dBHelper = new DBHelper();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.cake;
            var kindRaw = dBHelper.query($"select * from kindofcakes where id = '{cake.Kind}'", true);
            this.kind = kindRaw[0]["name"];
            KindLabel.Content = this.kind;
        }

        private void Minus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.quantity == 1)
            {
                return;
            }
            this.quantity--;
            _quantity.Text = this.quantity.ToString();
        }

        private void Plus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.quantity++;
            _quantity.Text = this.quantity.ToString();
        }

        private void _editProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new CakeDetailView(this.cake));
        }
    }
}
