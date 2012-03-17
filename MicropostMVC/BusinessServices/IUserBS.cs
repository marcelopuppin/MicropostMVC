using MicropostMVC.Framework.DependencyResolution;
using MicropostMVC.Models;

namespace MicropostMVC.BusinessServices
{
    [Inject]
    public interface IUserBS
    {
        UserModel Save(UserModel user);
    }
}