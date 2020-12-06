using System.Windows.Input;

namespace ShopCake.Models
{
    class Cake
    {
        public Cake()
        {

        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Entered_Date { get; set; }
        public string Kind { get; set; }
        public double Unit_Price { get; set; }
        public int In_Store { get; set; }

        //public ICommand ChangeNameCommand { get; }
    }
}
