using NetBank.Domain.Entidades;
using NetBank.DTOs;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class DetalhesContaPage : ContentPage
{
    ApiService _apiService;

    public ContaDTO Conta;

    public DetalhesContaPage(ContaDTO conta)
    {
        Conta = conta;
        InitializeComponent();

        _apiService = new ApiService();

        lblValorEmConta.Text = conta.ValorEmConta.ToString("c");
        lblAgencia.Text = $"Ag�ncia: {conta.Agencia}";
        lblNumero.Text = $"N�mero: {conta.Numero}";

        datePickerDataInicial.Date = DateTime.Now.AddMonths(-1);
        datePickerDataFinal.Date = DateTime.Now;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();



        listTransacoes.ItemsSource = await GetTransacoes();
    }


    private async Task<IEnumerable<TransacaoDTO>> GetTransacoes()
    {
        var dataInicial = datePickerDataInicial.Date;
        var dataFinal = datePickerDataFinal.Date.AddDays(1);

        var transacoes = await _apiService.GetTransacoesConta(
            Conta.Id, dataInicial, dataFinal);

        return transacoes.OrderByDescending(x => x.DataOperacao);
    }

    private async void btnPesquisar_Clicked(object sender, EventArgs e)
    {
        listTransacoes.ItemsSource = await GetTransacoes();
    }

    private void btnComprovante_Clicked(object sender, EventArgs e)
    {

    }


    private async void btnNovaTransacao_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovaTransacaoPage(Conta));
    }
}