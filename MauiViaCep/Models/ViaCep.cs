using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiViaCep.Models
{
    public class ViaCep
    {
        // crie a classe baseada na documentação do ViaCep
        // https://viacep.com.br/

        
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string complemento { get; set; }
            public string unidade { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
            public string estado { get; set; }
            public string regiao { get; set; }
            public string ibge { get; set; }
            public string gia { get; set; }
            public string ddd { get; set; }
            public string siafi { get; set; }
        

    }
}
