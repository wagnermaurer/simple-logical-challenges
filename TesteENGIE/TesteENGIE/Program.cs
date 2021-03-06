using System;
using ExercioPoker.Models;

namespace ExercioPoker
{
    /*     
         SAMPLE INPUT
            2H 3D 5S 9C KD 2C 3H 4S 8C AH
            2H 4S 4C 2D 4H 2S 8S AS QS 3S
            2H 3D 5S 9C KD 2C 3H 4S 8C KH
            2H 3D 5S 9C KD 2D 3H 5C 9S KH
        
        SAMPLE OUTPUT:
            White wins.
            White wins. (Este exemplo de output está errado no enunciado, pois para a 2ª linha brancas ganham por Flush de spades)
            Black wins.
            Tie.
     */

    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Insira as cartas dos jogadores (ex: 2H 3D 5S 9C KD 2C 3H 4S 8C AH).\nPressione Ctrl+C para encerrar.");

                    var input = Console.ReadLine();
                    var blacks = new Player(input.Substring(0, 14));
                    var whites = new Player(input.Substring(15));

                    switch (blacks.CompareTo(whites))
                    {
                        case 1:
                            Console.WriteLine("BLACK wins.");
                            break;

                        case -1:
                            Console.Write("WHITE wins.");
                            break;

                        case 0:
                            Console.WriteLine("TIE.");
                            break;
                    }

                    Console.WriteLine("\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error has occurred: \n {ex}");
                    Console.ReadLine();
                }
            }
        }
    }
}
