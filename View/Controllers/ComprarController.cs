using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ComprarController : Controller
    {
        private ComprarRepository repository;

        public ComprarController()
        {
            repository = new ComprarRepository();
        }

        // GET: Comprar
        public ActionResult Index()
        {
            List<Comprar> compras = repository.ObterTodos();
            ViewBag.Compras = compras;
            return View();
        }

        public ActionResult Cadastro()
        {
            CartaoCreditoRepository repository = new CartaoCreditoRepository();
            List<CartaoCredito> cartoesCreditos = repository.ObterTodos("");
            ViewBag.CartoesCreditos = cartoesCreditos;
            return View();
        }

        public ActionResult Store(int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Comprar comprar = new Comprar();
            comprar.IdCartaoCredito = idCartaoCredito;
            comprar.Valor = valor;
            comprar.DataCompra = dataCompra;
            repository.Inserir(comprar);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Comprar comprar = new Comprar();
            comprar.Id = id;
            comprar.IdCartaoCredito = idCartaoCredito;
            comprar.Valor = valor;
            comprar.DataCompra = dataCompra;
            repository.Atualizar(comprar);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Comprar comprar = repository.ObterPeloId(id);
            ViewBag.Comprar = comprar;

            CartaoCreditoRepository cartaoCreditoRepository = new CartaoCreditoRepository();
            List<CartaoCredito> cartoesCreditos = cartaoCreditoRepository.ObterTodos("");
            ViewBag.CartoesCreditos = cartoesCreditos;

            return View();
        }
    }
}