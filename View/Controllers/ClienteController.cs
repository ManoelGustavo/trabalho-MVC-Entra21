using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteRepository repository;

        public ClienteController()
        {
            repository = new ClienteRepository();
        }

        public ActionResult Index()
        {
            List<Cliente> clientes = repository.ObterTodos("");
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Cadastro()
        {
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.contabilidades = contabilidades;
            return View();
        }

        public ActionResult Store(int idContabilidade, string nome, string cpf)
        {
            Cliente cliente = new Cliente();
            cliente.IdContabilidade = idContabilidade;
            cliente.Nome = nome;
            cliente.Cpf = cpf;
            repository.Inserir(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Cliente cliente = repository.ObterPeloId(id);
            ViewBag.Cliente = cliente;

            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }

        public ActionResult Update(int id, string nome, string cpf, int idContabilidade)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.Cpf = cpf;
            cliente.IdContabilidade = idContabilidade;
            repository.Atualizar(cliente);
            return RedirectToAction("Index");
        }
    }
}