using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public static class EntityFactory
    {
        private static Dictionary<string, Entity> factory = new Dictionary<string, Entity>();
        public static event EventHandler<FactoryEventArgs> EntityCreated;
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

        public static bool AddPrototype(string entityName, Entity entity)
        {
            if (!factory.ContainsKey(entityName))
            {
                factory.Add(entityName, entity);
                return true;
            }

            return false;
        }

        public static bool RemovePrototype(string entityName)
        {
            return factory.Remove(entityName);
        }
    }

    public class FactoryEventArgs : EventArgs
    {
        public Entity CreatedEntity;
        public FactoryEventArgs(Entity createdEntity)
        {
            CreatedEntity = createdEntity;
        }
    }
}
