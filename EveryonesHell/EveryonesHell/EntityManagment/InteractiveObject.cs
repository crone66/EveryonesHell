using InventorySystem;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using EveryonesHell.HUD;

namespace EveryonesHell.EntityManagment
{
    public class InteractiveObject : Entity
    {
        private Inventory inventory;
        private AnimationManager animations;

        //TODO add Dialogs

        private int health;
        private int maxHealth;

        private bool isMoveAble;
        private bool freeze;
        private Vector2f velocity;
        private Vector2f viewDirection;
        private float speed;
        private int groupId;
        private int factionId;
        private Gaugebar healthBar;
        protected List<int> dialogIds;
        
        public event EventHandler OnMoved;
        public event EventHandler OnMoving;
        public event EventHandler<AttackerArgs> OnKill;
        public event EventHandler<AttackerArgs> OnReceiveDamage;
        public event EventHandler<AttackerArgs> OnReceiveHealth;
        public event EventHandler<CollisionArgs> OnCollision;


        public Vector2f Velocity
        {
            get
            {
                return velocity;
            }

            protected set
            {
                velocity = value;
                float x = velocity.X == 0 ? 0 : ((velocity.X / velocity.X) * (velocity.X < 0 ? -1 : 1));
                float y = velocity.Y == 0 ? 0 : ((velocity.Y / velocity.Y) * (velocity.Y < 0 ? -1 : 1));
                if (x != 0 || y != 0)              
                    viewDirection = new Vector2f(x, y);
            }
        }

        public bool IsMoveAble
        {
            get
            {
                return isMoveAble;
            }

            protected set
            {
                isMoveAble = value;
            }
        }

        public Vector2f ViewDirection
        {
            get
            {
                return viewDirection;
            }

            set
            {
                viewDirection = value;
            }
        }

        /// <summary>
        /// Returns the current Health
        /// </summary>
        public int Health
        {
            get
            {
                return health;
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }

            protected set
            {
                maxHealth = value;
                if (health > maxHealth)
                    health = maxHealth;
            }
        }

        public bool Freeze
        {
            get
            {
                return freeze;
            }

            set
            {
                freeze = value;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }

            protected set
            {
                speed = value;
            }
        }

        internal Gaugebar Healthbar
        {
            get
            {
                return healthBar;
            }

            set
            {
                healthBar = value;
            }
        }

        public int GroupID
        {
            get
            {
                return groupId;
            }
        }

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }

