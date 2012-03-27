using System;
using System.Collections.Generic;
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

        public MongoDatabase Database
        {
            get { return _database; }
        }

        public MongoRepository()
        {
            string environment = ConfigurationManager.AppSettings["environment"];

            string connection = ConfigurationManager.ConnectionStrings[environment].ConnectionString;
            _server = MongoServer.Create(connection);

            string databaseName = ConfigurationManager.ConnectionStrings[environment].Name;
            SafeMode safeMode = SafeMode.Create(1, TimeSpan.FromSeconds(30)); // 30 second timeout
            _database = _server.GetDatabase(databaseName, safeMode);
        }

        private MongoCollection GetCollection<T>()
        {
            return _database.GetCollection(typeof(T).Name);
        }

        public bool Save<T>(T item) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            try
            {
                SafeModeResult result = collection.Save(item);
                return (result != null && result.Ok);
            } 
            catch(MongoSafeModeException)
            {
                return false;
            }
        }

        public T FindById<T>(BoRef id) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            return collection.FindOneByIdAs<T>(ObjectIdConverter.ConvertBoRefToObjectId(id));
        }

        public T FindByKeyValue<T>(string key, object value) where T : IBoBase
        {
            BsonValue bsonValue = BsonValue.Create(value);
            MongoCollection collection = GetCollection<T>();
            IMongoQuery query = new QueryDocument(key, bsonValue);
            return collection.FindOneAs<T>(query);
        }

        public IEnumerable<T> FindAll<T>(int skip = 0, int take = int.MaxValue) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            MongoCursor<T> cursor = collection.FindAllAs<T>();
            cursor.SetSkip(skip);
            cursor.SetLimit(take);
            cursor.SetMaxScan(take);
            
            var list = new List<T>();
            list.AddRange(cursor);
            return list;
        }

        public void Dispose()
        {
            _server.Disconnect();
        }
    }
}