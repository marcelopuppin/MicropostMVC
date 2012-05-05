using System.Configuration;
using System.Web.Mvc;
using MicropostMVC.Framework.DependencyResolution;
using MicropostMVC.Framework.Repository;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MicropostMVC.App_Start.StructuremapMvc), "Start")]

namespace MicropostMVC.App_Start
{
    public static class StructuremapMvc
    {
        public static void Start()
        {
            string environment = ConfigurationManager.AppSettings["environment"];
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[environment];
            
            IContainer container = new Container(x => {
                x.For<IControllerActivator>().Use<SmControllerActivator>();
                x.For<IRepository>().Use<MongoRepository>()
                                    .Ctor<string>("connectionString").Is(settings.ConnectionString)
                                    .Ctor<string>("databaseName").Is(settings.Name);
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
