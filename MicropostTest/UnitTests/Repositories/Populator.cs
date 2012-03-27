// It's not a test !!!
// Used only to populate a database 
// Change 'environment' in App.Config to populate a development database 
  
  
/* 
using System;
using System.Globalization;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using MongoDB.Bson;
using NUnit.Framework;

namespace MicropostTest.UnitTests.Repositories
{
    
    [TestFixture]
    public class PopulatorTest
    {
        [Test, Ignore]
        public void Populate()
        {
            var populator = new Polulator();
            for (int i = 1; i < 1000; i++)
            {
                populator.CreateRandomUser(i);
            }
        }
                
        #region Nested Class
        public class Polulator
        {
            private readonly IRepository _repository;
            private readonly Random _random;

            private readonly string[] _names = {
                                                   "Jacob", "Isabella", "Ethan", "Sophia", "Michael",
                                                   "Emma", "Jayden", "Olivia", "William", "Eva",
                                                   "Alexander", "Emily", "Noah", "Abigail", "Daniel",
                                                   "Madison", "Aiden", "Chloe", "Anthony", "Mia"
                                               };

            public Polulator()
            {
                _repository = new MongoRepository();
                _random = new Random();
            }

            public void CreateRandomUser(int id)
            {
                int rnd = _random.Next(20);
                string name = _names[rnd] + id.ToString(CultureInfo.InvariantCulture);
                string email = name + "@email.com";
                string salt = Encryptor.CreateSalt(4);
                string hash = Encryptor.CreateHashedPassword(name, salt);

                var user = new UserBo();
                user.Id = ObjectId.GenerateNewId();
                user.Name = name;
                user.Email = email;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                _repository.Save(user);
            }
        }
        #endregion
    }
}
*/