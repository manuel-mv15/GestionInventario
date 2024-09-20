using Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Metodos
{
    internal class Metodos : Lote
    {
        public void salidaUEPS(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio)
        {
            int unidades = valInt(0);

            while (unidades > 0 && loteUEPS.Count > 0)
            {
                Lote lote = loteUEPS.Peek();

                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;
                    loteUEPS.Pop();
                }
                else
                {
                    lote.unidades -= unidades;
                    unidades = 0;
                }
            }
        }
        public void salidaPEPS(Stack<Lote> loteUEPS, Queue<Lote> lotePEPS, List<Lote> loteCostoPromedio)
        {
            int unidades = valInt(0);

            while (unidades > 0 && lotePEPS.Count > 0)
            {
                Lote lote = lotePEPS.Peek();

                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;
                    lote.
                    lotePEPS.Dequeue();
                }
                else
                {
                    lote.unidades -= unidades;
                    unidades = 0;
                }
            }
        }
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
                    loteCostoPromedio.RemoveAt(i);       // Eliminamos el lote completo
                    i--;  // Ajustamos el índice ya que eliminamos un elemento
                }
                else
                {
                    // Si el lote tiene más unidades de las que queremos eliminar
                    lote.unidades -= unidades;  // Restamos solo las unidades necesarias
                    unidades = 0;  // Ya eliminamos todas las unidades requeridas
                }
            }

            Console.WriteLine("unidades eliminadas correctamente.");
        }


        private void borrar()
        {


        }



        public void EliminarProductoDeColaYPila(Queue<Producto> cola, Stack<Producto> pila, Producto productoAEliminar)
        {
            // Eliminar de la cola
            EliminarDeCola(cola, productoAEliminar);

            // Eliminar de la pila
            EliminarDePila(pila, productoAEliminar);
        }

        private void EliminarDeCola(Queue<Producto> cola, Producto productoAEliminar)
        {
            Queue<Producto> colaTemporal = new Queue<Producto>();

            while (cola.Count > 0)
            {
                Producto producto = cola.Dequeue();
                if (!producto.Equals(productoAEliminar)) // Si no es el producto a eliminar
                {
                    colaTemporal.Enqueue(producto); // Lo guardamos en la cola temporal
                }
            }

            // Pasamos los elementos de vuelta a la cola original
            while (colaTemporal.Count > 0)
            {
                cola.Enqueue(colaTemporal.Dequeue());
            }
        }

        private void EliminarDePila(Stack<Producto> pila, Producto productoAEliminar)
        {
            Stack<Producto> pilaTemporal = new Stack<Producto>();

            while (pila.Count > 0)
            {
                Producto producto = pila.Pop();
                if (!producto.Equals(productoAEliminar)) // Si no es el producto a eliminar
                {
                    pilaTemporal.Push(producto); // Lo guardamos en la pila temporal
                }
            }

            // Pasamos los elementos de vuelta a la pila original
            while (pilaTemporal.Count > 0)
            {
                pila.Push(pilaTemporal.Pop());
            }
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
    }
}
