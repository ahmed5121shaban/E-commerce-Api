using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CartItemViewModel
    {
        [Required,MaxLength(100)]
        public int Quantity { get; set; }
        [Required]
        public double SupPrice { get; set; }
        [Required]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public string UserID { get; set; }
        public virtual User User { get; set; }
    }
}
