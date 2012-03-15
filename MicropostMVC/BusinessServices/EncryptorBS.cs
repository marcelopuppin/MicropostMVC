using System.Security.Cryptography;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;

namespace MicropostMVC.BusinessServices
{
    public class EncryptorBS : IEncryptorBS
    {
        private readonly IRepository _repository;
       
        public EncryptorBS(IRepository repository)
        {
            _repository = repository;
        }

        public string GetStoredPasswordHash(string id)
        {
            var userBo = _repository.FindById<UserBo>(id);
            return userBo != null ? userBo.PasswordHash : string.Empty;
        }

        public string GetStoredPasswordSalt(string id)
        {
            var userBo = _repository.FindById<UserBo>(id);
            return userBo != null ? userBo.PasswordSalt : string.Empty;
        }

        public IHashSalt CreatePasswordHashSalt(string password)
        {
            string salt = Encryptor.CreateSalt(4);
            string hash = Encryptor.CreateHashedPassword(password, salt);
            return new HashSalt(hash, salt);
        }
    }
}