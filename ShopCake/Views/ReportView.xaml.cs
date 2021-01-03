using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        
        public ReportView()
        {
            InitializeComponent();

            //cot 2
            /*List<DataForReport> listDataForReport2 = new List<DataForReport>();
            listDataForReport2.Add(new DataForReport() { x = "thang 1", y = 25 });
            listDataForReport2.Add(new DataForReport() { x = "thang 2", y = 14 });
            listDataForReport2.Add(new DataForReport() { x = "thang 3", y = 39 });
            listDataForReport2.Add(new DataForReport() { x = "thang 4", y = 65 });

            ColumnSeries series2 = new ColumnSeries();
            series2.DependentValuePath = "y";
            series2.IndependentValuePath = "x";
            series2.ItemsSource = listDataForReport2;
            series2.Title = "loai banh b";
            myChartReport.Series.Add(series2);*/
        }
        public class DataForReport
        {
            public string x { get; set; }
            public int y { get; set; }
        }

        public class PiePoint
        {
            public string Name { get; set; }
            public int Total { get; set; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int[] totalsByMonth = new int[12] { 0,0,0,0,0,0,0,0,0,0,0,0};
            DBHelper dBHelper = ApplicationContext.DBHelper;
            var dataRaw = dBHelper.query("select date_entered, total from orders", true);
            foreach(var record in dataRaw)
            {
                string gotMonth = $"{record["date_entered"][0]}{record["date_entered"][1]}";
                totalsByMonth[int.Parse(gotMonth) - 1] += int.Parse(record["total"]);
            }

            //cot 1
            List<DataForReport> listDataForReport1 = new List<DataForReport>();
            listDataForReport1.Add(new DataForReport() { x = "January", y = totalsByMonth[0] });
            listDataForReport1.Add(new DataForReport() { x = "February", y = totalsByMonth[1] });
            listDataForReport1.Add(new DataForReport() { x = "March", y = totalsByMonth[2] });
            listDataForReport1.Add(new DataForReport() { x = "April", y = totalsByMonth[3] });
            listDataForReport1.Add(new DataForReport() { x = "May", y = totalsByMonth[4] });
            listDataForReport1.Add(new DataForReport() { x = "June", y = totalsByMonth[5] });
            listDataForReport1.Add(new DataForReport() { x = "July", y = totalsByMonth[6] });
            listDataForReport1.Add(new DataForReport() { x = "August", y = totalsByMonth[7] });
            listDataForReport1.Add(new DataForReport() { x = "September", y = totalsByMonth[8] });
            listDataForReport1.Add(new DataForReport() { x = "October", y = totalsByMonth[9] });
            listDataForReport1.Add(new DataForReport() { x = "November", y = totalsByMonth[10] });
            listDataForReport1.Add(new DataForReport() { x = "December", y = totalsByMonth[11] });

            ColumnSeries series1 = new ColumnSeries();
            series1.DependentValuePath = "y";
            series1.IndependentValuePath = "x";
            series1.ItemsSource = listDataForReport1;
            series1.Title = "Money (VND)";

            myChartReport.Series.Add(series1);

            //pie chart
            ObservableCollection<PiePoint> PieCollection = new ObservableCollection<PiePoint>();

            var data2Raw = dBHelper.query("select kindofcakes.name, order_cake.amount from order_cake join cakes on order_cake.cake_id = cakes.id JOIN kindofcakes on cakes.kindofcake_id = kindofcakes.id;", true);
            foreach(var record in data2Raw)
            {
                PieCollection.Add(new PiePoint { Name = record["name"], Total = int.Parse(record["amount"]) });
            }
            pieSeries.ItemsSource = PieCollection;

        }
}
}
