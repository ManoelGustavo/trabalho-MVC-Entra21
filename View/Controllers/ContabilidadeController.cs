using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContabilidadeController : Controller
    {
        private ContabilidadeRepository repository;
        
        public ContabilidadeController()
        {
            repository = new ContabilidadeRepository();
        }

        public ActionResult Index()
        {
            List<Contabilidade> contabilidades = repository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Nome = nome;
            repository.Inserir(contabilidade);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Contabilidade contabilidade = repository.ObterPeloId(id);
            ViewBag.Contabilidade = contabilidade;
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Id = id;
            contabilidade.Nome = nome;
            repository.Atualizar(contabilidade);
            return RedirectToAction("Index");
        }

    }
}