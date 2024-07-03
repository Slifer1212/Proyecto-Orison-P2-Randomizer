    using System.Data.Common;
    using System.Threading.Tasks.Dataflow;
    using static System.Console;


    namespace RandomizerApp
    
    {
        class Seleccionador
        {
            private List<string>listado_Estudiantes;
            private Random rnd = new Random();

            private static List<string> nombresSeleccionados = new List<string>();
            private static List<string> desarrolladoresEnVivo = new List<string>();
            private static List<string> facilitadorEjercicio = new List<string>();

            public Seleccionador(List<string> estudiantes)
            {
                listado_Estudiantes = estudiantes;
            }

            public void mostrarEstudiante()
            {
               if (listado_Estudiantes.Count < 2 )
               {
                    WriteLine("No hay suficientes estudiantes");
                    return;
               }
               string primerEstudiante = seleccionadorEstudiante(listado_Estudiantes , desarrolladoresEnVivo);
               string segundoEstudiante = seleccionadorEstudiante (listado_Estudiantes , facilitadorEjercicio , primerEstudiante);

                nombresSeleccionados.Add($"Programador en vivo,{primerEstudiante}");
                nombresSeleccionados.Add($"Facilitador ejercicio,{segundoEstudiante}");
                
                WriteLine($"Programador en vivo: {primerEstudiante}");Thread.Sleep(2000);
                WriteLine($"Facilitador ejercicio: {segundoEstudiante}");Thread.Sleep(2000);
            } 

            private string seleccionadorEstudiante(List<string>estudiantes , List<string> historial, string estudiante_excluido = null)
            {
                List<string> estudiante_disponible = new List<string>(estudiantes);

                estudiante_disponible.RemoveAll(estudiante => historial.Contains(estudiante) || 
                estudiante == estudiante_excluido);

                if(estudiante_disponible.Count == 0)
                {
                    throw new Exception("No hay suficientes estudiantes");
                }
                int estudianteIndex = rnd.Next(estudiante_disponible.Count);
                string seleccionado = estudiante_disponible[estudianteIndex];
                
                return seleccionado;
            }

            
            public void listaEstudiantes()
            {
                Clear();

                WriteLine("Sus estudiantes son: ");
                foreach (var estudiantes in listado_Estudiantes)
                {
                    WriteLine(estudiantes);
                }
                WriteLine("Presiones cualquier tecla para salir");
                ReadKey(true);
            }

            public void agregarEstudiante()
            {
                Clear();
                WriteLine("Introduzca el nombre que desea introducir");
                ForegroundColor = ConsoleColor.Green;
                string nombre = ReadLine()!;
                ResetColor();

                if(string.IsNullOrEmpty(nombre))
                {
                    WriteLine("No ha agregado nada socio");
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Presione cualquier tecla para salir");
                    ResetColor();
                    ReadKey(true);
                }else if(listado_Estudiantes.Contains(nombre))
                {
                    WriteLine("Este estudiante ya esta en la lista");
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Presione cualquier tecla para salir");
                    ResetColor();
                    ReadKey(true);
                }
                else
                {
                    listado_Estudiantes.Add(nombre);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine($"{nombre} ha sido agregado");
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
                    WriteLine("No ha introducido ningun nombre.");
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
                using(StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Rol,Nombre");

                    foreach(var nombres in nombresSeleccionados)
                    {
                        string [] parts = nombres.Split(",");
                        writer.WriteLine($"{parts[0].Trim()} , {parts[1].Trim()}");
                    }
                }
                WriteLine($"CSV generado en {filePath}");
                WriteLine("Presione cualquier tecla para salir");
                ReadKey(true);
            }
        }
    }   