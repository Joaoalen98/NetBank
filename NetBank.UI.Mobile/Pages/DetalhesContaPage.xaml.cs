using NetBank.Domain.Entidades;
using NetBank.UI.Mobile.Services;

namespace NetBank.UI.Mobile.Pages;

public partial class DetalhesContaPage : ContentPage
{
    ApiService _apiService;

    public Conta Conta;

    public DetalhesContaPage(Conta conta)
    {
        Conta = conta;


        InitializeComponent();
        BindingContext = Conta;

        _apiService = new ApiService();

        datePickerDataInicial.Date = DateTime.Now.AddMonths(-1);
        datePickerDataFinal.Date = DateTime.Now;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        

        listTransacoes.ItemsSource = await GetTransacoes();
    }


    private async Task<IEnumerable<Transacao>> GetTransacoes()
    {
        var dataInicial = datePickerDataInicial.Date;
        var dataFinal = datePickerDataFinal.Date.AddDays(1);

        return await _apiService.GetTransacoesConta(
            Conta.Id, dataInicial, dataFinal);
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