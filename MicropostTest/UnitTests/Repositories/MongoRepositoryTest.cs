using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using MongoDB.Bson;
using NUnit.Framework;

namespace MicropostTest.UnitTests.Repositories
{
    [TestFixture]
    public class MongoRepositoryTest
    {
        private IRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new MongoRepository();
        }

        [Test]
        public void Given_UserModel_When_UserIsValid_Then_UserCanBeSaved()
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
    }
}