            protected set
            {
                inventory = value;
            }
        }

        public int FactionId
        {
            get
            {
                return factionId;
            }

            set
            {
                factionId = value;
            }
        }

        public AnimationManager Animations
        {
            get
            {
                return animations;
            }

            set
            {
                animations = value;
            }
        }

        /// <summary>
        /// Initzializes a new interactive object
        /// </summary>
        /// <param name="size">Size of the interactive object</param>
        /// <param name="animations">Animations of the interactive object</param>
        /// <param name="isMoveAble">Inidcates whenther the interactive object is moveable or not</param>
        /// <param name="viewDirection">Indicates the view direction of the object</param>
        /// <param name="speed">Indicates the movement speed of the object</param>
        public InteractiveObject(Vector2i size, AnimationManager animations, bool isMoveAble, float speed, int maxHealth, Gaugebar healthBar, int groupId, int factionId, bool isPrototyp)
            : base(true, true, size, animations.Sprite, isPrototyp)
        {
            this.inventory = null;
            this.Animations = animations;
            this.isMoveAble = isMoveAble;
            this.viewDirection = new Vector2f(1, 0);
            this.healthBar = healthBar;
            this.groupId = groupId;
            dialogIds = new List<int>();
            this.factionId = factionId;
            Speed = speed;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            velocity = new Vector2f(0, 0);
        }


        /// <summary>
        /// Initzializes a new interactive object
        /// </summary>
        /// <param name="size">Size of the interactive object</param>
        /// <param name="animations">Animations of the interactive object</param>
        /// <param name="isMoveAble">Inidcates whenther the interactive object is moveable or not</param>
        /// <param name="viewDirection">Indicates the view direction of the object</param>
        /// <param name="speed">Indicates the movement speed of the object</param>
        public InteractiveObject(Vector2i size, Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, Gaugebar healthBar, int groupId, int factionId, int[] dialogIds, bool isPrototyp)
            : base(true, true, size, animations.Sprite, isPrototyp)
        {
            this.inventory = inventory;
            this.Animations = animations;
            this.isMoveAble = isMoveAble;
            this.viewDirection = viewDirection;
            this.healthBar = healthBar;
            this.groupId = groupId;
            this.dialogIds = new List<int>();
            if (dialogIds != null)
                this.dialogIds.AddRange(dialogIds);
            this.factionId = factionId;
            Speed = speed;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            velocity = new Vector2f(0, 0);
        }

        /// <summary>
        /// Initzializes a new interactive object
        /// </summary>
        /// <param name="position">Spawn position of the object</param>
        /// <param name="size">Size of the interactive object</param>
        /// <param name="inventory">Inventory of the interactive object</param>
        /// <param name="animations">Animations of the interactive object</param>
        /// <param name="isMoveAble">Inidcates whenther the interactive object is moveable or not</param>
        /// <param name="viewDirection">Indicates the view direction of the object</param>
        /// <param name="speed">Indicates the movement speed of the object</param>
        public InteractiveObject(Vector2f position, Vector2i size, Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, Gaugebar healthBar, int groupId, int factionId, int[] dialogIds)
            :base(true, true, position, size, animations.Sprite)
        {
            this.inventory = inventory;
            this.Animations = animations;
            this.isMoveAble = isMoveAble;
            this.viewDirection = viewDirection;
            this.groupId = groupId;
            this.factionId = factionId;
            this.dialogIds = new List<int>();
            if(dialogIds != null)
                this.dialogIds.AddRange(dialogIds);
            Speed = speed;
            velocity = new Vector2f(0, 0);
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            Healthbar = healthBar;
        }

        /// <summary>
        /// Initzializes a new interactive object
        /// </summary>
        /// <param name="position">Spawn position of the object</param>
        /// <param name="size">Size of the interactive object</param>
        /// <param name="inventory">Inventory of the interactive object</param>
        /// <param name="animations">Animations of the interactive object</param>
        /// <param name="isMoveAble">Inidcates whenther the interactive object is moveable or not</param>
        /// <param name="viewDirection">Indicates the view direction of the object</param>
        /// <param name="speed">Indicates the movement speed of the object</param>
        public InteractiveObject(Vector2f position, Vector2i size, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, int groupId, int factionId)
            : base(true, true, position, size, animations.Sprite)
        {
            this.Animations = animations;
            this.isMoveAble = isMoveAble;
            this.viewDirection = viewDirection;
            Speed = speed;
            velocity = new Vector2f(0, 0);
            dialogIds = new List<int>();

            this.maxHealth = maxHealth;
            this.health = maxHealth;
            Healthbar = null;
            inventory = null;
        }


        /// <summary>
        /// Initzializes a new interactive object
        /// </summary>
        /// <param name="tileRow">Tile row index</param>
        /// <param name="tileColumn">Tile column index</param>
        /// <param name="size">Size of the interactive object</param>
        /// <param name="inventory">Inventory of the interactive object</param>
        /// <param name="animations">Animations of the interactive object</param>
        /// <param name="isMoveAble">Inidcates whenther the interactive object is moveable or not</param>
        /// <param name="viewDirection">Indicates the view direction of the object</param>
        /// <param name="speed">Indicates the movement speed of the object</param>
        public InteractiveObject(int tileRow, int tileColumn, Vector2i size, Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, Gaugebar healthBar, int groupId, int factionId, int[] dialogIds, bool isPrototyp)
            : base(true, true, tileRow, tileColumn, size, animations.Sprite, isPrototyp)
        {
            this.inventory = inventory;
            this.Animations = animations;
            this.isMoveAble = isMoveAble;
            this.viewDirection = viewDirection;

            this.dialogIds = new List<int>();
            if (dialogIds != null)
                this.dialogIds.AddRange(dialogIds);

            Speed = speed;
            velocity = new Vector2f(0, 0);
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.healthBar = healthBar;
            this.groupId = groupId;
            this.factionId = factionId;
        }

        /// <summary>
        /// Initzializes a new interactive object
        /// </summary>
        /// <param name="position">Spawn position of the object</param>
        /// <param name="size">Size of the interactive object</param>
        /// <param name="inventory">Inventory of the interactive object</param>
        /// <param name="animations">Animations of the interactive object</param>
        /// <param name="isMoveAble">Inidcates whenther the interactive object is moveable or not</param>
        /// <param name="viewDirection">Indicates the view direction of the object</param>
        /// <param name="speed">Indicates the movement speed of the object</param>
        public InteractiveObject(int tileRow, int tileColumn, Vector2i size, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, int groupId, int factionId, bool isPrototyp)
            : base(true, true, tileRow, tileColumn, size, animations.Sprite, isPrototyp)
        {
            this.Animations = animations;
            this.isMoveAble = isMoveAble;
            this.viewDirection = viewDirection;
            this.groupId = groupId;
            Speed = speed;
            velocity = new Vector2f(0, 0);
            dialogIds = new List<int>();

            this.maxHealth = maxHealth;
            this.health = maxHealth;
            Healthbar = null;
            inventory = null;
        }

        /// <summary>
        /// Updates the interactive object
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public override void Update(float elapsedSeconds)
        {
            //TODO update animation and spriteRect
            if (isMoveAble && !Freeze)
            {
                Move(elapsedSeconds);
                Animations.Update(elapsedSeconds, Velocity);
                SpriteRect = Animations.SpriteRect;
            }
            
            Velocity = new Vector2f(0, 0);

            if(Healthbar != null)
            {
                Healthbar.Update(Position, health, maxHealth);
            }
        }

        /// <summary>
        /// Moves the interactive object based on the velocity
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public void Move(float elapsedSeconds)
        {
            if (Velocity.X != 0 || Velocity.Y != 0)
            {
                int prevTileRow = TileRow;
                int prevTileColumn = TileColumn;
                Vector2i[] prevOverlappingTiles = OverlappingTiles.ToArray();

                OnMoving?.Invoke(this, EventArgs.Empty);
                Position += Velocity * elapsedSeconds;
                OnMoved?.Invoke(this, EventArgs.Empty);

                if (IsCollidable)
                {
                    Vector2f backward = new Vector2f(0, 0);
                    foreach (Vector2i tileInfo in OverlappingTiles)
                    {
                        TileMapSystem.Tile tile;
                        if (GlobalReferences.MainGame.CurrentScene.MapManager.CurrentLevel.GetTileValue(tileInfo.Y, tileInfo.X, out tile))
                        {
                            bool tileCollision = (tile.Flags & GlobalReferences.FlagCollision) == GlobalReferences.FlagCollision;
                            bool doObjectCollision = tile.Flags - (tileCollision ? 1 : 0) > 2;

                            backward += DoCollisionDetection(Velocity * elapsedSeconds, tileInfo, tileCollision, doObjectCollision);
                        }
                    }

                    if (backward != new Vector2f(0, 0))
                        Position += backward;
                }
            }
        }

        /// <summary>
        /// Does a collision detection with tiles a near objects
        /// </summary>
        /// <param name="moveVec">Movement vector of the interactive object</param>
        /// <param name="tileInfo">Tile indics</param>
        /// <param name="tileCollision">Indicates whenther the tile is collidable or not</param>
        /// <param name="objectCollision">Indicates whenther a collidable objcet is near or not</param>
        /// <returns>Returns a backwards vector which can be used to prevent clipping</returns>
        private Vector2f DoCollisionDetection(Vector2f moveVec, Vector2i tileInfo, bool tileCollision, bool objectCollision)
        {
            if (tileCollision)
            {
                IntRect tileBox = GetTileBoundingBox(tileInfo);
                CollisionArgs args = new CollisionArgs(this, null);
                OnCollision?.Invoke(this, args);
                if (args.CancelBackward)
                    return new Vector2f(0, 0);
                
                return GetBackwardVector(tileBox, moveVec);
            }
            else if (objectCollision)
            {
                Vector2f backwardVec = new Vector2f(0, 0);
                Entity[] collidableEntities = GlobalReferences.MainGame.CurrentScene.Entities.GetEntities().Where(ent => ent.IsCollidable && ent != this && ent.OverlappingTiles.Contains(tileInfo)).ToArray();
                foreach (Entity ent in collidableEntities)
                {                
                    Vector2f backward = GetBackwardVector(ent.BoundingBox, moveVec);
                    if (backward != new Vector2f(0, 0))
                    {
                        CollisionArgs sourceArgs = new CollisionArgs(this, ent);
                        CollisionArgs destArgs = new CollisionArgs(ent, this);
                        OnCollision?.Invoke(this, new CollisionArgs(this, ent));
                       
                        if (ent is InteractiveObject)
                        {
                            (ent as InteractiveObject).CallCollisionEvent(destArgs);
                        }

                        if (!sourceArgs.CancelBackward && !destArgs.CancelBackward)
                            backwardVec += backward;
                    }
                }
                return backwardVec;
            }
            return new Vector2f(0, 0);
        }

        /// <summary>
        /// Calls the on collision event
        /// </summary>
        /// <param name="destArgs">Collision event args</param>
        protected void CallCollisionEvent(CollisionArgs destArgs)
        {
            OnCollision?.Invoke(this, destArgs);
        }

        /// <summary>
        /// Call the on kill event
        /// </summary>
        /// <param name="args">Attacker event args</param>
        protected void CallOnKillEvent(AttackerArgs args)
        {
            OnKill?.Invoke(this, args);
        }

        /// <summary>
        /// Returns the current dialog id
        /// </summary>
        /// <returns>Returns a dialog id</returns>
        public int GetDialog()
        {
            if (dialogIds != null && dialogIds.Count > 0)
            {
                return dialogIds[0];
            }
            return -1;
        }

        /// <summary>
        /// Removes the current dialog
        /// </summary>
        public void RemoveDialog()
        {
            if(dialogIds.Count > 0)
                dialogIds.RemoveAt(0);
        }

        /// <summary>
        /// Calculates a backward vector from collisions
        /// </summary>
        /// <param name="tileBox">Tile bounding box</param>
        /// <param name="moveVec">Movement vector of interactive object</param>
        /// <returns>Returns a backward vector</returns>
        private Vector2f GetBackwardVector(IntRect tileBox, Vector2f moveVec)
        {
            Vector2f backward = new Vector2f(0, 0);
            if (Position.X > tileBox.Left - Size.X && Position.X < tileBox.Left + tileBox.Width)
            {
                //Position.X > tileBox.Left
                if (moveVec.X > 0 && ((Position.X + Size.X) > tileBox.Left && (Position.X + Size.X) < tileBox.Left + tileBox.Width))
                {
                    backward = new Vector2f((Position.X + Size.X) - tileBox.Left, 0) * -1;
                }
                else if (moveVec.X < 0 && (Position.X < tileBox.Left + tileBox.Width && Position.X > tileBox.Left))
                {
                    backward = new Vector2f((tileBox.Left + tileBox.Width) - Position.X, 0);
                }
            }

            if (Position.Y > tileBox.Top - Size.Y && Position.Y < tileBox.Top + tileBox.Height)
            {
                if (moveVec.Y > 0 && ((Position.Y + Size.Y) > tileBox.Top && (Position.Y + Size.Y) < tileBox.Top + tileBox.Height))
                {
                    backward = new Vector2f(backward.X, (Position.Y + Size.Y) - tileBox.Top) * -1;
                }
                else if (moveVec.Y < 0 && ((Position.Y < tileBox.Top + tileBox.Height) && Position.Y > tileBox.Height))
                {
                    backward = new Vector2f(backward.X, (tileBox.Top + tileBox.Height) - Position.Y);
                }
            }

            return backward;
        }

        /// <summary>
        /// Draws InteractiveObject
        /// </summary>
        /// <param name="window">Window to render</param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

        public int ChangeHealth(int healthValue, Entity activator)
        {
            int value = health + healthValue;
             
            if (value < health)
                OnReceiveDamage?.Invoke(this, new AttackerArgs(activator));
            else if (value > health)
                OnReceiveHealth?.Invoke(this, new AttackerArgs(activator));

            health = value;

            if (health > maxHealth)
                health = maxHealth;

            if (maxHealth > 0 && health <= 0)
            {
                OnKill?.Invoke(this, new AttackerArgs(activator));
                CallOnDestroyEvent();
                healthBar.IsOpen = false;
            }

            if (healthBar != null)
                healthBar.Update(health, maxHealth);

            return health;
        }

        /// <summary>
        /// Clones an interactive object
        /// </summary>
        /// <returns>Returns a copy of the interactive object</returns>
        public override Entity Clone()
        {
            return new InteractiveObject(Size, Animations.Clone(), isMoveAble, speed, maxHealth, healthBar?.Clone(healthBar.IsFixed), groupId, factionId, false);
        }
    }
}
