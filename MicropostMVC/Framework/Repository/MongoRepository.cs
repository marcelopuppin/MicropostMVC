using System;
using System.Configuration;
using MicropostMVC.Framework.Common;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MicropostMVC.Framework.Repository
{
    public class MongoRepository : IRepository
    {
        private readonly MongoServer _server;
        private readonly MongoDatabase _database;

        public MongoRepository()
        {
            string environment = ConfigurationManager.AppSettings["environment"];

            string connection = ConfigurationManager.ConnectionStrings[environment].ConnectionString;
            _server = MongoServer.Create(connection);

            string databaseName = ConfigurationManager.ConnectionStrings[environment].Name;
            _database = _server.GetDatabase(databaseName);
        }

        private MongoCollection GetCollection<T>()
        {
            return _database.GetCollection(typeof(T).Name);
        }

        public bool Save<T>(T item) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            SafeModeResult result = collection.Save(item, SafeMode.True);
            return (result != null && result.Ok);
        }

        public T FindById<T>(string id) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            return collection.FindOneByIdAs<T>(ObjectIdConverter.ConvertStringToObjectId(id));
        }
    
        public void Dispose()
        {
            _server.Disconnect();
        }
    }
}