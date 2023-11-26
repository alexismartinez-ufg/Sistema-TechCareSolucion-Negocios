using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ServiciosViewModel
    {
        public int? Id { get; set; }
        public int? IdPublic { get; set; }
        public string? Tipo { get; set; }
        public string? Tecnico { get; set; }
        public string? Cliente { get; set; }
        public string? Estado { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public double? Total { get; set; }
    }
}
