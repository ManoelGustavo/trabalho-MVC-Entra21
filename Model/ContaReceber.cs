using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class ContaReceber
    {
        public int Id;
        public string Nome;
        public DateTime DataPagamento;
        public decimal Valor;
        public int IdCliente;
        public int IdCategoria;
        public Categoria Categoria;
        public Cliente Cliente;
    }
}
