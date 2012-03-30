using System.Collections.Generic;
using MicropostMVC.Framework.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace MicropostMVC.BusinessObjects
{
    public class UserBo : MongoBo, IBoBase
    {
        public UserBo()
        {
            Microposts = new List<MicropostBo>();
        }

        public string Name { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public List<MicropostBo> Microposts { get; private set; }
    }
}