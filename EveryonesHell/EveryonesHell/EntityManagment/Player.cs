﻿using System;
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
        private bool jetpackActive = false;

        private QuestManagment.QuestTracker questTracker;

        /// <summary>
        /// Initializes a new Player - outdated
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <param name="size">Size of the player</param>
        /// <param name="sprite">Sprite of the player</param>
        /// <param name="dialog">Dialog system</param>
        public Player(Vector2f position, Vector2i size, Sprite sprite, HUD.DialogSystem dialog, Gaugebar healthBar, int groupID, QuestManagment.QuestTracker questTracker)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new Vector2f(1, 0), 620, healthBar, groupID)
        {
            lastDirection = new Vector2i(0, 0);
            this.dialog = dialog;
            OnShoot += Player_OnShoot;
            this.questTracker = questTracker;
            questTracker.inventory = Inventory;
        }

        /// <summary>
        /// Initializes a new Player
        /// </summary>
        /// <param name="tileRow">Tile row index</param>
        /// <param name="tileColumn">Tile column index</param>
        /// <param name="size">Size of the player</param>
        /// <param name="sprite">Sprite of the player</param>
        /// <param name="dialog">Dialog system</param>
        public Player(int tileRow, int tileColumn, Vector2i size, Sprite sprite, HUD.DialogSystem dialog, Gaugebar healthBar, int groupID, QuestManagment.QuestTracker questTracker)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new Vector2f(1, 0), 620, healthBar, groupID)
        {
            lastDirection = new Vector2i(0, 0);
            this.dialog = dialog;
            OnShoot += Player_OnShoot;
            this.questTracker = questTracker;
            questTracker.inventory = Inventory;
        }

        private void Player_OnShoot(object sender, EventArgs e)
        {
            if(sender != null && sender is Projectile)
            {
                Projectile projectile = (sender as Projectile);
                projectile.OnDoDamage += questTracker.Projectile_OnDoDamage;
            }
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);
            questTracker.ItemQuest();
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="window">Window to render</param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }

        /// <summary>
        /// Will be fired when the OnAction button is pressed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Command arguments</param>
        public void OnAction(object sender, ExecuteCommandArgs e)
        {
            dialog.Open(0);//, Position + (new Vector2f(Size.X / 2, Size.Y / 2)));
            //TODO: Action tile location
            // => Get object on action tile
            // => if object exists Execute Action 
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnJetpack(object sender, ExecuteCommandArgs e)
        {
            jetpackActive = !jetpackActive;

            if (jetpackActive)
            {
                IsCollidable = false;
            }
            else
            {
                IsCollidable = true;
            }
        }
    }
}
