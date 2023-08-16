using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace teste5.Models
{
    public class Contexto : IDisposable
    {
        private SqlConnection minhaConexao;

        public SqlConnection Conectando()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = @"Data Source = DESKTOP-395J393;Integrated Security=SSPI;Initial Catalog=Bd_JerseyBall";

            conexao.Open();

            minhaConexao = conexao;

            return minhaConexao; 
        }

        internal List<ProdutosPagina> LerDados()
        {
            throw new NotImplementedException();
        }

        //=============================================================================================================================


        public void ExecutarComando(string strQuery)
        {
            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };
            cmdComando.ExecuteNonQuery();
        }


        //==============================================================================================================================
     
        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new SqlCommand(strQuery, minhaConexao);
            return cmdComando.ExecuteReader();
        }

        //==============================================================================================================================
        public List<ProdutosPagina> LerDados(SqlDataReader reader)
        {
            var produtos = new List<ProdutosPagina>();

            while (reader.Read())
            {
                var objeto = new ProdutosPagina()
                {
                    ID_Produto = int.Parse(reader["ID_Produto"].ToString()),
                    Nome_Produto = reader["Nome_Produto"].ToString(),
                    Descricao = reader["Descricao"].ToString(),
                    URL = reader["URL"].ToString(),
                    QTDA = reader["QTDA"].ToString(),
                    Valor_Produto = Convert.ToDecimal(reader["Valor_Produto"])
                };

                produtos.Add(objeto);
            }

            return produtos;
        }

        //========================================================================================================================================

        public List<ProdutoPaginaIndividual> ListarPagina(string ID_Produto)
        {
            List<ProdutoPaginaIndividual> listaProdutos = new List<ProdutoPaginaIndividual>();

            var strQuery = $"SELECT * FROM Tbl_Produtos WHERE ID_Produto = '{ID_Produto}'";

            using (SqlCommand command = new SqlCommand(strQuery, minhaConexao))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProdutoPaginaIndividual produto = new ProdutoPaginaIndividual();
                        {
                            produto.ID_Produto = Convert.ToInt32(reader["ID_Produto"]);
                            produto.Nome_Produto = reader["Nome_Produto"].ToString();
                            produto.Descricao = reader["Descricao"].ToString();
                            produto.URL = reader["URL"].ToString();
                            produto.QTDA = reader["QTDA"].ToString();
                            produto.Valor_Produto = Decimal.Parse(reader["Valor_Produto"].ToString());
                        };

                        listaProdutos.Add(produto);
                    }
                }
            }

            return listaProdutos;
        }


        //========================================================================================================================================
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (minhaConexao != null)
                {
                    minhaConexao.Close();
                    minhaConexao.Dispose();
                    minhaConexao = null;
                }
            }
        }

        public void Cadastrando_Produto(string Nome_Produto, string Descricao, string URL, string QTDA, string Valor_produto)
        {
            string strQuery = $"INSERT INTO Tbl_Produtos " +
                              $"VALUES ('{Nome_Produto}', '{Descricao}', '{URL}','{QTDA}', '{Valor_produto}')";

            using (SqlCommand command = new SqlCommand(strQuery, minhaConexao))
            {
                command.ExecuteNonQuery();
            }
        }

        //=================================================================================================================
        public void AlterandoQuantidade(string ID_Produto, string QTDA)
        {
            string strQuery = $"UPDATE c SET c.QTDA = '{QTDA}' FROM Tbl_Carrinho_Cliente AS c JOIN Tbl_Produtos AS p ON c.FK_ID_Produto = p.ID_Produto WHERE p.ID_Produto = '{ID_Produto}' ";

            using (SqlCommand command = new SqlCommand(strQuery, minhaConexao))
            {
                command.ExecuteNonQuery();
            }

        }
        //==================================================================================================================
        public void AddDicionando(string ID_Produto)
        {
            if (string.IsNullOrEmpty(ID_Produto))
            {
                return;
            }

            int id = int.Parse(ID_Produto);
            string strQuery = $"INSERT INTO Tbl_Carrinho_Cliente VALUES ({id}, 3, 1,500,00)";

            using (SqlCommand command = new SqlCommand(strQuery, minhaConexao))
            {
                command.ExecuteNonQuery();
            }
        }

        //=========================================================================================================================
        public void RemovendoCard(string ID_Produto)
        {
            if (string.IsNullOrEmpty(ID_Produto))
            {
                return;
            }

            int id = int.Parse(ID_Produto);

            string strQuery = $"DELETE FROM Tbl_Carrinho_Cliente WHERE FK_ID_Produto = '{id}'";

            using (SqlCommand command = new SqlCommand(strQuery, minhaConexao))
            {
                command.ExecuteNonQuery();
            }
        }

        //==============================================================================================================================



        public List<CardProdutos> LerCarinho(string ID_Produto)
        {
            List<CardProdutos> listaProdutos = new List<CardProdutos>();


            var strQuery = $"SELECT c.FK_ID_Produto, p.ID_Produto, p.Nome_Produto, p.Descricao, p.URL, p.Valor_Produto FROM Tbl_Carrinho_Cliente c JOIN Tbl_Produtos p ON c.FK_ID_Produto = p.ID_Produto";

            using (SqlCommand command = new SqlCommand(strQuery, minhaConexao))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CardProdutos produtoss = new CardProdutos();
                        {
                            produtoss.FK_ID_Produto = Convert.ToInt32(reader["FK_ID_Produto"]);
                            produtoss.ID_Produto = Convert.ToInt32(reader["ID_Produto"]);
                            produtoss.Nome_Produto = reader["Nome_Produto"].ToString();
                            produtoss.Descricao = reader["Descricao"].ToString();
                            produtoss.URL = reader["URL"].ToString();
                            produtoss.Valor_Produto = Decimal.Parse(reader["Valor_Produto"].ToString());
                        };
                        listaProdutos.Add(produtoss);
                    };

                }
            }
            return listaProdutos;
        }

    }
}
