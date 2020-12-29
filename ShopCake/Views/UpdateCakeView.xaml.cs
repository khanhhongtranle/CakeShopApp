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
    /// Interaction logic for UpdateCakeView.xaml
    /// </summary>
    public partial class UpdateCakeView : UserControl
    {
        private Cake currentCake;
        private Cake newCake;
        private DBHelper dBHelper;
        private ObservableCollection<String> imagesList;
        private List<AKindOfCake> kindsList;

        public UpdateCakeView(Cake cake)
        {
            InitializeComponent();
            this.currentCake = cake;
            this.newCake = cake;
            dBHelper = new DBHelper();
            imagesList = new ObservableCollection<string>();
            kindsList = new List<AKindOfCake>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //binding data context
            this.DataContext = this.newCake;

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
            comboBoxitemKind.SelectedValue = this.newCake.Kind;

            //read list of images link from database
            var imagesPathListRaw = dBHelper.query($"select i.link as path from cake_img c_i join images i on c_i.img_id = i.id where c_i.cake_id = '{this.newCake.Id}'", true);
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var imagePath in imagesPathListRaw)
            {
                //add folder domain before path
                imagesList.Add(folder + imagePath["path"]);
            }
            Images.ItemsSource = imagesList;
        }

        private void Remove_Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tb = (Border)e.OriginalSource;
            var path = tb.DataContext;
            if (imagesList.Contains(path.ToString()))
            {
                imagesList.Remove(path.ToString());
            }
        }

        private void Upload_Image_MouseDown(object sender, MouseButtonEventArgs e)
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
            if (textBoxName.Text == "" || textBoxDescription.Text == "" || textBoxPrice.Text == "")
            {
                MessageBoxResult resultFailed = MessageBox.Show("Fields must be filled out", "Notification");
                return;
            }
            //fields is filled out
            MessageBoxResult resultComfirm = MessageBox.Show("Do you really want to update this cake?", "Notification", MessageBoxButton.OKCancel);
            if (resultComfirm == MessageBoxResult.OK)
            {
                DateTime localDate = DateTime.Now; //get current data time 
                newCake.Entered_Date = localDate.ToString(); //set entered date for new cake
                double unit_price_test;
                if (StringHelper.isANumber(textBoxPrice.Text, out unit_price_test))
                {
                    newCake.Unit_Price = unit_price_test;
                }
                else
                {
                    MessageBoxResult errorMsgBox = MessageBox.Show("Unit price field must be a number", "Notification");
                    return;
                }
                List<String> imgListToSave = new List<string>();

                var folderRoot = AppDomain.CurrentDomain.BaseDirectory;
                //copy uploading images
                //create a new folder images if not exists
                string pathImagesFolder = "Images\\"; //System.IO.Path.Combine(folder, "Images");
                
                if (!Directory.Exists(pathImagesFolder))
                {
                    //if not exists, create new folder
                    Directory.CreateDirectory(pathImagesFolder);
                }
                foreach (var image in imagesList)
                {
                    string reversedImage = StringHelper.Reverse(image);
                    string reversedFileName = StringHelper.CutStringTo(reversedImage, '\\');
                    string fileName = StringHelper.Reverse(reversedFileName);
                    string destFile = System.IO.Path.Combine(pathImagesFolder, fileName);
                    //check is this image is localed images folder
                    if (!image.Contains(folderRoot))
                    {
                        destFile = FileHelper.Copy(image, destFile); //return new destination file name
                    }
                    
                    imgListToSave.Add(destFile);
                }
                newCake.Images_List = imgListToSave;
                //update cake
                newCake.update(dBHelper);

                MessageBoxResult resultSuccess = MessageBox.Show("Updated successfully", "Notification");
            }
        }

        private void imgCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.newCake = this.currentCake;
        }
    }
}
