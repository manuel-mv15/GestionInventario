
namespace GestionInventario
{
    internal class Lote
    {
        public int idLote;
        public int unidades { get; set; }
        DateTime fecha { get; set; }
        public double precio { get; set; }
        string detalle { get; set; }
        public Lote()
        {

        }
        public Lote(int idLote, int unidades, DateTime fecha, double precio, string detalle)
        {
            this.idLote = idLote;
            this.unidades = unidades;
            this.fecha = fecha;
            this.precio = precio;
            this.detalle = detalle;
        }

        public override string ToString()
        {
            return $"Id: {idLote}, Unidades: {unidades}, Detalle: {detalle}, Precio: {precio.ToString("$0.00")} Fecha de ingreso: {fecha}.";
        }
        public override bool Equals(object obj)
        {
            if (obj is Lote lote)
            {
                return idLote == lote.idLote; // Comparamos por ID de lote
            }
            return false;
        }

        public override int GetHashCode()
        {
            return idLote.GetHashCode();
        }
    }
}
