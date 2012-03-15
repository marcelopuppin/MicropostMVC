using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using DataAnnotationsExtensions;
using MicropostMVC.BusinessObjects;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Repository;
using MongoDB.Bson;

namespace MicropostMVC.Models
{
    public class UserModel
    {
        [Required, ScaffoldColumn(false)]
        public string Id { get; set;  }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, Email]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required, StringLength(50, MinimumLength = 6), 
         EqualTo("Password"), 
         Display(Name = "Password Confirmation")]
        public string PasswordConfirmation { get; set; }



        public UserModel Save()
        {
            UserBo userBo = Mapper.Map<UserModel, UserBo>(this);
            if (string.IsNullOrEmpty(Id))
            {
                var encryptorBS = DependencyResolver.Current.GetService<IEncryptorBS>();
                IHashSalt hashSalt = encryptorBS.CreatePasswordHashSalt(Password);
                userBo.PasswordHash = hashSalt.Hash;
                userBo.PasswordSalt = hashSalt.Salt;
            }
            var repository = DependencyResolver.Current.GetService<IRepository>();
            if (repository.Save(userBo))
            {
                return Mapper.Map<UserBo, UserModel>(userBo);
            }
            return new UserModel();
        }
    }
}