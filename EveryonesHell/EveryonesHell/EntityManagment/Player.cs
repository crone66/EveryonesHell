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
        private Vector2f lastDirection;
        private HUD.DialogSystem dialog;
        private bool jetpackActive = false;
        private Entity lastCollisionObject;

        private QuestManagment.QuestTracker questTracker;

        public bool JetpackActive
        {
            get
            {
                return jetpackActive;
            }

            set
            {
                jetpackActive = value;
            }
        }

        /// <summary>
        /// Initializes a new Player - outdated
        /// </summary>
        /// <param name="position">Position of the player</param>
        /// <param name="size">Size of the player</param>
        /// <param name="sprite">Sprite of the player</param>
        /// <param name="dialog">Dialog system</param>
        public Player(Vector2f position, Vector2i size, Sprite sprite, HUD.DialogSystem dialog, Gaugebar healthBar, int groupID, QuestManagment.QuestTracker questTracker, int factionId)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 3, 4, 50, 50, 0.16f), true, new Vector2f(1, 0), 620, healthBar, groupID, factionId, null)
        {
            lastDirection = new Vector2f(0, 0);
            this.dialog = dialog;
            this.dialog.OnDialogChanged += Dialog_OnDialogChanged;
            OnShoot += Player_OnShoot;
            this.questTracker = questTracker;
            questTracker.inventory = Inventory;

            OnCollision += InteractiveObject_OnCollision;
        }

        /// <summary>
        /// Initializes a new Player
        /// </summary>
        /// <param name="tileRow">Tile row index</param>
        /// <param name="tileColumn">Tile column index</param>
        /// <param name="size">Size of the player</param>
        /// <param name="sprite">Sprite of the player</param>
        /// <param name="dialog">Dialog system</param>
        public Player(int tileRow, int tileColumn, Vector2i size, Sprite sprite, HUD.DialogSystem dialog, Gaugebar healthBar, int groupID, QuestManagment.QuestTracker questTracker, int factionId)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 3, 4, 50, 50, 0.16f), true, new Vector2f(1, 0), 620, healthBar, groupID, factionId, null)
        {
            lastDirection = new Vector2f(0, 0);
            this.dialog = dialog;
            this.dialog.OnDialogChanged += Dialog_OnDialogChanged;
            OnShoot += Player_OnShoot;
            this.questTracker = questTracker;
            questTracker.inventory = Inventory;
            OnCollision += InteractiveObject_OnCollision;
        }

        private void Dialog_OnDialogChanged(object sender, DialogChangedArgs e)
        {
            if (GlobalReferences.MainGame.CurrentScene.Quests.Quests.ToList().Exists(q => q.BasedOnDialogue == e.PrevDialogId))
            {
                FileDescriptions.Quest quest = GlobalReferences.MainGame.CurrentScene.Quests.Quests.First(q => q.BasedOnDialogue == e.PrevDialogId);
                questTracker.ActivateQuest(quest.QuestID);
            }
        }

        private void Player_OnShoot(object sender, EventArgs e)
        {
            if (sender != null && sender is Projectile)
            {
                Projectile projectile = (sender as Projectile);
                projectile.OnDoDamage += questTracker.Projectile_OnDoDamage;
            }
        }

        private void InteractiveObject_OnCollision(object sender, CollisionArgs e)
        {
            Entity otherObject = null;
            if (e.CollisionObjectDest != null && e.CollisionObjectDest != this)
                otherObject = e.CollisionObjectDest;
            else if (e.CollisionObjectSource != null && e.CollisionObjectSource != this)
                otherObject = e.CollisionObjectSource;

            if (otherObject != null)
            {
                lastCollisionObject = otherObject;
                
                //Interaction key pressed?
            }
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);
            if (lastDirection != ViewDirection)
               lastCollisionObject = null;
           
            questTracker.ItemQuest();
        }

        public override void OnMove(object sender, ExecuteCommandArgs e)
        {
            lastDirection = ViewDirection;
            base.OnMove(sender, e);
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
            if (lastCollisionObject != null && lastCollisionObject is InteractiveObject)
            {
                InteractiveObject io = lastCollisionObject as InteractiveObject;
                dialog.Open(io.GetDialog());
            }
            //dialog.Open(0);//, Position + (new Vector2f(Size.X / 2, Size.Y / 2)));
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
            JetpackActive = !JetpackActive;

            if (JetpackActive)
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
