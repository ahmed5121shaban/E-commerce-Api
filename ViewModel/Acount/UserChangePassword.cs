using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserChangePassword
    {
        [Required,DataType(DataType.Password), StringLength(30, MinimumLength = 8)]
        public string OldPassword { get; set; }
        [Required, DataType(DataType.Password), StringLength(30, MinimumLength = 8)]
        public string NewPassword { get; set; }
        public string UserID { get; set; }
    }
}
