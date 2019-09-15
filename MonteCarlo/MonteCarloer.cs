using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class MonteCarloer
    {
        Random rand = new Random();
        IGameState head;
        public void MonteCarlo(IGameState Head)
        {
            head = Head;
        }

        public MonteCarloer(int playThroughs)
        {
            IGameState state = head;
            for (int i = 0; i < playThroughs; i++)
            {
                Expansion(state);
            }
        }
        public IGameState Selection(IGameState state)
        {
            IGameState temp = state;
            int bestMove = 0;
            double bestProbability = 0;
            double tempProbability;

            for (int i = 0; i < temp.Moves.Count; i++)
            {
                tempProbability = temp.Moves[i].TimesWon / temp.Moves[i].TimesSimulated;
                if (bestProbability < tempProbability)
                {
                    bestMove = i;
                    bestProbability = tempProbability;
                }
            }
            return temp.Moves[bestMove];
        }
        public void Expansion(IGameState state)
        {
            IGameState temp = state.Moves[0];
            bool allVisited = true;
            foreach (IGameState node in state.Moves)
            {
                if (!node.Visited)
                {
                    temp = node;
                    allVisited = false;
                    break;
                }
            }
            if (allVisited)
            {
                double bestChoice = 0;
                foreach (IGameState node in state.Moves)
                {
                    double utc = UTC(node.TimesWon, node.TimesSimulated, state.TimesSimulated);
                    if (bestChoice < utc)
                    {
                        bestChoice = utc;
                        temp = node;
                    }
                }
                Expansion(temp);
                return;
            }
            
            Simulation(temp);
            Backpropogation(temp);
        }
        public double Simulation(IGameState state)
        {
            state.Visited = true;
            var temp = state;
            while (temp.Moves != null)
            {
                temp = temp.Moves[rand.Next(0, state.Moves.Count)];
            }
            return temp.Value;
        }
        public void Backpropogation(IGameState state)
        {
            foreach (IGameState move in state.Moves)
            {
                state.TimesWon += move.TimesWon;
                state.TimesSimulated += move.TimesSimulated;
            }
        }
        public double UTC(int childWins, int childSimulations, int parentSimulations, double c = 1.41)
        {
            double exploitation = childWins / childSimulations;
            double exploration = c * (Math.Sqrt(Math.Log(parentSimulations) / childWins));
            return exploitation + exploration;
        }
    }
}
