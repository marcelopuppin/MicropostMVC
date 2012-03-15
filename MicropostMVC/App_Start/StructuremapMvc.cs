using System.Web.Mvc;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.DependencyResolution;
using MicropostMVC.Framework.Repository;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MicropostMVC.App_Start.StructuremapMvc), "Start")]

namespace MicropostMVC.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            //IContainer container = IoC.Initialize();
            IContainer container = new Container(x =>
                                                     {
                                                         x.For<IRepository>().Use<MongoRepository>();
                                                         x.For<IEncryptorBS>().Use<EncryptorBS>();
                                                     });

            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}