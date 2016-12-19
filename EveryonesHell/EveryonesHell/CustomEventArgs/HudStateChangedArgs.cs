using System;

namespace EveryonesHell.HUD
{
    public class HudStateChangedArgs : EventArgs
    {
        public bool PreviouseState;

        /// <summary>
        /// Hud visability state changed
        /// </summary>
        /// <param name="prevState">Previouse visability state</param>
        public HudStateChangedArgs(bool prevState)
        {
            PreviouseState = prevState;
        }
    }
}
