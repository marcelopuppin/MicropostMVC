using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicropostMVC.Framework.Repository
{
    public abstract class MongoBo
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}