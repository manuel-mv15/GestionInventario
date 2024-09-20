using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario
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
        List<Lote> loteHistorial = new List<Lote>();

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
            loteUEPS.Push(new Lote(idLote, unidades, fecha, precio, detalle));
            lotePEPS.Enqueue(new Lote(idLote, unidades, fecha, precio, detalle));
            loteCostoPromedio.Add(new Lote(idLote, unidades, fecha, precio, detalle));
            loteHistorial.Add(new Lote(idLote, unidades, fecha, precio, detalle));
            idLote++;
        }

        public void Salida()
        {
            int opMetodo = 0;
            Console.WriteLine("Seleccione el metodo");
            opMetodo = valEntre(1, 3);

            switch (opMetodo)
            {
                case 1:// pila
                    salidaUEPS(loteUEPS, lotePEPS, loteCostoPromedio);
                    break;
                case 2://cola
                    salidaPEPS(loteUEPS, lotePEPS, loteCostoPromedio);
                    break;
                case 3: // lista
                    salidaCostoPromedio(loteUEPS, lotePEPS, loteCostoPromedio);
                    break;
            }

        }

        //--- PEPS Queue
        static void salidaPEPS(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio)
        {
            Console.Clear();
            Console.WriteLine("Eliminar unidades");
            int unidades = valInt(0);

            while (unidades > 0 && lotePEPS.Count > 0)
            {
                Lote lote = lotePEPS.Peek();

                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;

                    eliminarLoteUEPS(loteUEPS, lote);
                    eliminarLoteCostePromedio(loteCostoPromedio, lote);
                    lotePEPS.Dequeue();
                }
                else
                {
                    lote.unidades -= unidades;
                    eliminarParcialLoteUEPS(loteUEPS, lotePEPS.Peek(), unidades);
                    eliminarParcialLoteCostePromedio(loteCostoPromedio, lote, unidades);
                    unidades = 0;
                }
            }
        }
        
        static void eliminarLotePEPS(Queue<Lote> lotePEPS, Lote loteEliminar)
        {
            Queue<Lote> loteTem = new Queue<Lote>();

            while (lotePEPS.Count > 0)
            {
                Lote lote = lotePEPS.Dequeue();
                if (!lote.Equals(loteEliminar))
                {
                    loteTem.Enqueue(lote);
                }
            }

            while (loteTem.Count > 0)
            {
                lotePEPS.Enqueue(loteTem.Dequeue());
            }
        }

        static void eliminarParcialLotePEPS(Queue<Lote> lotePEPS, Lote loteEliminar, int unidades)
        {
            Queue<Lote> loteTem = new Queue<Lote>();

            while (lotePEPS.Count > 0)
            {
                Lote lote = lotePEPS.Dequeue();
                if (lote.Equals(loteEliminar))
                {
                    lote.unidades -= unidades;
                }
                loteTem.Enqueue(lote);
            }

            while (loteTem.Count > 0)
            {
                lotePEPS.Enqueue(loteTem.Dequeue());
            }
        }
        //--------------------------------------
        // UEPS Stack
        static void salidaUEPS(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio)
        {
            Console.Clear();
            Console.WriteLine("Eliminar unidades");
            int unidades = valInt(0);

            while (unidades > 0 && loteUEPS.Count > 0)
            {
                Lote lote = loteUEPS.Peek();

                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;
                    eliminarLoteCostePromedio(loteCostoPromedio, lote);

                    eliminarLotePEPS(lotePEPS, lote);
                    loteUEPS.Pop();
                }
                else
                {

                    lote.unidades -= unidades;
                    eliminarParcialLoteCostePromedio(loteCostoPromedio, lote, unidades);
                    eliminarParcialLotePEPS(lotePEPS, lote, unidades);
                    unidades = 0;
                }
            }
        }

        static void eliminarLoteUEPS(Stack<Lote> loteUEPS, Lote loteEliminar)
        {
            Stack<Lote> loteTemp = new Stack<Lote>();

            while (loteUEPS.Count > 0)
            {
                Lote lote = loteUEPS.Pop();
                if (!lote.Equals(loteEliminar))
                {
                    loteTemp.Push(lote);
                }
            }

            // Volvemos a llenar la pila original
            while (loteTemp.Count > 0)
            {
                loteUEPS.Push(loteTemp.Pop());
            }
        }

        static void eliminarParcialLoteUEPS(Stack<Lote> loteUEPS, Lote loteEliminar, int unidades)
        {
            Stack<Lote> loteTemp = new Stack<Lote>();

            while (loteUEPS.Count > 0)
            {
                Lote lote = loteUEPS.Pop();
                if (lote.Equals(loteEliminar))
                {
                    lote.unidades -= unidades;
                }
                loteTemp.Push(lote);
            }

            // Volvemos a llenar la pila original con los lotes actualizados
            while (loteTemp.Count > 0)
            {
                loteUEPS.Push(loteTemp.Pop());
            }
        }
        //---------------------------------------
        // coste promedio 
        public void salidaCostoPromedio(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio)
        {
            int unidades = valInt(0);

            // Paso 1: Verificamos si hay suficientes unidades en el inventario
            int totalunidades = loteCostoPromedio.Sum(l => l.unidades);
            if (unidades > totalunidades)
            {
                Console.WriteLine("No hay suficientes unidades en el inventario.");
                return;
            }

            // Paso 2: Calcular el costo promedio
            double costoTotal = loteCostoPromedio.Sum(l => l.precio * l.unidades);
            double costoPromedio = costoTotal / totalunidades;

            Console.WriteLine($"El costo promedio por unidad es: {costoPromedio}");

            // Paso 3: Eliminar unidades desde el inventario
            for (int i = 0; i < loteCostoPromedio.Count && unidades > 0; i++)
            {
                Lote lote = loteCostoPromedio[i];

                if (lote.unidades <= unidades)
                {
                    // Si el lote tiene menos o igual unidades de las que queremos eliminar
                    unidades -= lote.unidades;  // Restamos las unidades
                    eliminarLotePEPS(lotePEPS, lote);
                    eliminarLoteUEPS(loteUEPS, lote);

                    loteCostoPromedio.RemoveAt(i);       // Eliminamos el lote completo
                    i--;  // Ajustamos el índice ya que eliminamos un elemento
                }
                else
                {
                    // Si el lote tiene más unidades de las que queremos eliminar
                    lote.unidades -= unidades;  // Restamos solo las unidades necesarias

                    eliminarParcialLotePEPS(lotePEPS, lote, unidades);
                    eliminarParcialLoteUEPS(loteUEPS, lote, unidades);
                    unidades = 0;  // Ya eliminamos todas las unidades requeridas
                }
            }

            Console.WriteLine("unidades eliminadas correctamente.");
        }

        static void eliminarLoteCostePromedio(List<Lote> loteCostoPromedio, Lote loteEliminar)
        {
            Lote loteTem = loteCostoPromedio.FirstOrDefault(l => l.Equals(loteEliminar));
            if (loteTem != null)
            {
                loteCostoPromedio.Remove(loteTem);
            }
        }

        static void eliminarParcialLoteCostePromedio(List<Lote> loteCostoPromedio, Lote loteEliminar, int unidades)
        {
            Lote loteTem = loteCostoPromedio.FirstOrDefault(l => l.Equals(loteEliminar));
            if (loteTem != null)
            {
                loteTem.unidades -= unidades;
            }
        }
        //---------------------------------------

        public override string ToString()
        {
            return $"Id: {this.id}, Nombre: {this.nombre}, Categoría: {this.categoria}";
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
