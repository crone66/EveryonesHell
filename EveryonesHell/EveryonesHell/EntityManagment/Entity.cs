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

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public bool Visable
        {
            get
            {
                return visable;
            }

            set
            {
                visable = value;
            }
        }

        public bool Collidable
        {
            get
            {
                return collidable;
            }

            set
            {
                collidable = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public int TileRow
        {
            get
            {
                return tileRow;
            }

            set
            {
                tileRow = value;
            }
        }

        public int TileColumn
        {
            get
            {
                return tileColumn;
            }

            set
            {
                tileColumn = value;
            }
        }

        public Vector2f Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector2i Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public Entity()
        {
            Id = Count++;
        }

        public Entity(bool visable, bool collidable, bool active)
        {
            Id = Count++;
            this.Visable = visable;
            this.Collidable = collidable;
            this.Active = active;
        }

        public Entity(bool visable, bool collidable, bool active, int tileRow, int tileColumn, Vector2f position, Vector2i size, Sprite currentSprite)
        {
            Id = Count++;
            this.Visable = visable;
            this.Collidable = collidable;
            this.Active = active;
            this.TileRow = tileRow;
            this.TileColumn = tileColumn;
            this.Position = position;
            this.Size = size;
            this.currentSprite = currentSprite;
        }

        public abstract void Update(float elapsedMilliseconds);
        public abstract void Draw();
    }
}
