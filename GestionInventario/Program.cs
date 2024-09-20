using System;
using GestionInventario.Metodos;
using Metodos;
namespace GestionInventario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<String, Producto> Inventario = new Dictionary<String, Producto>();





           
        }
        static Dictionary<String, Producto> agregarProducto(Dictionary<String, Producto> inventario)
        {
            string nombre = Console.ReadLine();
            string categoria = Console.ReadLine();

            inventario.Add(nombre, new Producto(inventario.Count + 1, nombre, categoria));
            return inventario;
        }

        static int valInt(int limit)
        {
            int numero = 0;
            string entrada = "";

            Console.Write("Ingrese el numero mayor igual a {0}: ", limit);
            entrada = Console.ReadLine();

            while (!int.TryParse(entrada, out numero) || !(numero >= limit))
            {
                Console.WriteLine("Ingrese el numero mayor igual a {0} ", limit);
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

            Console.Write("Ingrese el numero mayor igual a {0}: ", limit);
            entrada = Console.ReadLine();

            while (!double.TryParse(entrada, out numero) || !(numero >= limit))
            {
                Console.WriteLine("Ingrese el numero mayor igual a {0} ", limit);
                Console.Write("Ingrese el numero: ");
                entrada = Console.ReadLine();
            }
            return numero;
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

    }
}
