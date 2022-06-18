using AndroidApp.Models;
using AndroidApp.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace AndroidApp.ViewModels
{
    [QueryProperty(nameof(Identity), nameof(Identity))] // Получаем id при переходе из warehouse
    public class CameraViewModel
        : BaseViewModel
    {
        private CameraModel cameraModel;

        public CameraModel CameraModel  // Хранение статуса модели камеры
        { 
            get => cameraModel; 
            private set 
            { 
                cameraModel = value;
                OnPropertyChanged(nameof(CameraModel)); // Обновление данных на view
            }
        }
        public Int32 Identity { set { SetModelAsync(value); } } // Инициализирует CameraModel

        private async void SetModelAsync(int identity)
        {
            CameraModel = DependencyService.Get<IWarehouseDataStore>()
                .Get()
                .FirstOrDefault(x => x.Id == identity);

            if (CameraModel is null)
            {
                await Application.Current.MainPage.DisplayAlert("Прикооооол", "Данные не найдены", "Ок", "Ок");

                await Shell.Current.GoToAsync(".."); // Возвращение на предыдущую активити
            }
        } // Получение камеры по id
    }
}
