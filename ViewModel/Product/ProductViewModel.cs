using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductViewModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessage = "Product Name Is Requird")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You Must Write The Discription")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You Must Enter The Price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "This Faild Is Required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This Faild Is Required")]
        public int CategoryID { get; set; }
        [Required,StringLength(maximumLength:50,MinimumLength =3)]
        public string CategoryName { get; set; }

        public string CategoryImage { get; set; }
        public bool DeleteOrNot { get; set; } = false;
        public IFormFileCollection Attachments { get; set; }
        public List<string> ProductsImageList { get; set; } = new List<string>();

    }
}
