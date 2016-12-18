using System;

namespace EveryonesHell.EntityManagment
{
    public class CollisionArgs : EventArgs
    {
        public Entity CollisionObjectSource;
        public Entity CollisionObjectDest;
        public bool CancelBackward;
        public CollisionArgs(Entity collisionObjectSource, Entity collisionObjectDest)
        {
            CollisionObjectSource = collisionObjectSource;
            CollisionObjectDest = collisionObjectDest;
            CancelBackward = false;
        }
    }
}
