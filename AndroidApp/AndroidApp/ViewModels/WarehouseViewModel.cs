using AndroidApp.Models;
using AndroidApp.Services;

using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AndroidApp.Views;

namespace AndroidApp.ViewModels
{
	public class WarehouseViewModel
		: BaseViewModel
	{
		private bool countChecked;
		private bool isRefreshing;
		private bool isCount;
		private readonly IWarehouseDataStore _datastore = DependencyService.Get<IWarehouseDataStore>();

		public ObservableCollection<CameraModel> Collection { get; }

		public Boolean CountChecked // Хранение статуса сортировки
		{
			get { return countChecked; }
			set
			{
				countChecked = value;
				OnPropertyChanged(nameof(CountChecked));
			}
		}
		public bool IsRefreshing // Хранение статуса обновления
		{
			get { return isRefreshing; }
			set
			{
				isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}

		public WarehouseViewModel()
		{
			Collection = new ObservableCollection<CameraModel>(_datastore.Get()); // Инициализируется коллекция и получение с сервера
		}

		public ICommand OnCountChecked => new Command(async (_) => // Сортировка по кол-ву
		{
			isCount = !isCount;
			CountChecked = isCount;

			try
			{
				IEnumerable<CameraModel> collection = null;

				if (CountChecked)
					collection = await Task.Run(() => _datastore.Get().OrderByDescending(x => x.Count));
				else
					collection = await Task.Run(() => _datastore.Get());

				Collection.Clear();

				foreach (var item in collection)
					Collection.Add(item);

				await Task.Delay(1);

				Collection.Add(null);
				Collection.RemoveAt(Collection.Count() - 1);
			}
			finally
			{
				IsRefreshing = false;
			}
		});

		public ICommand OnTapped => new Command<CameraModel>((ob) => // Открывает activity и передает туда id камеры
		{
			Shell.Current.GoToAsync(nameof(CameraPage) + $"?Identity={ob.Id}"); 
		});

		public ICommand RefreshingCommand => new Command(() => IsRefreshing = true);
		public ICommand RefreshCommand => new Command(async (_) => { // Обновление
			try
			{
				IEnumerable<CameraModel> collection = null;

				if (CountChecked)
					collection = await Task.Run(() => _datastore.Get().OrderByDescending(x => x.Count));
				else
					collection = await Task.Run(() => _datastore.Get());

				Collection.Clear();

				foreach (var item in collection)
					Collection.Add(item);

				await Task.Delay(1);

				Collection.Add(null);
				Collection.RemoveAt(Collection.Count() - 1);
			}
			finally
			{
				IsRefreshing = false;
			}
		});
	}
}