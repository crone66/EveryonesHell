using System;

namespace EveryonesHell.EntityManagment
{
    public class AttackerArgs : EventArgs
    {
        public Entity Attacker;

        /// <summary>
        /// Attack event args
        /// </summary>
        /// <param name="attacker">Attacker entity</param>
        public AttackerArgs(Entity attacker)
        {
            Attacker = attacker;
        }
    }
}
