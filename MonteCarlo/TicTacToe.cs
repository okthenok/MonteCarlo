using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPMonteCarlo
{
    public class TicTacToe
    {
        int[,] board = new int[3, 3];
        public Toe currToe;
        Random rand = new Random();
        //Minimaxer minimaxer = new Minimaxer();

        public TicTacToe(bool playerStarts)
        {
            currToe = new Toe(board, playerStarts, playerStarts);

            if (!playerStarts)
            {
                //minimaxer.Minimax(currToe, playerStarts);
            }   //probably will have to do the "call minimax after player moves if player starts" in Program.cs
        }

        public void playerMove(int x, int y, bool playerStarts)
        {
            var temp = (int[,])board.Clone();
            temp[x, y] = 1;
            foreach (Toe toe in currToe.Moves)
            {
                if (temp.Rank == toe.Board.Rank &&
                    Enumerable.Range(0, temp.Rank).All(dimension => temp.GetLength(dimension) == toe.Board.GetLength(dimension) &&
                    temp.Cast<int>().SequenceEqual(toe.Board.Cast<int>())))
                {
                    board = (int[,])temp.Clone();
                    currToe = toe;
                    return;
                }

                List<Toe> goodMoves = new List<Toe>();
                int worst = playerStarts ? -1 : 1;
                foreach (Toe t in currToe.Moves)
                {
                    if (playerStarts)
                    {
                        if (toe.Value > worst)
                        {
                            worst = toe.Value;
                        }
                    }
                    else
                    {
                        if (toe.Value < worst)
                        {
                            worst = toe.Value;
                        }
                    }
                }

                bool notOptimal = true;
                foreach (Toe t in goodMoves)
                {
                    if (t == currToe)
                    {
                        notOptimal = false;
                    }
                }
                if (notOptimal)
                {
                    //Minimaxer minimaxer = new Minimaxer();
                    //minimaxer.Minimax(currToe, playerStarts);
                }
            }
        }

        public void compMove(bool playerFirst)
        {
            List<Toe> goodMoves = new List<Toe>();

            int temp = playerFirst ? 1 : -1;
            foreach (Toe toe in currToe.Moves)
            {
                if (playerFirst)
                {
                    if (toe.Value < temp)
                    {
                        temp = toe.Value;
                    }
                }
                else
                {
                    if (toe.Value > temp)
                    {
                        temp = toe.Value;
                    }
                }
            }
            foreach (Toe toe in currToe.Moves)
            {
                if (toe.Value == temp)
                {
                    goodMoves.Add(toe);
                }
            }
            currToe = goodMoves[rand.Next(goodMoves.Count)];
            board = (int[,])currToe.Board.Clone();
        }


        public void compMinimax()
        {
            //minimaxer.Minimax(currToe, true);
        }
    }
}
