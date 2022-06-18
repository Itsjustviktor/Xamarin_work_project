/// #define DROP_CREATE

using AndroidApp.Models;

using SQLite;

using System;
using System.Collections.Generic;

namespace AndroidApp.Services.DataContext
{
	public class DataBaseContextQuestionModel
		: BaseDataBaseContext<QuestionModel>
	{
		public DataBaseContextQuestionModel(SQLiteConnection context)
			: base(context)
		{
            try
			{
#if DROP_CREATE
				this._context.DropTable<QuestionModel>();
#endif
				/// проверяем, есть ли таблица
				this._context.Table<QuestionModel>().Count();
            }
            catch (Exception)
            {
				this._context.CreateTable<QuestionModel>();

				this.Save(new QuestionModel() { Id = 1, BuyerFirstName = "Витя", BuyerLastName = "", BuyerMiddleName = "", Code = "3123", Phone = "78412847141", Status = "Решено", QuestionString = "Проблема с доставкой" });
				this.Save(new QuestionModel() { Id = 2, BuyerFirstName = "Витя", BuyerLastName = "", BuyerMiddleName = "", Code = "3124", Phone = "78412847141", Status = "Перезвонить", QuestionString = "Проблема с доставкой" });
				this.Save(new QuestionModel() { Id = 3, BuyerFirstName = "Витя", BuyerLastName = "", BuyerMiddleName = "", Code = "3125", Phone = "78412847141", Status = "Решено", QuestionString = "Проблема с доставкой" });
			}
		}
		public override IEnumerable<QuestionModel> Get(string search)
		{
			return String.IsNullOrWhiteSpace(search)
				? Get()
				: _context.Table<QuestionModel>().Where(x => x.Phone.Contains(search));
		}
	}
}
