using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste5.Models;

namespace teste5.Controllers
{
    public class AddController : Controller
    {
        //CADASTRO DE PRODUTOS NOVOS
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(CardProdutos batata)
        {
            Contexto abrindo = new Contexto();
            abrindo.Conectando();

            abrindo.Cadastrando_Produto(batata.Nome_Produto, batata.Descricao,batata.URL, batata.QTDA, batata.Valor_Produto);

            return View();
        }
    }
}