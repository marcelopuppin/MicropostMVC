using System.Collections.Generic;
using System.Web.Security;
using AutoMapper;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using System.Linq;
using MicropostMVC.Models;

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
    }
}