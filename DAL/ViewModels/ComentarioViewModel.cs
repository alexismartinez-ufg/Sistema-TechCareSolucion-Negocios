using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ComentarioViewModel
    {
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public int Idservicio { get; set; }
    }
}
