namespace Laberinto
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
             Ejercicio: Juego de Laberinto

             Crear un juego de laberinto en C# que permita a un jugador moverse a través de un
             laberinto en busca de una salida. El laberinto estará representado por una
             matriz bidimensional donde algunos elementos representarán obstáculos (paredes) y
             otros elementos representarán pasillos por los que el jugador puede moverse.

             Requisitos del juego:

             El laberinto debe ser representado por una matriz bidimensional y creado de forma manual, 
             donde los elementos pueden ser números enteros, donde:

             0 representa un pasillo (el jugador puede moverse aquí).

             1 representa una pared (el jugador no puede pasar por aquí).

             2 representa la posición inicial del jugador.

             3 representa la salida del laberinto.

             4 representa a un enemigo, si el jugador va hacia esta posicion muere y pierde xD

             El jugador deberá moverse por el laberinto utilizando las teclas de dirección 
             (arriba, abajo, izquierda, derecha). Debe haber un límite en los bordes del laberinto,
             por lo que el jugador no puede salirse de los límites.

             Cuando el jugador llega a la salida (elemento 3 en la matriz), se muestra un mensaje 
             de victoria y el juego termina.

             Se debe mostrar visualmente el estado del laberinto en la consola y actualizarlo en 
             tiempo real para reflejar la posición actual del jugador.
             */


            //Variables
            bool gameOver = false;
            bool victory = false;
            ConsoleKey keyInfo = 0;


            //Instanciamos un laberinto
            Laberinto laberinto = new();


            //Iniciamos el juego
            Console.WriteLine("Que comience el juego!");
            LoadingSeconds(2);
            while (!victory && keyInfo != ConsoleKey.Q && !gameOver)
            {

                //Refrescamos la pantalla
                Console.Clear();

                //Mostramos el estado actual del laberinto
                laberinto.DisplayLabyrinth();


                //Pedimos un paso
                keyInfo = laberinto.RequestPassage();

                //Actualizamos el laberinto segun el paso
                laberinto.UpdateLaberinto(keyInfo, ref gameOver);

                //verificamos si el usuario llego a la salido
                victory = laberinto.isVictory();

            }


            //Para mas suspenso xD
            Console.Clear();
            LoadingSeconds(2);


            //Mostramos mensaje de victoria
            if (victory)
            {
                laberinto.DisplayLabyrinth();
                Console.WriteLine("¡Felicidades, has ganado!");

            }
            //Mostramos mensaje de derrota
            else if (gameOver)
            {
                Console.WriteLine("Game over, has perdido");
            }
            else
            {
                Console.WriteLine("Has salido del juego.");
            }

            Console.WriteLine("¡Gracias por jugar!");


            Console.ReadKey();
        }

        //Metodo auxiliar para validar opcion
        public static int GetValidOption(string message)
        {
            int op;

            do
            {
                Console.Write(message);

                //se repite hasta que se digite un int o sea menor o igual a 0
            } while ((!int.TryParse(Console.ReadLine(), out op)) || op <= 0 || op > 3);

            return op;
        }

        //Metodo auxiliar para esperar n segundos
        public static void LoadingSeconds(int segundos)
        {
            Thread.Sleep(segundos * 1000);
        }
    }


    //Clase
    class Laberinto
    {

        //Propiedades
        private int[,] _laberinto;
        private int _posRowPlayer = 0;
        private int _posColPlayer = 0;


        //Constructor
        public Laberinto()
        {

            //Damos la bienvenida
            Console.WriteLine("Bienvenido al juego del laberinto!");
            Program.LoadingSeconds(1);


            //Pedimos la dificultad del laberinto
            Console.WriteLine("1. Facil");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. Dificil");
            int op = Program.GetValidOption("Elija la dificultad: ");



            //Creamos el laberinto 
            _laberinto = CreateLabyrinth(op);

            //inicializamos la posicion del jugador
            _posRowPlayer = 0;
            _posColPlayer = 0;
        }



        //Metodo para asignar tamaño del laberinto según la dificultad
        int[,] CreateLabyrinth(int op)
        {


            //Cosas a usar
            int[,] LaberintoCreado;
            Random randy = new();
            int TipoLaberinto;


            //Asigamos un valor random entre 1 y 3 (min 1 y max 4, excluye al 4)
            TipoLaberinto = randy.Next(1, 4);


            //Verificamos op
            if (op == 1)
            {

                //Asignamos el laberinto segun el tipo
                LaberintoCreado = TipoLaberinto switch
                {
                    1 => new int[,]
                    {
                   { 2, 0, 0, 0, 0 },
                   { 1, 1, 0, 1, 0 },
                   { 0, 0, 0, 0, 0 },
                   { 0, 1, 1, 1, 1 },
                   { 0, 0, 0, 0, 3 }
                    },

                    2 => new int[,]
                    {
                   { 2, 0, 0, 0, 0 },
                   { 0, 1, 1, 1, 0 },
                   { 0, 0, 0, 0, 0 },
                   { 1, 1, 0, 0, 1 },
                   { 0, 0, 1, 0, 3 }
                    },

                    3 => new int[,]
                    {
                   { 2, 1, 0, 0, 1 },
                   { 0, 0, 0, 1, 0 },
                   { 1, 0, 0, 0, 0 },
                   { 0, 0, 1, 1, 1 },
                   { 0, 0, 0, 0, 3 }
                    },

                    _ => throw new NotImplementedException(),
                };


            }
            else if (op == 2)
            {
                //Asignamos el laberinto segun el tipo
                LaberintoCreado = TipoLaberinto switch
                {
                    1 => new int[,]
                   {
                  { 2, 1, 0, 0, 0, 0 },
                  { 0, 1, 0, 1, 0, 1 },
                  { 0, 0, 0, 1, 0, 1 },
                  { 1, 1, 0, 1, 0, 0 },
                  { 1, 0, 0, 1, 1, 0 },
                  { 1, 1, 1, 0, 0, 3 }
                   },

                    2 => new int[,]
                   {
                  { 2, 1, 0, 0, 0, 0 },
                  { 0, 0, 1, 1, 0, 1 },
                  { 1, 0, 0, 1, 0, 1 },
                  { 1, 1, 0, 0, 0, 0 },
                  { 0, 0, 1, 1, 1, 0 },
                  { 0, 0, 0, 0, 0, 3 }
                   },

                    3 => new int[,]
                   {
                  { 2, 0, 0, 1, 0, 0 },
                  { 1, 1, 0, 1, 0, 1 },
                  { 0, 0, 0, 1, 0, 1 },
                  { 0, 1, 1, 0, 0, 0 },
                  { 0, 0, 0, 0, 1, 0 },
                  { 0, 0, 0, 1, 1, 3 }
                   },

                    _ => throw new NotImplementedException(),
                };

            }
            else
            {
                //Asignamos el laberinto segun el tipo
                LaberintoCreado = TipoLaberinto switch
                {
                    1 => new int[,]
                   {
                  { 2, 1, 0, 0, 0, 0, 1 },
                  { 0, 1, 0, 1, 1, 0, 0 },
                  { 0, 0, 0, 1, 0, 4, 0 },
                  { 1, 0, 1, 3, 0, 0, 0 },
                  { 0, 0, 0, 1, 1, 0, 1 },
                  { 0, 1, 1, 0, 0, 0, 0 },
                  { 0, 0, 0, 0, 1, 0, 0 }
                   },

                    2 => new int[,]
                   {
                  { 2, 0, 0, 0, 0, 0, 0 },
                  { 0, 1, 0, 1, 1, 1, 0 },
                  { 0, 0, 1, 1, 0, 0, 0 },
                  { 1, 1, 1, 0, 0, 1, 1 },
                  { 0, 0, 0, 4, 0, 0, 0 },
                  { 0, 1, 0, 0, 1, 1, 0 },
                  { 3, 1, 1, 0, 0, 0, 0 }
                   },

                    3 => new int[,]
                   {
                  { 2, 1, 3, 0, 0, 0, 0 },
                  { 0, 0, 1, 1, 0, 4, 0 },
                  { 1, 0, 0, 0, 1, 0, 0 },
                  { 0, 1, 1, 0, 1, 1, 0 },
                  { 0, 0, 0, 0, 1, 0, 0 },
                  { 1, 0, 1, 1, 1, 0, 1 },
                  { 1, 0, 0, 0, 0, 0, 0 }
                   },
                    _ => throw new NotImplementedException(),
                };


            }

            return LaberintoCreado;
        }





        //Metodo para mostrar el laberinto
        public void DisplayLabyrinth()
        {
            Console.WriteLine("Laberinto: ");
            Console.WriteLine();

            for (int i = 0; i < _laberinto.GetLength(0); i++)
            {
                for (int j = 0; j < _laberinto.GetLength(1); j++)
                {
                    if (i == _posRowPlayer && j == _posColPlayer)
                    {
                        Console.Write("P ");
                    }
                    else if (_laberinto[i, j] == 0)
                    {
                        Console.Write("  ");
                    }
                    else if (_laberinto[i, j] == 1)
                    {
                        Console.Write("█ ");
                    }
                    else if (_laberinto[i, j] == 3)
                    {
                        Console.Write("E ");
                    }
                    else if (_laberinto[i, j] == 4)
                    {
                        Console.Write("X ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();

        }


        //Metodo para pedir un paso
        public ConsoleKey RequestPassage()
        {

            //Objeto a usar
            ConsoleKey key;


            //Pedimos un paso
            key = GetValidConsoleKey("Presiona las teclas de dirección (↑, ↓, ←, →) para moverte. Presiona 'q' para salir: ");


            //Salto de linea
            Console.WriteLine();


            //Devolvemos la tecla presionada
            return key;

        }


        //Metodo para actualizar el laberinto segun el paso
        public void UpdateLaberinto(ConsoleKey key, ref bool gameOver)
        {

            //Copiamos la posición actual del jugador
            int newFila = _posRowPlayer;
            int newColumna = _posColPlayer;


            //Analizamos el pasos
            if (key == ConsoleKey.UpArrow && _posRowPlayer > 0)
            {
                newFila--;
            }
            else if (key == ConsoleKey.DownArrow && _posRowPlayer < _laberinto.GetLength(0) - 1)
            {
                newFila++;
            }
            else if (key == ConsoleKey.LeftArrow && _posColPlayer > 0)
            {
                newColumna--;
            }
            else if (key == ConsoleKey.RightArrow && _posColPlayer < _laberinto.GetLength(1) - 1)
            {
                newColumna++;
            }



            //Verificamos si el movimiento es válido
            if (_laberinto[newFila, newColumna] == 0 || _laberinto[newFila, newColumna] == 3)
            {
                //Actualizamos coordenadas
                _laberinto[_posRowPlayer, _posColPlayer] = 0;
                _posRowPlayer = newFila;
                _posColPlayer = newColumna;
            }
            else if (_laberinto[newFila, newColumna] == 4)
            {
                gameOver = true;
            }
        }



        //Metodo para verificar la posicion del jugador en base a la salida
        public bool isVictory()
        {
            if (_posRowPlayer >= 0 && _posRowPlayer < _laberinto.GetLength(0) &&
            _posColPlayer >= 0 && _posColPlayer < _laberinto.GetLength(1))
            {
                return _laberinto[_posRowPlayer, _posColPlayer] == 3;
            }
            return false;
        }



        //Metodo auxiliar para devolver laberinto
        public int[,] GetLaberinto() => _laberinto;



        //Metodo auxiliar para verificar tecla
        public static ConsoleKey GetValidConsoleKey(string message)
        {
            //Variables a usar
            ConsoleKey key;
            ConsoleKey[] validKeys = { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow, ConsoleKey.Q };

            do
            {
                Console.Write(message);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;
                Console.WriteLine();

                //Mostramos mensaje de error
                if (!validKeys.Contains(key)) Console.WriteLine("Tecla no válida. Inténtalo de nuevo.");

            } while (!validKeys.Contains(key));

            return key;
        }
    }
}