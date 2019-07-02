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
    public class UsuarioRepository : IUsuarioRepository
    {
        public int Inserir(Usuario usuario)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO usuarios(id_contabilidade, login, senha, data_nascimento) OUTPUT INSERTED.ID VALUES(@ID_CONTABILIDADE, @LOGIN, @SENHA, @DATA_NASCIMENTO)";
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", usuario.IdContabilidade);
            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Usuario ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt32(linha["id"]);
            usuario.IdContabilidade = Convert.ToInt32(linha["id_contabilidade"]);
            usuario.Login = linha["login"].ToString();
            usuario.Senha = linha["senha"].ToString();
            usuario.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
            return usuario;
        }

        public List<Usuario> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT contabilidades.id AS 'ContabilidadeId', contabilidades.nome AS 'ContabilidadeNome', usuarios.id AS 'UsuarioId', usuarios.login AS 'UsuarioLogin', " +
                "usuarios.senha AS 'UsuarioSenha', usuarios.data_nascimento AS 'UsuarioDataNascimento' FROM usuarios INNER JOIN contabilidades ON(usuarios.id_contabilidade = contabilidades.id)";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Usuario> usuarios = new List<Usuario>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Usuario usuario = new Usuario();
                usuario.Id = Convert.ToInt32(linha["id"]);
                usuario.Login = linha["login"].ToString();
                usuario.Senha = linha["senha"].ToString();
                usuario.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
                usuario.IdContabilidade = Convert.ToInt32(linha["ContabilidadeId"]);
                usuario.Contabilidade = new Contabilidade();
                usuario.Contabilidade.Id = Convert.ToInt32(linha["ContabilidadeId"]);
                usuario.Contabilidade.Nome = linha["ContabilidadeNome"].ToString();
                usuarios.Add(usuario);
            }
            return usuarios;
        }
    }
}
