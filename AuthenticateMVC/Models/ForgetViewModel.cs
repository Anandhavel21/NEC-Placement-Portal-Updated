using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticateMVC.Models
{
    public class ForgetViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Enter a valid E-Mail Id!")]
        public string Email { get; set; }
    }
}