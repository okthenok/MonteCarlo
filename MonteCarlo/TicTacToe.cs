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
            }
        }

        public void compMove(bool playerFirst)
        {
            
        }


        public void compMinimax()
        {
            //minimaxer.Minimax(currToe, true);
        }
    }
}
