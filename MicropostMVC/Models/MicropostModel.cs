using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MicropostMVC.Framework.Common;

namespace MicropostMVC.Models
{
    public class MicropostModel
    {
        [Required, HiddenInput(DisplayValue = false)]
        public BoRef Id { get; set; }

        [Required, StringLength(140)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}