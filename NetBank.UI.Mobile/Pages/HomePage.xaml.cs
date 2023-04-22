using NetBank.Domain.Entidades;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class HomePage : ContentPage
{
    readonly ApiService ApiService;

    public HomePage()
    {
        ApiService = new ApiService();

        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var tokenValido = await ValidaToken();

        if (tokenValido)
        {
            listContas.ItemsSource = await ApiService.GetContasUsuario();
        }
    }



    public async Task<bool> ValidaToken()
    {
        var token = await SecureStorage.GetAsync("Token");
        if (token == null)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
            return false;
        }
        else
        {
            try
            {
                var usuario = await ApiService.ValidaToken(token);
                return true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return false;
            }
        }
    }



    private async void listContas_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var conta = (Conta)e.Item;
        await Navigation.PushAsync(new DetalhesContaPage(conta));
    }
}