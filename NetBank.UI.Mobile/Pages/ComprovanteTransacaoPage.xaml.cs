using NetBank.DTOs;

namespace NetBank.UI.Mobile.Pages;

public partial class ComprovanteTransacaoPage : ContentPage
{
	public ComprovanteTransacaoPage(TransacaoDTO transacao)
	{
		InitializeComponent();
		BindingContext = transacao;
	}
}