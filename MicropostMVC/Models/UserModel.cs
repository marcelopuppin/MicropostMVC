using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using MicropostMVC.Framework.Common;

namespace MicropostMVC.Models
{
    public class UserModel : ModelWithId
    {
        public UserModel()
        {
            Microposts = new List<MicropostModel>();   
            Following = new List<BoRef>();
            Followers = new List<BoRef>();
        }

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

        public List<MicropostModel> Microposts { get; private set; }

        public List<BoRef> Following { get; private set; }
        public List<BoRef> Followers { get; private set; }
    }
}