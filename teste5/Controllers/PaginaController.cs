using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste5.Models;

namespace teste5.Controllers
{
    public class PaginaController : Controller
    {
        //MOSTRA OS ITENS QUANDO VOCÊ CLICA E ADICIONA ITENS AO CARRINHO
        public ActionResult Index(string ID_Produto)
        {
            using (Contexto contexto = new Contexto())
            {
                contexto.Conectando();

                List<ProdutoPaginaIndividual> Produtos = contexto.ListarPagina(ID_Produto);

                return View("Index", Produtos);
            }
        }

    }
}