using System;
using System.Collections.Generic;
using System.Text;

namespace JPMonteCarlo
{
    public class IGameState
    {
        public IGameState()
        {

        }
        public double Value { get; set; }
        public bool IsTerminal { get; set; }
        public virtual List<IGameState> Moves { get; }
        public bool Visited { get; set; }
        public int TimesSimulated { get; set; }
        public int TimesWon { get; set; }
    }
}
