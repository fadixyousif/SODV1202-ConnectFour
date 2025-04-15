using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODV1202_ConnectFour
{
    public interface IWinLogic
    {
        bool CheckWinner(char[,] grid, char player);
    }

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
            } else {
                // else return false grid is not full yet there could be a winner.
                return false;
            }
        }
    }

    // game logic test until player logic is fully implemented.
    class GameLogicTester
    {

        public void testGame()
        {

            // a grid for testing 
            char[,] grid = new char[6, 7]
            {
                { 'X', 'X', 'O', 'O', 'X', 'X', 'O' },
                { 'O', 'O', 'X', 'X', 'O', 'O', 'X' },
                { 'X', 'X', 'O', 'O', 'X', 'X', 'O' },
                { 'O', 'O', 'X', 'X', 'O', 'O', 'X' },
                { 'X', 'X', 'O', 'O', 'X', 'X', 'O' },
                { 'O', 'O', 'X', 'X', 'O', 'O', 'X' }
            };

            var Horichecker = new HorizontalCheck().CheckWinner(grid, 'X');

            var VertChecker = new VerticalCheck().CheckWinner(grid, 'X');

            var DiagChecker = new DiagonalCheck().CheckWinner(grid, 'X');

            var DrawChecker = new DrawCheck().CheckDraw(grid);

            Console.WriteLine($"is there a Horizontal winner?: {(Horichecker ? "Yes" : "No")}");
            Console.WriteLine($"is there a Vertical winner?: {(VertChecker ? "Yes" : "No")}");
            Console.WriteLine($"is there a Diag winner?: {(DiagChecker ? "Yes" : "No")}");
            Console.WriteLine($"is there a Draw?: {(DrawChecker ? "Yes" : "No")}");

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
    
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new GameLogicTester();
            test.testGame();
            /*
            // make 2 player objects
            Player player1 = new Player("Player 1", 'X');
            Player player2 = new Player("Player 2", 'O');

            // simulate a turn
            Player current = player1;

            int move = current.GetMove();
            Console.WriteLine($"{current.Name} picked column {move + 1}");

            // switch player manually (test)
            current = (current == player1) ? player2 : player1;

            move = current.GetMove();
            Console.WriteLine($"{current.Name} picked column {move + 1}"); 
            */
        }
    }
}
