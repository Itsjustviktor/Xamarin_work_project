using AndroidApp.Models;
using System.Collections.Generic;

namespace AndroidApp.Services.DataContext
{
	public interface IGetModel<T>
		where T : BaseModel
	{
		IEnumerable<T> Get();
		IEnumerable<T> Get(string search);

		T Get(int id);
	}
}
