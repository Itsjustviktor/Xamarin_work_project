/// #define DROP_CREATE


using AndroidApp.Models;

using SQLite;

using System;
using System.Collections.Generic;

namespace AndroidApp.Services.DataContext
{
	public class DataBaseContextCameraModel
		: BaseDataBaseContext<CameraModel>
	{
		public DataBaseContextCameraModel(SQLiteConnection context)
			: base(context)
		{
			try
			{
#if DROP_CREATE
				this._context.DropTable<CameraModel>();
#endif

				/// проверяем, есть ли таблица
				this._context.Table<CameraModel>().Count();
			}
			catch (Exception)
			{
				this._context.CreateTable<CameraModel>();

				this.Save(new CameraModel() { Source = "camera.png", Id = 0, Category = "Red", Count = 6, Title = "kamodo" });
				this.Save(new CameraModel() { Source = "camera.png", Id = 0, Category = "Canon", Count = 3, Title = "5dm4" });
				this.Save(new CameraModel() { Source = "camera.png", Id = 0, Category = "Sony", Count = 3, Title = "a7m2" });
				this.Save(new CameraModel() { Source = "camera.png", Id = 0, Category = "Canon", Count = 23, Title = "60d" });
				this.Save(new CameraModel() { Source = "camera.png", Id = 0, Category = "Sony", Count = 10, Title = "a6000" });
				this.Save(new CameraModel() { Source = "camera.png", Id = 0, Category = "Canon", Count = 7, Title = "R5" });
			}
        }
        public override IEnumerable<CameraModel> Get(string search)
		{
			return String.IsNullOrWhiteSpace(search)
				? this.Get()
				: _context.Table<CameraModel>().Where(x => x.Category.Contains(search));
		}
	}
}
