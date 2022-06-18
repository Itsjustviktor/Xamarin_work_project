using AndroidApp.Models;
using AndroidApp.Services.DataContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace AndroidApp.Services
{
    public class WarehouseDataStore
        : IWarehouseDataStore
    {
        public DataBaseContextCameraModel DbContext { get; }
        public APIContextCameraModel ApiContext { get; }

        public WarehouseDataStore(DataBaseContextCameraModel dbContext, APIContextCameraModel apiContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            ApiContext = apiContext ?? throw new ArgumentNullException(nameof(apiContext));
        }

        public IEnumerable<CameraModel> Get()
        {
            IEnumerable<CameraModel> list;

            try
            {
                list = ApiContext.Get();

                SaveAll(list);
            }
            catch (Exception ex)
            {
                Log.Warning("WARNING", ex.Message);
                list = DbContext.Get();
            }

            return list;
        }
        public IEnumerable<CameraModel> Get(string search)
        {
            IEnumerable<CameraModel> list;

            try
            {
                list = ApiContext.Get(search);
            }
            catch (Exception ex)
            {
                Log.Warning("WARNING", ex.Message);
                list = DbContext.Get(search);
            }

            return list;
        }

        public void Save(CameraModel model)
        {
            try
            {
                ApiContext.Save(model);

                SaveAll(Get());
            }
            catch (Exception ex)
            {
                Log.Warning("WARNING", ex.Message);
            }

            SaveAll(Get());
        }
        private async void SaveAll(IEnumerable<CameraModel> list)
        {
            await Task.Run(() => {
                foreach (var item in list)
                {
                    DbContext.Save(item);
                }
            });
        }
    }
}
