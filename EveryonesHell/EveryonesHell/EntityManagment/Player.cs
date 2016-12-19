using System;
using System.Linq;
using SFML.Graphics;
using DebugConsole;
using SFML.System;
using EveryonesHell.HUD;
using EveryonesHell.EntityManagment.Items;

namespace EveryonesHell.EntityManagment
{
    public class Player : Character
    {
        private Vector2f lastDirection;
        private HUD.DialogSystem dialog;
        private bool jetpackActive = false;
        private InteractiveObject lastCollisionObject;
        private float elapsedTime, delay;
        private QuestManagment.QuestTracker questTracker;
        private int ammo;
        private int maxAmmo;
        private Gaugebar ammunitionBar;

        /// <summary>
        /// Getter and setter for activation of the Jetpack
        /// </summary>
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
        /// Creates a new Player and passes the data to parent class.
        /// </summary>
        /// <param name="position">Initial player Position</param>
        /// <param name="size">Size of the players hitbox</param>
        /// <param name="viewDirection">Initial view Direction</param>
        /// <param name="animation">Provides the animation Manager with the animation for the player</param>
        /// <param name="dialog">Provides dialogs for the created player</param>
        /// <param name="healthBar">Passes the players healthbar</param>
        /// <param name="ammunition">Passes the players max Ammunition</param>
        /// <param name="speed">Sets the players movement speed</param> 
        /// <param name="maxHealth">Sets the players maximum Health</param>
        /// <param name="fireRate">Controls players fire Rate</param> 
        /// <param name="groupID">Defines the group the player belongs to</param> 
        /// <param name="questTracker">Passes the questtracker</param>
        /// <param name="factionId">Defines the factione the player belongs to</param> 
        public Player(Vector2f position, Vector2i size, Vector2f viewDirection, AnimationManager animation, DialogSystem dialog, Gaugebar healthBar, Gaugebar ammunition, float speed, int maxHealth, float fireRate, int groupID, QuestManagment.QuestTracker questTracker, int factionId)
            :base(position, size, new InventorySystem.Inventory(32), animation, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID, factionId, null)
        {
            lastDirection = new Vector2f(0, 0);
            this.dialog = dialog;
            this.dialog.OnDialogChanged += Dialog_OnDialogChanged;
            OnShoot += Player_OnShoot;
            this.questTracker = questTracker;
            questTracker.inventory = Inventory;
            questTracker.OnQuestFinished += QuestTracker_OnQuestFinished;
            delay = 3.0f;
            ammo = 100;
            maxAmmo = 100;
            OnCollision += InteractiveObject_OnCollision;
            ammunitionBar = ammunition;
            ammunitionBar.Init(ammo, maxAmmo);
        }

