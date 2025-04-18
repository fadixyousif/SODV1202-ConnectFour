using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODV1202_ConnectFour
{
    // Winner Checker Logic Interface.
    public interface IWinLogic
    {
        bool CheckWinner(char[,] grid, char player);
    }

    // Draw Checker Logic Interface.
    public interface IDrawLogic
    {
        bool CheckDraw(char[,] grid);
    }

    public class HorizontalCheck : IWinLogic
    {

        // Horizontal Winner Check Method
        public bool CheckWinner(char[,] grid, char player)
        {
            /*
               Horizontal loop check
               start boths loops from 0 and subtract columns by 3 on condition check since we do not want to get out of bounds
               first start checking the first row and column with current loop iteration then
               make and (&&) statement by add column by n until the count of 3 since the index start array index start at 0
               if true then return true other ignore and loop again until 4 Horizontal line for either player X or O is found.
               otherwise none found when loop is done return will be false
            */
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1) - 3; column++)
                {
                    if (grid[row, column] == player && grid[row, column + 1] == player && grid[row, column + 2] == player && grid[row, column + 3] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }

    public class VerticalCheck : IWinLogic
    {

        // Vertical Winner Check Method
        public bool CheckWinner(char[,] grid, char player)
        {
            /*
               Vertical loop check
               start boths loops from 0 and subtract rows by 3 on condition check since we do not want to get out of bounds
               first start checking the first row and column with current loop iteration then
               make and (&&) statement by add row by n until the count of 3 since the index start array index start at 0
               if true then return true other ignore and loop again until 4 Vertical line for either player X or O is found.
               otherwise none found when loop is done return will be false

            */
            for (int row = 0; row < grid.GetLength(0) - 3; row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    if (grid[row, column] == player && grid[row + 1, column] == player && grid[row + 2, column] == player && grid[row + 3, column] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class DiagonalCheck : IWinLogic
    {
        // Diagonal Winner Check Method
        public bool CheckWinner(char[,] grid, char player)
        {
            /*
              Upward Diagonal loop check 
              start row loop from 3 and start column loop from 0 and subtract the amount of columns by 3 on condition check since we do not want to get out of bounds
              first start checking the first row and column with current loop iteration then 
              make and (&&) statement by subtract row by n and adding column by n until the count of 3 since the index start array index start at 0
              if true then return true other ignore and loop again until 4 Upward Diagonal line for either player X or O is found.
              otherwise none found when loop is done return will be false
            */
            for (int row = 3; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1) - 3; column++)
                {
                    if (grid[row, column] == player && grid[row - 1, column + 1] == player && grid[row - 2, column + 2] == player && grid[row - 3, column + 3] == player)
                    {
                        return true;
                    }
                }
            }


            /*
              Downward Diagonal loop check 
              start row loop from 0 and start column loop from 0 and subtract the amount of both row and column by 3 on condition check since we do not want to get out of bounds
              first start checking the first row and column with current loop iteration then 
              make and (&&) statement by adding row by n and adding column by n until the count of 3 since the index start array index start at 0
              if true then return true other ignore and loop again until 4 Downward Diagonal line for either player X or O is found.
              otherwise none found when loop is done return will be false
            */

            for (int row = 0; row < grid.GetLength(0) - 3; row++)
            {
                for (int column = 0; column < grid.GetLength(1) - 3; column++)
                {
                    if (grid[row, column] == player && grid[row + 1, column + 1] == player && grid[row + 2, column + 2] == player && grid[row + 3, column + 3] == player)
                    {
                        return true;
                    }
                }
            }

            // if none true return false.
            return false;
        }
    }

    public class DrawCheck : IDrawLogic
    {
        public bool CheckDraw(char[,] grid)
        {
            // int variable to count how many X and O are in the grid
            int DrawCounter = 0;

            /* 
                Draw loop check
                start both loops from zero and increment and make condition check by getting row from the grind and then same thing for column
                then use an if statement to check for current row and current column if either X or O are in the column count increment draw counter
                otherwise do nothing if nothing matches
            */
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    if (grid[row, column] == 'X' || grid[row, column] == 'O')
                    {
                        DrawCounter++;
                    }
                }
            }

            // if DrawCounter is equal to rows * columns, the grid is full, which means it's a draw (no winner)
            if ((grid.GetLength(0) * grid.GetLength(1)) == DrawCounter)
            {
                // return true which means grid full (no winner).
                return true;
            }
            else
            {
                // else return false grid is not full yet there could be a winner.
                return false;
            }
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public char Symbol { get; private set; }

        // constructor to set player name and symbol (like X or O)
        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        // method to ask the player to pick a column (1-7)
        public int GetMove()
        {
            int column;
            Console.Write($"{Name} ({Symbol}) - Choose column (1-7): ");

            while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
            {
                Console.Write("It's Invalid. Please try again (1-7): ");
            }

            return column - 1; // convert to 0-indexed for board logic
        }
    }

    public class ConnectFourEngine
    {
        //board size constants – 6 rows, 7 columns (standard Connect Four)
        private const int Rows = 6;
        private const int Columns = 7;

        // this method runs the full game loop and handles everything
        public void Run()
        {
            do
            {
                Console.Clear();

                // creating a blank game board (2D array of chars)
                char[,] grid = new char[Rows, Columns];

                for (int row = 0; row < Rows; row++)
                    for (int col = 0; col < Columns; col++)
                        grid[row, col] = ' ';

                // getting names from both players
                string name1 = AskForPlayerName(1);
                string name2 = AskForPlayerName(2);

                // creating two players, assigning X to player1 and O to player2
                Player player1 = new Player(name1, 'X');
                Player player2 = new Player(name2, 'O');
                Player current = player1;

                // initializing all the win checking logic (horizontal, vertical, diagonal)
                IWinLogic[] winCheckers = {
                new HorizontalCheck(),
                new VerticalCheck(),
                new DiagonalCheck()
                };

                // drawing checker just to see if board is full with no winner
                IDrawLogic drawChecker = new DrawCheck();

                bool gameOver = false;

                // this loop runs the turns until someone wins or it’s a draw
                while (!gameOver)
                {
                    PrintGrid(grid);
                    int col = current.GetMove();

                    // if column is full, show message and retry
                    if (!DropPiece(grid, col, current.Symbol))
                    {
                        Console.WriteLine("Column is full. Try again.");
                        continue;
                    }

                    // checking if current player has won after this move
                    foreach (var checker in winCheckers)
                    {
                        if (checker.CheckWinner(grid, current.Symbol))
                        {
                            PrintGrid(grid);
                            Console.WriteLine($"\n{current.Name} wins!\nIt's a Connect Four!!!");
                            gameOver = true;
                            break;
                        }
                    }

                    // if no winner, check if the board is full = draw
                    if (!gameOver && drawChecker.CheckDraw(grid))
                    {
                        PrintGrid(grid);
                        Console.WriteLine("\nIt's a draw!");
                        gameOver = true;
                    }

                    current = (current == player1) ? player2 : player1;
                }

                Console.Write("\nDo you want to play again? (y/n): ");
            }
            while (Console.ReadLine().ToLower() == "y");

            Console.WriteLine("Thanks for playing!");
        }

        // this draws the board on the console screen
        private void PrintGrid(char[,] grid)
        {
            Console.Clear();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                Console.Write("| ");

                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write((grid[row, col] == ' ' ? '#' : grid[row, col]) + " ");
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("  1 2 3 4 5 6 7");
        }

        // tries to drop the current player's disc in the chosen column
        private bool DropPiece(char[,] grid, int col, char symbol)
        {
            // start from bottom row and go up
            for (int row = grid.GetLength(0) - 1; row >= 0; row--)
            {
                if (grid[row, col] == ' ')
                {
                    grid[row, col] = symbol;
                    return true;
                }
            }
            return false;
        }

        // asks for the player name and makes sure it's not empty
        private string AskForPlayerName(int playerNumber)
        {
            string name;
            do
            {
                Console.Write($"Enter name for Player {playerNumber}: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Please enter a name. It can't be empty.");
                }
            }
            while (string.IsNullOrWhiteSpace(name));

            return name;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new ConnectFourEngine().Run();
        }
    }
}
