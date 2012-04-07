using System.Collections.Generic;

namespace MicropostMVC.Models
{
    public class MicropostsForUserModel
    {
        public UserModel User { get; set; }
        public IEnumerable<MicropostOwnerModel> Microposts { get; set; }
    }
}