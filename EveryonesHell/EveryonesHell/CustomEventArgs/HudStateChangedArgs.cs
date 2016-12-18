using System;

namespace EveryonesHell.HUD
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
