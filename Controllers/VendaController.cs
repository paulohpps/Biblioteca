using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class VendaController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Autenticacao.CheckLogin(this);
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int page = 1)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            BibliotecaContext bc = new();
            FiltrosVendas objFiltros = null;
            VendaService vs = new();

            if(!string.IsNullOrEmpty(filtro))
            {       
                objFiltros = new();
                objFiltros.Filtro = filtro;
                objFiltros.TipoFiltro = tipoFiltro;
            }

            ViewBag.TotalPaginas = Convert.ToInt32(Math.Ceiling((decimal)bc.Vendas.Count() / pageSize));
            ViewBag.PaginaAtual = page;

            return View(vs.ListarTodos(skip, objFiltros));
        }

        public IActionResult Reembolsar(int id)
        {
            BibliotecaContext bc = new();
            Venda venda = bc.Vendas.Find(id);
            venda.Rembolsar();

            return RedirectToAction("Listagem");
        }
    }
}
