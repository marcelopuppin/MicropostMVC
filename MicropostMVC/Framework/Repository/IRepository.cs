using System;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.DependencyResolution;

namespace MicropostMVC.Framework.Repository
{
    [Inject]
    public interface IRepository : IDisposable
    {
        bool Save<T>(T item) where T : IBoBase;
        T FindById<T>(BoRef id) where T : IBoBase;
    }
}
