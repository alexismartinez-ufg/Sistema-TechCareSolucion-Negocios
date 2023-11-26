using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Usuario : BaseModel
    {                
        public string? Nombre { get; set; }
        
        public string? UserName { get; set; }
        
        public string? Password { get; set; }
        
        public string? Email { get; set; }
        
        public string? Telefono { get; set; }
        
        public string? Direccion { get; set; }

        public string? Role { get; set; }
    }
}
