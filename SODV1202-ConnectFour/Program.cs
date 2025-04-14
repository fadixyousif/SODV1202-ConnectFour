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
        bool CheckWinner(char[,] grid, int rows, int columns, char player);
    }

    public interface IDrawLogic
    {
        bool CheckDraw();
    }

    public class HorizontalCheck : IWinLogic
    {
        public bool CheckWinner(char[,] grid, int rows, int columns, char player)
        {
            for (int row = 0; row < rows; row++) {
                // Row Debugger if X or O matches I know that this check works
                Console.WriteLine($"Current row: {row}");
                for (int column = 0; column < columns - 3; column++) {
                    if (grid[row, column] == player && grid[row, column + 1] == player && grid[row, column + 2] == player && grid[row, column + 3] == player) {
                        return true; 
                    }
                }
            }
            return false;
        }

    }

    public class VerticalCheck : IWinLogic
    {
        public bool CheckWinner(char[,] grid, int rows, int columns, char player)
        {
            for (int row = 0; row < rows - 3; row++)
            {
                for (int column = 0; column < columns; column++)
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
        public bool CheckWinner(char[,] grid, int rows, int columns, char player)
        {
            return true;
        }
    }

        public class DrawCheck : IDrawLogic
    {

        public bool CheckDraw()
        {
            return false;
        }
    }

    // game logic test until player logic is fully implemented.
    class GameLogicTester
    {
        const int rows = 6;
        const int columns = 7;

        public void testGame()
        {

            // a grid for testing 
            char[,] grid = new char[rows, columns]
            {
                { 'X', 'X', 'O', 'X', 'X', 'O', 'X' },
                { 'O', 'X', 'O', 'X', 'O', 'X', 'X' },
                { 'O', 'O', 'X', 'O', 'X', 'O', 'X' },
                { 'O', 'X', 'O', 'X', 'O', 'X', 'O' },
                { 'O', 'O', 'X', 'O', 'X', 'S', 'X' },
                { 'O', 'X', 'O', 'X', 'O', 'X', 'O' }
            };

            var Horichecker = new HorizontalCheck().CheckWinner(grid, rows, columns, 'O');

            var VertChecker = new VerticalCheck().CheckWinner(grid, rows, columns, 'O');

            Console.WriteLine($"is there a Horizontal winner?: {(Horichecker ? "Yes" : "No")}");
            Console.WriteLine($"is there a Vertical winner?: {(VertChecker ? "Yes" : "No")}");

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
