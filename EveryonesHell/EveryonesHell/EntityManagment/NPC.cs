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

        public NPC(Vector2f position, Vector2i size, Vector2f viewDirection, AnimationManager animations, Gaugebar healthBar, float speed, int maxHealth, float fireRate, int groupID, int factionId, int[] dialogIds)
            :base(position, size, new InventorySystem.Inventory(32), animations, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID,factionId, dialogIds)
        {
            moveTime = 2.5f;
            idleTime = 5f;
            elapsedIdleTime = 0;
            elapsedMoveTime = 0;
            idle = true;
        }

        public NPC(int tileRow, int tileColumn, Vector2i size, Vector2f viewDirection, AnimationManager animations, Gaugebar healthBar, float speed, int maxHealth, float fireRate, int groupID, int factionId, int[] dialogIds)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), animations, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID, factionId, dialogIds)
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
        /// <param name="elapsedSeconds"></param>
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

        private void Movement(float elapsedSeconds)
        {
            elapsedDirectionTime += elapsedSeconds;
            if(elapsedDirectionTime >= directionTime)
            {
                CreateNewDirection();
            }
        }

        private void CreateNewDirection()
        {
            x = GlobalReferences.Randomizer.Next(-1, 2);
            y = GlobalReferences.Randomizer.Next(-1, 2);

            directionTime = GlobalReferences.Randomizer.Next(5, 15) / 10;
            elapsedDirectionTime = 0f;
        }

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

        private bool IsEnemyInRange()
        {
            //player can be replaced with all entities/NPCS
            int width = (int)GlobalReferences.MainGame.WindowWidth;
            int height = (int)GlobalReferences.MainGame.WindowHeight;
            Player player = GlobalReferences.MainGame.CurrentScene.Player;
            if (player != null && player.FactionId != this.FactionId && player.Visable)
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

        private void Hunt()
        {
            y = 0;
            x = 0;
            int distance = 300;

            float xDiff = enemy.Position.X - Position.X;
            float yDiff = enemy.Position.Y - Position.Y;

            if (!(Between(xDiff, -1, 1) && (yDiff <= distance && yDiff >= -distance)) && !(Between(yDiff, -1, 1) && (xDiff <= distance && xDiff >= -distance)))
            {
                if ((xDiff < 0 ? xDiff * -1 : xDiff) < (yDiff < 0 ? yDiff * -1 : yDiff) || Between(yDiff, -1, 1))
                {
                    if (Between(yDiff, -1, 1) && (xDiff > distance || xDiff < -distance))
                    {
                        x = xDiff > 0 ? 1 : -1;
                    }
                    else if (!Between(yDiff, -1, 1))
                    {
                        x = xDiff > 0 ? 1 : -1;
                    }
                }
                else
                {
                    if (Between(xDiff, -1, 1) || (yDiff > distance || yDiff < -distance))
                    {
                        y = yDiff > 0 ? 1 : -1;
                    }
                    else if (!Between(xDiff, -1, 1))
                    {
                        y = yDiff > 0 ? 1 : -1;
                    }
                }

                SetVelocity();
            }
            else
            {
                ViewDirection = new Vector2f(Between(xDiff, -1, 1) ? 0 : xDiff > 0 ? 1 : -1, Between(yDiff, -1, 1) ? 0 : yDiff > 0 ? 1 : -1);
            }

            if (Between(xDiff, -1, 1) || Between(yDiff, -1, 1))
                Attack();
        }

        private void Attack()
        {
            //attack when in view
            OnAttack(this, null);
        }

        private bool Between(float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Draws the NPC
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);      
        }
    }
}
