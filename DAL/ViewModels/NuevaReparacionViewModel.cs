using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class NuevaReparacionViewModel
    {
        public bool IniciarReparacionYa { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Dispositivo { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Problema { get; set; }
    }
}
