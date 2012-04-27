using System;
using System.Collections.Generic;
using MicropostMVC.Framework.Common;
using MicropostMVC.Framework.DependencyResolution;

namespace MicropostMVC.Framework.Repository
{
    [Inject]
    public interface IRepository : IDisposable
    {
        bool Save<T>(T item) where T : IBoBase;
        bool Remove<T>(BoRef id) where T : IBoBase;
        T FindById<T>(BoRef id) where T : IBoBase;
        T FindByKeyValue<T>(string key, object value) where T : IBoBase;
        IEnumerable<T> FindUsingFilter<T>(Func<T, bool> filter, int skip = 0, int take = int.MaxValue) where T : IBoBase;
        IEnumerable<T> FindAll<T>(int skip = 0, int take = int.MaxValue) where T : IBoBase;
    }
}
