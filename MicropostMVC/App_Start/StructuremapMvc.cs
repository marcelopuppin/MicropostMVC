using System.Globalization;
using System.Web.Mvc;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.DependencyResolution;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MicropostMVC.App_Start.StructuremapMvc), "Start")]

namespace MicropostMVC.App_Start
{
    public static class StructuremapMvc
    {
        public static void Start()
        {
            //IContainer container = IoC.Initialize();
            IContainer container = new Container(x => {
                x.For<IControllerActivator>().Use<SmControllerActivator>();
                x.Scan(c => {
                    c.TheCallingAssembly();
                    c.RegisterConcreteTypesAgainstTheFirstInterface();
                    c.Convention<SmInjectAttributeConvention>();
                });
            });

            DependencyResolver.SetResolver(new SmDependencyResolver(container));

            ObjectFactory.AssertConfigurationIsValid();
        }
    }
} 
