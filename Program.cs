using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Program
    {
        private static bool? GameOver { get; set; } = null;
        private static List<Player> players = new List<Player>(2);
        private static Random rand = new Random();
        private static Board board = new Board();
        private enum PlayerStates { Player1Turn, Player2Turn }
        private static PlayerStates playerTurn;
        private static readonly int Player1Turn = Player1First();

        [STAThread()]
        private static void Main(string[] args)
        {
            for (int i = 0; i < players.Capacity; i++)
                players.Add(GetPlayer());
            // Init Check
            if (GameOver == null)
            {
                PlayerFirstTurn();
                board.SetBoard();
                GameOver = false;
            }
            // Game Loop
            while (GameOver == false)
            {
                board.DisplayBoard();
                PlayerTurn();

                if (Exit())
                    GameOver = true;

            }
            // Reset Game
            if (GameOver == true)
                board.ResetGame();

            CloseConsole();
        }
        private static PlayerStates PlayerFirstTurn()
        {
            if (Player1Turn == 1)
                return PlayerStates.Player1Turn;
            return PlayerStates.Player2Turn;
        }
        private static void PlayerTurn()
        {

            switch (playerTurn)
            {
                case PlayerStates.Player1Turn:
                    Console.WriteLine($"{players[0].Name}'s Turn");
                    if (PlayerSelectSpot('x'))
                        playerTurn = PlayerStates.Player2Turn;

                    break;
                case PlayerStates.Player2Turn:
                    Console.WriteLine($"{players[1].Name}'s Turn");
                    if (PlayerSelectSpot('o'))
                        playerTurn = PlayerStates.Player1Turn;
                    break;
            }


        }
        private static bool PlayerSelectSpot(char c)
        {
            bool valid = false;
            do
            {
                try
                {

                    Console.WriteLine("Please Choose the x row to go");
                    sbyte x = Convert.ToSByte(Console.ReadLine());
                    Console.WriteLine("Please Choose the y row to go");
                    sbyte y = Convert.ToSByte(Console.ReadLine());
                    if (board.gameBoard[x, y].Equals('e'))
                    {
                        board.gameBoard[y, x] = c;
                        valid = true;
                        return true;
                    }
                    else
                        Console.WriteLine("Invalid Space Try Again");

                }
                catch (IndexOutOfRangeException) { Console.WriteLine("Invalid move"); valid = false; }
            } while (!valid);
            return false;
        }
        private static Player GetPlayer()
        {
            string name = string.Empty;
            bool isplayer = false;
            Console.WriteLine("Please Enter Your Name (leave blank for bot)");
            name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
                isplayer = false;
            else
                isplayer = true;


            return new Player(name, isplayer);
        }
        private static int Player1First() => rand.Next(0, 2);
        private static bool Exit()
        {
            var key = Console.ReadKey();
            Console.Clear();
            if (key.Key == ConsoleKey.Escape)
                return true;
            return false;
        }
        private static void CloseConsole()
        {
            Console.Write("Press Any Key To Exit...");
            Console.ReadKey();
        }
    }
}
