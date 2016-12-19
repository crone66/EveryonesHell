using System;
using System.Collections.Generic;

namespace EveryonesHell.EntityManagment
{
    public static class EntityFactory
    {
        private static Dictionary<string, Entity> factory = new Dictionary<string, Entity>();
        public static event EventHandler<FactoryEventArgs> EntityCreated;

        /// <summary>
        /// Clones a Entity by it's name
        /// </summary>
        /// <param name="entityName">Entity name</param>
        /// <returns>Returns a cloned entity</returns>
        public static Entity Clone(string entityName)
        {
            if (factory.ContainsKey(entityName))
            {
                Entity ent = factory[entityName].Clone();
                EntityCreated?.Invoke(null, new FactoryEventArgs(ent));
                return ent;
            }
            return null;
        }

        /// <summary>
        /// Adds a new prototyp entity
        /// </summary>
        /// <param name="entityName">Entity name</param>
        /// <param name="entity">Entity object</param>
        /// <returns>Returns true on success</returns>
        public static bool AddPrototype(string entityName, Entity entity)
        {
            if (!factory.ContainsKey(entityName))
            {
                factory.Add(entityName, entity);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a prototyp by its name
        /// </summary>
        /// <param name="entityName">Entity name</param>
        /// <returns>Returns true on success</returns>
        public static bool RemovePrototype(string entityName)
        {
            return factory.Remove(entityName);
        }
    }
}
