using System.Linq;
using MicropostMVC.BusinessObjects;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.Repository;
using MicropostMVC.Models;
using AutoMapper;

namespace MicropostMVC.BusinessServices
{
    public class MicropostBS : IMicropostBS
    {
        private readonly IRepository _repository;

        public MicropostBS(IRepository repository)
        {
            _repository = repository;
        }

        public bool Save(UserModel user, string micropostContent)
        {
            UserBo userBo = Mapper.Map<UserModel, UserBo>(user);
            var micropost = new MicropostModel { Content = micropostContent };
            MicropostBo micropostBo = Mapper.Map<MicropostModel, MicropostBo>(micropost);
            userBo.Microposts.Add(micropostBo);
            return _repository.Save(userBo);
        }

        public bool Delete(UserModel user, BoRef micropostId)
        {
            var userBo = _repository.FindById<UserBo>(user.Id);
            var micropostOid = ObjectIdConverter.ConvertBoRefToObjectId(micropostId);
            MicropostBo micropostBo = userBo.Microposts.FirstOrDefault(m => m.Id == micropostOid);
            if (micropostBo != null)
            {
                userBo.Microposts.Remove(micropostBo);
                return _repository.Save(userBo);
            }
            return false;
        }

        public MicropostsForUserModel GetMicropostsForUser(UserModel user)
        {
            var micropostsForUser = new MicropostsForUserModel();
            micropostsForUser.User = user;

            var micropostOwners = user.Microposts
                                      .Select(m => new MicropostOwnerModel {Owner = user, Micropost = m})
                                      .ToList();
                                  
            foreach (BoRef userId in user.Following)
            {
                var userBo = _repository.FindById<UserBo>(userId);
                UserModel followingUser = Mapper.Map<UserBo, UserModel>(userBo);
                if (followingUser != null)
                {
                    var micropostsFollowOwner = followingUser.Microposts
                                                             .Where(m => m != null)
                                                             .Select(m => new MicropostOwnerModel { Owner = followingUser, Micropost = m })
                                                             .ToList();
                    micropostOwners.AddRange(micropostsFollowOwner);
                }
            }

            micropostsForUser.Microposts = micropostOwners.OrderByDescending(mo => mo.Micropost.CreatedAt);
            return micropostsForUser;
        }
    }
}