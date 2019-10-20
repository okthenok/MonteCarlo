using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class Fou : IGameState
    {
        public List<IGameState> moves;
        public int[,] Board = new int[7, 7];
        public bool playerTurn;
        public bool playerStarted;
        public Fou(int[,] board, bool playerstarted, bool playerturn)
        {
            playerTurn = playerturn;
            Board = board;
            playerStarted = playerstarted;
        }
        public void findMoves(Fou state)
        {
            bool[] colunmFilled = new bool[7];
            for (int i = 0; i < )
            for (int i = 0; i < state.Board.GetLength(0); i++)
            {
                for (int j = 0; j < state.Board.GetLength(1); j++)
                {
                    if ()
                }
            }
        }

    }
}
