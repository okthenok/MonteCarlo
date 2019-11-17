using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPMonteCarlo
{
    public class ConnectFour
    {
        int[,] board = new int[7, 6];
        public Fou currFou;
        Random rand = new Random();
        MonteCarloer2 monte;
        bool isMax;

        public ConnectFour(bool playerStarts)
        {
            currFou = new Fou(board, playerStarts, playerStarts);
            monte = new MonteCarloer2();
            monte.Current = currFou;
            monte.MonteCarlo(currFou, playerStarts);
            isMax = false;
        }

        public void PlayerMove(int column)
        {
            var temp = (int[,])board.Clone();
            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                if (temp[column, y] == 0)
                {
                    temp[column, y] = 1;
                    break;
                }
            }
            foreach (Fou fou in currFou.Moves)
            {
                if (temp.Rank == fou.Board.Rank &&
                        Enumerable.Range(0, temp.Rank).All(dimension => temp.GetLength(dimension) == fou.Board.GetLength(dimension) &&
                        temp.Cast<int>().SequenceEqual(fou.Board.Cast<int>())))
                {
                    board = (int[,])fou.Board.Clone();
                    currFou = fou;
                    monte.Current = currFou;
                    return;
                }
            }
        }

        public void CompMove()
        {
            currFou = (Fou)monte.OptimalMove(isMax, 1000);
            monte.Current = currFou;
            board = (int[,])currFou.Board.Clone();
        }
    }
}
