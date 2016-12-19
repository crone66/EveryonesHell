using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace EveryonesHell.EntityManagment
{
    public class EntityManager
    {
        private List<Entity> entities;

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
        public void Update(float elapsedSeconds, IntRect viewBox)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].BoundingBox.Intersects(viewBox))
                    entities[i].Update(elapsedSeconds);
            }
        }

        /// <summary>
        /// Draws all entities
        /// </summary>
        /// <param name="window">Window to render</param>
        public void Draw(RenderWindow window, IntRect viewBox)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if(entities[i].BoundingBox.Intersects(viewBox))
                    entities[i].Draw(window);
            }
        }

        public void AddEntity(Entity entity)
        {
            if (entity != null)
            {
                entity.OnDestroy += Entity_OnDestroy;
                entities.Add(entity);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            Entity_OnDestroy(entity, null);
        }

        private void Entity_OnDestroy(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Entity ent = sender as Entity;
                ent.OnDestroy -= Entity_OnDestroy;
                entities.Remove(sender as Entity);
            }
        }

        private void Entity_OnSpawn(object sender, EventArgs e)
        {
            if(sender != null)
            {
                Entity ent = sender as Entity;
                AddEntity(ent);
            }
        }

        public Entity[] GetEntities()
        {
            return entities.ToArray();
        }
    }
}
