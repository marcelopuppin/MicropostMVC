using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using MicropostMVC.Framework.Common;

namespace MicropostMVC.Models
{
    public class UserModel
    {
        [Required, HiddenInput(DisplayValue = false)]
        public BoRef Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, Email]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required, StringLength(50, MinimumLength = 6),
         EqualTo("Password"),
         Display(Name = "Confirmation")]
        public string PasswordConfirmation { get; set; }

    }
}