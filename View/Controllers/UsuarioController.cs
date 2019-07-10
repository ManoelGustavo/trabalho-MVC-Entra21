using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository;

        public UsuarioController()
        {
            repository = new UsuarioRepository();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            List<Usuario> usuarios = repository.ObterTodos("");
            ViewBag.Usuarios = usuarios;
            return View();
        }

        public ActionResult Cadastro()
        {
            ContabilidadeRepository repository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = repository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }

        public ActionResult Store(int idContabilidade, string login, string senha, DateTime dataNascimento)
        {
            Usuario usuario = new Usuario();
            usuario.IdContabilidade = idContabilidade;
            usuario.Login = login;
            usuario.Senha = senha;
            usuario.DataNascimento = dataNascimento;
            repository.Inserir(usuario);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, int idContabilidade, string login, string senha, DateTime dataNascimento)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            usuario.IdContabilidade = idContabilidade;
            usuario.Login = login;
            usuario.Senha = senha;
            usuario.DataNascimento = dataNascimento;
            repository.Atualizar(usuario);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Usuario usuario = repository.ObterPeloId(id);
            ViewBag.Usuario = usuario;
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }
    }
}