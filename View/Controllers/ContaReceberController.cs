using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaReceberController : Controller
    {
        private ContaReceberRepository repository;

        public ContaReceberController()
        {
            repository = new ContaReceberRepository();
        }

        public ActionResult Index()
        {
            List<ContaReceber> contasReceber = repository.ObterTodos("");
            ViewBag.ContasReceber = contasReceber;
            return View();
        }

        public ActionResult Cadastro()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos("");
            ViewBag.Clientes = clientes;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos("");
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Store(int idCliente, int idCategoria, string nome, DateTime dataPagamento, decimal valor)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Nome = nome;
            contaReceber.DataPagamento = dataPagamento;
            contaReceber.Valor = valor;
            contaReceber.IdCliente = idCliente;
            contaReceber.IdCategoria = idCategoria;
            repository.Inserir(contaReceber);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult  Editar(int id)
        {
            ContaReceber contaReceber = repository.ObterPeloId(id);
            ViewBag.ContaReceber = contaReceber;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos("");
            ViewBag.Clientes = clientes;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos("");
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Update(int id, int idCliente, int idCategoria, string nome, DateTime dataPagamento, decimal valor)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = id;
            contaReceber.Nome = nome;
            contaReceber.DataPagamento = dataPagamento;
            contaReceber.Valor = valor;
            contaReceber.IdCliente = idCliente;
            contaReceber.IdCategoria = idCategoria;
            repository.Atualizar(contaReceber);
            return RedirectToAction("Index");
        }

    }
}