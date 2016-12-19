using System;

namespace EveryonesHell.EntityManagment
{
    public class FactoryEventArgs : EventArgs
    {
        public Entity CreatedEntity;

        /// <summary>
        /// Entity created event args
        /// </summary>
        /// <param name="createdEntity">Created entity</param>
        public FactoryEventArgs(Entity createdEntity)
        {
            CreatedEntity = createdEntity;
        }
    }
}
