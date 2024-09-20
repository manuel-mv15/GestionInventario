using System;
namespace GestionInventario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<String, Producto> Inventario = new Dictionary<String, Producto>();
            int opmneu = 0;
            

             
            








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




    }
}
















