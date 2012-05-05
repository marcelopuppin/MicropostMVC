using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using MongoDB.Bson;
using NUnit.Framework;

namespace MicropostTest.UnitTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private IRepository _repository;

        [SetUp]
        public void Setup()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["test"];
            _repository = new MongoRepository(settings.Name, settings.ConnectionString);
            DBCleanUp();
        }

        [TearDown]
        public void TearDown()
        {
            DBCleanUp();
        }

        [Test]
        public void Given_UserBo_When_UserIsValid_Then_UserCanBeSaved()
        {
            UserBo user = CreateUserBo("Name1", "name1@email.com", "xxxxxxxxxx", "abcd");
            bool saved = _repository.Save(user);
            Assert.That(saved, Is.True);

            BoRef id = ObjectIdConverter.ConvertObjectIdToBoRef(user.Id);
            var userSaved = _repository.FindById<UserBo>(id);
            Assert.That(userSaved, Is.Not.Null);
            Assert.That(userSaved.Id, Is.EqualTo(user.Id));
        }

        [Test]
        public void Given_UserBo_When_EmailExistsInDB_Then_UserCannotBeSaved()
        {
            UserBo user1 = CreateUserBo("Name1", "name1@email.com", "xxxxxxxxxx", "abcd");
            bool saved = _repository.Save(user1);
            Assert.That(saved, Is.True);

            UserBo user2 = CreateUserBo("Name2", user1.Email, "yyyyyyyyy", "efgh");
            saved = _repository.Save(user2);
            Assert.That(saved, Is.False);
        }

        [Test]
        public void Given_UserBos_When_Skiping1AndTaking3_Then_UsersFoundAre3()
        {
            UserBo user1 = CreateUserBo("Name1", "name1@email.com", "xxxxxxxxxx", "abcd");
            _repository.Save(user1);
            UserBo user2 = CreateUserBo("Name2", "name2@email.com", "xxxxxxxxxx", "abcd");
            _repository.Save(user2);
            UserBo user3 = CreateUserBo("Name3", "name3@email.com", "xxxxxxxxxx", "abcd");
            _repository.Save(user3);
            UserBo user4 = CreateUserBo("Name1", "name4@email.com", "xxxxxxxxxx", "abcd");
            _repository.Save(user4);
            UserBo user5 = CreateUserBo("Name5", "name5@email.com", "xxxxxxxxxx", "abcd");
            _repository.Save(user5);

            var usersFound = new List<UserBo>();
            usersFound.AddRange(_repository.FindAll<UserBo>(1, 3));
            Assert.False(usersFound.Any(u => u.Email == user1.Email));
            Assert.True(usersFound.Any(u => u.Email == user2.Email));
            Assert.True(usersFound.Any(u => u.Email == user3.Email));
            Assert.True(usersFound.Any(u => u.Email == user4.Email));
            Assert.False(usersFound.Any(u => u.Email == user5.Email));
        }

        private UserBo CreateUserBo(string name, string email, string hash, string salt)
        {
            var user = new UserBo();
            user.Id = ObjectId.GenerateNewId();
            user.Name = name;
            user.Email = email;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            return user;
        }

        private void DBCleanUp()
        {
            ((MongoRepository)_repository).Database.GetCollection<UserBo>("UserBo").RemoveAll();
        
        }
    }
}
