using System;
using System.Collections.Generic;
using static System.Console;
using NAudio.Wave;

namespace RandomizerApp
{
    class Juego
    {
        private static List<string> estudiantes = new List<string>
        { 
        "Abdul Djalo Encarnacion", "Awilka Jerome Puente",
        "Coralis Natalie Cordones Santos", "Cristian Luna Rosario", "Deybby Angel Rosario Almonte",
        "Enmanuel Alfonso Ferreras Vargas", "Esteban L?Pez Madera", "Felix David Reyes Duarte",
        "Gabriel Jose Suarez Peralta", "Geraldo Alfredo Cabrera Puentes", "Isaac Cabrera Silverio",
        "Jean Emmanuel Castellanos Jimenez", "Jonaifry Rodríguez De Jesús", "Jose Gerardo Severino Calderon",
        "Jowell Sebastian Ramirez Wilson", "Juan David Vásquez Alcántara", "Levigne Fresco",
        "Lissa Marie Gonz?Lez Feliz", "Ludwing Esaydel Santana Espinal", "Maria Franchesca Beltre Orozco",
        "Melvin Emmanuel Marte Cuevas", "Pilar Medina Garc?A", "Porfirio Ramirez", "Wilbert Jos? Santos P?Rez",
        "Yudit Maria Velasquez Matias", "Yunior Alexander Mo Mo"
        };
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;

        public void Start()
        {
            CorrerMenuPrincipal();
        }

        private void CorrerMenuPrincipal()
        {
            string prompt = @"                    
_____        _                 _____                    _                    _                
|  __ \      | |               |  __ \                  | |                  (_)                
| |__) | ___ | |_  _ __  ___   | |__) | __ _  _ __    __| |  ___   _ __ ___   _  ____ ___  _ __
|  _  / / _ \| __|| '__|/ _ \  |  _  / / _` || '_ \  / _` | / _ \ | '_ ` _ \ | ||_  // _ \| '__|
| | \ \|  __/| |_ | |  | (_) | | | \ \| (_| || | | || (_| || (_) || | | | | || | / /|  __/| |  
|_|  \_\\___| \__||_|   \___/  |_|  \_\\__,_||_| |_| \__,_| \___/ |_| |_| |_||_|/___|\___||_|  
                                                                                                            
Bienvenido a mi Randomizer retro, usa las flechas para moverte, dale a enter para usar las opciones y 
elige la música de tu preferencia";   
            string[] opciones = { "Vamos a Randomizar", "Ver nombres de los estudiantes",
            "Agregar Estudiante", "Eliminar estudiante",
            "Elige un soundtrack baby", "Parar la música","Generar CSV", "Salir" };

            Menu menuPrincipal = new Menu(prompt, opciones);
            int seleccionado = menuPrincipal.Run();

            switch (seleccionado)
            {
                case 0:
                    Randomizar();
                    break;
                case 1:
                    NombresEstudiantes();
                    CorrerMenuPrincipal();
                    break;
                case 2:
                    AgregarNombre();
                    CorrerMenuPrincipal();
                    break;
                case 3:
                    eliminarEstudiante();
                    CorrerMenuPrincipal();
                    break;

                case 4:
                    EligeSoundTrack();
                    CorrerMenuPrincipal();
                    break;
                case 5:
                    PararMusica();
                    CorrerMenuPrincipal();
                    break;
                case 6:
                    generarCSV();
                    CorrerMenuPrincipal();
                    break;
                case 7:
                    Salir();
                    break;
            }
        }

        private void Salir()
        {
            WriteLine("Presione cualquier tecla para salir");
            ReadKey(true);
            Environment.Exit(0);
        }

        private void Randomizar()
        {
            Clear();
            Seleccionador seleccionador = new Seleccionador(estudiantes);
            seleccionador.seleccionadorEstudiantes();
            
            WriteLine("Presione cualquier tecla para continuar");
            ReadKey(true); 
            
            string prompt = "Desea continuar?";
            string[] opciones = { "Si", "No" };
            
            Menu menuEstudiantes = new Menu(prompt, opciones);
            int seleccionado = menuEstudiantes.Run();

            switch (seleccionado)
            {
                case 0:
                    Randomizar(); 
                    break;
                case 1:
                    CorrerMenuPrincipal(); 
                    break;
            }
        }

        private void NombresEstudiantes()
        {
            Seleccionador seleccionador = new Seleccionador(estudiantes);
            seleccionador.mostrarEstudiantes();
        }

        private void AgregarNombre()
        {
            Seleccionador seleccionador = new Seleccionador(estudiantes);
            seleccionador.agregarEstudiante();
        }

        private void EligeSoundTrack()
        {
            string[] opciones = { "Tetris", "Mario", "Contra", "Volver al menu anterior" };
            string prompt = "Elige un tema retro";
            Menu menuMusica = new Menu(prompt, opciones);
            int seleccionado = menuMusica.Run();

            switch (seleccionado)
            {
                case 0:
                    ReproducirMusica(@"C:\Users\Supre\Desktop\Proyecto Orison P2\Sonidos\Original Tetris theme Tetris Soundtrack.mp3");
                    break;
                case 1:
                    ReproducirMusica(@"C:\Users\Supre\Desktop\Proyecto Orison P2\Sonidos\Super Mario Bros Theme Song.mp3");
                    break;
                case 2:
                    ReproducirMusica(@"C:\Users\Supre\Desktop\Proyecto Orison P2\Sonidos\Contra NES Music  Jungle Theme.mp3");
                    break;
                case 3:
                    return; 
            }
        }

        private void ReproducirMusica(string rutaArchivo)
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                audioFile?.Dispose();
            }

            audioFile = new AudioFileReader(rutaArchivo);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            WriteLine("Presione cualquier tecla para volver al menú principal...");
            ReadKey(true);
        }

        private void PararMusica()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                audioFile?.Dispose();

                outputDevice = null;
                audioFile = null;
                WriteLine("La música ha sido detenida.");
            }
            else
            {
                WriteLine("No hay música reproduciéndose.");
            }
            WriteLine("Presione cualquier tecla para continuar...");
            ReadKey(true);
        }
        private void generarCSV()
        {
            Seleccionador seleccionador = new Seleccionador(estudiantes);
            seleccionador.generarCSV();
        }
        static void eliminarEstudiante()
        {
            Seleccionador seleccionador = new Seleccionador(estudiantes);
            seleccionador.eliminarEstudiante();
        }

    }
}
