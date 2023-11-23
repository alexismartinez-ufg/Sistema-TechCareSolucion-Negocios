using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAsync();
        Task<T> AddAsync<T>(T entity) where T : BaseModel;
        Task<int> UpdateAsync<T>(T entity) where T : BaseModel;
        Task<int> DeleteAsync<T>(T entity) where T : BaseModel;
    }
}
