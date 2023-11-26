using BAL.Interfaces;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace Sistema_TechCareSolucion.Controllers
{
    [Authorize(Roles = "administrador")]
    public class ServicioController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IServicioRepository servicioRepository;

        public ServicioController(IUsuarioRepository _usuarioRepository, IServicioRepository _servicioRepository)
        {
            usuarioRepository = _usuarioRepository;
            servicioRepository = _servicioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Servicio/Nuevo", Name = "servicionuevo")]
        public IActionResult NuevoServicio()
        {
            return View();
        }

        [Route("/Servicio/Reparacion", Name = "nuevareparacion")]
        public IActionResult NuevaReparacion()
        {
            return View();
        }

        public async Task<IActionResult> CrearReparacion(NuevaReparacionViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var usuario = await usuarioRepository.CreateClienteIfNotExist(model);

                    var reparacionJson = JsonConvert.SerializeObject(new ReparacionViewModel(model));

                    var servicio = await servicioRepository.AddAsync(new Servicio()
                    {
                        IdCliente = usuario.Id,
                        IdTecnico = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                        EstadoServicio = model.IniciarReparacionYa ? "En progreso" : "En Espera",
                        FechaInicio = model.IniciarReparacionYa ? new DateTime() : null,
                        IdServicioPublic = Guid.NewGuid().ToString(),
                        TipoServicio = "reparación",
                    });

                    var detalleServicio = await servicioRepository.AddAsync(new DetalleServicio()
                    {
                        IdServicio = servicio.Id,
                        ContentService = reparacionJson
                    });

                    if (model.IniciarReparacionYa)
                    {
                        //TODO redirect to ReparacionModule
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                var message = ex;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetServicios(DataTableJS request)
        {

            var data = await servicioRepository.ListServicios(request);

            return Json(new
            {
                draw = request.Draw,
                recordsTotal = data.CountTotal,
                recordsFiltered = data.CountFiltered,
                request_ = request,
                data = data.Result
            }); ;
        }

        [Route("/Servicio/{typeView}/{publicId}", Name = "doservicio")]
        public async Task<IActionResult> Servicio(string typeView, string publicId)
        {
            try
            {
                int id;
                Guid publicGuid;

                if (typeView == "WorkFlow" && int.TryParse(publicId, out id))
                {
                    //Vistatecnica
                    return View(typeView);
                }
                else if (typeView == "Public" && Guid.TryParse(publicId, out publicGuid))
                {
                    return View(typeView);
                }
            }
            catch (Exception ex)
            {
                var message = ex;
            }

            return RedirectToAction("Index");
        }
    }
}
