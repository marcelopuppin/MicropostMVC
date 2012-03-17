using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace MicropostMVC.Framework.DependencyResolution
{
    public class SmControllerActivator : IControllerActivator
    {
        private readonly IContainer _container;
        
        public SmControllerActivator(IContainer container)
        {
            _container = container;
        } 
        
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IController;
        }
    }
}