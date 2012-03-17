using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.DependencyResolution;

namespace MicropostMVC.BusinessServices
{
    [Inject]
    public interface IEncryptorBS
    {
        string GetStoredPasswordHash(BoRef id);
        string GetStoredPasswordSalt(BoRef id);
        IHashSalt CreatePasswordHashSalt(string password);
    }
}