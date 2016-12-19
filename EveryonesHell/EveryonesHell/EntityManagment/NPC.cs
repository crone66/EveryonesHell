using SFML.Graphics;
using SFML.System;
using EveryonesHell.HUD;

namespace EveryonesHell.EntityManagment
{
    public class NPC : Character
    {
        private float moveTime;
        private float idleTime;
        private float elapsedMoveTime;
        private float elapsedIdleTime;
        private bool idle;
        private int x;
        private int y;

        private float directionTime;
        private float elapsedDirectionTime;
        
        private Player enemy;

        /// <summary>
        /// Creates an NPC with position and standard params
        /// </summary>
        /// <param name="position">Initial position of the NPC</param>
        /// <param name="size">The NPC's hitbox size</param>
        /// <param name="viewDirection">Initial viewing direction</param>
        /// <param name="animations">Passes NPC's animation </param>
        /// <param name="healthBar">Passes the NPC's healthbar</param>
        /// <param name="speed">Defines the movement speed</param>
        /// <param name="maxHealth">Sets the maximum health</param>
        /// <param name="fireRate">Controls the Fire Rate</param>
        /// <param name="groupID">Defines which group the NPC belongs to</param>
        /// <param name="factionId">Defines which faction the NPC belongs to</param>
        /// <param name="dialogIds">Passes the dialog ID's the NPC communicates with</param>
        public NPC(Vector2f position, Vector2i size, Vector2f viewDirection, AnimationManager animations, Gaugebar healthBar, float speed, int maxHealth, float fireRate, int groupID, int factionId, int[] dialogIds)
            :base(position, size, new InventorySystem.Inventory(32), animations, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID,factionId, dialogIds)
        {
            moveTime = 2.5f;
            idleTime = 5f;
            elapsedIdleTime = 0;
            elapsedMoveTime = 0;
            idle = true;
        }

