using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Carrera_de_caballos
{
    class Program
    {
        static List<(string Nombre, int Posicion)> resultados = new List<(string, int)>();
        static object lockObject = new object();

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a la carrera de F1!");

            // Definir los nombres de los caballos
            string[] nombresCaballos = { "Colapinto", "Hamillton", "Verstappen", "Charles LeClerc", "Rayo Mcqueen" };

            // Listar los nombres de los caballos
            Console.WriteLine("\nLista de caballos participantes:");
            for (int i = 0; i < nombresCaballos.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {nombresCaballos[i]}");
            }

            // Esperar el comando de lanzamiento
            Console.WriteLine("\nEscriba 'lanzar' para comenzar la carrera:");
            while (Console.ReadLine().ToLower() != "lanzar")
            {
                Console.WriteLine("Comando no reconocido. Por favor, escriba 'lanzar' para comenzar:");
            }

            Console.WriteLine("\n¡La carrera ha comenzado!\n");

            // Crear y iniciar un hilo para cada caballo
            List<Thread> hilos = new List<Thread>();
            foreach (string nombre in nombresCaballos)
            {
                Thread hilo = new Thread(() => CorrerCaballo(nombre));
                hilos.Add(hilo);
                hilo.Start();
            }

            // Esperar a que todos los hilos terminen
            foreach (Thread hilo in hilos)
            {
                hilo.Join();
            }

            // Mostrar resultados
            Console.WriteLine("\nResultados de la carrera:");
            for (int i = 0; i < resultados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {resultados[i].Nombre}");
            }

            // Evitar que se cierre la consola
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static Random rnd = new Random();

        static void CorrerCaballo(string nombre)
        {
            int distanciaTotal = 0;
            const int metaFinal = 100;

            while (distanciaTotal < metaFinal)
            {
                // Simular el avance del caballo
                int avance = rnd.Next(1, 6);
                distanciaTotal += avance;

                // Mostrar progreso
                Console.WriteLine($"{nombre} ha avanzado a {distanciaTotal} metros.");

                // Pausa para simular tiempo entre avances
                Thread.Sleep(rnd.Next(100, 1000));
            }

            // Registrar la posición final del caballo
            lock (lockObject)
            {
                resultados.Add((nombre, resultados.Count + 1));
            }
        }
    }
}
 