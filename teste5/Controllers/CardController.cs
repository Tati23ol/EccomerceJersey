using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste5.Models;

namespace teste5.Controllers
{
    public class CardController : Controller
    {
        //VER A PAGINA, ADICIONAR PRODUTOS NOVOS
        public ActionResult Index(String ID_Produto,string QTDA)
        {
            using (Contexto contexto = new Contexto())
            {
                contexto.Conectando();


                List<CardProdutos> Produtos = contexto.LerCarinho(ID_Produto);
                contexto.AddDicionando(ID_Produto);

                return View(Produtos);
            }
        }


        //TIRA O PRODUTO DO CARRINHO
        public void RemoveCard(String ID_Produto)
        {
            Contexto contexto = new Contexto();
            contexto.Conectando();
            contexto.RemovendoCard(ID_Produto);
        }


        //ALTERA A QUANTIDADE DE PRODUTOS
        public void AlterandoQuatidade(string ID_Produto, string QTDA)
        {
            Contexto contexto = new Contexto();
            contexto.Conectando();
            contexto.AlterandoQuantidade(ID_Produto, QTDA);
        }

    }
}