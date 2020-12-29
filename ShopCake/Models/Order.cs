using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCake.Models
{
    public class Order
    {
        public Order()
        {
            List_Order = new List<OrderCake>();
        }

        public string OrderID { get; set; }
        public string Created_Date { get; set; }
        public List<OrderCake> List_Order { get; set; }
        public double Total { get; set; }
    }

    public class OrderCake
    {
        public OrderCake()
        {

        }

        public OrderCake(string _id, string _name, int _quantity, double _price, double _amount)
        {
            Id = _id;
            CakeName = _name;
            Quantity = _quantity;
            Price = _price;
            Amount = _amount;
        }

        public string Id { get; set; }
        public string CakeName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
    }
}
