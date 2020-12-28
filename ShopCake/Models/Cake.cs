using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ShopCake.Models
{
    public class Cake
    {
        public Cake()
        {
            this.Name = "";
            this.Description = "";
            this.Unit_Price = 0;
            this.Entered_Date = "";
            this.Images_List = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Entered_Date { get; set; }
        public int Kind { get; set; }
        public double Unit_Price { get; set; }
        public List<String> Images_List { get; set; }
        private string _thumbnailPath;
        public string ThumbnailPath
        {
            get
            {
                return Images_List[0];
            }
            set
            {
                _thumbnailPath = value;
            }
        }

        public void insertToDatabase(DBHelper dBHelper)
        {
            //insert into cakes table 
            //and insert into images table, cake_img table

            dBHelper.query("insert into cakes(id, name, date_entered, kindofcake_id, unit_price) " +
                $"values('{this.Entered_Date}', '{this.Name}', '{this.Entered_Date}', {this.Kind}, '{this.Unit_Price}')");

            foreach(var img in this.Images_List)
            {
                var img_id = this.Name + img;
                dBHelper.query("insert into images(id, link)" +
                                $"values('{img_id}', '{img}')");
                dBHelper.query("insert into cake_img(cake_id, img_id)" +
                                $"values('{this.Entered_Date}', '{img_id}')");
            }
        }
    }
}
