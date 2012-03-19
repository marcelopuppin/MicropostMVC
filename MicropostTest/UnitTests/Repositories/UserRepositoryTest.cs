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
            _repository = new MongoRepository();
            ((MongoRepository) _repository).Database.GetCollection<UserBo>("UserBo").RemoveAll();
        }

        [Test]
        public void Given_UserBo_When_UserIsValid_Then_UserCanBeSaved()
        {
            var user = new UserBo();
            user.Id = ObjectId.GenerateNewId();
            user.Name = "Name";
            user.Email = "name@email.com";
            user.PasswordHash = "xxxxxxxxxx";
            user.PasswordSalt = "abcd";
            
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
            var user1 = new UserBo();
            user1.Id = ObjectId.GenerateNewId();
            user1.Name = "Name1";
            user1.Email = "name1@email.com";
            user1.PasswordHash = "xxxxxxxxxx";
            user1.PasswordSalt = "abcd";

            bool saved = _repository.Save(user1);
            Assert.That(saved, Is.True);

            var user2 = new UserBo();
            user2.Id = ObjectId.GenerateNewId();
            user2.Name = "Name2";
            user2.Email = user1.Email;
            user2.PasswordHash = "yyyyyyyyy";
            user2.PasswordSalt = "efgh";

            saved = _repository.Save(user2);
            Assert.That(saved, Is.False);
        }
    }
}
