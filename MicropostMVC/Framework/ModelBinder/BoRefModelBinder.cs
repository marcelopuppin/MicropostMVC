using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicropostMVC.Framework.Common;

namespace MicropostMVC.Framework.Binder
{
    public class BoRefModelBinder : IModelBinder
    {
        //protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, 
        //                                     System.ComponentModel.PropertyDescriptor propertyDescriptor)
        //{
        //    if (propertyDescriptor.Name == "Id" && propertyDescriptor.PropertyType == typeof(BoRef))
        //    {
        //        NameValueCollection parameters = controllerContext.RequestContext.HttpContext.Request.Form;
        //        string[] values = parameters.GetValues(propertyDescriptor.Name);
        //        if (values != null && values.Length > 0)
        //        {
        //            SetProperty(controllerContext, bindingContext, propertyDescriptor, new BoRef(values[0]));
        //        }
        //    }
        //    else
        //    {
        //        base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        //    }
        //}

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            string id = request.Form.Get("Id");
            if (string.IsNullOrEmpty(id))
            {
                bindingContext.ModelState.AddModelError("Id", "The id is required.");
            }
            return new BoRef(id);
        }
    }
}