using MicropostMVC.Framework.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace MicropostMVC.BusinessObjects
{
    public class UserBo : MongoBo
    {
        public string Name { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}