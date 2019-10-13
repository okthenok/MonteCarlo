using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class MonteCarloer2
    {
        Random rand = new Random();
        IGameState Current { get; set; }
        public IGameState OptimalMove(bool isMax, int playouts)
        {
            if (Current.IsTerminal)
            {
                return Current;
            }
            for (int i = 0; i < playouts; i++)
            {
                MonteCarlo(Current, isMax);
            }
        }

        public double MonteCarlo(IGameState curr, bool isMax)
        {
            if (curr.IsTerminal)
            {
                UpdateState(curr, isMax, curr.Value);
                return curr.Value;
            }

            bool allVisited = true;
            for (int i = 0; i < curr.Moves.Count; i++)
            {
                if (curr.Moves[i].Visited == false)
                {
                    allVisited = false;
                }
            }
            if (allVisited)
            {
                IGameState child = Expand(curr);

                double termVal = Simulate(child);

                Update
            }
        }

        public double UCT(double childWins, int childSimulations, int parentSimulations, double c = 1.41)
        {
            double exploitation = childWins / childSimulations;
            double exploration = c * (Math.Sqrt(Math.Log(parentSimulations) / childSimulations));
            return exploitation + exploration;
        }

        public IGameState Expand(IGameState state)
        {
            List<IGameState> unvisitedNodes = new List<IGameState>();
            for (int i = 0; i < state.Moves.Count; i++)
            {
                if (!state.Moves[i].Visited)
                {
                    unvisitedNodes.Add(state.Moves[i]);
                }
            }

            return unvisitedNodes[rand.Next(unvisitedNodes.Count)];
        }

        public double Simulate(IGameState startState)
        {
            startState.Visited = true;

            if (startState.IsTerminal)
            {
                return startState.Value;
            }

            IGameState temp = startState.Moves[rand.Next(startState.Moves.Count)];
            while (!temp.IsTerminal)
            {
                temp = temp.Moves[rand.Next(temp.Moves.Count)];
            }

            startState.Moves = null;

            return temp.Value;
        }

        public void UpdateState(IGameState state, bool isMax, double terminalValue)
        {
            if ((isMax && terminalValue == 0) || (!isMax && terminalValue == 1))
            {
                state.TimesWon++;
            }
            else if (terminalValue == 0.5)
            {
                state.TimesWon += 0.5;
            }

            state.TimesSimulated++;
        }
    }
}
