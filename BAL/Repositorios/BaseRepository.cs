using BAL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BAL.Repositorios
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly EmprendimientosContext _dbContext;

        protected BaseRepository(EmprendimientosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync<T>(T entity) where T : BaseModel
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }


        public async Task<int> DeleteAsync<T>(T entity) where T : BaseModel
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);

        public async Task<List<T>> ListAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<int> UpdateAsync<T>(T entity) where T : BaseModel
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}