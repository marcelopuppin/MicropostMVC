using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MicropostMVC.Framework.Common;

namespace MicropostMVC.Models
{
    public abstract class ModelWithId
    {
        protected ModelWithId()
        {
            Id = new BoRef();
        }

        [Required, HiddenInput(DisplayValue = false)]
        public BoRef Id { get; set; }
    }
}