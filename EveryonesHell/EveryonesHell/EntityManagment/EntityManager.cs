using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public class EntityManager
    {
        private List<Entity> entities;

        public List<Entity> Entities
        {
            get
            {
                return entities;
            }

            set
            {
                entities = value;
            }
        }

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        public void Update(float elapsedMilliseconds)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(elapsedMilliseconds);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw();
            }
        }
    }
}
