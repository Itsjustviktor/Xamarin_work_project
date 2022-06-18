using AndroidApp.Services;
using AndroidApp.Services.DataContext;

using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace AndroidApp
{
    public partial class App 
		: Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();

			var context = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db")); //Подключение к бд

			DependencyService.RegisterSingleton<IFeedbackDataStore>(new FeedbackDataStore(new DataBaseContextQuestionModel(context), new APIContextQuestionModel())); // Получаем данный из БД или из сервера
			DependencyService.RegisterSingleton<IWarehouseDataStore>(new WarehouseDataStore(new DataBaseContextCameraModel(context), new APIContextCameraModel())); // Получаем данный из БД или из сервера
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
