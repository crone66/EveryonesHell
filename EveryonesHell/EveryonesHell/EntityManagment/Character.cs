/* 
 * Purpose: Gives Attributes and Important Methods to Characters
 * Author: Fabian Subat
 * Date: 04.11.2016
 */

using DebugConsole;
using SFML.Graphics;
using SFML.System;
using System;
using EveryonesHell.HUD;
namespace EveryonesHell.EntityManagment
{
    public class Character : InteractiveObject
    {
        private Vector2i lastDirection;
        protected float fireRate;
        protected float elaspedAttackTime;
        public event EventHandler OnShoot;

        /// <summary>
        /// Creates a character and passes the data to parent class with combined position
        /// </summary>
        /// <param name="position"></param> Sets the characters position
        /// <param name="size"></param> Defines the hitbox size for the character
        /// <param name="inventory"></param> Sets the inventory for the created character
        /// <param name="animations"></param> Passes the characters animations to the animation manager
        /// <param name="isMoveAble"></param> Defines if the character can move or not
        /// <param name="viewDirection"></param> Sets the initial viewing direction
        /// <param name="speed"></param> Sets the movement speed
        /// <param name="maxHealth"></param> Defines the maximum Health
        /// <param name="fireRate"></param> Defines the fire Rate
        /// <param name="healthBar"></param> Passes the healthbar for each character
        /// <param name="groupID"></param> Defines the group the character belongs to
        /// <param name="factionId"></param> Defines the faction the character belongs to
        /// <param name="dialogIds"></param> Passes the dialog IDs the character communicates with
        public Character(Vector2f position, Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, float fireRate, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds)
            :base(position, size, inventory, animations, isMoveAble, viewDirection, speed, maxHealth, healthBar, groupID, factionId, dialogIds)
        {
            this.fireRate = fireRate;
        }

        /// <summary>
        /// Creates a character and passes the data to parent class with position set by Tiles
        /// </summary>
        /// <param name="tileRow"></param> Character Position by Row of Tiles
        /// <param name="tileColumn"></param> Character Position by Column of Tiles
        /// <param name="size"></param> Defines the hitbox size for the character
        /// <param name="inventory"></param> Sets the inventory for the created character
        /// <param name="animations"></param> Passes the characters animations to the animation manager
        /// <param name="isMoveAble"></param> Defines if the character can move or not
        /// <param name="viewDirection"></param> Sets the initial viewing direction
        /// <param name="speed"></param> Sets the movement speed
        /// <param name="maxHealth"></param> Defines the maximum Health
        /// <param name="fireRate"></param> Defines the fire Rate
        /// <param name="healthBar"></param> Passes the healthbar for each character
        /// <param name="groupID"></param> Defines the group the character belongs to
        /// <param name="factionId"></param> Defines the faction the character belongs to
        /// <param name="dialogIds"></param> Passes the dialog IDs the character communicates with
        /// <param name="isPrototyp"></param> Defines whether the character is used for prototyping
        public Character(int tileRow, int tileColumn, Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, float fireRate, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds, bool isPrototyp)
            : base(tileRow, tileColumn, size, inventory, animations, isMoveAble, viewDirection, speed, maxHealth, healthBar, groupID, factionId, dialogIds, isPrototyp)
        {
            this.fireRate = fireRate;
        }

        /// <summary>
        /// Creates a character and passes the data to parent class
        /// /// </summary>
        /// <param name="size"></param> Defines the hitbox size for the character
        /// <param name="inventory"></param> Sets the inventory for the created character
        /// <param name="animations"></param> Passes the characters animations to the animation manager
        /// <param name="isMoveAble"></param> Defines if the character can move or not
        /// <param name="viewDirection"></param> Sets the initial viewing direction
        /// <param name="speed"></param> Sets the movement speed
        /// <param name="maxHealth"></param> Defines the maximum Health
        /// <param name="fireRate"></param> Defines the fire Rate
        /// <param name="healthBar"></param> Passes the healthbar for each character
        /// <param name="groupID"></param> Defines the group the character belongs to
        /// <param name="factionId"></param> Defines the faction the character belongs to
        /// <param name="dialogIds"></param> Passes the dialog IDs the character communicates with
        /// <param name="isPrototyp"></param> Defines whether the character is used for prototyping
        public Character(Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, int maxHealth, float fireRate, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds, bool isPrototyp)
            : base(size, inventory, animations, isMoveAble, viewDirection, speed, maxHealth, healthBar, groupID, factionId, dialogIds, isPrototyp)
        {
            this.fireRate = fireRate;
        }

        /// <summary>
        /// Updates the character
        /// </summary>
        /// <param name="elapsedSeconds"></param> Elapsed game Time in Seconds
        public override void Update(float elapsedSeconds)
        {
            elaspedAttackTime += elapsedSeconds;
            base.Update(elapsedSeconds);
        }

        /// <summary>
        /// Draws the character to the game window
        /// </summary>
        /// <param name="window"></param>
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
        /// Will be fired when the attack button is pressed aca the trigger is pulled
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Command arguments</param>
        public virtual void OnAttack(object sender, ExecuteCommandArgs e)
        {
            if (elaspedAttackTime > fireRate)
            {
                elaspedAttackTime = 0f;
                Entity ent = EntityFactory.Clone("Bullet");
                if (ent is Projectile)
                {
                    Projectile projectile = ent as Projectile;
                    projectile.Init(Position, Size, ViewDirection, FactionId, Id);
                    OnShoot?.Invoke(projectile, null);
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
