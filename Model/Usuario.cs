using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usuario
    {
        public int Id;
        public int IdContabilidade;
        public Contabilidade Contabilidade;
        public string Login;
        public string Senha;
        public DateTime DataNascimento;
    }
}
