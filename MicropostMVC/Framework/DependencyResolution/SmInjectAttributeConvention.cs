using System;
using System.Linq;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MicropostMVC.Framework.DependencyResolution
{
    public class SmInjectAttributeConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            bool hasInjectAttribute = type.GetCustomAttributes(false)
                                          .Any(attr => attr.Equals(typeof(InjectAttribute)));
            if (hasInjectAttribute)
            {
                registry.AddType(type);
            }
        }
    }
}