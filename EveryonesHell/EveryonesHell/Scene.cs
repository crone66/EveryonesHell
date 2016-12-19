using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using TileMapSystem;
using FileDescriptions;
using EveryonesHell.EntityManagment;
using EveryonesHell.HUD;
using EveryonesHell.EntityManagment.Items;

namespace EveryonesHell
{
    /// <summary>
    /// 
    /// </summary>
    public class Scene
    {        
        private TileMapManager mapManager;
        private GeneratorSettings settings;
        private AreaSpread[] areaSpreads;

        private ContentManager content;
        private HUD.HudManager hudManager;
        private Dictionary<int, Sprite> sprites;

        private EntityManager entities;
        private DialogCollection dialogs;
        //private ItemCollection items;
        private QuestCollection quests;

        private IntRect viewBox;

        private Player player;

        private QuestManagment.QuestTracker questTracker;
        private HUD.QuestTrackerWindow questTrackerWindow;

        private int x = 837;//-657;
        private int y = 779;//-340;
        private float zoomFactor;

        public TileMapManager MapManager
        {
            get
            {
                return mapManager;
            }
        }

        public DialogCollection Dialogs
        {
            get
            {
                return dialogs;
            }
        }

        /*public ItemCollection Items
        {
            get
            {
                return items;
            }
        }*/

        public QuestCollection Quests
        {
            get
            {
                return quests;
            }
        }

        public EntityManager Entities
        {
            get
            {
                return entities;
            }
        }

        public GeneratorSettings Settings
        {
            get
            {
                return settings;
            }
        }

        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        public HudManager HudManager
        {
            get
            {
                return hudManager;
            }

            set
            {
                hudManager = value;
            }
        }

        /// <summary>
        /// A Scene can be used as represantation of a level or menu
        /// </summary>
        /// <param name="zoomFactor">indicates the zoom factor to zoom in or out (default value 1)</param>
        public Scene(ContentManager content, float zoomFactor)
        {
            this.zoomFactor = zoomFactor;
            this.content = content;
            settings = new GeneratorSettings(1, 50, 1.5f, 1000000, 10000000, true, 1000f);
            areaSpreads = new AreaSpread[7]
            {
                new AreaSpread(1, GlobalReferences.FlagCollision, 0.30f, 20, 250, true, true, 5, SpreadOption.Circle, LayerType.Height),
                new AreaSpread(2, GlobalReferences.FlagCollision, 0.125f, 20, 200, true, true, 5, SpreadOption.Circle, LayerType.Height),
                new AreaSpread(2, 0, 0.0005f, 1, 2, false, false, 0, SpreadOption.None, LayerType.PointsOfInterest),
                new AreaSpread(3, 0, 0.001f, 1, 2, false, false, 0, SpreadOption.None, LayerType.PointsOfInterest),
                new AreaSpread(4, 0, 0.0005f, 1, 2, false, false, 0, SpreadOption.None, LayerType.PointsOfInterest),
                new AreaSpread(5, 0, 0.001f, 1, 2, false, false, 0, SpreadOption.None, LayerType.PointsOfInterest),
                new AreaSpread(6, 0, 0.0005f, 1, 2, false, false, 0, SpreadOption.None, LayerType.PointsOfInterest),
            };

            mapManager = new TileMapManager(Settings, areaSpreads);
            mapManager.GridChanged += MapManager_GridChanged;
            mapManager.GridChangeRequested += MapManager_GridChangeRequested;
            mapManager.GridGenerationIsSlow += MapManager_GridGenerationIsSlow;           

            HudManager = new HudManager();
        }

