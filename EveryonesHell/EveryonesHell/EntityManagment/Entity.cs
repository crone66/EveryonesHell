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
        private bool isVisable;
        private bool isCollidable;

        private int tileRow;
        private int tileColumn;

        private Vector2f position;
        private Vector2i size;
        private Sprite currentSprite;
        private IntRect spriteRect;
        private IntRect boundingBox;

        private List<Vector2i> overlappingTiles;

        public int Id
        {
            get
            {
                return id;
            }
        }

        public bool Visable
        {
            get
            {
                return isVisable;
            }

            protected set
            {
                isVisable = value;
            }
        }

        public bool IsCollidable
        {
            get
            {
                return isCollidable;
            }

            protected set
            {
                isCollidable = value;
            }
        }

        public int TileRow
        {
            get
            {
                return tileRow;
            }

            protected set
            {
                tileRow = value;
                UpdatePosition();
            }
        }

        public int TileColumn
        {
            get
            {
                return tileColumn;
            }

            protected set
            {
                tileColumn = value;
                UpdatePosition();
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
                boundingBox = new IntRect(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.X, size.Y);
                UpdateTileIndices();
            }
        }

        public Vector2i Size
        {
            get
            {
                return size;
            }

            protected set
            {
                size = value;
                boundingBox = new IntRect(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.X, size.Y);
                UpdateOverlappingTiles();
            }
        }

        public Sprite CurrentSprite
        {
            get
            {
                return currentSprite;
            }

            protected set
            {
                currentSprite = value;
            }
        }

        public IntRect BoundingBox
        {
            get
            {
                return boundingBox;
            }

            protected set
            {
                boundingBox = value;
                position = new Vector2f(boundingBox.Top, boundingBox.Top);
                size = new Vector2i(boundingBox.Width, boundingBox.Height);
            }
        }

        public IntRect SpriteRect
        {
            get
            {
                return spriteRect;
            }

            protected set
            {
                spriteRect = value;
            }
        }

        public List<Vector2i> OverlappingTiles
        {
            get
            {
                return overlappingTiles;
            }
        }

        /// <summary>
        /// Initzializes a new entity
        /// </summary>
        public Entity()
        {
            id = Count++;
            overlappingTiles = new List<Vector2i>();
        }

        /// <summary>
        /// Initzializes a new entity
        /// </summary>
        /// <param name="visable">Indicates whenther the entity is visable or not</param>
        /// <param name="collidable">Indicates whenther the entity is collidable or not</param>
        public Entity(bool visable, bool collidable)
        {
            id = Count++;
            Visable = visable;
            IsCollidable = collidable;
            overlappingTiles = new List<Vector2i>();
        }

        /// <summary>
        /// Initzializes a new entity
        /// </summary>
        /// <param name="visable">Indicates whenther the entity is visable or not</param>
        /// <param name="collidable">Indicates whenther the entity is collidable or not</param>
        /// <param name="position">Position of the entity</param>
        /// <param name="size">Size of entity</param>
        /// <param name="currentSprite">Sprite of entity</param>
        public Entity(bool visable, bool collidable, Vector2f position, Vector2i size, Sprite currentSprite)
        {
            id = Count++;
            Visable = visable;
            IsCollidable = collidable;
            overlappingTiles = new List<Vector2i>();
            Size = size;
            Position = position;
            CurrentSprite = currentSprite;
            SpriteRect = currentSprite.TextureRect;
        }

        /// <summary>
        /// Initzializes a new entity
        /// </summary>
        /// <param name="visable">Indicates whenther the entity is visable or not</param>
        /// <param name="collidable">Indicates whenther the entity is collidable or not</param>
        /// <param name="tileRow">Tile row index</param>
        /// <param name="tileColumn">Tile column index</param>
        /// <param name="size">Size of entity</param>
        /// <param name="currentSprite">Sprite of entity</param>
        public Entity(bool visable, bool collidable, int tileRow, int tileColumn, Vector2i size, Sprite currentSprite)
        {
            id = Count++;
            Visable = visable;
            IsCollidable = collidable;
            overlappingTiles = new List<Vector2i>();
            Size = size;
            TileRow = tileRow;
            TileColumn = tileColumn;
            CurrentSprite = currentSprite;
            SpriteRect = currentSprite.TextureRect;
        }

        /// <summary>
        /// Initzializes a new entity
        /// </summary>
        /// <param name="visable">Indicates whenther the entity is visable or not</param>
        /// <param name="collidable">Indicates whenther the entity is collidable or not</param>
        /// <param name="position">Position of the entity</param>
        /// <param name="size">Size of entity</param>
        /// <param name="currentSprite">Sprite of entity</param>
        /// <param name="spriteRect">Texture rectangle of the current sprite</param
        public Entity(bool visable, bool collidable, Vector2f position, Vector2i size, Sprite currentSprite, IntRect spriteRect)
        {
            id = Count++;
            Visable = visable;
            IsCollidable = collidable;
            overlappingTiles = new List<Vector2i>();
            Size = size;
            Position = position;      
            CurrentSprite = currentSprite;
            SpriteRect = spriteRect;
        }

        /// <summary>
        /// Initzializes a new entity
        /// </summary>
        /// <param name="visable">Indicates whenther the entity is visable or not</param>
        /// <param name="collidable">Indicates whenther the entity is collidable or not</param>
        /// <param name="tileRow">Tile row index</param>
        /// <param name="tileColumn">Tile column index</param>
        /// <param name="size">Size of entity</param>
        /// <param name="currentSprite">Sprite of entity</param>
        /// <param name="spriteRect">Texture rectangle of the current sprite</param>
        public Entity(bool visable, bool collidable, int tileRow, int tileColumn, Vector2i size, Sprite currentSprite, IntRect spriteRect)
        {
            id = Count++;
            Visable = visable;
            IsCollidable = collidable;
            overlappingTiles = new List<Vector2i>();
            Size = size;
            TileRow = tileRow;
            TileColumn = tileColumn;
            CurrentSprite = currentSprite;
            SpriteRect = spriteRect;
        }

        /// <summary>
        /// Calculates entity tile indices from position
        /// </summary>
        protected void UpdateTileIndices()
        {
            tileColumn = Convert.ToInt32(Math.Floor(position.X / GlobalReferences.MainGame.CurrentScene.Settings.TileSize));
            tileRow = Convert.ToInt32(Math.Floor(position.Y / GlobalReferences.MainGame.CurrentScene.Settings.TileSize));
            UpdateOverlappingTiles();
        }

        /// <summary>
        /// Calculates entity position from tile indices
        /// </summary>
        protected void UpdatePosition()
        {
            position.X = Convert.ToInt32(Math.Floor(tileColumn * (double)GlobalReferences.MainGame.CurrentScene.Settings.TileSize));
            position.Y = Convert.ToInt32(Math.Floor(tileRow * (double)GlobalReferences.MainGame.CurrentScene.Settings.TileSize));
            boundingBox = new IntRect(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.X, size.Y);
            UpdateOverlappingTiles();
        }

        /// <summary>
        /// Searches all overlapping tile indices
        /// </summary>
        protected void UpdateOverlappingTiles()
        {
            int tileColumnCount = Convert.ToInt32(Math.Ceiling(boundingBox.Width / (double)GlobalReferences.MainGame.CurrentScene.Settings.TileSize));
            int tileRowCount = Convert.ToInt32(Math.Ceiling(boundingBox.Height / (double)GlobalReferences.MainGame.CurrentScene.Settings.TileSize));

            overlappingTiles.Clear();
            for (int r = tileRow - tileRowCount; r < tileRow + tileRowCount + 1; r++)
            {
                for (int c = tileColumn - tileColumnCount; c < tileColumn + tileColumnCount + 1; c++)
                {
                    if(boundingBox.Intersects(GetTileBoundingBox(new Vector2i(c, r))))
                        overlappingTiles.Add(new Vector2i(c, r));
                }
            }
        }

        /// <summary>
        /// Creates an IntRect from tile indices
        /// </summary>
        /// <param name="tileInfo">tile indeces</param>
        /// <returns>Returns an integer rectangle</returns>
        protected IntRect GetTileBoundingBox(Vector2i tileInfo)
        {
            IntRect tileBox = new IntRect(
                tileInfo.X * GlobalReferences.MainGame.CurrentScene.Settings.TileSize,
                tileInfo.Y * GlobalReferences.MainGame.CurrentScene.Settings.TileSize,
                GlobalReferences.MainGame.CurrentScene.Settings.TileSize,
                GlobalReferences.MainGame.CurrentScene.Settings.TileSize);
            return tileBox;
        }

        /// <summary>
        /// Draws current sprite of the entity
        /// </summary>
        /// <param name="window">Window to render</param>
        public virtual void Draw(RenderWindow window)
        {
            if (isVisable && currentSprite != null)
            {
                currentSprite.Position = position;
                currentSprite.TextureRect = spriteRect;
                currentSprite.Scale = UIHelper.GetScale(currentSprite.TextureRect, boundingBox);
                window.Draw(currentSprite);
            }
        }

        /// <summary>
        /// Updates the current entity
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public abstract void Update(float elapsedSeconds);

        public abstract Entity Clone();

    }
}
