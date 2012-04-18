using System.Collections.Generic;
using System.Linq;
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
                            opt => opt.ResolveUsing<BoRefs2OidsResolver>().FromMember(src => src.Following))
                 .ForMember(dest => dest.Followers,
                            opt => opt.ResolveUsing<BoRefs2OidsResolver>().FromMember(src => src.Followers));

            Mapper.CreateMap<UserBo, UserModel>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertObjectIdToBoRef(src.Id)))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordConfirmation, opt => opt.Ignore())
                .ForMember(dest => dest.Following, 
                           opt => opt.MapFrom(src => src.Following))
                .ForMember(dest => dest.Following,
                           opt => opt.ResolveUsing<Oids2BoRefsResolver>().FromMember(src => src.Following))
                .ForMember(dest => dest.Followers,
                           opt => opt.ResolveUsing<Oids2BoRefsResolver>().FromMember(src => src.Followers));
           
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
    public class BoRefs2OidsResolver : ValueResolver<List<BoRef>, List<ObjectId>>
    {
        protected override List<ObjectId> ResolveCore(List<BoRef> source)
        {
            return source.Select(ObjectIdConverter.ConvertBoRefToObjectId).ToList();
        }
    }

    public class Oids2BoRefsResolver : ValueResolver<List<ObjectId>, List<BoRef>>
    {
        protected override List<BoRef> ResolveCore(List<ObjectId> source)
        {
            return source.Select(ObjectIdConverter.ConvertObjectIdToBoRef).ToList();
        }
    }
    #endregion
}