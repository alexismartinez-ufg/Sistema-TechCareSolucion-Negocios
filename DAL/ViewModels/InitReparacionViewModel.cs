using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class InitReparacionViewModel
    {
        public string Tecnico { get; set; }
        public string Usuario { get; set; }
        public string Equipo { get; set; }
        public string UrlPublic { get; set; }
        public string Server { get; set; } = "https://localhost:44360";
    }
}
