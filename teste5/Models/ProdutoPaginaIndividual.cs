using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teste5.Models
{
    public class ProdutoPaginaIndividual
    {
        public int ID_Produto { get; set; }
        public string Nome_Produto { get; set; }

        public string Descricao { get; set; }
        public string URL { get; set; }
        public string QTDA { get; set; }
        public decimal Valor_Produto { get; set; }
    }
}