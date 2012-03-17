using MongoDB.Bson;

namespace MicropostMVC.Framework.Common
{
    public static class ObjectIdConverter
    {
        public static ObjectId ConvertBoRefToObjectId(BoRef id)
        {
            if (id == null || id.IsEmpty())
            {
                return ObjectId.GenerateNewId();
            }
            return new ObjectId(id.Value.Replace("\"", ""));
        }

        public static BoRef ConvertObjectIdToBoRef(ObjectId id)
        {
            if (id == ObjectId.Empty)
            {
                return new BoRef();
            }
            return new BoRef(id.ToString());
        }
    }
}