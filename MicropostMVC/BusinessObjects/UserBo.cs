using MicropostMVC.Framework.Repository;

namespace MicropostMVC.BusinessObjects
{
    public class UserBo : MongoBo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}