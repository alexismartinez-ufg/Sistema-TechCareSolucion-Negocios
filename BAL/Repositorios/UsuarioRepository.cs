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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        protected readonly EmprendimientosContext dbContext;
        public UsuarioRepository(EmprendimientosContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Usuario> CreateClienteIfNotExist(NuevaReparacionViewModel user)
        {
            var newUser = new Usuario()
            {
                Email = user.Correo,
                Direccion = user.Direccion,
                Nombre = $"{user.Nombre} {user.Apellido}",
                Telefono = user.Telefono,
                Role = "cliente"
            };

            Func<Usuario, bool> clientePredicate = x =>
                x.Email.ToLower() == newUser.Email.ToLower()
                && x.Nombre.ToLower().Contains(newUser.Nombre.ToLower())
                && x.Nombre.ToLower().Contains(user.Apellido.ToLower());

            return await CreateIfNotExist(clientePredicate, newUser);
        }

        public async Task<Usuario> CreateUserIfNotExist(Usuario user)
        {
            Func<Usuario, bool> userExist = x =>
                (x.Email.ToLower() == user.Email.ToLower()
                && x.Nombre.ToLower().Contains(user.Nombre.ToLower())
                && x.Role.ToLower() == user.Role.ToLower()
                && x.Password.ToLower() == user.Password.ToLower())
                || x.UserName.ToLower() == user.UserName.ToLower();

            return await CreateIfNotExist(userExist, user);
        }

        public async Task<Usuario> CreateIfNotExist(Func<Usuario, bool> predicate, Usuario newUser)
        {
            var usuario = dbContext.Usuarios.FirstOrDefault(predicate);

            if (usuario == null)
            {
                usuario = await AddAsync(newUser);
            }

            return usuario;
        }

        public async Task<Usuario> CanDoLogin(LoginViewModel login)
        {
            return await dbContext.Usuarios.FirstOrDefaultAsync(x => (x.UserName.ToLower() == login.UserName.ToLower() || x.Email.ToLower() == login.UserName.ToLower()) && x.Password.ToLower() == login.Password.ToLower() && !string.IsNullOrEmpty(x.Role));
        }
    }
}
