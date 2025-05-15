using MauiViaCep.Models;
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


        public MainPage()
        {
            InitializeComponent();
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
            //UrlCompleta = UrlBase + txtCep.Text + UrlFinal;

            // metodo assíncrono
            
            try
            {

                var response = await _httpClient.GetAsync(UrlCompleta);
                if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        _viaCep = JsonSerializer.Deserialize<ViaCep>(json);
                        // preenche os campos
                        PreencheCampos(_viaCep);
                    }
                   
                }
                catch (Exception ex)
                {
                   await DisplayAlert("Atenção", "Erro ao buscar o CEP: " + ex.Message, "OK");
                }
       




        }

      
        private async void PreencheCampos(ViaCep? viaCep)
        {
            lblCepLogradouro.Text = txtCep.Text;
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
            if (_isUpdatingCep)
                return;

            _isUpdatingCep = true;

            var entry = (Entry)sender;

            // Salva posição do cursor antes da mudança
            int oldCursorPosition = entry.CursorPosition;

            // Remove tudo que não for número
            string digits = new string(e.NewTextValue?.Where(char.IsDigit).ToArray());

            // Aplica a máscara 00000-000
            if (digits.Length > 5)
                digits = digits.Insert(5, "-");

            if (digits.Length > 9)
                digits = digits.Substring(0, 9);

            // Atualiza texto somente se for diferente
            if (entry.Text != digits)
                entry.Text = digits;

            // Reposiciona cursor no fim ou no local adequado
            entry.CursorPosition = digits.Length;

            _isUpdatingCep = false;
        }
    }

}
