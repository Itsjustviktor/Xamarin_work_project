using AndroidApp.Models;
using AndroidApp.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AndroidApp.ViewModels
{
	public class FeedbackViewModel
		: BaseViewModel
	{
		private IFeedbackDataStore _datastore { get; } = DependencyService.Get<IFeedbackDataStore>();

		private bool isAll = true;
		private bool isCallBack;
		private bool isRefresh;

		private string search;

		public ObservableCollection<QuestionModel> Collection { get; private set; } // При ихзменении колекции изменяется изображение

		public bool IsAll // Хранение статуса отбражения всех
		{
			get { return isAll; }
			set
			{
				isAll = value;
				OnPropertyChanged(nameof(IsAll)); // Обновление на view
			}
		}

		public bool IsCallBack // Хранение статуса перезвонить
		{
			get { return isCallBack; }
			set
			{
				isCallBack = value;
				OnPropertyChanged(nameof(IsCallBack));
			}
		}

		public FeedbackViewModel()
		{
			var list = _datastore.Get();
			Collection = new ObservableCollection<QuestionModel>(list);
		}

		public ICommand UpdateQuestionCommand => new Command<QuestionModel>((ob) => //Обновление
		{
			_datastore.Save(ob); 
			IsRefresh = true; 
		});

		public ICommand SortCommand => new Command(async (_) => // Обновление страницы
		{
			Collection.Clear();

			IEnumerable<QuestionModel> collection;

			Search = "";

			if (IsAll) // Отображаем все
				collection = await Task.Run(() => _datastore.Get());
			else // Отображаем которые не решены
				collection = await Task.Run(() => _datastore.Get().Where(x => x.Status != "Решено"));

			Collection.Clear();

			foreach (var item in collection)
				Collection.Add(item);

			/// TODO: Ипсравить, но не удалять!!
			await Task.Delay(1);

			Collection.Add(null);
			Collection.RemoveAt(Collection.Count() - 1);

			IsRefresh = false;
		}); 
		public ICommand SearchCommand => new Command<String>(async (text) => // Поиск по номеру телефона
		{
			var search = text ?? this.Search;

			Collection.Clear();

			IEnumerable<QuestionModel> collection;

			if (IsAll)
				collection = await Task.Run(() => _datastore.Get(search));
			else
				collection = await Task.Run(() => _datastore.Get(search).Where(x => x.Status != "Решено"));

			Collection.Clear();

			foreach (var item in collection)
				Collection.Add(item);

			/// TODO: Ипсравить, но не удалять!!
			await Task.Delay(1);

			Collection.Add(null);
			Collection.RemoveAt(Collection.Count() - 1);
		});
		public bool IsRefresh
		{
			get { return isRefresh; }
			set
			{
				isRefresh = value;
				OnPropertyChanged(nameof(IsRefresh));
			}
		}
        public string Search
		{
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged(nameof(Search));
            }
        }
    }
}
