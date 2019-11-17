using System;
using System.Text;

namespace JPMonteCarlo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectFour game;
            bool playerStarts;

            Console.WriteLine("Do you want to play first? Y/N");
            if (Console.ReadLine().ToLower() == "y")
            {
                playerStarts = true;
            }
            else
            {
                playerStarts = false;
            }

            game = new ConnectFour(playerStarts);
            StringBuilder sb = new StringBuilder();
            int location = 0;
            if (playerStarts)
            {
                for (int j = 0; j < game.currFou.Board.GetLength(1); j++) //shows the board
                {
                    sb.Remove(0, sb.Length);
                    for (int i = 0; i < game.currFou.Board.GetLength(0); i++)
                    {
                        sb.Append(((game.currFou.Board[i, j] == 0) ? "o" : (game.currFou.Board[i, j] == 1) ? "r" : "y") + " ");
                    }
                    Console.WriteLine(sb.ToString());
                }

                Console.WriteLine("Which column do you want to place a chip in?"); //player move
                location = Convert.ToInt32(Console.ReadLine()) - 1;
                game.PlayerMove(location);
            }
            while (!game.currFou.IsTerminal)
            {
                if (!game.currFou.IsTerminal) //computer move
                {
                    game.CompMove();
                }

                if (!game.currFou.IsTerminal)
                {
                    for (int j = 0; j < game.currFou.Board.GetLength(1); j++) //shows the board
                    {
                        sb.Remove(0, sb.Length);
                        for (int i = 0; i < game.currFou.Board.GetLength(0); i++)
                        {
                            sb.Append(((game.currFou.Board[i, j] == 0) ? "o" : (game.currFou.Board[i, j] == 1) ? "r" : "y") + " ");
                        }
                        Console.WriteLine(sb.ToString());
                    }

                    Console.WriteLine("Which column do you want to place a chip in?"); //player move
                    location = Convert.ToInt32(Console.ReadLine()) - 1;
                    game.PlayerMove(location);
                }
            }

            for (int j = 0; j < game.currFou.Board.GetLength(1); j++) //shows the final board
            {
                sb.Remove(0, sb.Length);
                for (int i = 0; i < game.currFou.Board.GetLength(0); i++)
                {
                    sb.Append(((game.currFou.Board[i, j] == 0) ? "o" : (game.currFou.Board[i, j] == 1) ? "r" : "y") + " ");
                }
                Console.WriteLine(sb.ToString());
            }
            if (game.currFou.Value == 1)
            {
                Console.WriteLine("Player Wins");
            }
            else if (game.currFou.Value == 0)
            {
                Console.WriteLine("Computer Wins");
            }
            else
            {
                Console.WriteLine("Tie");
            }

            #region tictactoe
            /*game = new TicTacToe(playerStarts);

            StringBuilder sb = new StringBuilder();
            int[] location = new int[2];

            if (playerStarts)
            {
                for (int i = 0; i < game.currToe.Board.GetLength(0); i++)
                {
                    sb.Remove(0, sb.Length);
                    for (int j = 0; j < game.currToe.Board.GetLength(1); j++)
                    {
                        sb.Append(game.currToe.Board[i, j] + " ");
                    }
                    Console.WriteLine(sb.ToString());
                }
                Console.WriteLine("Which block would you like to select? (x, y)");
                location = Array.ConvertAll(Console.ReadLine().Split(", "), int.Parse);
                game.playerMove(location[1] - 1, location[0] - 1, playerStarts);
            }
            game.compMove(game.currToe);
            while (!game.currToe.IsTerminal)
            {
                if (!game.currToe.IsTerminal)
                {
                    for (int i = 0; i < game.currToe.Board.GetLength(0); i++)
                    {
                        sb.Remove(0, sb.Length);
                        for (int j = 0; j < game.currToe.Board.GetLength(1); j++)
                        {
                            sb.Append(game.currToe.Board[i, j] + " ");
                        }
                        Console.WriteLine(sb.ToString());
                    }

                    Console.WriteLine("Which block would you like to select? (x, y)");
                    location = Array.ConvertAll(Console.ReadLine().Split(", "), int.Parse);
                    game.playerMove(location[1] - 1, location[0] - 1, playerStarts);
                }
                if (!game.currToe.IsTerminal)
                {
                    game.compMove(game.currToe);
                }
            }
            for (int i = 0; i < game.currToe.Board.GetLength(0); i++)
            {
                sb.Remove(0, sb.Length);
                for (int j = 0; j < game.currToe.Board.GetLength(1); j++)
                {
                    sb.Append(game.currToe.Board[i, j] + " ");
                }
                Console.WriteLine(sb.ToString());
            }
        }

        void playerMove(TicTacToe game, StringBuilder sb, int[] location, bool playerStarts)
        {
            for (int i = 0; i < game.currToe.Board.GetLength(0); i++)
            {
                sb.Remove(0, sb.Length);
                for (int j = 0; j < game.currToe.Board.GetLength(1); j++)
                {
                    sb.Append(game.currToe.Board[i, j] + " ");
                }
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("Which block would you like to select? (x, y)");
            location = Array.ConvertAll(Console.ReadLine().Split(", "), int.Parse);
            game.playerMove(location[1] - 1, location[0] - 1, playerStarts);
        }*/
            #endregion
        }
    }
}