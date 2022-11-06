using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticateMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Enter your E-Mail ID!!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the Password!!"),StringLength(16,MinimumLength =6)]
        public string Password { get; set; }
    }
}