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
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
