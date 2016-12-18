using System;

namespace EveryonesHell.EntityManagment
{
    public class AttackerArgs : EventArgs
    {
        public Entity Attacker;
        public AttackerArgs(Entity attacker)
        {
            Attacker = attacker;
        }
    }
}