        /// <summary>
        /// Creates a new Player and passes the data to parent class.
        /// </summary>
        /// <param name="tileRow">Player Position by Row of Tiles</param> 
        /// <param name="tileColumn"> Player Position by Column of Tiles</param>
        /// <param name="size">Size of the players hitbox</param>
        /// <param name="viewDirection">Initial view Direction</param>
        /// <param name="animation">Provides the animation Manager with the animation for the player</param>
        /// <param name="dialog">Provides dialogs for the created player</param>
        /// <param name="healthBar">Passes the players healthbar</param>
        /// <param name="ammunition">Passes the players max Ammunition</param>
        /// <param name="speed">Sets the players movement speed</param> 
        /// <param name="maxHealth">Sets the players maximum Health</param>
        /// <param name="fireRate">Controls players fire Rate</param> 
        /// <param name="groupID">Defines the group the player belongs to</param> 
        /// <param name="questTracker">Passes the questtracker</param>
        /// <param name="factionId">Defines the factione the player belongs to</param> 
        public Player(int tileRow, int tileColumn, Vector2i size, Vector2f viewDirection, AnimationManager animation, DialogSystem dialog, Gaugebar healthBar, Gaugebar ammunition, float speed, int maxHealth, float fireRate, int groupID, QuestManagment.QuestTracker questTracker, int factionId)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), animation, true, viewDirection, speed, maxHealth, fireRate, healthBar, groupID, factionId, null, false)
        {
            lastDirection = new Vector2f(0, 0);
            this.dialog = dialog;
            this.dialog.OnDialogChanged += Dialog_OnDialogChanged;
            OnShoot += Player_OnShoot;
            this.questTracker = questTracker;
            questTracker.inventory = Inventory;
            questTracker.OnQuestFinished += QuestTracker_OnQuestFinished;
            delay = 3.0f;
            ammo = 100;
            maxAmmo = 100;
            OnCollision += InteractiveObject_OnCollision;
            ammunitionBar = ammunition;
            ammunitionBar.Init(ammo, maxAmmo);
        }

        /// <summary>
        /// Replenishes the Players Ammo after finishing a quest
        /// </summary>
        /// <param name="sender">Caller of the method</param> 
        /// <param name="e">Event Arguments</param> 
        private void QuestTracker_OnQuestFinished(object sender, EventArgs e)
        {
            ammo += 100;
            maxAmmo += 100;
            if (maxAmmo > ammo)
                ammo = maxAmmo;

            ammunitionBar.Update(ammo, maxAmmo);
        }

        /// <summary>
        /// Handles interacting with the Dialog
        /// </summary>
        /// <param name="sender">Caller of the method</param> 
        /// <param name="e">Event Arguments</param> 
        private void Dialog_OnDialogChanged(object sender, DialogChangedArgs e)
        {
            if (GlobalReferences.MainGame.CurrentScene.Quests.Quests.ToList().Exists(q => q.BasedOnDialogue == e.PrevDialogId))
            {                
                FileDescriptions.Quest quest = GlobalReferences.MainGame.CurrentScene.Quests.Quests.First(q => q.BasedOnDialogue == e.PrevDialogId);
                if(questTracker.ActivateQuest(quest.QuestID))
                    lastCollisionObject.RemoveDialog();
            }

            if(e.NewDialogId == -1)
            {
                lastCollisionObject.Freeze = false;
                lastCollisionObject = null;
            }
        }

        /// <summary>
        /// Called if the player pulls the trigger
        /// </summary>
        /// <param name="sender">Caller of the method</param> 
        /// <param name="e">Event Arguments</param> 
        private void Player_OnShoot(object sender, EventArgs e)
        {
            if (sender != null && sender is Projectile)
            {
                Projectile projectile = (sender as Projectile);
                projectile.OnDoDamage += questTracker.Projectile_OnDoDamage;
            }
        }

        /// <summary>
        /// Called on collision with other interactive Objects / Entitys
        /// </summary>
        /// <param name="sender">Caller of the method</param> 
        /// <param name="e">Event Arguments</param> 
        private void InteractiveObject_OnCollision(object sender, CollisionArgs e)
        {
            Entity otherObject = null;
            if (e.CollisionObjectDest != null && e.CollisionObjectDest != this)
                otherObject = e.CollisionObjectDest;
            else if (e.CollisionObjectSource != null && e.CollisionObjectSource != this)
                otherObject = e.CollisionObjectSource;

            if (otherObject != null && lastCollisionObject == null && otherObject is InteractiveObject)
            {
                lastCollisionObject = otherObject as InteractiveObject;
            }
        }

        /// <summary>
        /// Updates the player with the game Time
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public override void Update(float elapsedSeconds)
        {
            if (!Visable)
            {
                elapsedTime += elapsedSeconds;

                if (elapsedTime >= delay)
                {
                    Respawn();
                }
            }

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
            if (lastCollisionObject != null)
            {
                if (lastCollisionObject is Item)
                {
                    Item item = lastCollisionObject as Item;
                    if (!item.PickUp(this))
                        item.Use(this);
                }
                else
                {
                    int id = lastCollisionObject.GetDialog();
                    if (id > -1)
                    {
                        lastCollisionObject.Freeze = true;
                        dialog.Open(lastCollisionObject.GetDialog());
                    }
                }
            }
            //dialog.Open(0);//, Position + (new Vector2f(Size.X / 2, Size.Y / 2)));
            //TODO: Action tile location
            // => Get object on action tile
            // => if object exists Execute Action 
        }

        /// <summary>
        /// Is called for the player shooting and ammunition decrease
        /// </summary>
        /// <param name="sender">Caller of the method</param> 
        /// <param name="e">Event Arguments</param> 
        public override void OnAttack(object sender, ExecuteCommandArgs e)
        {
            if (elaspedAttackTime > fireRate)
            {
                if (ammo > 0)
                {
                    ammo--;
                    ammunitionBar.Update(ammo, maxAmmo);
                    base.OnAttack(sender, e);
                }
            }
        }

        /// <summary>
        /// Sets the Player on a Jetpack to enable flying over obstacles
        /// </summary>
        /// <param name="sender">Caller of the method</param> 
        /// <param name="e">Event Arguments</param> 
        public void OnJetpack(object sender, ExecuteCommandArgs e)
        {
            TileMapSystem.Tile tile;
            if (GlobalReferences.MainGame.CurrentScene.MapManager.CurrentLevel.GetTileValue(TileRow, TileColumn, out tile))
            {
                if (tile.Flags == 2 || tile.Flags == 0)
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

        public void Initialize()
        {
            elapsedTime = 0;
            Visable = false;
        }

        /// <summary>
        /// Respawns the Player after death
        /// </summary>
        private void Respawn()
        {
            ChangeHealth(MaxHealth, this);
            Visable = true;
        }
    }
}
