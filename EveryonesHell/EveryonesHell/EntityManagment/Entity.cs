using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public abstract class Entity
    {
        private static int Count = 0;

        private int id;
        private bool visable;
        private bool collidable;
        private bool active;

        private int tileRow;
        private int tileColumn;

        private Vector2f position;
        private Vector2i size;
        private Sprite currentSprite;

        public Entity()
        {
            id = Count++;
        }

        public Entity(bool visable, bool collidable, bool active)
        {
            id = Count++;
            this.visable = visable;
            this.collidable = collidable;
            this.active = active;
        }

        public Entity(bool visable, bool collidable, bool active, int tileRow, int tileColumn, Vector2f position, Vector2i size, Sprite currentSprite)
        {
            id = Count++;
            this.visable = visable;
            this.collidable = collidable;
            this.active = active;
            this.tileRow = tileRow;
            this.tileColumn = tileColumn;
            this.position = position;
            this.size = size;
            this.currentSprite = currentSprite;
        }

        public abstract void Update(float elapsedMilliseconds);
        public abstract void Draw();
    }
}
