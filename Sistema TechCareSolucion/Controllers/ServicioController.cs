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
        private readonly IComentarioRepository comentarioRepository;

        public ServicioController(IUsuarioRepository _usuarioRepository, IServicioRepository _servicioRepository, IComentarioRepository _comentarioRepository)
        {
            usuarioRepository = _usuarioRepository;
            servicioRepository = _servicioRepository;
            comentarioRepository = _comentarioRepository;
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
                    var model = new WorkFlowServicioViewModel();
                    var servicio = await servicioRepository.GetByIdWithIncludeAsync(id);

                    if (servicio != null )
                    {
                        servicio.FechaInicio = servicio.EstadoServicio == "En Espera" ? DateTime.Now : servicio.FechaInicio;
                        servicio.EstadoServicio = servicio.EstadoServicio == "En Espera" ? "En Progreso" : servicio.EstadoServicio;

                        model.Servicio = servicio;
                        model.Reparacion = JsonConvert.DeserializeObject<ReparacionViewModel>(servicio.DetalleServicio.ContentService);
                    }

                    return View(typeView, model);
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

        public async Task<IActionResult> GetComentarios(int idServicio)
        {
            var comentarios = await comentarioRepository.GetComentariosByServicioId(idServicio);
            return Json(comentarios);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarComentario(ComentarioViewModel model)
        {
            try
            {
                await comentarioRepository.AddAsync(new ComentarioServicio
                {
                    Comentario = model.Comentario,
                    FechaComentario = model.Fecha,
                    IdServicio = model.Idservicio
                });
            }
            catch (Exception ex)
            {
                var message = ex;
            }
            
            return Ok("Comentario agregado con éxito");
        }
    }
}
