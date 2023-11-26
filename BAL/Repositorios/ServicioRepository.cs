using BAL.Interfaces;
using DAL;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositorios
{
    public class ServicioRepository : BaseRepository<Servicio>, IServicioRepository
    {
        protected readonly EmprendimientosContext dbContext;
        public ServicioRepository(EmprendimientosContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<bool> AgregarRepuestoPorServicio(int idServicio, Repuestos repuesto)
        {
            try
            {
                var detalle = await dbContext.DetalleServicios.FirstOrDefaultAsync(x => x.IdServicio == idServicio);

                var reparacion = JsonConvert.DeserializeObject<ReparacionViewModel>(detalle.ContentService);

                if (reparacion.HistorialRepuestos == null)
                    reparacion.HistorialRepuestos = new List<Repuestos>();

                reparacion.HistorialRepuestos.Add(repuesto);

                detalle.ContentService = JsonConvert.SerializeObject(reparacion);

                dbContext.Entry(detalle).State = EntityState.Modified;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<Servicio> GetByIdWithIncludeAsync(int id)
        {
            return await dbContext.Servicios.Include(x => x.Cliente).Include(x => x.Tecnico).Include(x => x.DetalleServicio).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Servicio> GetByPublicWithIncludeAsync(string publicId)
        {
            return await dbContext.Servicios.Include(x => x.Cliente).Include(x => x.Tecnico).Include(x => x.DetalleServicio).FirstOrDefaultAsync(x => x.IdServicioPublic == publicId);
        }

        public async Task<DataTableResponse<Repuestos>> ListRepuestosPorId(DataTableJS model, int idServicio)
        {
            var detalle = await dbContext.DetalleServicios.FirstOrDefaultAsync(x => x.IdServicio == idServicio);

            var reparacion = JsonConvert.DeserializeObject<ReparacionViewModel>(detalle.ContentService);

            if (reparacion.HistorialRepuestos == null)
                reparacion.HistorialRepuestos = new List<Repuestos>();

            var result = reparacion.HistorialRepuestos.ToList();

            return new DataTableResponse<Repuestos> { CountFiltered = result.Count, CountTotal = reparacion.HistorialRepuestos.Count, Result = result };
        }

        public async Task<DataTableResponse<ServiciosViewModel>> ListServicios(DataTableJS model)
        {
            var query = dbContext.Servicios.Include(x => x.DetalleServicio).Include(x=>x.Cliente).Include(x=>x.Tecnico).AsQueryable();

            if (!string.IsNullOrEmpty(model.Search.Value))
            {
                var txt = model.Search.Value;

                query = query.Where(x => x.Tecnico.Nombre.ToLower().Contains(txt.ToLower()) 
                                    || x.Cliente.Nombre.ToLower().Contains(txt.ToLower()) 
                                    || x.EstadoServicio.ToLower().Contains(txt.ToLower())
                                    || x.TipoServicio.ToLower().Contains(txt.ToLower())
                                    || x.Id.ToString().Contains(txt));
            }

            var count = query.Count();

            var data = query.Select(x => new ServiciosViewModel
            {
                Id = x.Id,
                Cliente = x.Cliente.Nombre ?? "",
                Estado = x.EstadoServicio,
                Fin = x.FechaFin ?? DateTime.MinValue,
                Inicio = x.FechaInicio ?? DateTime.MinValue,
                Tecnico = x.Tecnico.Nombre ?? "",
                Tipo = x.TipoServicio,
                Total = x.TotalServicio ?? 0,
            })
            .Skip(model.Start)
            .Take(model.Length)
            .ToList();

            return new DataTableResponse<ServiciosViewModel> { CountFiltered = data.Count, CountTotal = count, Result = data };
        }
    }
}
