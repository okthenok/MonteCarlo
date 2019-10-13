using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class MonteCarloer
    {
        Random rand = new Random();
        IGameState head;
        int playthroughs;
        public MonteCarloer(IGameState Head)
        {
            head = Head;
        }

        public void MonteCarlo(int playThroughs)
        {
            IGameState state = head;
            for (int i = 0; i < playThroughs; i++)
            {
                Expansion(state);   //counted playthroughs and how many playthroughs there should be are different
                                    //check the expansion function and the recursive part of it (especially simulation)
            }
        }
        public IGameState Selection(IGameState state)
        {
            IGameState temp = state;
            List<IGameState> badMoves = new List<IGameState>();
            bool goodMove = true;
            int bestMove = 0;
            double bestProbability = 0;
            double tempProbability;

            for (int i = 0; i < temp.Moves.Count; i++)
            {
                if (temp.Moves[i].IsTerminal)
                {
                    bestMove = i;
                    break;
                }
                for (int j = 0; j < temp.Moves[i].Moves.Count; j++)
                {
                    if (temp.Moves[i].Moves[j].IsTerminal)
                    {
                        goodMove = false;
                        badMoves.Add(temp.Moves[i]);
                        break;
                    }
                }
                if (goodMove)
                {
                    bestMove = i;
                }

                tempProbability = temp.Moves[i].TimesWon / temp.Moves[i].TimesSimulated;
                if (bestProbability < tempProbability)
                {
                    bestMove = i;
                    bestProbability = tempProbability;
                }
                goodMove = true;
            }
            if (temp.Moves.Count > 0 && !badMoves.Contains(temp.Moves[bestMove]))
            {
                return temp.Moves[bestMove];
            }
            else
            {
                return temp.Moves[rand.Next(0, temp.Moves.Count)];
            }
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
            if (allVisited) //fix this logic
            {
                double bestChoice = 0;
                foreach (IGameState node in state.Moves)
                {
                    double utc = UCT(node.TimesWon, node.TimesSimulated, state.TimesSimulated);
                    if (bestChoice < utc)
                    {
                        bestChoice = utc;
                        temp = node;
                    }
                }

                if (temp.Moves.Count > 0)
                {
                    Expansion(temp);
                    Backpropogation(state);
                }
                else
                {
                    playthroughs++;
                    Backpropogation(state);
                }
                return;
            }
            playthroughs++;
            Simulation(temp);
            Backpropogation(state);
        }
        public double Simulation(IGameState state)
        {
            state.Visited = true;
            var temp = state;
            while (temp.Moves.Count > 0)
            {
                temp = temp.Moves[rand.Next(0, temp.Moves.Count)];
            }
            state.TimesWon += temp.Value;
            state.TimesSimulated++;
            return temp.Value;
        }
        public void Backpropogation(IGameState state)
        {
            state.TimesWon = 0;
            state.TimesSimulated = 0;
            foreach (IGameState move in state.Moves)
            {
                state.TimesWon += move.TimesWon;
                state.TimesSimulated += move.TimesSimulated;
            }
        }
        public double UCT(double childWins, int childSimulations, int parentSimulations, double c = 1.41)
        {
            double exploitation = childWins / childSimulations;
            double exploration = c * (Math.Sqrt(Math.Log(parentSimulations) / childSimulations));
            return exploitation + exploration;
        }
    }
}
