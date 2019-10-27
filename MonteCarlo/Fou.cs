using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class Fou : IGameState
    {
        public List<IGameState> moves;
        public int[,] Board = new int[7, 6];
        public bool playerTurn;
        public bool playerStarted;
        public Fou(int[,] board, bool playerstarted, bool playerturn)
        {
            playerTurn = playerturn;
            Board = board;
            playerStarted = playerstarted;
        }
        public List<IGameState> findMoves(Fou state)
        {
            if (IsTerminal)
            {
                return new List<IGameState>();
            }

            List<IGameState> possibleMoves = new List<IGameState>();
            for (int i = 0; i < state.Board.GetLength(0); i++)
            {
                for (int j = 0; j < state.Board.GetLength(1); j++)
                {
                    if (state.Board[i, j] == 0)
                    {
                        var temp = (int[,])Board.Clone();
                        temp[i, j] = playerTurn ? 1 : -1;
                        possibleMoves.Add(new Fou(state.Board, playerStarted, !playerTurn));
                        break;
                    }
                }
            }
            return possibleMoves;
        }

        public void CheckGameOver()
        {
            int count = 0;
            for (int y = 0; y < Board.GetLength(1); y++) //checking horizantal 4
            {
                for (int x = 0; x < Board.GetLength(0); x++)
                {
                    if (Board[x, y] == 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        count += Board[x, y];
                    }
                    if (count >= 4) { this.Value = 1; this.IsTerminal = true; return; }
                    else if (count <= -4) { this.Value = 0; this.IsTerminal = true; return; }
                    count = 0;
                }
            }

            for (int x = 0; x < Board.GetLength(0); x++) //checking vertical 4
            {
                for (int y = 0; y < Board.GetLength(1); y++)
                {
                    if (Board[x, y] == 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        count += Board[x, y];
                    }
                    if (count >= 4) { this.Value = 1; this.IsTerminal = true; return; }
                    else if (count <= -4) { this.Value = 0; this.IsTerminal = true; return; }
                    count = 0;
                }
            }

            for (int x = 0; x < Board.GetLength(1) - 2; x++) //left to right diagonal
            {
                count = 0;
                int row, col;
                for (row = x, col = 0; row < Board.GetLength(1) && col < Board.GetLength(0); row++, col++)
                {
                    count += Board[row, col];
                }
                if (count >= 4) { this.Value = 1; this.IsTerminal = true; return; }
                else if (count <= 4) { this.Value = 0; this.IsTerminal = true; return; }
            }
            for (int y = 1; y < Board.GetLength(0) - 2; y++)
            {
                count = 0;
                int row, col;
                for (row = 0, col = y; row < Board.GetLength(1) && col < Board.GetLength(0); row++, col++)
                {
                    count += Board[row, col];
                }
                if (count >= 4) { this.Value = 1; this.IsTerminal = true; return; }
                else if (count <= 4) { this.Value = 0; this.IsTerminal = true; return; }
            }

            for (int y = Board.GetLength(0) - 1; y > 2; y--) //right to left diagonal
            {
                count = 0;
                int row, col;
                for (row = Board.GetLength(1) - 1, col = y; row >= 0 && col >= 0; row--, col--)
                {
                    count += Board[row, col];
                }
                if (count >= 4) { this.Value = 1; this.IsTerminal = true; return; }
                else if (count <= 4) { this.Value = 0; this.IsTerminal = true; return; }
            }
            for (int x = Board.GetLength(1) - 1; x > 2; x--) 
            {
                count = 0;
                int row, col;
                for (row = x, col = Board.GetLength(0) - 1; row >= 0 && col >= 0; row--, col--)
                {
                    count += Board[row, col];
                }
                if (count >= 4) { this.Value = 1; this.IsTerminal = true; return; }
                else if (count <= 4) { this.Value = 0; this.IsTerminal = true; return; }
            }

            bool isFull = true;
            for (int x = 0; x < Board.GetLength(0); x++)
            {
                for (int y = 0; y < Board.GetLength(1); y++)
                {
                    if (Board[x, y] == 0)
                    {
                        isFull = false;
                    }
                }
            }
            if (isFull)
            {
                this.IsTerminal = true;
                this.Value = 0.5;
                return;
            }
        }
    }
}
