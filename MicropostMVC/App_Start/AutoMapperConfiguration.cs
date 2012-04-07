using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MicropostMVC.BusinessObjects;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;
using MongoDB.Bson;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MicropostMVC.App_Start.AutoMapperConfiguration), "Start")]

namespace MicropostMVC.App_Start {
    public static class AutoMapperConfiguration {
        public static void Start()
        {
            // User

            Mapper.CreateMap<UserModel, UserBo>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertBoRefToObjectId(src.Id)))
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.ResolveUsing(src =>
                           {
                               var encryptorBS = DependencyResolver.Current.GetService<IEncryptorBS>();
                               return encryptorBS.GetStoredPasswordHash(src.Id);
                           }))
                .ForMember(dest => dest.PasswordSalt,
                           opt => opt.ResolveUsing(src =>
                           {
                               var encryptorBS = DependencyResolver.Current.GetService<IEncryptorBS>();
                               return encryptorBS.GetStoredPasswordSalt(src.Id);
                           }))
                 .ForMember(dest => dest.Following, 
                            opt => opt.ResolveUsing<FollowingBoRefResolver>())
                 .ForMember(dest => dest.Followers, 
                            opt => opt.ResolveUsing<FollowersBoRefResolver>());

            Mapper.CreateMap<UserBo, UserModel>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertObjectIdToBoRef(src.Id)))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordConfirmation, opt => opt.Ignore())
                .ForMember(dest => dest.Microposts, 
                           opt => opt.ResolveUsing(src => src.Microposts.OrderByDescending(m => m.Id.CreationTime)))
                .ForMember(dest => dest.Following,
                           opt => opt.ResolveUsing<FollowingOidResolver>())
                .ForMember(dest => dest.Followers,
                           opt => opt.ResolveUsing<FollowersOidResolver>());
           
            // Micropost

            Mapper.CreateMap<MicropostBo, MicropostModel>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertObjectIdToBoRef(src.Id)))
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.ResolveUsing(src => src.Id.CreationTime));
                
            Mapper.CreateMap<MicropostModel, MicropostBo>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertBoRefToObjectId(src.Id)));

            // Validate
            Mapper.AssertConfigurationIsValid();
        }
    }

    #region [ Resolvers ]
    public class FollowingBoRefResolver : ValueResolver<UserModel, List<ObjectId>>
    {
        protected override List<ObjectId> ResolveCore(UserModel source)
        {
            return source.Following.Select(ObjectIdConverter.ConvertBoRefToObjectId).ToList();
        }
    }

    public class FollowingOidResolver : ValueResolver<UserBo, List<BoRef>>
    {
        protected override List<BoRef> ResolveCore(UserBo source)
        {
            return source.Following.Select(ObjectIdConverter.ConvertObjectIdToBoRef).ToList();
        }
    }

    public class FollowersBoRefResolver : ValueResolver<UserModel, List<ObjectId>>
    {
        protected override List<ObjectId> ResolveCore(UserModel source)
        {
            return source.Followers.Select(ObjectIdConverter.ConvertBoRefToObjectId).ToList();
        }
    }

    public class FollowersOidResolver : ValueResolver<UserBo, List<BoRef>>
    {
        protected override List<BoRef> ResolveCore(UserBo source)
        {
            return source.Followers.Select(ObjectIdConverter.ConvertObjectIdToBoRef).ToList();
        }
    }
    #endregion
}