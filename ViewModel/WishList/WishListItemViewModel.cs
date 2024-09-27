using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class WishListItemViewModel
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}
