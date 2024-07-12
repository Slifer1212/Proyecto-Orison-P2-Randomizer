using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static System.Console;

namespace RandomizerApp
{
    class Seleccionador
    {
        private List<string> listado_Estudiantes;
        private Random rnd = new Random();
        private static List<string> nombresSeleccionados = new List<string>();
        private static List<string> desarrolladoresEnVivo = new List<string>();
        private static List<string> facilitadoresEjercicio = new List<string>();

        public Seleccionador(List<string> estudiantes)
        {
            listado_Estudiantes = estudiantes;
            desarrolladoresEnVivo = new List<string>(estudiantes);
            facilitadoresEjercicio = new List<string>(estudiantes);
        }

        public void mostrarEstudiante()
        {
            Clear();
            if (desarrolladoresEnVivo.Count < 1 || facilitadoresEjercicio.Count < 1)
            {
                WriteLine("No hay suficientes estudiantes");
                return;
            }

            int desarrolladorIndex;
            int facilitadorEjercicioIndex;

            string desarrolladorEnVivo;
            string facilitadorEjercicio;

            do
            {
                desarrolladorIndex = rnd.Next(desarrolladoresEnVivo.Count);
                desarrolladorEnVivo = desarrolladoresEnVivo[desarrolladorIndex];
                facilitadorEjercicioIndex = rnd.Next(facilitadoresEjercicio.Count);
                facilitadorEjercicio = facilitadoresEjercicio[facilitadorEjercicioIndex];
            } while (desarrolladorEnVivo == facilitadorEjercicio);

            nombresSeleccionados.Add($"Desarrollador en vivo: {desarrolladorEnVivo}");
            nombresSeleccionados.Add($"Facilitador Ejercicio: {facilitadorEjercicio}");

            animacion_estudiantes(desarrolladorEnVivo, facilitadorEjercicio, desarrolladorIndex, facilitadorEjercicioIndex);

            desarrolladoresEnVivo.Remove(desarrolladorEnVivo);
            facilitadoresEjercicio.Remove(facilitadorEjercicio);
        }

        private void animacion_estudiantes(string desarrolladorEnVivo, string facilitadorEjercicio, int desarrolladorIndex, int facilitadorEjercicioIndex)
        {
            for (int i = 0; i < 50; i++) 
            {
                int highlightedIndex = rnd.Next(listado_Estudiantes.Count);
                Console.Clear();
                PrintNombresConResaltado(listado_Estudiantes, highlightedIndex);
                Thread.Sleep(200); 
            }

            Console.Clear();
            for (int i = 0; i < listado_Estudiantes.Count; i++)
            {
                if (listado_Estudiantes[i] == desarrolladorEnVivo)
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (listado_Estudiantes[i] == facilitadorEjercicio)
                {
                    ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    ResetColor();
                }
                WriteLine(listado_Estudiantes[i]);
            }
            Console.ResetColor();
            WriteLine("\nDesarrollador en vivo: " + desarrolladorEnVivo);
            WriteLine("Facilitador Ejercicio: " + facilitadorEjercicio);

            listado_Estudiantes.Remove(desarrolladorEnVivo);
            listado_Estudiantes.Remove(facilitadorEjercicio);
        }

        static void PrintNombresConResaltado(List<string> nombres, int highlightedIndex)
        {
            for (int i = 0; i < nombres.Count; i++)
            {
                if (i == highlightedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ResetColor();
                }
                Console.WriteLine(nombres[i]);
            }
            Console.ResetColor();
        }

        public void verDesarrolladoresEnvivo()
        {
            Clear();

            WriteLine("Desarrolladores en vivo:");
            foreach (var estudiante in desarrolladoresEnVivo)
            {
                WriteLine(estudiante);
            }
            WriteLine("Presione cualquier tecla para salir");
            ReadKey(true);
        }

        public void verFacilitadores()
        {
            Clear();

            WriteLine("Facilitadores de Ejercicio:");
            foreach (var estudiante in facilitadoresEjercicio)
            {
                WriteLine(estudiante);
            }
            WriteLine("Presione cualquier tecla para salir");
            ReadKey(true);
        }

        public void agregarEstudiante()
        {
            Clear();
            WriteLine("Introduzca el nombre que desea introducir:");
            ForegroundColor = ConsoleColor.Green;
            string nombre = ReadLine()!;
            ResetColor();

            if (string.IsNullOrEmpty(nombre))
            {
                WriteLine("No ha agregado nada socio");
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Presione cualquier tecla para salir");
                ResetColor();
                ReadKey(true);
            }
            else if (listado_Estudiantes.Contains(nombre))
            {
                WriteLine("Este estudiante ya está en la lista");
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("Presione cualquier tecla para salir");
                ResetColor();
                ReadKey(true);
            }
            else
            {
                listado_Estudiantes.Add(nombre);
                desarrolladoresEnVivo.Add(nombre);
                facilitadoresEjercicio.Add(nombre);
                ForegroundColor = ConsoleColor.Green;
                WriteLine($"{nombre} ha sido agregado");
                ResetColor();
                WriteLine("Presione cualquier tecla para salir");
                ReadKey(true);
            }
        }

        public void eliminarEstudiante()
        {
            Clear();
            WriteLine("Introduzca el nombre que desea eliminar:");
            string nombre = ReadLine()!.ToUpper();

            if (string.IsNullOrEmpty(nombre))
            {
                WriteLine("No ha introducido ningún nombre.");
                WriteLine("Presione cualquier tecla para salir.");
                ReadKey(true);
            }
            else
            {
                int conteo = listado_Estudiantes.Count;
                listado_Estudiantes.RemoveAll(estudiante => estudiante.Split(' ')[0].Equals(nombre, StringComparison.OrdinalIgnoreCase));

                if (conteo == listado_Estudiantes.Count)
                {
                    WriteLine("Estudiante no encontrado.");
                }
                else
                {
                    desarrolladoresEnVivo.RemoveAll(estudiante => estudiante.Split(' ')[0].Equals(nombre, StringComparison.OrdinalIgnoreCase));
                    facilitadoresEjercicio.RemoveAll(estudiante => estudiante.Split(' ')[0].Equals(nombre, StringComparison.OrdinalIgnoreCase));
                    WriteLine($"{nombre} ha sido eliminado.");
                }
                WriteLine("Presione cualquier tecla para salir.");
                ReadKey(true);
            }
        }

        public void generarCSV()
        {
            Clear();

            string filePath = "nombresSeleccionados.csv";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Rol,Nombre");

                foreach (var nombres in nombresSeleccionados)
                {
                    string[] parts = nombres.Split(":");
                    writer.WriteLine($"{parts[0].Trim()},{parts[1].Trim()}");
                }
            }
            WriteLine($"CSV generado en {filePath}");
            WriteLine("Presione cualquier tecla para salir");
            ReadKey(true);
        }
    }
}
