using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage 
        : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }

        private async void ReloadLayoutPage(object sender, EventArgs e) { }
    }
}