using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
