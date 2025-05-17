using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiViaCep.Models
{

    public class EstadosArray
    {
        //public Class1[] Property1 { get; set; }
        public List<Estados> EstadosLista { get; set; }

    }

    public class Estados
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public RegiaoEst regiao { get; set; }
    }

    public class RegiaoEst
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
    }



    // fim da model
}
