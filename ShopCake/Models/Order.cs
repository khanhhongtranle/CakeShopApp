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
        private double _total = 0;
        public double Total
        {
            get
            {
                _total = 0;
                foreach(var order in List_Order)
                {
                    _total += order.Amount;
                }
                return _total;
            }
            set { _total = value; }
        }
        public int getNumberItems()
        {
            int num = 0;
            foreach(var o in List_Order)
            {
                num += o.Quantity;
            }
            return num;
        }
        public void deleteOrderItem(string id)
        {
            foreach (var o in List_Order)
            {
                if (o.Id == id)
                {
                    List_Order.Remove(o);
                    break;
                }
            }
        }


        public void insert()
        {
            DBHelper dBHelper = ApplicationContext.DBHelper;
            DateTime today = DateTime.Now;
            OrderID = today.ToString();
            Created_Date = today.ToString();

            dBHelper.query($"insert into orders(id, date_entered, total) values ('{OrderID}', '{Created_Date}', '{Total}')");

            foreach(var oc in List_Order)
            {
                dBHelper.query($"insert into order_cake(order_id, cake_id, quantity, price, amount) values ('{OrderID}', '{oc.Id}', {oc.Quantity}, {oc.Price}, {oc.Amount})");
            }
        }
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
