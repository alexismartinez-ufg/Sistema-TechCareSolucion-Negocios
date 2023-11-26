using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DetalleServicio : BaseModel
    {
        [ForeignKey("Servicio")]
        public int? IdServicio { get; set; }

        public string? ContentService { get; set; }
        
        public virtual Servicio Servicio { get; set; }
    }
}
