using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using DebugConsole;
using SFML.System;

namespace EveryonesHell.EntityManagment
{
    public class Player : Character
    {
        private Vector2i lastDirection;
        private HUD.DialogSystem dialog;

        /// <summary>
        /// Initzializes a new Player
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <param name="size">Size of the player</param>
        /// <param name="sprite">Sprite of the player</param>
        /// <param name="dialog">Dialog system</param>
        public Player(Vector2f position, Vector2i size, Sprite sprite, HUD.DialogSystem dialog)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new Vector2f(1, 0), 620)
        {
            lastDirection = new Vector2i(0, 0);
            this.dialog = dialog;
        }

        /// <summary>
        /// Initzializes a new Player
        /// </summary>
        /// <param name="tileRow">Tile row index</param>
        /// <param name="tileColumn">Tile column index</param>
        /// <param name="size">Size of the player</param>
        /// <param name="sprite">Sprite of the player</param>
        /// <param name="dialog">Dialog system</param>
        public Player(int tileRow, int tileColumn, Vector2i size, Sprite sprite, HUD.DialogSystem dialog)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new Vector2f(1, 0), 620)
        {
            lastDirection = new Vector2i(0, 0);
            this.dialog = dialog;
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);
            dialog.Input(elapsedSeconds);
            dialog.Update(elapsedSeconds);
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="window">Window to render</param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
            dialog.Draw(window);
        }

        /// <summary>
        /// Will be fired when one of the OnMove buttons is pressed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Command arguments</param>
        public void OnMove(object sender, ExecuteCommandArgs e)
        {
            if (!dialog.IsVisable)
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
        }

        /// <summary>
        /// Will be fired when the OnAction button is pressed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Command arguments</param>
        public void OnAction(object sender, ExecuteCommandArgs e)
        {
            dialog.Open(0);
            //TODO: Action tile location
            // => Get object on action tile
            // => if object exists Execute Action 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAttack(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJetpack(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Validates command args
        /// </summary>
        /// <typeparam name="T">Expected argument type</typeparam>
        /// <param name="args">Array of arguments</param>
        /// <param name="index">Array index</param>
        /// <returns>Returns true when Argument equals the expected type</returns>
        private bool ValidateArgs<T>(object[] args, int index)
        {
            return (args != null && args.Length > index && args[index] is T);
        }
    }
}
