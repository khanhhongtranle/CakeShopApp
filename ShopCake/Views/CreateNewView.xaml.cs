using Microsoft.Win32;
using ShopCake.Helpers;
using ShopCake.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for CreateNewView.xaml
    /// </summary>
    public partial class CreateNewView : UserControl
    {
        private DBHelper dBHelper;
        private Cake cake;
        private ObservableCollection<String> imagesList;
        private List<AKindOfCake> kindsList;

        public CreateNewView()
        {
            InitializeComponent();
            dBHelper = new DBHelper();
            cake = new Cake();
            imagesList = new ObservableCollection<string>();
            kindsList = new List<AKindOfCake>();

            //read kind of cakes list from database
            var kindsRaw = dBHelper.query("select * from kindofcakes", true);
            foreach (var aKind in kindsRaw)
            {
                kindsList.Add(new AKindOfCake(aKind["id"], aKind["name"]));
            }
            //setup binding kind combobox element
            comboBoxitemKind.ItemsSource = kindsList;
            comboBoxitemKind.DisplayMemberPath = "Name";
            comboBoxitemKind.SelectedValuePath = "Id";
        }

        private void ChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                foreach (string item in open.FileNames)
                {
                    imagesList.Add(item);
                }
                Images.ItemsSource = imagesList;
            }
        }

        private void imgSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //check fields
            AKindOfCake typeItem = (AKindOfCake)comboBoxitemKind.SelectedItem;
            /*if (cake.Name == "" || cake.Description == "" || typeItem == null)
            {
                MessageBoxResult resultFailed = MessageBox.Show("Fields must be filled out", "Notification");
                return;
            }*/
            //fields is filled out
            MessageBoxResult resultComfirm = MessageBox.Show("Do you really want to create new cake?", "Notification", MessageBoxButton.OKCancel);
            if (resultComfirm == MessageBoxResult.OK) {
                DateTime localDate = DateTime.Now; //get current data time 
                cake.Entered_Date = localDate.ToString(); //set entered date for new cake
                cake.Kind = int.Parse(typeItem.Id);
                cake.Name = textBoxName.Text;
                cake.Description = textBoxDescription.Text;
                double unit_price_test;
                if (StringHelper.isANumber(textBoxPrice.Text, out unit_price_test))
                {
                    cake.Unit_Price = unit_price_test;
                }
                else
                {
                    MessageBoxResult errorMsgBox = MessageBox.Show("Unit price field must be a number", "Notification");
                    return;
                }
                List<String> imgListToSave = new List<string>(); 

                //copy uploading images
                //create a new folder images if not exists
                string pathImagesFolder = "Images\\"; //System.IO.Path.Combine(folder, "Images");
                if (!Directory.Exists(pathImagesFolder))
                {
                    //if not exists, create new folder
                    Directory.CreateDirectory(pathImagesFolder);
                }
                foreach(var image in imagesList)
                {
                    string reversedImage = StringHelper.Reverse(image);
                    string reversedFileName = StringHelper.CutStringTo(reversedImage, '\\');
                    string fileName = StringHelper.Reverse(reversedFileName);
                    string destFile = System.IO.Path.Combine(pathImagesFolder, fileName);
                    File.Copy(image, destFile, true);
                    imgListToSave.Add(destFile);
                }
                cake.Images_List = imgListToSave;
                //insert to database
                cake.insertToDatabase(dBHelper);

                MessageBoxResult resultSuccess = MessageBox.Show("Created successfully", "Notification");

                _addCakes.Children.Clear();
                _addCakes.Children.Add(new ProductsMenuView());
            }
        }

        private void imgCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //clear all
            textBoxName.Text = "";
            textBoxDescription.Text = "";
            textBoxPrice.Text = "";
            imagesList.Clear();
        }
    }
}
