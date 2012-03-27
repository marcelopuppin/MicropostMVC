using System;
using System.Collections.Generic;
using MicropostMVC.Framework.Common;
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
        UserModel GetUser(BoRef id);
        IEnumerable<UserModel> GetUsers(int skip, int take);
    }
}