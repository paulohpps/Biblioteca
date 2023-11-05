using Biblioteca.Cryptography;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Autenticacao.CheckLogin(this);
        }

        [Route("/Usuario/Cadastro")]
        public IActionResult Cadastro()
        {
            if (HttpContext.Request.Cookies["user"] == "admin")
            {
                return View();
            }

            HttpContext.Response.Redirect("/Home");
            return View();
        }

        public IActionResult Excluir(int id)
        {
            if (HttpContext.Session.GetString("user") == "admin")
            {
                UsuarioService usuarioService = new UsuarioService();
                Usuario user = usuarioService.ObterPorId(id);
                return View(user);
            }
            HttpContext.Response.Redirect("/Home");

            return View();
        }

        [HttpPost]
        public IActionResult Excluir(Usuario user)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(user);
                bc.SaveChanges();
            }
            return RedirectToAction("Listagem");
        }

        [HttpPost]
        public IActionResult CadastroUser(Usuario user)
        {
            UsuarioValidator validator = new();
            ValidationResult results = validator.Validate(user);
            UsuarioService usuarioService = new();

            if (!results.IsValid)
            {
                ViewData["Erro"] = results.Errors.First();
                return View();
            }

            if (usuarioService.ListarTodos().Any(u => u.Login == user.Login))
            {
                ViewData["Erro"] = "Já existe um usuário cadastrado com esse login.";
                return View();
            }

            user.Senha = MD5Service.RetornarMD5(user.Senha);
            if (user.Id == 0)
            {
                user.Inserir();
            }
            else
            {
                user.Atualizar();
            }
            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int page = 1)
        {
            int pageSize = 10;
            BibliotecaContext bc = new BibliotecaContext();
            int skip = (page - 1) * pageSize;

            if (HttpContext.Request.Cookies["user"] == "admin")
            {
                FiltrosUsuarios objFiltro = null;
                if (!string.IsNullOrEmpty(filtro))
                {
                    objFiltro = new FiltrosUsuarios();
                    objFiltro.Filtro = filtro;
                    objFiltro.TipoFiltro = tipoFiltro;
                }
                ViewBag.TotalPaginas = Convert.ToInt32(Math.Ceiling((decimal)bc.Usuarios.Count() / pageSize));
                ViewBag.PaginaAtual = page;
                UsuarioService usuarioService = new UsuarioService();

                return View(usuarioService.ListarTodos(skip, objFiltro));
            }
            else
            {
                HttpContext.Response.Redirect("/Home");
            }

            return NotFound();
        }

        public IActionResult Edicao(int id)
        {
            if (HttpContext.Session.GetString("user") == "admin")
            {
                UsuarioService usuarioService = new UsuarioService();
                Usuario user = usuarioService.ObterPorId(id);
                return View(user);
            }
            else
            {
                HttpContext.Response.Redirect("/Home");
            }
            return View();
        }
    }
}
