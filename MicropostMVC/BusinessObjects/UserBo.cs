using System.Collections.Generic;
using MicropostMVC.Framework.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicropostMVC.BusinessObjects
{
    public class UserBo : MongoBo, IBoBase
    {
        public UserBo()
        {
            Microposts = new List<MicropostBo>();
            Following = new List<ObjectId>();
            Followers = new List<ObjectId>();
        }

        public string Name { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public string Avatar { get; set; }

        public List<MicropostBo> Microposts { get; private set; }
        
        public List<ObjectId> Following { get; private set; }
        public List<ObjectId> Followers { get; private set; }
    }
}