        /// <summary>
        /// Loads required content for the scene
        /// </summary>
        public void LoadContent()
        {
            //Sprites for the Map
            content.Load<Sprite, Texture>("error", "Content/error.png");
            Sprite green = content.Load<Sprite, Texture>("0", "Content/Enviroment/grass.png");
            Sprite gray = content.Load<Sprite, Texture>("2", "Content/Enviroment/hill.png");
            Sprite blue = content.Load<Sprite, Texture>("1", "Content/Enviroment/water.png");
            //Sprites for the Characters's
            Sprite testNPC = content.Load<Sprite, Texture>("3", "Content/Npc/NPCAnimation.png");
            Sprite playerSpriteSheet = content.Load<Sprite, Texture>("-2", "Content/PlayerSpriteSheet.png");
            Sprite red = content.Load<Sprite, Texture>("-1", "Content/Npc/testRed.png");


            //Sprites for the Gaugebar
            Sprite gaugebar = content.Load<Sprite, Texture>("5", "Content/Hud/gaugebar.png");
            Sprite gaugebarborder = content.Load<Sprite, Texture>("6", "Content/Hud/gaugebarborder.png");
            Sprite fireBall = content.Load<Sprite, Texture>("7", "Content/fireBall.png");


            Sprite flower = content.Load<Sprite, Texture>("8", "Content/Items/healthflower.png");
            Sprite qFlower = content.Load<Sprite, Texture>("9", "Content/Items/questflower.png");

            Font font = content.GetValue<Font>("font");
            dialogs = content.Load<DialogCollection>("Content/Descriptions/DialogCollection.xml");

            quests = content.Load<QuestCollection>("Content/Descriptions/QuestCollection.xml");

            DialogSystem dialog = new DialogSystem(Dialogs, new Vector2f(0, GlobalReferences.MainGame.WindowHeight - 200), new Vector2f(GlobalReferences.MainGame.WindowWidth, 200), font, Color.White, Color.Yellow);

            sprites = new Dictionary<int, Sprite>();
            sprites.Add(0, green);
            sprites.Add(1, blue);
            sprites.Add(2, gray);
            sprites.Add(-1, red);
            sprites.Add(3, testNPC);
            sprites.Add(5, gaugebar);
            sprites.Add(6, gaugebarborder);
            
            questTracker = new QuestManagment.QuestTracker(null, quests);
            questTrackerWindow = new QuestTrackerWindow(new Vector2f(70, 150), font, questTracker);

            Gaugebar healthBar = new Gaugebar(100, 100, new Vector2f(0, GlobalReferences.MainGame.WindowHeight - gaugebarborder.Texture.Size.Y), gaugebar, gaugebarborder, new Vector2f(0.6f, 1), Color.Red, true);
            Gaugebar ammunition = new Gaugebar(30, 30, new Vector2f(GlobalReferences.MainGame.WindowWidth - (gaugebarborder.Texture.Size.X * 0.6f), GlobalReferences.MainGame.WindowHeight - (gaugebarborder.Texture.Size.Y * gaugebar.Scale.Y)), gaugebar, gaugebarborder, new Vector2f(0.6f, 1), Color.Yellow, true);

            /*NPC TheMightyTester = new NPC(NPCy, NPCx, new Vector2i(50, 50), new Vector2f(1, 0), new AnimationManager(testNPC, 3, 4, 50, 50, 0.16f), healthBar.Clone(false), 220, 100, 0.5f, 2, 1, new int[] { 4 });
            NPC TheEvilTester = new NPC(NPCy - 10, NPCx - 10, new Vector2i(50, 50), new Vector2f(1, 0), new AnimationManager(red, 3, 4, 50, 50, 0.16f), healthBar.Clone(false), 220, 50, 0.5f, 3, 2 , null);
            HealFlower healFlower = new HealFlower(y + 5, x - 5, new Vector2i(50, 50), new AnimationManager(flower, 1, 1, 50, 50, 0.16f), 1);
            QuestFlower questFlower = new QuestFlower(y - 10, x + 10, new Vector2i(50, 50), new AnimationManager(qFlower, 1, 1, 50, 50, 0.16f), 1);
            */

            /*entities.AddEntity(TheMightyTester);
            entities.AddEntity(TheEvilTester);
            entities.AddEntity(healFlower);
            entities.AddEntity(questFlower);*/

            entities = new EntityManager();
            EntityFactory.EntityCreated += EntityFactory_EntityCreated;
            EntityFactory.AddPrototype("Bullet", new Projectile(new Vector2i((int)fireBall.Texture.Size.X, (int)fireBall.Texture.Size.Y), new AnimationManager(fireBall, 0, 0, 0, 0, 0), true, 1000, 5000, 0, 10, true));
            EntityFactory.AddPrototype("2", new NPC(new Vector2i(50, 50), new Vector2f(1, 0), new AnimationManager(testNPC, 3, 4, 50, 50, 0.16f), healthBar.Clone(false), 220, 100, 0.5f, 2, 1, new int[] { 0 }, true));
            EntityFactory.AddPrototype("3", new NPC(new Vector2i(50, 50), new Vector2f(1, 0), new AnimationManager(red, 3, 4, 50, 50, 0.16f), healthBar.Clone(false), 220, 50, 0.5f, 3, 2, null, true));
            EntityFactory.AddPrototype("4", new NPC(new Vector2i(50, 50), new Vector2f(1, 0), new AnimationManager(testNPC, 3, 4, 50, 50, 0.16f), healthBar.Clone(false), 220, 100, 0.5f, 2, 1, new int[] { 4 }, true));
            EntityFactory.AddPrototype("5", new HealFlower(new Vector2i(50, 50), new AnimationManager(flower, 1, 1, 50, 50, 0.16f), 1, true));
            EntityFactory.AddPrototype("6", new QuestFlower(new Vector2i(50, 50), new AnimationManager(qFlower, 1, 1, 50, 50, 0.16f), 1, true));
            mapManager.Changelevel(Settings, areaSpreads, x, y, false);

            Player = new Player(y, x, new Vector2i(50, 50), new Vector2f(1, 0), new AnimationManager(playerSpriteSheet, 3, 4, 50, 50, 0.16f), dialog, healthBar, ammunition, 620, 100, 0.5f, 1, questTracker, 1);
            Player.OnDestroy += Player_OnDestroy;
            
            entities.AddEntity(Player);
        }

