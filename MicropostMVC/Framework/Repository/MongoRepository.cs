using System;
using System.Linq;
using System.Collections.Generic;
using MicropostMVC.Framework.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        public MongoRepository(string databaseName, string connectionString)
        {
            _server = MongoServer.Create(connectionString);

            SafeMode safeMode = SafeMode.Create(1, TimeSpan.FromSeconds(30)); // timeout (sec)
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
                //Safemode detected an error 'E11000 duplicate key error index'
                return false;
            }
        }

        public bool Remove<T>(BoRef id) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            IMongoQuery query = new QueryDocument("Id", ObjectIdConverter.ConvertBoRefToObjectId(id));
            SafeModeResult result = collection.Remove(query);
            return (result != null && result.Ok);
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

        public IEnumerable<T> FindUsingFilter<T>(Func<T, bool> filter, int skip = 0, int take = int.MaxValue) where T : IBoBase
        {
            MongoCollection collection = GetCollection<T>();
            
            IEnumerable<T> query = collection.AsQueryable<T>()
                                             .Where(filter)
                                             .Skip(skip)
                                             .Take(take);
                                             
            var list = new List<T>();
            list.AddRange(query);
            return list;
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