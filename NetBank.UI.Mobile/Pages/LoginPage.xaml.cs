using NetBank.DTOs;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void btnSemConta_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroPage());
    }

    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
        var loginDTO = new LoginDTO
        {
            CPF = entryCPF.Text,
            Senha = entrySenha.Text
        };

        var apiService = new ApiService();

        try
        {
            var auth = await apiService.AutenticarUsuario(loginDTO);

            await SecureStorage.SetAsync("Token", auth.Token);
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Fechar");
        }
    }
}