using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IServicioRepository : IRepository<Servicio>
    {
        Task<Servicio> GetByIdWithIncludeAsync(int id);
        Task<Servicio> GetByPublicWithIncludeAsync(string id);
        Task<DataTableResponse<ServiciosViewModel>> ListServicios(DataTableJS model);
        Task<DataTableResponse<Repuestos>> ListRepuestosPorId(DataTableJS model, int idservicio);

        Task<bool> AgregarRepuestoPorServicio(int idServicio, Repuestos repuesto);
    }
}