        private void Player_OnDestroy(object sender, EventArgs e)
        {
            player.Initialize();
            entities.AddEntity(player);
        }

        private void EntityFactory_EntityCreated(object sender, FactoryEventArgs e)
        {
            entities.AddEntity(e.CreatedEntity);
        }

        /// <summary>
        /// Updates the current scene
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public void Update(float elapsedSeconds)
        {
            entities.Update(elapsedSeconds, viewBox);
            mapManager.Update(Player.TileRow, Player.TileColumn);
            hudManager.Update(elapsedSeconds);
            
            //mapManager.Update(TheMightyTester.TileRow, TheMightyTester.TileColumn);
            //fieldsInView = MapManager.CurrentLevel.GetTileMapInScreen(Convert.ToInt32((GlobalReferences.MainGame.WindowWidth) * zoomFactor), Convert.ToInt32((GlobalReferences.MainGame.WindowHeight) * zoomFactor));       
        }

        /// <summary>
        /// Handles input for the current scene
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public void Input(float elapsedSeconds)
        {
            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    {
                        if (!GlobalReferences.MainGame.ConsoleManager.DebugConsole.IsOpen)
                        {
                            if (!hudManager.HasOpenWindow)
                            {
                                Vector2i direction = new Vector2i(0, 0);

                                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                                {
                                    direction.Y -= 1;
                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                                {
                                    direction.Y += 1;
                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                                {
                                    direction.X -= 1;
                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                                {
                                    direction.X += 1;
                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.F))
                                {
                                    Player.OnAction(this, null);
                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && Player.JetpackActive == false)
                                {
                                    Player.OnAttack(this, null);
                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.H))
                                {
                                    Player.OnJetpack(this, null);

                                }

                                if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                                {
                                    questTrackerWindow.IsOpen = true;
                                }

                                if (direction.X != 0 || direction.Y != 0)
                                    Player.OnMove(this, new DebugConsole.ExecuteCommandArgs(direction));
                            }
                        }
                    }
                    break;
                case GameState.Pause:
                    break;
            }
        }

