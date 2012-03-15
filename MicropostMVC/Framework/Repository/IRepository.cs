using System;

namespace MicropostMVC.Framework.Repository
{
    public interface IRepository : IDisposable
    {
        bool Save<T>(T item) where T : IBoBase;
        T FindById<T>(string id) where T : IBoBase;
    }
}
