using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.DependencyResolution;
using MicropostMVC.Models;

namespace MicropostMVC.BusinessServices
{
    [Inject]
    public interface IMicropostBS
    {
        bool Save(UserModel user, string micropostContent);
        MicropostModel SaveNew(UserModel user, string micropostContent);
        bool Delete(UserModel user, BoRef micropostId);
        MicropostsForUserModel GetMicropostsForUser(UserModel user);
    }
}