        /// <summary>
        /// Draws the current scene
        /// </summary>
        /// <param name="window">Window to render</param>
        public void Draw(RenderWindow window)
        {
            Vector2f playerCenter = new Vector2f(player.Position.X + (player.Size.X / 2), player.Position.Y + (player.Size.Y / 2));
            Vector2f offset = new Vector2f(playerCenter.X - (GlobalReferences.MainGame.WindowWidth / 2), playerCenter.Y - (GlobalReferences.MainGame.WindowHeight / 2));
            View v = new View(playerCenter, new Vector2f(GlobalReferences.MainGame.WindowWidth, GlobalReferences.MainGame.WindowHeight));
            viewBox = new IntRect((int)playerCenter.X - (int)GlobalReferences.MainGame.WindowWidth, (int)playerCenter.Y - (int)GlobalReferences.MainGame.WindowHeight, (int)GlobalReferences.MainGame.WindowWidth * 2, (int)GlobalReferences.MainGame.WindowHeight * 2);
            v.Zoom(zoomFactor);
            window.SetView(v);

            float zoomWidth = ((GlobalReferences.MainGame.WindowWidth * zoomFactor) - (GlobalReferences.MainGame.WindowWidth)) / 2f;
            float zoomHeight = ((GlobalReferences.MainGame.WindowHeight * zoomFactor) - (GlobalReferences.MainGame.WindowHeight)) / 2f;

            int rowCount = Convert.ToInt32((GlobalReferences.MainGame.WindowHeight * zoomFactor) / Settings.TileSize);
            int columnCount = Convert.ToInt32((GlobalReferences.MainGame.WindowWidth * zoomFactor) / Settings.TileSize);

            int fromRow = (player.TileRow - (rowCount / 2)) - 2;
            int toRow = (player.TileRow + (rowCount / 2)) + 2;
            int fromCol = (player.TileColumn - (columnCount / 2)) - 2;
            int toCol = (player.TileColumn + (columnCount / 2)) +2;
            for (int r = fromRow; r < toRow; r++)
            {
                for (int c = fromCol; c < toCol; c++)
                {
                    Tile tile;
                    if (mapManager.CurrentLevel.GetTileValue(r, c, out tile))
                    {
                        Sprite sprite = sprites[tile.Id];
                        sprite.Position = new Vector2f(c * settings.TileSize, r * settings.TileSize);

                        window.Draw(sprite);
                    }
                }
            }

            entities.Draw(window, viewBox);
            HudManager.Draw(window);
            HudManager.DrawFixed(window, zoomFactor);
        }      

        /// <summary>
        /// An eventhandler that is fired when a grid on screen isn't generated yet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridGenerationIsSlow(object sender, GridEventArgs e)
        {
            GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(string.Format("Grid generation is slow From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 0, 0);
        }

        /// <summary>
        /// Eventhandler will be fired when a new grid generation starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridChangeRequested(object sender, GridEventArgs e)
        {
            GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(string.Format("Grid change requested From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 255, 255);
        }

        /// <summary>
        /// Eventhandler will be fired when grid was changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridChanged(object sender, GridEventArgs e)
        {
            GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(string.Format("Grid changed From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 255, 255);
            DeSpawn(e);
            Spawn(e);
        }

        private void Spawn(GridEventArgs args)
        {
            TileMapPart map = mapManager.CurrentLevel.Maps[mapManager.CurrentLevel.CurrentMapIndex];
            foreach (ObjectTile tile in map.ObjectPlacement)
            {
                Entity ent = EntityFactory.Clone(tile.ObjectId.ToString());
                int row, column;
                TileMathHelper.ToPosition(tile.TileIndex, mapManager.CurrentLevel.TileColumnCount, out row, out column);
                row = TileMathHelper.ToIndex(map.GridRow, row, mapManager.CurrentLevel.TileRowCount);
                column = TileMathHelper.ToIndex(map.GridColumn, column, mapManager.CurrentLevel.TileColumnCount);
                ent.Init(row, column);
            }
        }

        private void DeSpawn(GridEventArgs args)
        {
            Entity[] ents = entities.GetEntities();
            foreach (Entity entity in ents)
            {
                if (!entity.BoundingBox.Intersects(viewBox) && !(entity is Player))
                    entities.RemoveEntity(entity);
            }
        }
    }
}
