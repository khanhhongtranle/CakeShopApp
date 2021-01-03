using ShopCake.Models;
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
using System.Windows.Shapes;

namespace ShopCake
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    /// 

    public static class ApplicationContext
    {
        private static Order _order = new Order();
        public static Order Order
        {
            get { return _order; }
            set { _order = value; }
        }
        private static DBHelper _dBHelper = new DBHelper();
        public static DBHelper DBHelper
        {
            get { return _dBHelper; }
            set { _dBHelper = value; }
        }
        private static Random _rand = new Random();
        public static Random Rand
        {
            get { return _rand; }
            set { _rand = value; }
        }
    }
    public partial class SplashScreen : Window
    {
        private DBHelper dBHelper;
        private ObservableCollection<Cake> cakeList;

        public SplashScreen()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cakeList = new ObservableCollection<Cake>();
            this.dBHelper = ApplicationContext.DBHelper;
            this.dBHelper.createDatabase();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int check;
            var temp = this.dBHelper.query("select value from config where name = 'check'", true);
            check = int.Parse(temp[0]["value"]);
            if (check == 1)
            {
                this.Hide();
                var screen = new MainWindow();
                screen.ShowDialog();
                return;
            }
            var cakeListRaw = dBHelper.query("select * from cakes", true);
            foreach (var cakeRaw in cakeListRaw)
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
                cakeList.Add(_cake);
            }

            
            int i = ApplicationContext.Rand.Next(0, cakeList.Count);
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            Image img = new Image();
            _image.Source = new BitmapImage(new Uri(folder + cakeList[i].ThumbnailPath));
            _name.Text = cakeList[i].Name;
            _des.Text = cakeList[i].Description;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var screen = new MainWindow();
            screen.ShowDialog();
        }

        private void chbMain_Checked(object sender, RoutedEventArgs e)
        {
            this.dBHelper.query("update config set value = 1 where name = 'check'");
        }

        private void chbMain_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
