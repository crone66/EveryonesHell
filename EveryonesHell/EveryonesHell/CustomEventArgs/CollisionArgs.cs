using System;

namespace EveryonesHell.EntityManagment
{
    public class CollisionArgs : EventArgs
    {
        public Entity CollisionObjectSource;
        public Entity CollisionObjectDest;
        public bool CancelBackward;

        /// <summary>
        /// Collision event args
        /// </summary>
        /// <param name="collisionObjectSource">Source collision entity</param>
        /// <param name="collisionObjectDest">Destination collision entity</param>
        public CollisionArgs(Entity collisionObjectSource, Entity collisionObjectDest)
        {
            CollisionObjectSource = collisionObjectSource;
            CollisionObjectDest = collisionObjectDest;
            CancelBackward = false;
        }
    }
}
