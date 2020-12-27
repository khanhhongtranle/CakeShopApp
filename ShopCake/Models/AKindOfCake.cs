using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCake.Models
{
    class AKindOfCake
    {
        public AKindOfCake()
        {

        }

        public AKindOfCake(String _id, String _name)
        {
            this.Id = _id;
            this.Name = _name;
        }

        public String Id { get; set; }
        public String Name { get; set; }
    }
}
