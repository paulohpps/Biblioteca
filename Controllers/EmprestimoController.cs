using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Biblioteca.Controllers
{

    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();
            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();
            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            if (string.IsNullOrEmpty(viewModel.Emprestimo.NomeUsuario)
                || string.IsNullOrEmpty(viewModel.Emprestimo.Telefone)
                || string.IsNullOrEmpty(viewModel.Emprestimo.DataDevolucao.ToString())
                || string.IsNullOrEmpty(viewModel.Emprestimo.DataEmprestimo.ToString()))
            {
                ViewData["Erro"] = "Todos os campos devem ser preenchidos";
                return View(cadModel);
            }
            else if (livroService.ListarDisponiveis().Count == 0)
            {
                ViewData["Erro"] = "Nenhum livro disponivel para empréstimo.";
                return View(cadModel);
            }
            else
            {
                if (viewModel.Emprestimo.DataEmprestimo > viewModel.Emprestimo.DataDevolucao)
                {
                    ViewData["Erro"] = "A data de devolução deve ser maior ou igual a data de emprestimo";
                    return View(cadModel);
                }
                if (viewModel.Emprestimo.Id == 0)
                {
                    emprestimoService.Inserir(viewModel.Emprestimo);
                }
                else
                {
                    emprestimoService.Atualizar(viewModel.Emprestimo);
                }
                return RedirectToAction("Listagem");
            }
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int page = 1)
        {
            Autenticacao.CheckLogin(this);

            FiltrosEmprestimos objFiltro = null;
            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosEmprestimos();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }
            EmprestimoService emprestimoService = new EmprestimoService();
            return View(emprestimoService.ListarTodos(objFiltro).OrderBy(a => a.DataDevolucao).ToPagedList(page, 10));
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarTodos();
            cadModel.Emprestimo = e;

            return View(cadModel);
        }
    }
}