using MongoDB.Bson;

namespace MicropostMVC.Framework.Common
{
    public static class ObjectIdConverter
    {
        public static ObjectId ConvertStringToObjectId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ObjectId.GenerateNewId();
            }
            return new ObjectId(id.Replace("\"", ""));
        }

        public static string ConvertObjectIdToString(ObjectId id)
        {
            if (id == ObjectId.Empty)
            {
                return string.Empty;
            }
            return id.ToString();
        }
    }
}