using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Board
    {
        // Game board
        public char[,] gameBoard = new char[,]
           {
                {'0','1','2'},
                {'3','4','5'},
                {'6','7','8'}
           };
        public void ResetGame()
        {
            SetBoard();
        }
        public void SetBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    gameBoard[i, j] = 'e';
        }
        public void DisplayBoard()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                    Console.Write($"{gameBoard[i, j]}\t");
                Console.WriteLine("\n");
            }
        }
    }
}
