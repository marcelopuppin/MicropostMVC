using AutoMapper;
using MicropostMVC.App_Start;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Models;
using MongoDB.Bson;
using NUnit.Framework;

namespace MicropostTest.UnitTests.Mapping
{
    [TestFixture]
    public class UserMappingTest
    {
        [SetUp]
        public void SetUp()
        {
            StructuremapMvc.Start();
            AutoMapperConfiguration.Start();    
        }

        [Test]
        public void ViewModelToBusinessObject_Name()
        {
            var user = new UserModel();
            user.Name = "abcde";
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            Assert.That(userBo.Name, Is.EqualTo(user.Name));
        }

        [Test]
        public void BusinessObjectToViewModel_Name()
        {
            var userBo = new UserBo();
            userBo.Name = "abcde";
            UserModel userModel = Mapper.Map<UserBo, UserModel>(userBo);
            Assert.That(userModel.Name, Is.EqualTo(userBo.Name));
        }

        [Test]
        public void ViewModelToBusinessObject_Email()
        {
            var user = new UserModel();
            user.Email = "email@test.com";
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            Assert.That(userBo.Email, Is.EqualTo(user.Email));
        }

        [Test]
        public void BusinessObjectToViewModel_Email()
        {
            var userBo = new UserBo();
            userBo.Email = "email@test.com";
            UserModel userModel = Mapper.Map<UserBo, UserModel>(userBo);
            Assert.That(userModel.Email, Is.EqualTo(userBo.Email));
        }

        [Test]
        public void ViewModelToBusinessObject_Empty_Id()
        {
            var user = new UserModel();
            user.Id = string.Empty;
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            Assert.That(userBo.Id, Is.Not.EqualTo(ObjectId.Empty));
        }

        [Test]
        public void BusinessObjectToViewModel_Empty_Id()
        {
            var userBo = new UserBo();
            userBo.Id = new ObjectId();
            UserModel userModel = Mapper.Map<UserBo, UserModel>(userBo);
            Assert.That(userModel.Id, Is.Empty);
        }

        [Test]
        public void ViewModelToBusinessObject_Id()
        {
            var user = new UserModel();
            user.Id = "4f5ea430ee62da0cc0df2483";
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            Assert.That(userBo.Id.ToString(), Is.EqualTo(user.Id));
        }

        [Test]
        public void BusinessObjectToViewModel_Id()
        {
            var userBo = new UserBo();
            userBo.Id = ObjectId.GenerateNewId();
            UserModel userModel = Mapper.Map<UserBo, UserModel>(userBo);
            Assert.That(userModel.Id, Is.EqualTo(userBo.Id.ToString()));
        }

        [Test]
        public void ViewModelToBusinessObject_Password()
        {
            var user = new UserModel();
            user.Password = "abcdef";
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            Assert.That(userBo.PasswordHash, Is.Not.Null);
            Assert.That(user.Password, Is.Not.EqualTo(userBo.PasswordHash));
        }

        [Test]
        public void BusinessObjectToViewModel_Password()
        {
            var userBo = new UserBo();
            userBo.PasswordHash = "abcdef";
            userBo.PasswordSalt = "1234";
            UserModel userModel = Mapper.Map<UserBo, UserModel>(userBo);
            Assert.That(userModel.Password, Is.Null);
        }
    }
}
