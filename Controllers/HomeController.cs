using Biblioteca.Cryptography;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                senha = MD5Service.RetornarMD5(senha);
                Console.WriteLine(senha);
                var user = bc.Usuarios.Where(e => e.Login == login).Where(e => e.Senha == senha);
                if (user.Count() > 0)
                {
                    HttpContext.Session.SetString("user", user.First().Nome);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Erro"] = "Usuário ou Senha inválido";
                    return View();
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
