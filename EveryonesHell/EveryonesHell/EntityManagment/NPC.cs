using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public NPC(Vector2f position, Vector2i size, Sprite sprite, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 3, 4, 50, 50, 0.16f), true, new Vector2f(1, 0), 220, healthBar, groupID,factionId, dialogIds)
        {
            moveTime = 2.5f;
            idleTime = 5f;
            elapsedIdleTime = 0;
            elapsedMoveTime = 0;
            idle = true;
        }

        public NPC(int tileRow, int tileColumn, Vector2i size, Sprite sprite, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 3, 4, 50, 50, 0.16f), true, new Vector2f(1, 0), 220, healthBar, groupID, factionId, dialogIds)
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
                Do(elapsedSeconds);
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

            ViewDirection = new Vector2f(x, y);
            Velocity = new Vector2f(x, y) * Speed;
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
