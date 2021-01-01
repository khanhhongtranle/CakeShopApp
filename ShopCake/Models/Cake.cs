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
            this.Other_Image_List = new List<string>();
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
        public List<String> Other_Image_List { set; get; }

        private DBHelper dBHelper = ApplicationContext.DBHelper;

        public void insertToDatabase()
        {
            //insert into cakes table 
            //and insert into images table, cake_img table

            dBHelper.query("insert into cakes(id, name, date_entered, kindofcake_id, unit_price, description) " +
                $"values('{this.Entered_Date}', '{this.Name}', '{this.Entered_Date}', {this.Kind}, '{this.Unit_Price}', '{this.Description}')");

            foreach(var img in this.Images_List)
            {
                var img_id = this.Name + img;
                dBHelper.query("insert into images(id, link)" +
                                $"values('{img_id}', '{img}')");
                dBHelper.query("insert into cake_img(cake_id, img_id)" +
                                $"values('{this.Entered_Date}', '{img_id}')");
            }
        }

        public void update()
        {
            //update cakes
            dBHelper.query($"update cakes set name = '{this.Name}', kindofcake_id = '{this.Kind}', description = '{this.Description}', unit_price = '{this.Unit_Price}' where id = '{this.Id}'");

            //delete cake_img, img where cake_id = this.Id
            dBHelper.query($"delete from cake_img where cake_id = '{this.Id}'");
            foreach(var img in this.Images_List)
            {
                //delete old images
                dBHelper.query($"delete from images where link = '{img}'");

                //insert new cake_img, img
                var img_id = this.Name + img;
                dBHelper.query("insert into images(id, link)" +
                                $"values('{img_id}', '{img}')");
                dBHelper.query("insert into cake_img(cake_id, img_id)" +
                                $"values('{this.Id}', '{img_id}')");
            }
        }
    }
}
