using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    interface ICategoriaRepository
    {
        int Inserir(Categoria categoria);
        bool Atualizar(Categoria categoria);
        bool Apagar(int id);
        List<Categoria> ObterTodos(string pesquisa);
        Categoria ObterPeloId(int id);
    }
}
