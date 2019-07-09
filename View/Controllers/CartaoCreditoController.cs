using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CartaoCreditoController : Controller
    {
        private CartaoCreditoRepository repository;

        public CartaoCreditoController()
        {
            repository = new CartaoCreditoRepository();
        }

        public ActionResult Index()
        {
            List<CartaoCredito> cartoesCredito = repository.ObterTodos("");
            ViewBag.CartoesCredito = cartoesCredito;
            return View();
        }

        public ActionResult Cadastro()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos("");
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Store(int idCliente, string numero, DateTime dataVencimento, string cvv)
        {
            CartaoCredito cartaoCredito = new CartaoCredito();
            cartaoCredito.Numero = numero;
            cartaoCredito.DataVencimento = dataVencimento;
            cartaoCredito.Cvv = cvv;
            cartaoCredito.IdCliente = idCliente;
            repository.Inserir(cartaoCredito);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            CartaoCredito cartaoCredito = repository.ObterPeloId(id);
            ViewBag.CartaoCredito = cartaoCredito;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos("");
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Update(int id, string numero, DateTime dataVencimento, string cvv, int idCliente)
        {
            CartaoCredito cartaoCredito = new CartaoCredito();
            cartaoCredito.Id = id;
            cartaoCredito.Numero = numero;
            cartaoCredito.DataVencimento = dataVencimento;
            cartaoCredito.Cvv = cvv;
            cartaoCredito.IdCliente = idCliente;
            repository.Atualizar(cartaoCredito);
            return RedirectToAction("Index");
        }
    }
}