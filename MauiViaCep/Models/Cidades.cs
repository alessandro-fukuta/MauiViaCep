using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiViaCep.Models
{

    public class CidadesArray
    {
        //public Class1[] Property1 { get; set; }
        public List<Cidades> CidadesLista { get; set; }
    }

    public class Cidades
    {
        public int id { get; set; }
        public string nome { get; set; }
        public Microrregiao microrregiao { get; set; }
        public RegiaoImediata regiaoimediata { get; set; }
    }

    public class Microrregiao
    {
        public int id { get; set; }
        public string nome { get; set; }
        public Mesorregiao mesorregiao { get; set; }
    }

    public class Mesorregiao
    {
        public int id { get; set; }
        public string nome { get; set; }
        public UF UF { get; set; }
    }

    public class UF
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public Regiao regiao { get; set; }
    }

    public class Regiao
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
    }

    public class RegiaoImediata
    {
        public int id { get; set; }
        public string nome { get; set; }
        public RegiaoIntermediaria regiaointermediaria { get; set; }
    }

    public class RegiaoIntermediaria
    {
        public int id { get; set; }
        public string nome { get; set; }
        public UF1 UF { get; set; }
    }

    public class UF1
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public Regiao1 regiao { get; set; }
    }

    public class Regiao1
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
    }


    // fim da model
}
