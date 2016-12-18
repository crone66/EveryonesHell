using System;

namespace EveryonesHell.EntityManagment
{
    public class VictimArgs : EventArgs
    {
        public Entity Victim;
        public int Damage;
        public bool Killed;

        public VictimArgs(Entity victim, int damage, bool killed)
        {
            Victim = victim;
            Damage = damage;
            Killed = killed;
        }
    }
}
