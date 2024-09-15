using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalID { get; set; }
        public string Picture { get; set; }
        public virtual ICollection<WishListItem> WishList { get; set; }
        public virtual ICollection<CartItem> CartList { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual Seller Seller { get; set; }
        public string Role { get; set; }
    }
}
