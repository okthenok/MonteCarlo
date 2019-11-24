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
                Console.WriteLine("Computer Wins");
            }
            else if (game.currFou.Value == 0)
            {
                Console.WriteLine("Player Wins");
            }
            else
            {
                Console.WriteLine("Tie");
            }
            Console.WriteLine(CheckGameOver(game.currFou));

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

        static int CheckGameOver(Fou fou)
        {
            int count = 0;
            int chainvalue = 1;
            for (int y = 0; y < fou.Board.GetLength(1); y++) //checking horizantal 4
            {
                for (int x = 0; x < fou.Board.GetLength(0); x++)
                {
                    if (fou.Board[x, y] != chainvalue)
                    {
                        count = 0;
                    }
                    count += fou.Board[x, y];
                    chainvalue = fou.Board[x, y];

                    if (count >= 4) { return 0; }
                    else if (count <= -4) { return 1; }
                }
                count = 0;
            }

            for (int x = 0; x < fou.Board.GetLength(0); x++) //checking vertical 4
            {
                for (int y = 0; y < fou.Board.GetLength(1); y++)
                {
                    if (fou.Board[x, y] != chainvalue)
                    {
                        count = 0;
                    }
                    count += fou.Board[x, y];
                    chainvalue = fou.Board[x, y];

                    if (count >= 4) { return 2; }
                    else if (count <= -4) { return 3; }
                }
                count = 0;
            }

            for (int x = 1; x < fou.Board.GetLength(0) - 2; x++) //left to right diagonal
            {
                count = 0;
                int row, col;
                for (col = x, row = 0; row < fou.Board.GetLength(1) && col < fou.Board.GetLength(0); row++, col++)
                {
                    if (fou.Board[col, row] != chainvalue)
                    {
                        count = 0;
                    }
                    count += fou.Board[col, row];
                    chainvalue = fou.Board[col, row];
                    if (count >= 4) { return 4; }
                    else if (count <= -4) { return 5; }
                }
                count = 0;
            }
            for (int y = 0; y < fou.Board.GetLength(1) - 2; y++)
            {
                count = 0;
                int row, col;
                for (row = y, col = 0; row < fou.Board.GetLength(1) && col < fou.Board.GetLength(0); row++, col++)
                {
                    if (fou.Board[col, row] != chainvalue)
                    {
                        count = 0;
                    }
                    count += fou.Board[col, row];
                    chainvalue = fou.Board[col, row];
                    if (count >= 4) { return 6; }
                    else if (count <= -4) { return 7; }
                }
                count = 0;
            }

            for (int y = 0; y < fou.Board.GetLength(1) - 2; y++) //right to left diagonal
            {
                count = 0;
                int row, col;
                for (row = y, col = fou.Board.GetLength(0) - 1; row < fou.Board.GetLength(1) && col >= 0; row++, col--)
                {
                    if (fou.Board[col, row] != chainvalue)
                    {
                        count = 0;
                    }
                    count += fou.Board[col, row];
                    chainvalue = fou.Board[col, row];
                    if (count >= 4) { return 8; }
                    else if (count <= -4) { return 9; }
                }
                count = 0;
            }
            for (int x = fou.Board.GetLength(1) - 2; x > 2; x--)
            {
                count = 0;
                int row, col;
                for (row = 0, col = x; row < fou.Board.GetLength(1) && col >= 0; row++, col--)
                {
                    if (fou.Board[col, row] == 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        count += fou.Board[col, row];
                    }
                    if (count >= 4) { return 10; }
                    else if (count <= -4) { return 11; }
                }
                count = 0;
            }

            bool isFull = true;
            for (int x = 0; x < fou.Board.GetLength(0); x++)
            {
                for (int y = 0; y < fou.Board.GetLength(1); y++)
                {
                    if (fou.Board[x, y] == 0)
                    {
                        isFull = false;
                    }
                }
            }
            if (isFull)
            {
                fou.IsTerminal = true;
                fou.Value = 0.5;
                return 12;
            }
            return 13;
        }
    }
}