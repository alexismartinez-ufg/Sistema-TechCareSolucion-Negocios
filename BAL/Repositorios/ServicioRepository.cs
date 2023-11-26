using BAL.Interfaces;
using DAL;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
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
