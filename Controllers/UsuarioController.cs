using Biblioteca.Cryptography;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            if (HttpContext.Session.GetString("user") == "admin")
            {
                return View();
            }
            else
            {
                HttpContext.Response.Redirect("/Home");
            }
            return View();
        }
        public IActionResult Excluir(int id)
        {
            Autenticacao.CheckLogin(this);
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
        public IActionResult Cadastro(Usuario user)
        {
            UsuarioService usuarioService = new UsuarioService();
            if (string.IsNullOrEmpty(user.Nome) || string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Senha))
            {
                ViewData["Erro"] = "Todos os campos devem ser preenchidos.";
                return View();
            }
            else if (usuarioService.ListarTodos().Count(e => e.Login == user.Login) > 0 && user.Id == 0)
            {
                ViewData["Erro"] = "Já existe um usuário cadastrado com esse login.";
                return View();
            }
            else
            {

                user.Senha = MD5Service.RetornarMD5(user.Senha);
                if (user.Id == 0)
                {
                    usuarioService.Inserir(user);
                }
                else
                {
                    usuarioService.Atualizar(user);
                }
                return RedirectToAction("Listagem");
            }
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int page = 1)
        {
            Autenticacao.CheckLogin(this);
            UsuarioService usuarioService = new UsuarioService();
            if (HttpContext.Session.GetString("user") == "admin")
            {
                FiltrosUsuarios objFiltro = null;
                if (!string.IsNullOrEmpty(filtro))
                {
                    objFiltro = new FiltrosUsuarios();
                    objFiltro.Filtro = filtro;
                    objFiltro.TipoFiltro = tipoFiltro;
                }

                return View(usuarioService.ListarTodos(objFiltro).OrderBy(a => a.Nome).ToPagedList(page, 10));
            }
            else
            {
                HttpContext.Response.Redirect("/Home");
            }
            return View(usuarioService.ListarTodos());
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);

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
