using Xamarin.Forms;

namespace AndroidApp.Views
{
    public partial class WarehousePage : ContentPage
    {
        bool tap = true;

        public WarehousePage()
        {
            InitializeComponent();
        }

        private void OnTapped(object sender, System.EventArgs e)
        {
            if (sender is RadioButton)
            {
                ((RadioButton)sender).IsChecked = tap;
                tap = !tap;
            }
        }
    }
}