using AndroidApp.Models;

using System.Collections.Generic;

namespace AndroidApp.Services
{
    public interface IDataStore<T>
        where T : BaseModel
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(string search);
        void Save(T model);
    }
}
