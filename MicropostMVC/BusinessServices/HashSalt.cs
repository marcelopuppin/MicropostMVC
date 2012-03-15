namespace MicropostMVC.BusinessServices
{
    public class HashSalt : IHashSalt
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public HashSalt(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}