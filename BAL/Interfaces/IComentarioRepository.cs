using BAL.Repositorios;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IComentarioRepository : IRepository<ComentarioServicio>
    {
        Task<List<ComentarioServicio>> GetComentariosByServicioId(int IdServicio);
    }
}