        /// <summary>
        /// Creates NPC with position set by tiles and standard params and prototyping
        /// </summary>
        /// <param name="tileRow">Sets the X Position by row of tiles</param>
        /// <param name="tileColumn">Sets the Y position by column of tiles</param>
        /// <param name="size">The NPC's hitbox size</param>
        /// <param name="viewDirection">Initial viewing direction</param>
        /// <param name="animations">Passes NPC's animation </param>
        /// <param name="healthBar">Passes the NPC's healthbar</param>
        /// <param name="speed">Defines the movement speed</param>
        /// <param name="maxHealth">Sets the maximum health</param>
        /// <param name="fireRate">Controls the Fire Rate</param>
        /// <param name="groupID">Defines which group the NPC belongs to</param>
        /// <param name="factionId">Defines which faction the NPC belongs to</param>
        /// <param name="dialogIds">Passes the dialog ID's the NPC communicates with</param>
        /// <param name="isPrototyp"></param> Defines whether the NPC is used for prototyping
        public NPC(int tileRow, int tileColumn, Vector2i size, Vector2f viewDirection, AnimationManager animations, Gaugebar healthBar, float speed, int maxHealth, float fireRate, int groupID, int factionId, int[] dialogIds, bool isPrototyp)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), animations, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID, factionId, dialogIds, isPrototyp)
        {
            moveTime = 2.5f;
            idleTime = 5f;
            elapsedIdleTime = 0;
            elapsedMoveTime = 0;
            idle = true;
        }

        /// <summary>
        ///  Creates NPC without position
        /// </summary>
        /// <param name="size">The NPC's hitbox size</param>
        /// <param name="viewDirection">Initial viewing direction</param>
        /// <param name="animations">Passes NPC's animation </param>
        /// <param name="healthBar">Passes the NPC's healthbar</param>
        /// <param name="speed">Defines the movement speed</param>
        /// <param name="maxHealth">Sets the maximum health</param>
        /// <param name="fireRate">Controls the Fire Rate</param>
        /// <param name="groupID">Defines which group the NPC belongs to</param>
        /// <param name="factionId">Defines which faction the NPC belongs to</param>
        /// <param name="dialogIds">Passes the dialog ID's the NPC communicates with</param>
        /// <param name="isPrototyp"></param> Defines whether the NPC is used for prototyping
        public NPC(Vector2i size, Vector2f viewDirection, AnimationManager animations, Gaugebar healthBar, float speed, int maxHealth, float fireRate, int groupID, int factionId, int[] dialogIds, bool isPrototyp)
            : base(size, new InventorySystem.Inventory(32), animations, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID, factionId, dialogIds, isPrototyp)
        {
            moveTime = 2.5f;
            idleTime = 5f;
            elapsedIdleTime = 0;
            elapsedMoveTime = 0;
            idle = true;
        }

        /// <summary>
        /// Updates the NPC
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed Game Time in Seconds</param>
        public override void Update(float elapsedSeconds)
        {
            if (IsMoveAble && !Freeze)
            {
                if (IsEnemyInRange())
                {
                    Hunt();
                }
                else
                {
                    Do(elapsedSeconds);
                }

                SetVelocity();
            }
            base.Update(elapsedSeconds);
        }

        /// <summary>
        /// Lets the NPC move around if idle
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed Game Time in Seconds</param>
        private void Do(float elapsedSeconds)
        {
            if (idle)
            {
                elapsedIdleTime += elapsedSeconds;
                if (elapsedIdleTime >= idleTime)
                {
                    elapsedIdleTime = 0f;
                    idle = false;
                    
                    CreateNewDirection();
                }
            }
            else
            {
                elapsedMoveTime += elapsedSeconds;
                if (elapsedMoveTime < moveTime)
                {
                    Movement(elapsedSeconds);
                }
                else
                {
                    elapsedMoveTime = 0f;
                    idle = true;
                    x = 0;
                    y = 0;
                }
            }
        }

        /// <summary>
        /// Moves the NPC for idling
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed Game Time in Seconds</param>
        private void Movement(float elapsedSeconds)
        {
            elapsedDirectionTime += elapsedSeconds;
            if(elapsedDirectionTime >= directionTime)
            {
                CreateNewDirection();
            }
        }

        /// <summary>
        /// Creates a new direction for random idle movement
        /// </summary>
        private void CreateNewDirection()
        {
            x = GlobalReferences.Randomizer.Next(-1, 2);
            y = GlobalReferences.Randomizer.Next(-1, 2);

            directionTime = GlobalReferences.Randomizer.Next(5, 15) / 10;
            elapsedDirectionTime = 0f;
        }

        /// <summary>
        /// Sets the velocity for hunting the player
        /// </summary>
        private void SetVelocity()
        {
            if(x != 0 && y != 0)
            {
                if (GlobalReferences.Randomizer.Next(0, 2) == 0)
                    y = 0;
                else
                    x = 0;
            }

            if (x != 0 || y != 0)
            {
                ViewDirection = new Vector2f(x, y);
                Velocity = new Vector2f(x, y) * Speed;
            }
        }

        /// <summary>
        /// Checks whether and enemy for the NPC is in range
        /// </summary>
        /// <returns></returns>
        private bool IsEnemyInRange()
        {
            //player can be replaced with all entities/NPCS
            int width = (int)GlobalReferences.MainGame.WindowWidth;
            int height = (int)GlobalReferences.MainGame.WindowHeight;
            Player player = GlobalReferences.MainGame.CurrentScene.Player;
            if (player != null && player.FactionId != this.FactionId && player.IsVisable)
            {
                IntRect boundingBox = new IntRect(new Vector2i((int)Position.X - (height / 2), (int)Position.Y - (height / 2)), new Vector2i(width, height));
                if (player.BoundingBox.Intersects(boundingBox))
                {
                    enemy = player;
                    return true;
                }
                else
                {
                    enemy = null;
                }
            }
            return false;
        }

        /// <summary>
        /// Makes the NPC Hunt his enemies
        /// </summary>
        private void Hunt()
        {
            y = 0;
            x = 0;
            int distance = 300;

            float xDiff = enemy.Position.X - Position.X;
            float yDiff = enemy.Position.Y - Position.Y;

            if (!(Between(xDiff, -4, 4) && (yDiff <= distance && yDiff >= -distance)) && !(Between(yDiff, -4, 4) && (xDiff <= distance && xDiff >= -distance)))
            {
                if ((xDiff < 0 ? xDiff * -1 : xDiff) < (yDiff < 0 ? yDiff * -1 : yDiff) || Between(yDiff, -4, 4))
                {
                    if (Between(yDiff, -4, 4) && (xDiff > distance || xDiff < -distance))
                    {
                        x = xDiff > 0 ? 1 : -1;
                    }
                    else if (!Between(yDiff, -4, 4))
                    {
                        x = xDiff > 0 ? 1 : -1;
                    }
                }
                else
                {
                    if (Between(xDiff, -4, 4) || (yDiff > distance || yDiff < -distance))
                    {
                        y = yDiff > 0 ? 1 : -1;
                    }
                    else if (!Between(xDiff, -4, 4))
                    {
                        y = yDiff > 0 ? 1 : -1;
                    }
                }

                SetVelocity();
            }
            else
            {
                Vector2f preViewDirection = ViewDirection;
                ViewDirection = new Vector2f((Between(xDiff, -4, 4) ? 0 : (xDiff > 0 ? 1 : -1)), (Between(yDiff, -4, 4) ? 0 : (yDiff > 0 ? 1 : -1)));
                if(preViewDirection != ViewDirection)
                    Velocity = ViewDirection;
            }

            if (Between(xDiff, -4, 4) || Between(yDiff, -4, 4))
            {
                Attack();
            }
        }

        /// <summary>
        /// Attacks his enemies if they are in range
        /// </summary>
        private void Attack()
        {
            //attack when in view
            OnAttack(this, null);
        }

        /// <summary>
        /// Value helper for hunting method
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private bool Between(float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Draws the NPC
        /// </summary>
        /// <param name="window">Sets the gamewindow the NPC is drawn to</param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);      
        }

        /// <summary>
        /// Handlings cloning the NPC for prototyping
        /// </summary>
        /// <returns>Returns the prototype NPC</returns>
        public override Entity Clone()
        {
            return new NPC(Position, Size, ViewDirection, Animations, Healthbar, Speed, MaxHealth, fireRate, GroupID, FactionId, dialogIds.ToArray());
        }
    }
}
