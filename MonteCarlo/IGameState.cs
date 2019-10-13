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
        public virtual List<IGameState> Moves { get; set; }
        public bool Visited { get; set; }
        public int TimesSimulated { get; set; }
        public double TimesWon { get; set; }
    }
}
