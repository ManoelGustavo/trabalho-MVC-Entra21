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
            List<CartaoCredito> cartoesCredito = repository.ObterTodos("");
            ViewBag.CartoesCreditos = cartoesCredito;
            return View();
        }

        public ActionResult Store(int idCartaoCredito, decimal valor, DateTime data_compra)
        {
            Comprar comprar = new Comprar();
            comprar.IdCartaoCredito = idCartaoCredito;
            comprar.Valor = valor;
            comprar.DataCompra = data_compra;
            repository.Inserir(comprar);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id, int idCartaoCredito, decimal valor, DateTime data_compra)
        {
            Comprar comprar = new Comprar();
            comprar.Id = id;
            comprar.IdCartaoCredito = idCartaoCredito;
            comprar.Valor = valor;
            comprar.DataCompra = data_compra;
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
            List<CartaoCredito> cartoesCredito = cartaoCreditoRepository.ObterTodos("");
            ViewBag.CartoesCredito = cartoesCredito;

            return View();
        }
    }
}