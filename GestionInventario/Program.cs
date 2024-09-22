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
                Console.WriteLine(" _  _      _     _____ _____  ____  ____  _  ____  ____    ____  ____  ____  _     ____  ____ ");
                Thread.Sleep(100);
                Console.WriteLine("/ \\/ \\  /|/ \\ |\\/  __//__ __\\/  _ \\/  __\\/ \\/  _ \\/ ___\\  /   _\\/  _ \\/  __\\/ \\   /  _ \\/ ___\\");
                Thread.Sleep(100);
                Console.WriteLine("| || |\\ ||| | //|  \\    / \\  | / \\||  \\/|| || / \\||    \\  |  /  | / \\||  \\/|| |   | / \\||    \\");
                Thread.Sleep(100);
                Console.WriteLine("| || | \\||| \\// |  /_   | |  | |-|||    /| || \\_/|\\___ |  |  \\__| |-|||    /| |_/\\| \\_/|\\___ |");
                Thread.Sleep(100);
                Console.WriteLine("\\_/\\_/  \\|\\__/  \\____\\  \\_/  \\_/ \\|\\_/\\_\\\\_/\\____/\\____/  \\____/\\_/ \\|\\_/\\_\\\\____/\\____/\\____/");
                Thread.Sleep(100);
                Console.WriteLine("                                                                                            ");
                                                                                   
                Thread.Sleep(400);
                Console.WriteLine("--- Menú de Inventario ---");
                Thread.Sleep(100);
                Console.WriteLine("1. Agregar Producto");
                Thread.Sleep(100);
                Console.WriteLine("2. Productos");
                Thread.Sleep(100);
                Console.WriteLine("3. Ver Lotes");
                Thread.Sleep(100);
                Console.WriteLine("4. Salir");
                Thread.Sleep(100);
                Console.Write("Seleccione una opción: ");
                opmneu = valEntre(1, 4);
                Console.Clear();
                switch (opmneu)
                {
                    case 1:
                       Inventario = agregarProducto(Inventario);
                        break;
                    case 2:
                        Thread.Sleep(100);
                        Console.WriteLine("1. Ver productos");
                        Thread.Sleep(100);
                        Console.WriteLine("2. Ingresar lote");
                        Thread.Sleep(100);
                        Console.WriteLine("3. salir");
                        opmneu = valEntre(1, 3);
                        Console.Clear();
                        if (opmneu!=3)
                        {
                            Thread.Sleep(100);
                            Console.WriteLine("Productos");
                            Thread.Sleep(100);
                            mostrarInventario(Inventario);
                            if (Inventario.Count > 0 && opmneu == 2)
                            {
                                Thread.Sleep(100);
                                Console.WriteLine("Ingrese el código del producto:");
                                codigo = valInt(1);
                                if (!Inventario.ContainsKey(codigo))
                                {
                                    Thread.Sleep(100);
                                    Console.WriteLine("Código de producto no encontrado.");
                                    break;
                                }
                                else
                                {
                                    Inventario[codigo].Entrada();
                                }
                            }

                        }

                            break;
                    case 3:
                        mostrarInventario(Inventario);
                        
                        if (Inventario.Count > 0)
                        {
                            Thread.Sleep(100);
                            Console.WriteLine("Ingrese el código del producto:");
                            codigo = valInt(1);
                            if (!Inventario.ContainsKey(codigo))
                            {
                                Thread.Sleep(100);
                                Console.WriteLine("Código de producto no encontrado.");
                                break;
                            }
                            Thread.Sleep(100);
                            Console.WriteLine("1. Ver lotes existentes");
                            Thread.Sleep(100);
                            Console.WriteLine("2. Ver historial de lotes");
                            Thread.Sleep(100);
                            Console.WriteLine("3. Salir");
                            opmneu = valEntre(1, 3);

                            mostrarLotes(opmneu, Inventario, codigo);
                        }
                        else
                        {
                            Console.Clear();
                        }
                        break;
                    case 4:
                        Thread.Sleep(100);
                        Console.WriteLine("Cerrando el programa, por favor espere");
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(1000); 
                            Console.Write(".");
                        }
                        salir = true;
                        break;
                    default:
                        Thread.Sleep(100);
                        Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
                        break;
                }
                Console.Clear();
            }

        }

        static void mostrarLotes(int opmenu, Dictionary<int, Producto> inventario,int codigo)
        {
            if (!inventario.ContainsKey(codigo) ||
                (inventario[codigo].loteCostoPromedio.Count == 0 &&
                 inventario[codigo].loteUEPS.Count == 0 &&
                 inventario[codigo].lotePEPS.Count == 0))
            {
                Thread.Sleep(100);
                Console.WriteLine("No hay productos o lotes disponibles para mostrar.");
                return;
            }

            if (opmenu == 1)
            {
                inventario[codigo].mostrarLotesExistentes();
            }
            else if (opmenu == 2)
            {
                inventario[codigo].mostrarHistorialLotes();
            }
        }

       static void mostrarInventario(Dictionary<int, Producto> inventario)
        {
            if (inventario.Count == 0)
            {
                Console.Clear();
                Thread.Sleep(100);
                Console.WriteLine("/////////////////////////////////");
                Thread.Sleep(100);
                Console.WriteLine("No hay productos en el inventario.");
                Thread.Sleep(2000);
                return;
            }

            foreach (var inve in inventario)
            {
                Console.WriteLine($"Código: {inve.Key}. {inve.Value.ToString()} ");
               
            }
        }

        static Dictionary<int, Producto> agregarProducto(Dictionary<int, Producto> inventario)
        {
            Console.Clear();
            Thread.Sleep(100);
            Console.WriteLine("///////////////////////////////");
            Thread.Sleep(100);
            Console.WriteLine("Ingrese el nombre del producto");
            string nombre = Console.ReadLine();
            Thread.Sleep(100);
            Console.WriteLine("/////////////////////////////////");
            Thread.Sleep(100);
            Console.WriteLine("ingrese la categoria del producto");
            string categoria = Console.ReadLine();

            inventario.Add(inventario.Count + 1, new Producto( nombre, categoria));
            return inventario;
        }

        static int valInt(int limit)
        {
            int numero = 0;
            string entrada = "";

            
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

            
            entrada = Console.ReadLine();

            while (!int.TryParse(entrada, out numero) || !(numero >= min && numero <= max))
            {
                Console.WriteLine("Ingrese el numero entre {0} y {1} ", min, max);
                
                entrada = Console.ReadLine();
            }
            return numero;
        }
    }
}
















