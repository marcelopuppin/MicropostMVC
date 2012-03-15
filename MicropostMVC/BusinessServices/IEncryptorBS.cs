namespace MicropostMVC.BusinessServices
{
    public interface IEncryptorBS
    {
        string GetStoredPasswordHash(string id);
        string GetStoredPasswordSalt(string id);
        IHashSalt CreatePasswordHashSalt(string password);
    }
}