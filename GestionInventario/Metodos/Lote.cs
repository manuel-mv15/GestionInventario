using GestionInventario.Metodos;

namespace Metodos
{
    internal class Lote
    {
     public   int unidades { get; set; }
        DateTime fecha { get; set; }
      public  double precio { get; set; }
        string detalle {  get; set; }
        public Lote()
        {
            
        }
        public Lote(int unidades, DateTime fecha, double precio, string detalle)
        {
            this.unidades = unidades;
            this.fecha = fecha;
            this.precio = precio;
            this.detalle = detalle;
        }

        public override string ToString()
        {
            return $"Unidades: {unidades},Detalle: {detalle}, Precio: {precio.ToString("$0.00")} Fecha de ingreso: {fecha}.";
        }

    }
}
