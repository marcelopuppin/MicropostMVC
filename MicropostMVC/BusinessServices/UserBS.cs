using System.Collections.Generic;
using System.Web.Security;
using AutoMapper;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using System.Linq;
using MicropostMVC.Models;
using MongoDB.Bson;

namespace MicropostMVC.BusinessServices
{
    public class UserBS : IUserBS {
        private readonly IRepository _repository;
        private readonly IEncryptorBS _encryptorBS;

        public UserBS(IRepository repository, 
                      IEncryptorBS encryptorBS)
        {
            _repository = repository;
            _encryptorBS = encryptorBS;
        }

        public UserModel Save(UserModel user)
        {
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            if (string.IsNullOrEmpty(userBo.PasswordHash) ||
                string.IsNullOrEmpty(userBo.PasswordSalt))
            {
                IHashSalt hashSalt = _encryptorBS.CreatePasswordHashSalt(user.Password);
                userBo.PasswordHash = hashSalt.Hash;
                userBo.PasswordSalt = hashSalt.Salt;
            }
            if (_repository.Save(userBo))
            {
                return Mapper.Map<UserBo, UserModel>(userBo);
            }
            return new UserModel();
        }

        public bool IsEmailUsedBySomeone(UserModel user)
        {
            UserBo thisUserBo = Mapper.Map<UserModel, UserBo>(user);
            var otherUserBo = _repository.FindByKeyValue<UserBo>("Email", thisUserBo.Email.ToLower());
            return (otherUserBo != null && thisUserBo.Id != otherUserBo.Id);
        }

        public UserModel Login(UserModel user)
        {
            var userBo = _repository.FindByKeyValue<UserBo>("Email", user.Email.ToLower());
            if (userBo != null)
            {
                string hash2Authenticate = Encryptor.CreateHashedPassword(user.Password, userBo.PasswordSalt);
                if (hash2Authenticate == userBo.PasswordHash)
                {
                    return Mapper.Map<UserBo, UserModel>(userBo);
                }
            }

            user.Id = new BoRef();
            return user;
        }

        public void Authenticate(UserModel user)
        {
            FormsAuthentication.SetAuthCookie(user.Id.Value, false);
        }

        public UserModel GetUser(BoRef id)
        {
            var userBo = _repository.FindById<UserBo>(id);
            return Mapper.Map<UserBo, UserModel>(userBo);
        }

        public IEnumerable<UserModel> GetUsers(int skip, int take)
        {
            IEnumerable<UserBo> userBos = _repository.FindAll<UserBo>(skip, take);
            return userBos.Select(Mapper.Map<UserBo, UserModel>)
                          .OrderBy(u => u.Name);
        }

        public IEnumerable<UserModel> GetUsers(IEnumerable<BoRef> userIds)
        {
            IEnumerable<ObjectId> oids = userIds.Select(ObjectIdConverter.ConvertBoRefToObjectId);
            IEnumerable<UserBo> userBos = _repository.FindAll<UserBo>()
                                                     .Where(u => oids.Contains(u.Id));
            return userBos.Select(Mapper.Map<UserBo, UserModel>)
                          .OrderBy(u => u.Name);
        }

        public bool Follow(BoRef loggedId, UserModel userToFollow)
        {
            bool result = false;
            var loggedUserBo = _repository.FindById<UserBo>(loggedId);
            var followUserBo = _repository.FindById<UserBo>(userToFollow.Id);
            if (loggedUserBo != null && followUserBo != null)
            {
                //TODO: Transactional?
                if (!loggedUserBo.Following.Contains(followUserBo.Id))
                {
                    loggedUserBo.Following.Add(followUserBo.Id);
                    result = _repository.Save(loggedUserBo);
                }
                if (result && !followUserBo.Followers.Contains(loggedUserBo.Id))
                {
                    followUserBo.Followers.Add(loggedUserBo.Id);
                    result = _repository.Save(followUserBo);
                }
            }
            return result; 
        }

        public bool Unfollow(BoRef loggedId, UserModel userToUnfollow)
        {
            bool result = false;
            var loggedUserBo = _repository.FindById<UserBo>(loggedId);
            var unfollowUserBo = _repository.FindById<UserBo>(userToUnfollow.Id);
            if (loggedUserBo != null && unfollowUserBo != null)
            {
                //TODO: Transactional?
                if (loggedUserBo.Following.Contains(unfollowUserBo.Id))
                {
                    loggedUserBo.Following.Remove(unfollowUserBo.Id);
                    result = _repository.Save(loggedUserBo);
                }
                if (result && unfollowUserBo.Followers.Contains(loggedUserBo.Id))
                {
                    unfollowUserBo.Followers.Remove(loggedUserBo.Id);
                    result = _repository.Save(unfollowUserBo);
                }
            }
            return result;
        }
    }
}