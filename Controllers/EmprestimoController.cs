using Biblioteca.Models;
using Biblioteca.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using X.PagedList;

namespace Biblioteca.Controllers
{
    public class EmprestimoController : Controller
    {
        LivroService livroService = new();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Autenticacao.CheckLogin(this);
        }

        public IActionResult Cadastro()
        {
            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(Emprestimo emprestimo)
        {
            EmprestimoValidator ev = new();
            BibliotecaContext bc = new();
            ValidationResult results = ev.Validate(emprestimo);
            CadEmprestimoViewModel cadModel = new();

            cadModel.Livros = livroService.ListarDisponiveis();

            if (!results.IsValid)
            {
                ViewData["Erro"] = results.Errors.FirstOrDefault().ErrorMessage;
                return View(cadModel);
            }

            if (emprestimo.Id == 0)
            {
                emprestimo.Inserir();
            }
            else
            {
                if(emprestimo.Devolvido == false)
                { 
                    emprestimo.Atualizar();
                }
                else if(emprestimo.Devolvido == true)
                {
                    emprestimo.Devolver();
                }
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int page = 1)
        {
            int pageSize = 10;
            int skip = (page - 1) * pageSize;
            BibliotecaContext bc = new();
            FiltrosEmprestimos objFiltro = null;

            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosEmprestimos();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            ViewBag.TotalPaginas = Convert.ToInt32(Math.Ceiling((decimal)bc.Emprestimos.Count() / pageSize));
            ViewBag.PaginaAtual = page;
            EmprestimoService emprestimoService = new();

            return View(emprestimoService.ListarTodos(skip, objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            LivroService livroService = new LivroService();
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            cadModel.Emprestimo = e;

            return View(cadModel);
        }


        public IActionResult Devolver(int id)
        {
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            e.Devolver();
            return RedirectToAction("Listagem");
        }

        public IActionResult Emprestar(int id)
        {
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            e.Emprestar();
            return RedirectToAction("Listagem");
        }

        public IActionResult Excluir(int id)
        {
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            e.Excluir();
            return RedirectToAction("Listagem");
        }
    }
}