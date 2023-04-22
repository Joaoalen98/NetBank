using NetBank.UI.Mobile.Pages;

namespace NetBank.UI.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }
    }
}