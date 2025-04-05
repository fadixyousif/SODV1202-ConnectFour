using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODV1202_ConnectFour
{
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
