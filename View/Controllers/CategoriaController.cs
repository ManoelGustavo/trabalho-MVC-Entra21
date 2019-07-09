using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepository repository;

        public CategoriaController()
        {
            repository = new CategoriaRepository();
        }
        
        public ActionResult Index()
        {
            List<Categoria> categorias = repository.ObterTodos("");
            ViewBag.Categorias = categorias;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            Categoria categoria = new Categoria();
            categoria.Nome = nome;
            repository.Inserir(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Categoria categoria = repository.ObterPeloId(id);
            ViewBag.Categoria = categoria;
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            Categoria categoria = new Categoria();
            categoria.Id = id;
            categoria.Nome = nome;
            repository.Atualizar(categoria);
            return RedirectToAction("Index");
        }
    }
}