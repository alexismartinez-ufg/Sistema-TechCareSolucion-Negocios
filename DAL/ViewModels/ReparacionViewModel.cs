namespace DAL.ViewModels
{
    public class ReparacionViewModel : TipoServicio
    {
        public string Diagnostico {  get; set; }
               
        public string Problema { get; set; }
        
        public string DetalleProblema {  get; set; }

        public List<Repuestos> HistorialRepuestos { get; set; }
    }

    public class Repuestos
    {
        public string Nombre { get; set; }

        public int Cantidad { get; set; }

        public double Costo { get; set; }

        public double Venta { get; set; }
    }
}
