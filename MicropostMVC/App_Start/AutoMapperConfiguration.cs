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

[assembly: WebActivator.PreApplicationStartMethod(typeof(MicropostMVC.App_Start.AutoMapperConfiguration), "Start")]

namespace MicropostMVC.App_Start {
    public static class AutoMapperConfiguration {
        public static void Start()
        {
            Mapper.CreateMap<UserModel, UserBo>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertBoRefToObjectId(src.Id)))
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.ResolveUsing(src => {
                                  var encryptorBS = DependencyResolver.Current.GetService<IEncryptorBS>();
                                  return encryptorBS.GetStoredPasswordHash(src.Id);
                           }))
                .ForMember(dest => dest.PasswordSalt, 
                           opt => opt.ResolveUsing(src => {
                                  var encryptorBS = DependencyResolver.Current.GetService<IEncryptorBS>();
                                  return encryptorBS.GetStoredPasswordSalt(src.Id);
                           }));

            Mapper.CreateMap<UserBo, UserModel>()
                .ForMember(dest => dest.Id,
                           opt => opt.ResolveUsing(src => ObjectIdConverter.ConvertObjectIdToBoRef(src.Id)))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordConfirmation, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        

    }
}