using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IUsuarioRepository
    {
        int Inserir(Usuario usuario);
        bool Atualizar(Usuario usuario);
        bool Apagar(int id);
        List<Usuario> ObterTodos();
        Usuario ObterPeloId(int id);
    }
}
