using ShopCake.Helpers;
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
    /// Interaction logic for ProductsMenuView.xaml
    /// </summary>
    public partial class ProductsMenuView : UserControl
    {
        private DBHelper dBHelper;
        private ObservableCollection<Cake> cakesList;
        private ObservableCollection<AKindOfCake> kindsList;
        private PagingHelper pagingHelper;

        public ProductsMenuView()
        {
            InitializeComponent();
            dBHelper = new DBHelper();
            cakesList = new ObservableCollection<Cake>();
            kindsList = new ObservableCollection<AKindOfCake>();
            pagingHelper = new PagingHelper();

            //read kind of cake from database
            var kindsRaw = dBHelper.query("select * from kindofcakes", true);
            kindsList.Add(new AKindOfCake("0", "All of kinds"));
            foreach (var aKind in kindsRaw)
            {
                kindsList.Add(new AKindOfCake(aKind["id"], aKind["name"]));
            }
            //setup binding kind combobox element
            comboBoxitemKind.ItemsSource = kindsList;
            comboBoxitemKind.DisplayMemberPath = "Name";
            comboBoxitemKind.SelectedValuePath = "Id";
            comboBoxitemKind.SelectedValue = "0";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //read all of cakes from database
            var cakesListRaw = dBHelper.query("select * from cakes", true);
            foreach (var cakeRaw in cakesListRaw)
            {
                Cake _cake = new Cake();
                _cake.Id = cakeRaw["id"];
                _cake.Name = cakeRaw["name"];
                _cake.Description = cakeRaw["description"];
                _cake.Unit_Price = Double.Parse(cakeRaw["unit_price"]);
                _cake.Entered_Date = cakeRaw["date_entered"];
                _cake.Kind = int.Parse(cakeRaw["kindofcake_id"]);

                var cakeImagesListRaw = dBHelper.query($"select i.link as link from cake_img c_i join images i on c_i.img_id = i.id where c_i.cake_id = '{_cake.Id}'", true);
                List<String> _imagesLinkList = new List<string>();
                foreach (var cakeImage in cakeImagesListRaw)
                {
                    _imagesLinkList.Add(cakeImage["link"]);
                }
                _cake.Images_List = _imagesLinkList;
                cakesList.Add(_cake);
            }
            this._pagination.Visibility = Visibility.Hidden;
            if (cakesList.Count > 12)
            {
                this._pagination.Visibility = Visibility.Visible;
            }

            pagingHelper.CurrentPage = 1;
            pagingHelper.ItemsPerPage = 12;
            pagingHelper.Count = cakesList.Count;
            pagingHelper.TotalPages = (pagingHelper.Count / pagingHelper.ItemsPerPage) + (pagingHelper.Count % pagingHelper.ItemsPerPage == 0 ? 0 : 1);

            //setup binding data cake list
            Thread thread = new Thread(delegate ()
            {
                // Update UI
                Dispatcher.Invoke(() =>
                {
                    dataListview.ItemsSource = cakesList.Take(pagingHelper.ItemsPerPage);
                });
            });

            thread.Start();
        }

        private void Next_Click(object sender, MouseButtonEventArgs e)
        {
            if (pagingHelper.CurrentPage < pagingHelper.TotalPages)
            {
                pagingHelper.CurrentPage++;
                dataListview.ItemsSource =
                cakesList
                    .Skip((pagingHelper.CurrentPage - 1) * pagingHelper.ItemsPerPage)
                    .Take(pagingHelper.ItemsPerPage);
            }
        }

        private void Prev_Click(object sender, MouseButtonEventArgs e)
        {
            if (pagingHelper.CurrentPage <= pagingHelper.TotalPages)
            {
                pagingHelper.CurrentPage--;
                dataListview.ItemsSource =
                cakesList
                    .Skip((pagingHelper.CurrentPage - 1) * pagingHelper.ItemsPerPage)
                    .Take(pagingHelper.ItemsPerPage);
                if (pagingHelper.CurrentPage <= 1)
                {
                    pagingHelper.CurrentPage = 1;
                }
            }
        }

        private void dataListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as Cake;
            int index = dataListview.Items.IndexOf(item) + ((pagingHelper.CurrentPage - 1) * pagingHelper.ItemsPerPage);
            if (item != null)
            {
                _frame.Children.Clear();
                _frame.Children.Add(new CakeDetailView(item));
            }
        }
    }
}
