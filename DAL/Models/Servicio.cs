using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Servicio : BaseModel
    {
        public string? TipoServicio { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public string? EstadoServicio { get; set; }

        [ForeignKey("IdTecnico")]
        public int? IdTecnico { get; set; }

        [ForeignKey("IdCliente")]
        public int? IdCliente{ get; set; }

        public double? CostoServicio { get; set; }
        
        public double? TotalServicio { get; set; }
        
        public string? IdServicioPublic { get; set; }
        
        public Usuario Tecnico { get; set; }

        public Usuario Cliente { get; set; }

        public virtual ICollection<ComentarioServicio> Comentarios { get; set; }
        
        public virtual DetalleServicio DetalleServicio { get; set; }
    }
}
