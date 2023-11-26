using BAL.Interfaces;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Sistema_TechCareSolucion.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Sistema_TechCareSolucion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepository usuarioRepository;

        public HomeController(ILogger<HomeController> logger, IUsuarioRepository _usuarioRepository)
        {
            _logger = logger;
            usuarioRepository = _usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await usuarioRepository.CreateUserIfNotExist(new DAL.Models.Usuario
                {
                    Nombre = "Alexis Martínez",
                    UserName = "Alexis",
                    Password = "qwerty",
                    Email = "alexislelele@gmail.com",
                    Role = "administrador"
                });
            }
            catch (Exception ex)
            {
                var message = ex;
            }

            return View();
        }

        public IActionResult Producto()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await usuarioRepository.CanDoLogin(model);
            
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) 
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                return RedirectToAction("Index", "Servicio");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}