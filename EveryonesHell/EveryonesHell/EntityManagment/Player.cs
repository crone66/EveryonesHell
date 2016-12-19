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
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="viewDirection"></param>
        /// <param name="animation"></param>
        /// <param name="dialog"></param>
        /// <param name="healthBar"></param>
        /// <param name="ammunition"></param>
        /// <param name="speed"></param>
        /// <param name="maxHealth"></param>
        /// <param name="fireRate"></param>
        /// <param name="groupID"></param>
        /// <param name="questTracker"></param>
        /// <param name="factionId"></param>
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
        /// <param name="tileRow"></param>
        /// <param name="tileColumn"></param>
        /// <param name="size"></param>
        /// <param name="viewDirection"></param>
        /// <param name="animation"></param>
        /// <param name="dialog"></param>
        /// <param name="healthBar"></param>
        /// <param name="ammunition"></param>
        /// <param name="speed"></param>
        /// <param name="maxHealth"></param>
        /// <param name="fireRate"></param>
        /// <param name="groupID"></param>
        /// <param name="questTracker"></param>
        /// <param name="factionId"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestTracker_OnQuestFinished(object sender, EventArgs e)
        {
            ammo += 100;
            maxAmmo += 100;
            if (maxAmmo > ammo)
                ammo = maxAmmo;

            ammunitionBar.Update(ammo, maxAmmo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (otherObject != null && lastCollisionObject == null && otherObject is InteractiveObject)
            {
                lastCollisionObject = otherObject as InteractiveObject;
            }
        }

        /// <summary>
        /// Updates the player
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
