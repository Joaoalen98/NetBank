using NetBank.Domain.Entidades;
using NetBank.DTOs;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class NovaTransacaoPage : ContentPage
{
    ApiService ApiService;

    public ContaDTO Conta { get; set; }

    public NovaTransacaoPage(ContaDTO conta)
    {
        Conta = conta;
        ApiService = new ApiService();

        InitializeComponent();
        BindingContext = Conta;

        lblValorEmConta.Text = conta.ValorEmConta.ToString("c");
        lblAgencia.Text = $"Ag�ncia: {conta.Agencia}";
        lblNumero.Text = $"N�mero: {conta.Numero}";
    }


    private async void btnEnviaTransacao_Clicked(object sender, EventArgs e)
    {
        var valor = Convert.ToDecimal(entryValor.Text);
        var agencia = entryAgencia.Text;
        var numero = entryNumero.Text;

        var novaTransacaoDTO = new CriarTransacaoDTO
        {
            Agencia = agencia,
            Numero = numero,
            Valor = valor
        };

        try
        {
            await ApiService.EnviarTransacao(Conta.Id, novaTransacaoDTO);
            await DisplayAlert("Sucesso", "Transa��o enviada com �xito!", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }
}