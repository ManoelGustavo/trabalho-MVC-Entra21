using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ContaPagarRepository : IContaPagarRepository
    {
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Atualizar(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_pagar SET id_cliente = @ID_CLIENTE, id_categoria = @ID_CATEGORIA, nome = @NOME, data_vencimento = @DATA_VENCIMENTO, data_pagamento = @DATA_PAGAMENTO, valor = @VALOR WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", contaPagar.Id);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO contas_pagar(id_cliente, id_categoria, nome, data_vencimento, data_pagamento, valor) OUTPUT INSERTED.ID VALUES(@ID_CLIENTE, @ID_CATEGORIA, @NOME, @DATA_VENCIMENTO, @DATA_PAGAMENTO, @VALOR)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public ContaPagar ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = Convert.ToInt32(linha["id"]);
            contaPagar.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            contaPagar.IdCategoria = Convert.ToInt32(linha["id_categoria"]);
            contaPagar.Nome = linha["nome"].ToString();
            contaPagar.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
            contaPagar.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
            contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
            return contaPagar;
        }

        public List<ContaPagar> ObterTodos(string pesquisa)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT clientes.id AS 'ClienteId', clientes.nome AS 'ClienteNome', clientes.cpf AS 'ClienteCpf', categorias.id AS 'CategoriaId', " +
                "categorias.nome AS 'CategoriaNome', contas_pagar.id AS 'Id', contas_pagar.nome AS 'Nome', contas_pagar.data_vencimento AS 'DataVencimento', contas_pagar.data_pagamento AS 'DataPagamento', " +
                "contas_pagar.valor AS 'Valor' FROM contas_pagar INNER JOIN clientes ON(contas_pagar.id_cliente = clientes.id) INNER JOIN categorias ON(contas_pagar.id_categoria = categorias.id)";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaPagar> contasPagar = new List<ContaPagar>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaPagar contaPagar = new ContaPagar();
                contaPagar.Id = Convert.ToInt32(linha["id"]);
                contaPagar.Nome = linha["nome"].ToString();
                contaPagar.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
                contaPagar.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
                contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
                contaPagar.IdCliente = Convert.ToInt32(linha["ClienteId"]);
                contaPagar.Cliente = new Cliente();
                contaPagar.Cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                contaPagar.Cliente.Nome = linha["ClienteNome"].ToString();
                contaPagar.Cliente.Cpf = linha["ClienteCpf"].ToString();
                contaPagar.IdCategoria = Convert.ToInt32(linha["CategoriaId"]);
                contaPagar.Categoria = new Categoria();
                contaPagar.Categoria.Id = Convert.ToInt32(linha["CategoriaId"]);
                contaPagar.Categoria.Nome = linha["CategoriaNome"].ToString();
                contasPagar.Add(contaPagar);
            }
            return contasPagar;
        }
    }
}
