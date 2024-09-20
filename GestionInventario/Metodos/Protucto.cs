using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionInventario.Metodos;
using Metodos;

namespace GestionInventario.Metodos
{
    internal class Producto
    {
        int id { get; set; }
        string nombre { get; set; }
        string categoria { get; set; }
        int idLote = 1;
        
        Stack<Lote> loteUEPS = new Stack<Lote>();
        Queue<Lote> lotePEPS = new Queue<Lote>();
        List<Lote> loteCostoPromedio = new List<Lote>();
        List<Lote> loteHistorial= new List<Lote>();

        public void eliminar(int unidades)
        {
            List<Lote> loteCostoPromedio = new List<Lote>();
        }

        public Producto()
        {

        }
        public Producto(int id, string nombre, string categoria)
        {
            this.id = id;
            this.nombre = nombre;
            this.categoria = categoria;
            Entrada();
        }

        public void Entrada()
        {
            int unidades;
            DateTime fecha;
            double precio;
            string detalle;
            Console.WriteLine("Unidades");
            unidades = valInt(0);
            Console.WriteLine("Precio");
            precio = valDou(0);
            fecha = DateTime.Now;
            Console.WriteLine("Detalle");
            detalle = Console.ReadLine();
            loteUEPS.Push(new Lote(idLote++,unidades, fecha, precio,detalle));
            lotePEPS.Enqueue(new Lote(id++, unidades, fecha, precio, detalle));
            loteCostoPromedio.Add(new Lote(id++, unidades, fecha, precio, detalle));
            loteHistorial.Add(new Lote(id++, unidades, fecha, precio, detalle));

        }
        
        public void Salida()
        {
            Metodos metodos = new Metodos();
            int opMetodo = 0;
            Console.WriteLine("Seleccione el metodo");
            opMetodo = valEntre(1, 3);

            switch(opMetodo)
            {
                case 1:// pila
                    metodos.salidaUEPS(loteUEPS, lotePEPS,loteCostoPromedio);
                    break;
                    case 2://cola
                    metodos.salidaPEPS(loteUEPS, lotePEPS, loteCostoPromedio);
                    break;
                case 3: // lista
                    metodos.salidaCostoPromedio(loteUEPS, lotePEPS, loteCostoPromedio);
                    break;
            }

        }























        public override string ToString()
        {
            return $"nombre";
        }
        static int valInt(int limit)
        {
            int numero = 0;
            string entrada = "";

            Console.Write("Ingrese el numero mayor a {0}: ", limit);
            entrada = Console.ReadLine();

            while (!int.TryParse(entrada, out numero) || !(numero > limit))
            {
                Console.WriteLine("Ingrese el numero mayor a {0} ", limit);
                Console.Write("Ingrese el numero: ");
                entrada = Console.ReadLine();
            }
            return numero;
        }

        static int valEntre(int min, int max)
        {
            int numero = 0;
            string entrada = "";

            Console.Write("Ingrese el numero entre {0} y {1}: ", min, max);
            entrada = Console.ReadLine();

            while (!int.TryParse(entrada, out numero) || !(numero >= min && numero <= max))
            {
                Console.WriteLine("Ingrese el numero entre {0} y {1} ", min, max);
                Console.Write("Ingrese el numero: ");
                entrada = Console.ReadLine();
            }
            return numero;
        }

        static double valDou(int limit)
        {
            double numero = 0;
            string entrada = "";

            Console.Write("Ingrese el numero mayor a {0}: ", limit);
            entrada = Console.ReadLine();

            while (!double.TryParse(entrada, out numero) || !(numero > limit))
            {
                Console.WriteLine("Ingrese el numero mayor a {0} ", limit);
                Console.Write("Ingrese el numero: ");
                entrada = Console.ReadLine();
            }
            return numero;
        }
    }
}
