/* 
 * Purpose: Gives Attributes and Important Methods to Characters
 * Author: Fabian Subat
 * Date: 04.11.2016
 */

using DebugConsole;
using SFML.Graphics;
using SFML.System;
using System;

namespace EveryonesHell.EntityManagment
{
    public class Character : InteractiveObject
    {
        private Vector2i lastDirection;
        private float attackDelay = 0.5f;
        private float elaspedAttackTime;
        public event EventHandler OnShoot;

        public Character(Vector2f position, Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, Gaugebar healthBar, int groupID)
            :base(position, size, inventory, animations, isMoveAble, viewDirection, speed, healthBar, groupID)
        {
        }

        public Character(int tileRow, int tileColumn, Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, Gaugebar healthBar, int groupID)
            : base(tileRow, tileColumn, size, inventory, animations, isMoveAble, viewDirection, speed, healthBar, groupID)
        {
        }

        public override void Update(float elapsedSeconds)
        {
            elaspedAttackTime += elapsedSeconds;
            base.Update(elapsedSeconds);
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

        /// <summary>
        /// Will be fired when one of the OnMove buttons is pressed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Command arguments</param>
        public virtual void OnMove(object sender, ExecuteCommandArgs e)
        {
            if (ValidateArgs<Vector2i>(e.Args, 0))
            {
                Vector2i direction = (Vector2i)e.Args[0];
                if (direction != lastDirection)
                {
                    if (lastDirection.X != direction.X)
                        direction.Y = 0;
                    else
                        direction.X = 0;
                }

                Velocity = new Vector2f(direction.X * Speed, direction.Y * Speed);
                lastDirection = direction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAttack(object sender, ExecuteCommandArgs e)
        {
            if (elaspedAttackTime > attackDelay)
            {
                elaspedAttackTime = 0f;
                Entity ent = EntityFactory.Clone("Bullet");
                if (ent is Projectile)
                {
                    Projectile projectile = ent as Projectile;
                    projectile.Init(Position, Size, ViewDirection, -1);
                    OnShoot(projectile, null);
                }
            }
        }

        /// <summary>
        /// Validates command args
        /// </summary>
        /// <typeparam name="T">Expected argument type</typeparam>
        /// <param name="args">Array of arguments</param>
        /// <param name="index">Array index</param>
        /// <returns>Returns true when Argument equals the expected type</returns>
        protected bool ValidateArgs<T>(object[] args, int index)
        {
            return (args != null && args.Length > index && args[index] is T);
        }
    }
}
