using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IComprarRepository
    {
        int Inserir(Comprar comprar);
        bool Atualizar(Comprar comprar);
        bool Apagar(int id);
        List<Comprar> ObterTodos();
        Comprar ObterPeloId(int id);
    }
}
