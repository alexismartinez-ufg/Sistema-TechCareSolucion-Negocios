using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ComentarioServicio : BaseModel
    {

        [ForeignKey("Servicio")]
        public int IdServicio { get; set; }

        public virtual Servicio Servicio { get; set; }

        public string Comentario { get; set; }

        public DateTime FechaComentario { get; set; }
    }
}
