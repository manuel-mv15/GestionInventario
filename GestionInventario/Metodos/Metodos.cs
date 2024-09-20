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
            int unidades = 0;

            while (unidades > 0 && loteUEPS.Count > 0)
            {
                Lote lote = loteUEPS.Pop();
                
                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;
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
            int unidades = 0;

            while (unidades > 0 && loteUEPS.Count > 0)
            {
                Lote lote = lotePEPS.Peek();

                if (lote.unidades <= unidades)
                {
                    unidades -= lote.unidades;
                    lote.unidades = 0;
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
            int unidadesAEliminar = 0;

            // Paso 1: Verificamos si hay suficientes unidades en el inventario
            int totalunidades = loteCostoPromedio.Sum(l => l.unidades);
            if (unidadesAEliminar > totalunidades)
            {
                Console.WriteLine("No hay suficientes unidades en el inventario.");
                return;
            }

            // Paso 2: Calcular el costo promedio
            double costoTotal = loteCostoPromedio.Sum(l => l.precio * l.unidades);
            double costoPromedio = costoTotal / totalunidades;

            Console.WriteLine($"El costo promedio por unidad es: {costoPromedio}");

            // Paso 3: Eliminar unidades desde el inventario
            for (int i = 0; i < loteCostoPromedio.Count && unidadesAEliminar > 0; i++)
            {
                Lote lote = loteCostoPromedio[i];

                if (lote.unidades <= unidadesAEliminar)
                {
                    // Si el lote tiene menos o igual unidades de las que queremos eliminar
                    unidadesAEliminar -= lote.unidades;  // Restamos las unidades
                    loteCostoPromedio.RemoveAt(i);       // Eliminamos el lote completo
                    i--;  // Ajustamos el índice ya que eliminamos un elemento
                }
                else
                {
                    // Si el lote tiene más unidades de las que queremos eliminar
                    lote.unidades -= unidadesAEliminar;  // Restamos solo las unidades necesarias
                    unidadesAEliminar = 0;  // Ya eliminamos todas las unidades requeridas
                }
            }

            Console.WriteLine("unidades eliminadas correctamente.");
        }


    }
}
