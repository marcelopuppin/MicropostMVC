using MicropostMVC.Framework.DependencyResolution;
using MicropostMVC.Models;

namespace MicropostMVC.BusinessServices
{
    [Inject]
    public interface IUserBS
    {
        UserModel Save(UserModel user);
        bool IsEmailUsedBySomeone(UserModel user);
        UserModel Login(UserModel user);
        void Authenticate(UserModel user);
    }
}