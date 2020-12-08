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
        private static Player currentPlayer { get; set; }

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

                if (CheckWinConditions(currentPlayer))
                {
                    Console.WriteLine($"{currentPlayer.Name} Wins!");
                    GameOver = true;
                }

                if (Exit())
                    GameOver = true;

            }
            // Reset Game
            if (GameOver == true)
                board.ResetGame();

            CloseConsole();
        }
        private static bool CheckWinConditions(Player currentPlayer)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    // Check x
                    if (board.gameBoard[0, 0].Equals(currentPlayer.xo) && board.gameBoard[0, 1].Equals(currentPlayer.xo) && board.gameBoard[0, 2].Equals(currentPlayer.xo))
                        return true;
                    else if (board.gameBoard[1, 0].Equals(currentPlayer.xo) && board.gameBoard[1, 1].Equals(currentPlayer.xo) && board.gameBoard[1, 2].Equals(currentPlayer.xo))
                        return true;
                    else if (board.gameBoard[2, 0].Equals(currentPlayer.xo) && board.gameBoard[2, 1].Equals(currentPlayer.xo) && board.gameBoard[2, 2].Equals(currentPlayer.xo))
                        return true;
                }
            }
            return false;
        }
        private static PlayerStates PlayerFirstTurn() => Player1Turn == 1 ? PlayerStates.Player1Turn : PlayerStates.Player2Turn;
        private static void PlayerTurn()
        {
            switch (playerTurn)
            {
                case PlayerStates.Player1Turn:
                    Console.WriteLine($"{players[0].Name}'s Turn");
                    currentPlayer = players[0];
                    currentPlayer.xo = 'x';
                    if (PlayerSelectSpot('x'))
                        playerTurn = PlayerStates.Player2Turn;

                    break;
                case PlayerStates.Player2Turn:
                    Console.WriteLine($"{players[1].Name}'s Turn");
                    currentPlayer = players[1];
                    currentPlayer.xo = 'o';
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
                    if (board.gameBoard[y, x].Equals('e') && !board.gameBoard[y, x].Equals('x') && !board.gameBoard[y, x].Equals('o'))
                    {
                        board.gameBoard[y, x] = c;
                        valid = true;
                        return true;
                    }
                    else
                        Console.WriteLine("Invalid Space Try Again");
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is IndexOutOfRangeException)
                        Console.WriteLine("Invalid move Please Select Between [0,0] - [2,2]");
                    valid = false;
                }
            } while (!valid);
            return false;
        }
        private static Player GetPlayer()
        {
            string name = string.Empty;
            bool isplayer = false;
            Console.WriteLine("Please Enter Your Name (leave blank for bot)");
            name = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
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
