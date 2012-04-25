using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MicropostMVC.App_Start;
using MicropostMVC.BusinessObjects;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using MicropostMVC.Models;
using MongoDB.Bson;
using Rhino.Mocks;
using NUnit.Framework;

namespace MicropostTest.UnitTests.BusinessServices
{
    [TestFixture]
    public class MicropostBSTests
    {
        private MockRepository _mocks;
        private IRepository _repository;
        
        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();
            _repository = _mocks.DynamicMock<IRepository>();
            AutoMapperConfiguration.Start(); 
        }

        [TestCase(false)]
        [TestCase(true)]
        public void When_SavingMicropost_Then_ResultIs(bool saved)
        {
            UserModel user = GetUserForTest("test", "testpwd", "test@email.com");
            Expect.Call(_repository.Save(new UserBo())).IgnoreArguments().Return(saved);
            _mocks.ReplayAll();

            IMicropostBS micropostBS = new MicropostBS(_repository);
            bool result = micropostBS.Save(user, "Hello World");
            Assert.That(result, Is.EqualTo(saved));
        }

        [Test]
        public void When_SavingNewMicropost_Then_MicropostModelContainsNewContent()
        {
            UserModel user = GetUserForTest("test", "testpwd", "test@email.com");
            Expect.Call(_repository.Save(new UserBo())).IgnoreArguments().Return(true);
            _mocks.ReplayAll();

            IMicropostBS micropostBS = new MicropostBS(_repository);
            MicropostModel micropost = micropostBS.SaveNew(user, "Hello World");
            Assert.That(micropost.Id.IsEmpty(), Is.False);
            Assert.That(micropost.Content, Is.EqualTo("Hello World"));
        }

        [Test]
        public void MicropostsFollowedByUser()
        {
            var micropostsA = new List<MicropostBo>();
            micropostsA.Add(new MicropostBo { Content = "AaaaaaA"});
            micropostsA.Add(new MicropostBo { Content = "aAAAAAa"});
            var userA = new UserBo {Email = "userA@test.com", Id = new ObjectId(1,1,1,1), Name = "userA"};
            userA.Microposts.AddRange(micropostsA);
            
            var micropostsB = new List<MicropostBo>();
            micropostsB.Add(new MicropostBo { Content = "BbbbbbB"});
            var userB = new UserBo { Email = "userB@test.com", Id = new ObjectId(2, 2, 2, 2), Name = "userB" };
            userB.Microposts.AddRange(micropostsB);
            userB.Following.Add(userA.Id);
            
            Expect.Call(_repository.FindById<UserBo>(new BoRef())).IgnoreArguments().Return(userA).Repeat.Any();
            _mocks.ReplayAll();

            IMicropostBS micropostBS = new MicropostBS(_repository);
            MicropostsForUserModel result = micropostBS.GetMicropostsForUser(Mapper.Map<UserBo, UserModel>(userB));
            Assert.That(result.Microposts.Count(), Is.EqualTo(3));
        }

        private UserModel GetUserForTest(string name, string password, string email)
        {
            var user = new UserModel();
            user.Id = new BoRef();
            user.Name = name;
            user.Password = password;
            user.Email = email;
            return user;
        }
    }
}