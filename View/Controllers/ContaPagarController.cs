using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {
        private ContaPagarRepository repository;

        public ContaPagarController()
        {
            repository = new ContaPagarRepository();
        }

        // GET: ContaPagar
        public ActionResult Index()
        {
            List<ContaPagar> contasPagar = repository.ObterTodos("");
            ViewBag.ContasPagar = contasPagar;
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

        public ActionResult Store(int idCliente, int idCategoria, string nome, DateTime data_vencimento, DateTime data_pagamento, decimal valor)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.IdCliente = idCliente;
            contaPagar.IdCategoria = idCategoria;
            contaPagar.Nome = nome;
            contaPagar.DataVencimento = data_vencimento;
            contaPagar.DataPagamento = data_pagamento;
            contaPagar.Valor = valor;
            repository.Inserir(contaPagar);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, int idCliente, int idCategoria, string nome, DateTime data_vencimento, DateTime data_pagamento, decimal valor)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = id;
            contaPagar.IdCliente = idCliente;
            contaPagar.IdCategoria = idCategoria;
            contaPagar.Nome = nome;
            contaPagar.DataVencimento = data_vencimento;
            contaPagar.DataPagamento = data_pagamento;
            contaPagar.Valor = valor;
            repository.Atualizar(contaPagar);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContaPagar contaPagar = repository.ObterPeloId(id);
            ViewBag.ContaPagar = contaPagar;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos("");
            ViewBag.Clientes = clientes;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos("");
            ViewBag.Categorias = categorias;

            return View();
        }
    }
}