using System;

namespace EveryonesHell
{
    public class HudStateChangedArgs : EventArgs
    {
        public bool PreviouseState;

        public HudStateChangedArgs(bool prevState)
        {
            PreviouseState = prevState;
        }
    }
}
