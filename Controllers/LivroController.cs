using Biblioteca.Models;
using Biblioteca.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using FluentValidation.Results;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Autenticacao.CheckLogin(this);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            LivroValidator lv = new();
            ValidationResult results = lv.Validate(l);

            if (!results.IsValid)
            {
                ViewData["Erro"] = results.Errors.FirstOrDefault().ErrorMessage;
                return View();
            }

            if (l.Id == 0)
            {
                l.Inserir();
            }
            else
            {
                l.Atualizar();
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int page = 1)
        {
            int pageSize = 10;
            BibliotecaContext bc = new();
            FiltrosLivros objFiltro = null;
            int skip = (page - 1) * pageSize;

            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            ViewBag.TotalPaginas = Convert.ToInt32(Math.Ceiling((decimal)bc.Livros.Count() / pageSize));
            ViewBag.PaginaAtual = page;
            LivroService ls = new();

            return View(ls.ListarTodos(skip, objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            LivroService ls = new LivroService();
            return View(ls.ObterPorId(id));
        }
    }
}