using AndroidApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace AndroidApp.Services.DataContext
{
    public abstract class BaseDataBaseContext<T> // Взаимодействие с локальной БД
        : IGetModel<T>, ISaveModel<T>
        where T : BaseModel, new()
    {
        protected readonly SQLiteConnection _context;
        public BaseDataBaseContext(SQLiteConnection context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

			/// _context.CreateTable<T>();
		}

		public virtual IEnumerable<T> Get()
		{
			return _context.Table<T>().ToList();
		}

		public abstract IEnumerable<T> Get(string search);

		public virtual T Get(int id)
		{
			return _context.Get<T>(id);
		}
		public virtual void Save(T model)
		{
            try
            {
				var _model = this.Get(model.Id);
				_context.Update(model);
			}
            catch (Exception ex)
            {
				Log.Warning("INFO", ex.Message);
				_context.Insert(model);
			}
		}
	}
}
