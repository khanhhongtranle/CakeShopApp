using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCake.Models
{
    class Order
    {
        public Order()
        {

        }

        public string OrderID { get; set; }
        public string Created_Date { get; set; }
        public List<OrderCake> List_Order { get; set; }
        public double Total { get; set; }
    }

    class OrderCake
    {
        public OrderCake()
        {

        }

        public string CakeName { get; set; }
        public int Number_Of_Cake { get; set; }
        public double Sold_Price { get; set; }
        public double Money { get; set; }
    }
}
