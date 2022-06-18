using AndroidApp.Views;
using Xamarin.Forms;

namespace AndroidApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WarehousePage), typeof(WarehousePage));
            Routing.RegisterRoute(nameof(OrdersPage), typeof(OrdersPage));
            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
            Routing.RegisterRoute(nameof(CameraPage), typeof(CameraPage));
        } // Регистрация путей
    }
}
