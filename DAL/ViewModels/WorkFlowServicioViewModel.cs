using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class WorkFlowServicioViewModel
    {
        public Servicio Servicio { get; set; }

        public ReparacionViewModel Reparacion { get; set; }
    }
}
