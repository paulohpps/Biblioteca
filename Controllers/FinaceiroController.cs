using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class FinaceiroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LucroVendas()
        {
            BibliotecaContext bc = new BibliotecaContext();
            return View();
        }

        [Route("Finaceiro/Lucro")]
        public IActionResult LucroAlugueis()
        {
            BibliotecaContext bc = new BibliotecaContext();
            decimal valorTotal = bc.Emprestimos.Where(e => e.Devolvido == true).Sum(e => e.Valor);
            return Json(valorTotal);
        }


    }
}
