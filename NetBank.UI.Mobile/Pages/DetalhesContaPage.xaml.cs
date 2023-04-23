using NetBank.DTOs;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class DetalhesContaPage : ContentPage
{
    string _idConta;

    ApiService _apiService;

    ContaDTO _conta;

    public DetalhesContaPage(string idConta)
    {
        _idConta = idConta;
        InitializeComponent();
        _apiService = new ApiService();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _conta = await _apiService.GetContaUsuarioPorId(_idConta);

        var dataInicial = DateTime.Now.AddMonths(-1);
        var dataFinal = DateTime.Now;

        datePickerDataInicial.Date = dataInicial;
        datePickerDataFinal.Date = dataFinal;

        listTransacoes.ItemsSource = _conta.Transacoes
            .Where(x => x.DataOperacao >= dataInicial
            && x.DataOperacao <= dataFinal.AddDays(1))
            .OrderByDescending(x => x.DataOperacao);


        lblValorEmConta.Text = _conta.ValorEmConta.ToString("c");
        lblAgencia.Text = $"Agência: {_conta.Agencia}";
        lblNumero.Text = $"Número: {_conta.Numero}";
    }


    private async Task<IEnumerable<TransacaoDTO>> GetTransacoes()
    {
        var dataInicial = datePickerDataInicial.Date;
        var dataFinal = datePickerDataFinal.Date.AddDays(1);

        var transacoes = await _apiService.GetTransacoesConta(
            _conta.Id, dataInicial, dataFinal);

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
        await Navigation.PushAsync(new NovaTransacaoPage(_conta));
    }
}