using NetBank.DTOs;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class CadastroPage : ContentPage
{
    ApiService _apiService;

    public CadastroPage()
    {
        InitializeComponent();
        datePickerDataNascimento.Date = DateTime.Now.AddYears(-18);

        _apiService = new ApiService();
    }

    private async void btnCadastrar_Clicked(object sender, EventArgs e)
    {
        if (entrySenha.Text != entryConfirmacaoSenha.Text)
        {
            await DisplayAlert("Informa��es incorretas", "As duas senhas n�o informadas n�o s�o iguais", "Fechar");
        }

        var usuarioDTO = new UsuarioDTO()
        {
            CPF = entryCPF.Text,
            DataNascimento = datePickerDataNascimento.Date,
            Email = entryEmail.Text,
            NomeCompleto = entryNome.Text,
            Senha = entrySenha.Text,
            Telefone = entryTelefone.Text,
        };

        try
        {
            await _apiService.CadastrarUsuario(usuarioDTO);
            await DisplayAlert("Sucesso", "Usu�rio cadastrado com sucesso", "Ir para login");
            await Navigation.PushAsync(new LoginPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro ao cadastrar", ex.Message, "Fechar");
        }
    }
}