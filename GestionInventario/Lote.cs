
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
        // facturas dependiendo del metodo 
        public string factura(int cantidad)
        {
            return $"Unidades: {cantidad}. Precio: {precio.ToString("$0.00")}. Costo total: {(precio * (double)cantidad).ToString("$0.00")}";
        }
        public string factura(int cantidad,int precio)
        {
            return $"Unidades: {cantidad}. Precio: {precio.ToString("$0.00")}. Costo total: {(precio * (double)cantidad).ToString("$0.00")}";
        }
        public override string ToString()
        {
            return $"Id: {idLote}, Unidades: {unidades}, Detalle: {detalle}, Precio: {precio.ToString("$0.00")} Fecha de ingreso: {fecha}.";
        }
        // Sobrescribir Equals para comparar correctamente los lotes
        public override bool Equals(object obj)
        {
            if (obj is Lote lote)
            {
                // Aquí puedes decidir cómo comparar los lotes, por ejemplo, por idLote
                return this.idLote == lote.idLote;
            }
            return false;
        }

        // Sobrescribir GetHashCode para asegurar que Equals funcione correctamente
        public override int GetHashCode()
        {
            return idLote.GetHashCode(); // Utilizamos el idLote para generar un código hash único
        }

    }
}
