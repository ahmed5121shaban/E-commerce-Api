using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserResetPasswordViewModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,DataType(DataType.Password),StringLength(maximumLength:30,MinimumLength =8)]
        public string newPassword { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
