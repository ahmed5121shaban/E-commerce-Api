using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CategoryViewModel
    {
        public int? ID { get; set; }
        [Required, StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
