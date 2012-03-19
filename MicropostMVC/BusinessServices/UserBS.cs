using AutoMapper;
using MicropostMVC.BusinessObjects;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Repository;

namespace MicropostMVC.Models
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
    }
}