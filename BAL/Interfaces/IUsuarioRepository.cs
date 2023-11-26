using DAL.Models;
using DAL.ViewModels;

namespace BAL.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> CreateClienteIfNotExist(NuevaReparacionViewModel user);
        Task<Usuario> CreateUserIfNotExist(Usuario user);
        Task<Usuario> CanDoLogin(LoginViewModel login);
    }
}
