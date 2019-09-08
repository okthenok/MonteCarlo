using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class MonteCarloer
    {
        public MonteCarloer()
        {

        }

        public void MonteCarlo(IGameState state)
        {
            
            Selection();
            Expansion();
            Simulation();
            Backpropogation();
        }
        public void Selection()
        {

        }
        public void Expansion()
        {

        }
        public void Simulation()
        {

        }
        public void Backpropogation()
        {

        }
        public double UTC(int childWins, int childSimulations, int parentSimulations, double c = 1.41)
        {
            double exploitation = childWins / childSimulations;
            double exploration = c * (Math.Sqrt(Math.Log(parentSimulations) / childWins));
            return exploitation + exploration;
        }
    }
}
