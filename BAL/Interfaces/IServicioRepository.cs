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
        Task<DataTableResponse<ServiciosViewModel>> ListServicios(DataTableJS model);
    }
}
