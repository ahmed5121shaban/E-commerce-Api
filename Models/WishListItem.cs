using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WishListItem
    {
        public int ID { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
