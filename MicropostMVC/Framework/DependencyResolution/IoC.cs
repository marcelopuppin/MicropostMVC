//using StructureMap;

//namespace MicropostMVC.Framework.DependencyResolution {
//    public static class IoC {
//        public static IContainer Initialize() {
//            ObjectFactory.Initialize(x => x.Scan(scan => 
//                                        { 
//                                            scan.TheCallingAssembly();
//                                            scan.WithDefaultConventions();
//                                         }));
//            return ObjectFactory.Container;
//        }
//    }
//}