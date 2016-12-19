using System;

namespace EveryonesHell.EntityManagment
{
    public class VictimArgs : EventArgs
    {
        public Entity Victim;
        public int Damage;
        public bool Killed;

        /// <summary>
        /// Victim event args
        /// </summary>
        /// <param name="victim">Victim of attack</param>
        /// <param name="damage">Received damage</param>
        /// <param name="killed">Indicates whenther a victim was killed or not</param>
        public VictimArgs(Entity victim, int damage, bool killed)
        {
            Victim = victim;
            Damage = damage;
            Killed = killed;
        }
    }
}
