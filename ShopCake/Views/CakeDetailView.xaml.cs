using ShopCake.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for CakeDetailView.xaml
    /// </summary>
    public partial class CakeDetailView : UserControl
    {
        private DBHelper dBHelper;
        private Cake cake;
        private string kind;
        private int quantity = 1;
        private ObservableCollection<Cake> _listForShowImages;

        public CakeDetailView(Cake item)
        {
            InitializeComponent();
            this.cake = item;
            this.dBHelper = new DBHelper();
            _listForShowImages = new ObservableCollection<Cake>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.cake;
            var kindRaw = dBHelper.query($"select * from kindofcakes where id = '{cake.Kind}'", true);
            this.kind = kindRaw[0]["name"];
            KindLabel.Content = this.kind;

            Cake temp_cake = new Cake();

            for (int i = 1; i < this.cake.Images_List.Count; i++)
            {
                temp_cake.Other_Image_List.Add(AppDomain.CurrentDomain.BaseDirectory + this.cake.Images_List[i]);
            }
            _listForShowImages.Add(temp_cake);
            PreviewPhoto.ItemsSource = _listForShowImages;
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
            _frame.Children.Add(new UpdateCakeView(this.cake));
        }

        private void Add_To_Cart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ApplicationContext.Order.List_Order.Add(new OrderCake(cake.Id, cake.Name, quantity, cake.Unit_Price, quantity * cake.Unit_Price));
           
            MessageBoxResult result = MessageBox.Show("Added to cart", "Notification");
        }
    }
}
