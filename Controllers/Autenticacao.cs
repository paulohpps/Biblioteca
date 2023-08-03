using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {
            if (string.IsNullOrEmpty(controller.HttpContext.Request.Cookies["user"]))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
    }
}