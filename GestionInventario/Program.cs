using System;
namespace GestionInventario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Producto> Inventario = new Dictionary<int, Producto>();
            int opmneu = 0;
            int codigo = 0;

            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- Menú de Inventario ---");
                Console.WriteLine("1. Agregar Producto");
                Console.WriteLine("2. Ver Productos");
                Console.WriteLine("3. Ver Lotes");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opmneu = valEntre(1, 4);

                switch (opmneu)
                {
                    case 1:
                       Inventario = agregarProducto(Inventario);
                        break;
                    case 2:
                        mostrarInventario(Inventario);
                        break;
                    case 3:
                        mostrarInventario(Inventario);
                        Console.WriteLine("Ingrese el el codigo del producto");
                        codigo = valEntre(1, Inventario.Count);

                        Console.WriteLine("1. Ver lotes existentes");
                        Console.WriteLine("2. ver historial de lostes");
                        Console.WriteLine("3. salir");
                        opmneu = valEntre(1, 3);

                        mostrarLotes(opmneu, Inventario, codigo);

                        break;
                    case 4:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
                        break;
                }
            }

        }
        static void mostrarLotes(int opmenu, Dictionary<int, Producto> inventario,int codigo)
        {
            if(opmenu ==1)
            {
                inventario[codigo].mostrarLotesExistentes();
            }
           else if(opmenu == 2)
            {
                inventario[codigo].mostrarHistorialLotes();
            }
        }
       static void mostrarInventario(Dictionary<int, Producto> inventario)
        {
            foreach (var inve in inventario)
            {
                Console.WriteLine($"Código: {inve.Key}. {inve.Value.ToString()} "); 
            }
        }

        static Dictionary<int, Producto> agregarProducto(Dictionary<int, Producto> inventario)
        {
            string nombre = Console.ReadLine();
            string categoria = Console.ReadLine();

            inventario.Add(inventario.Count + 1, new Producto( nombre, categoria));
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
















