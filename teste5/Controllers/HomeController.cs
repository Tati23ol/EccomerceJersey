using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste5.Models;

namespace teste5.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Contexto abrindo = new Contexto();
            abrindo.Conectando();
            List<ProdutosPagina> Produtos = abrindo.LerDados();

            return View("Index", Produtos);
        }
    }
}