using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_TechCareSolucion.Controllers
{
    public class ServicioController : Controller
    {
        [Authorize(Roles = "administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Servicio/Nuevo", Name = "servicionuevo")]
        public IActionResult NuevoServicio()
        {
            return View();
        }
    }
}
