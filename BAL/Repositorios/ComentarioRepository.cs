using BAL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositorios
{
    public class ComentarioRepository : BaseRepository<ComentarioServicio>, IComentarioRepository
    {
        protected readonly EmprendimientosContext dbContext;

        public ComentarioRepository(EmprendimientosContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<ComentarioServicio>> GetComentariosByServicioId(int IdServicio)
        {
            return await dbContext.ComentarioServicio.Where(x=>x.IdServicio == IdServicio).ToListAsync();
        }
    }
}
