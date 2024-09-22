using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario
{
    internal class Producto
    {
        string nombre { get; set; }
        string categoria { get; set; }
        int idLote = 1;

       public Stack<Lote> loteUEPS = new Stack<Lote>();
        public Queue<Lote> lotePEPS = new Queue<Lote>();
        public List<Lote> loteCostoPromedio = new List<Lote>();
        List<Lote> loteHistorial = new List<Lote>();

        public Producto()
        {

        }
        public Producto( string nombre, string categoria)
        {
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
            unidades = valInt(1);
            Console.WriteLine("Precio");
            precio = valDou(0);
            fecha = DateTime.Now;
            Console.WriteLine("Detalles del producto");
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


            int totalunidades = loteCostoPromedio.Sum(l => l.unidades);

            Console.WriteLine($"Unidades totales del producto: {totalunidades}");
            Console.WriteLine("1. continuar");
            Console.WriteLine("2. salir");
            opMetodo = valEntre(1, 2);

            if (opMetodo==1)
            {

                Thread.Sleep(100);
                Console.WriteLine("Seleccione el metodo");
                Thread.Sleep(100);
                Console.WriteLine("1. Metodo UEPS");
                Thread.Sleep(100);
                Console.WriteLine("2. Metodo PEPS");
                Thread.Sleep(100);
                Console.WriteLine("3. Metodo Costo Promedio");
                Thread.Sleep(100);
                Console.WriteLine("4. Salir");
                opMetodo = valEntre(1, 4);

                int unidades;

                Thread.Sleep(100);
                Console.WriteLine("Ingrese el numero de uinidades");
                unidades = valEntre(1,totalunidades);

                switch (opMetodo)
                {
                    case 1:// pila
                        salidaUEPS(loteUEPS, lotePEPS, loteCostoPromedio,unidades);
                        break;
                    case 2://cola
                        salidaPEPS(loteUEPS, lotePEPS, loteCostoPromedio, unidades);
                        break;
                    case 3: // lista
                        salidaCostoPromedio(loteUEPS, lotePEPS, loteCostoPromedio, unidades);
                        break;
                }

            }
            
        }

        //--- PEPS Queue
         void salidaPEPS(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio, int unidades)
        {
            Console.Clear();

            while (unidades > 0 && lotePEPS.Count > 0)
            {
                Lote lote = lotePEPS.Peek();
                double costetotal = 0;
                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;

                    factura(unidades,lote);

                    eliminarLoteUEPS(loteUEPS, lote);
                    eliminarLoteCostePromedio(loteCostoPromedio, lote);
                    lotePEPS.Dequeue();
                }
                else
                {
                    factura(unidades, lote);
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
         void salidaUEPS(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio, int unidades)
        {
            Console.Clear();

            while (unidades > 0 && loteUEPS.Count > 0)
            {
                Lote lote = loteUEPS.Peek();

                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;

                    factura(unidades, lote);

                    eliminarLoteCostePromedio(loteCostoPromedio, lote);

                    eliminarLotePEPS(lotePEPS, lote);
                    loteUEPS.Pop();
                }
                else
                {

                    lote.unidades -= unidades;

                    factura(unidades, lote);

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
        public void salidaCostoPromedio(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio, int unidades)
        {
            // Paso 1: Verificamos si hay suficientes unidades en el inventario
            int totalunidades = loteCostoPromedio.Sum(l => l.unidades);
            if (unidades > totalunidades)
            {
                Thread.Sleep(100);
                Console.WriteLine("No hay suficientes unidades en el inventario.");
                return;
            }

            // Paso 2: Calcular el costo promedio
            double costoTotal = loteCostoPromedio.Sum(l => l.precio * l.unidades);
            double costoPromedio = costoTotal / totalunidades;

            Thread.Sleep(100);
            Console.WriteLine($"El costo promedio por unidad es: {costoPromedio}");

            // Paso 3: Eliminar unidades desde el inventario
            for (int i = 0; i < loteCostoPromedio.Count && unidades > 0; i++)
            {
                Lote lote = loteCostoPromedio[i];

                if (lote.unidades <= unidades)
                {
                    // Si el lote tiene menos o igual unidades de las que queremos eliminar
                    unidades -= lote.unidades;  // Restamos las unidades

                    factura(unidades, lote);

                    eliminarLotePEPS(lotePEPS, lote);
                    eliminarLoteUEPS(loteUEPS, lote);

                    loteCostoPromedio.RemoveAt(i);       // Eliminamos el lote completo
                    i--;  // Ajustamos el índice ya que eliminamos un elemento
                }
                else
                {
                    // Si el lote tiene más unidades de las que queremos eliminar
                    lote.unidades -= unidades;  // Restamos solo las unidades necesarias

                    factura(unidades, lote);

                    eliminarParcialLotePEPS(lotePEPS, lote, unidades);
                    eliminarParcialLoteUEPS(loteUEPS, lote, unidades);
                    unidades = 0;  // Ya eliminamos todas las unidades requeridas
                }
            }

            Thread.Sleep(100);
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

        void factura(int unidades,Lote lote)
        {
            Console.WriteLine(ToString()+" "+lote.factura(unidades));
        }

        public void mostrarHistorialLotes()
        {
            for (int i = 0; i < loteHistorial.Count; i++)
            {
                Console.WriteLine(loteCostoPromedio[i].ToString());
            }
        }

        public void mostrarLotesExistentes()
        {
            for (int i = 0; i < loteCostoPromedio.Count; i++)
            {
                Console.WriteLine(loteCostoPromedio[i].ToString());
            }
        }

        public  override string ToString()
        {
            return $"Nombre: {this.nombre}, Categoría: {this.categoria}";
        }
        
        static int valInt(int limit)
        {
            int numero = 0;
            string entrada = "";

            entrada = Console.ReadLine();

            while (!int.TryParse(entrada, out numero) || !(numero >= limit))
            {
                Console.WriteLine("Ingrese el numero mayor o igual a {0} ", limit);
                Console.Write("Ingrese el numero: ");
                entrada = Console.ReadLine();
            }
            return numero;
        }

        static int valEntre(int min, int max)
        {
            int numero = 0;
            string entrada = "";

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
