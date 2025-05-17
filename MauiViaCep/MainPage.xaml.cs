
using MauiViaCep.Models;
using MauiViaCep.Services;
using System.Text.Json;

namespace MauiViaCep
{
    public partial class MainPage : ContentPage
    {
        bool _isUpdatingCep = false;
        HttpClient _httpClient = new HttpClient();
        public string UrlBase = "https://viacep.com.br/ws/";
        public string UrlFinal = "/json/";
        public string UrlCompleta = string.Empty;
        public string UrlCompletaErro = string.Empty;
        public string UrlCompletaMensagem = string.Empty;

        ViaCep _viaCep = new ViaCep();
        public List<ViaCep> _listaViaCep = new List<ViaCep>();

        bool Controle = false;


        public MainPage()
        {
            InitializeComponent();
            txtCep.Focus();
        }

       
        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {

          

            xAtividade.IsVisible = true;

            // consumindo a api viacep
            if (string.IsNullOrEmpty(txtCep.Text))
            {
                DisplayAlert("Atenção", "Informe o CEP", "OK");
                return;
            }
            if (txtCep.Text.Length < 9)
            {
                DisplayAlert("Atenção", "CEP inválido", "OK");
                return;
            }
            // tirar o traço do cep no txtCep.Text
            string cep = txtCep.Text.Replace("-", "");

            UrlCompleta = UrlBase + cep + UrlFinal;
            UrlCompletaErro = UrlBase + txtCep.Text + "/erro" + UrlFinal;
            UrlCompletaMensagem = UrlBase + txtCep.Text + "/mensagem" + UrlFinal;

            // troquei por essa linha, todo codigo comentando abaixo

            List<ViaCep> resultado = await ApiClass.GetJsonAsync(UrlCompleta);

            PreencheCampos(resultado[0]);

        }


        private async void PreencheCampos(ViaCep? viaCep)
        {
           // lblCepLogradouro.Text = txtCep.Text;
            lblLogradouro.Text = viaCep.logradouro;
            lblBairro.Text = viaCep.bairro;
            lblCidade.Text = viaCep.localidade;
            lblEstado.Text = viaCep.uf;

            // aguarda 2 segundos
            await Task.Delay(2000);
            // desabilita o activity
            xAtividade.IsVisible = false;

        }

        private void FormataCep(object sender, TextChangedEventArgs e)
        {
            MetodosGerais mg = new MetodosGerais(); // instancia a classe MetodosGerais no objeto mg
            mg.FormataCep(sender, e);   // objeto mg chama o método FormataCep passando o sender e o e
        }

        private void swFiltro_Toggled(object sender, ToggledEventArgs e)
        {
            if(swFiltro.IsToggled)
            {
                // se o switch estiver ligado, frame por endereço aparece
                Controle = false;
                frameEndereco.IsVisible = true;
                frameCep.IsVisible = false;
                lblNomeFiltro.Text = "Filtro por Endereço";
            }
            else
            {
                // se o switch estiver desligado, frame por endereço não aparece
                Controle = true;
                frameCep.IsVisible = true;
                frameEndereco.IsVisible = false;
                lblNomeFiltro.Text = "Filtro por CEP";
            }
        }

        private async void btnBuscarLogradouro_Clicked(object sender, EventArgs e)
        {
            xAtividade.IsVisible = true;

            // consumindo a api viacep
            if (string.IsNullOrEmpty(txtLogradouro.Text))
            {
                await DisplayAlert("Atenção", "Informe o LOGRADOURO", "OK");
                return;
            }
            if (txtLogradouro.Text.Length < 3)
            {
                await DisplayAlert("Atenção", "Logradouro Min. 3 Caracteres", "OK");
                return;
            }

            
            UrlCompleta = UrlBase + "SP/FRANCA/" + txtLogradouro.Text.ToUpper() + "/json";
            List<ViaCep> resultado = await ApiClass.GetJsonAsync(UrlCompleta);
            lstEnds.ItemsSource = resultado;
            xAtividade.IsVisible = false;

        }
    }

}
