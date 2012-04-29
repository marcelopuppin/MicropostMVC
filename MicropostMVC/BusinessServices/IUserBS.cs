using System.Collections.Generic;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.DependencyResolution;
using MicropostMVC.Models;

namespace MicropostMVC.BusinessServices
{
    [Inject]
    public interface IUserBS
    {
        UserModel Save(UserModel user, bool updatePassword);
        bool IsEmailUsedBySomeone(UserModel user);
        UserModel Login(UserModel user);
        void Authenticate(UserModel user);
        UserModel GetUser(BoRef id);
        IEnumerable<UserModel> GetUsers(int skip, int take);
        IEnumerable<UserModel> GetUsers(IEnumerable<BoRef> userIds);
        IEnumerable<UserModel> GetUsersByName(string search, int skip, int take);
        bool Follow(BoRef id, UserModel userToFollow);
        bool Unfollow(BoRef id, UserModel userToUnfollow);
    }
}