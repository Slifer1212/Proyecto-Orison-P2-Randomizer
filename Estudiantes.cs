using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using NAudio.Wave;

class Seleccionador
{
    private List<string> listado_Estudiantes;
    private Random rnd = new Random();
    private static List<string> nombresSeleccionados = new List<string>();
    private List<string> desarrolladoresEnVivo;
    private List<string> facilitadoresEjercicio;

    public Seleccionador(List<string> estudiantes)
    {
        listado_Estudiantes = estudiantes;
        desarrolladoresEnVivo = new List<string>(listado_Estudiantes);
        facilitadoresEjercicio = new List<string>(listado_Estudiantes);
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
        animacion_estudiantes(desarrolladorEnVivo , facilitadorEjercicio,desarrolladorIndex, facilitadorEjercicioIndex);

        desarrolladoresEnVivo.Remove(desarrolladorEnVivo);
        facilitadoresEjercicio.Remove(facilitadorEjercicio);
    }
    private void animacion_estudiantes(string desarrolladorEnVivo, string facilitadorEjercicio , int desarrolladorIndex,int facilitadorEjercicioIndex)
    {
        
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
        WriteLine("Facilitadores de ejercicio:");

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
        WriteLine("Introduzca el nombre que desea introducir");
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
            WriteLine("Presione cualquier tecla para salir");
            ReadKey(true);
        }ResetColor();
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
            desarrolladoresEnVivo.RemoveAll(estudiante => estudiante.Split(' ')[0].Equals(nombre, StringComparison.OrdinalIgnoreCase));
            facilitadoresEjercicio.RemoveAll(estudiante => estudiante.Split(' ')[0].Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (conteo == listado_Estudiantes.Count)
            {
                WriteLine("Estudiante no encontrado.");
            }
            else
            {
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
                writer.WriteLine($"{parts[0].Trim()}: {parts[1].Trim()}");
            }
        }
        WriteLine($"CSV generado en {filePath}");
        WriteLine("Presione cualquier tecla para salir");
        ReadKey(true);
    }
}