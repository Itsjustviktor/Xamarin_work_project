using AndroidApp.Models;

namespace AndroidApp.Services.DataContext
{
    public interface ISaveModel<T>
        where T: BaseModel
    {
        void Save(T model);
    }
}
