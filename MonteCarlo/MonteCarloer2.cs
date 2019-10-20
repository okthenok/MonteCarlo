using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class MonteCarloer2
    {
        Random rand = new Random();
        public IGameState Current { get; set; }
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

            int bestMove = 0;
            int mostSimulations = 0;
            for (int i = 0; i < Current.Moves.Count; i++)
            {
                if (Current.Moves[i].TimesSimulated > mostSimulations)
                {
                    mostSimulations = Current.Moves[i].TimesSimulated;
                    bestMove = i;
                }
            }
            return Current.Moves[bestMove];
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
            if (!allVisited)
            {
                IGameState child = Expand(curr);

                double termVal = Simulate(child);

                UpdateState(child, !isMax, termVal);
                UpdateState(curr, isMax, termVal);

                return termVal;
            }

            int bestMove = 0;
            double bestUCT = 0;
            for (int i = 0; i < curr.Moves.Count; i++)
            {
                double temp = UCT(curr.Moves[i].TimesWon, curr.Moves[i].TimesSimulated, curr.TimesSimulated);
                if (temp > bestUCT)
                {
                    bestUCT = temp;
                    bestMove = i;
                }
            }

            double terminalValue = MonteCarlo(curr.Moves[bestMove], !isMax);
            UpdateState(curr, isMax, terminalValue);
            return terminalValue; //check the recursion here
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
            if ((isMax && terminalValue == 1) || (!isMax && terminalValue == 0))
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
