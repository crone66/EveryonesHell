using SFML.Graphics;
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

        /// <summary>
        /// Initziaizes an entity manager
        /// </summary>
        public EntityManager()
        {
            entities = new List<Entity>();
        }

        /// <summary>
        /// Updates all entities
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public void Update(float elapsedSeconds)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(elapsedSeconds);
            }
        }

        /// <summary>
        /// Draws all entities
        /// </summary>
        /// <param name="window">Window to render</param>
        public void Draw(RenderWindow window)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(window);
            }
        }
    }
}
