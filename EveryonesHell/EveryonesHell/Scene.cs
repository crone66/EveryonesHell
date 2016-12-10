using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using TileMapSystem;
using FileDescriptions;
using EveryonesHell.EntityManagment;

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
        private Dictionary<int, Sprite> sprites;

        private EntityManager entities;
        private DialogCollection dialogs;
        private ItemCollection items;
        private QuestCollection quests;

        private Player player;

        //private Tile[] fieldsInView;
        private float elapsedTime;
        private int x = -657;
        private int y = -340; 
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

        public ItemCollection Items
        {
            get
            {
                return items;
            }
        }

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

        /// <summary>
        /// A Scene can be used as represantation of a level or menu
        /// </summary>
        /// <param name="zoomFactor">indicates the zoom factor to zoom in or out (default value 1)</param>
        public Scene(ContentManager content, float zoomFactor)
        {
            this.zoomFactor = zoomFactor;
            this.content = content;
            settings = new GeneratorSettings(1, 50, 1.5f, 1000000, 10000000, true, 1000f);
            areaSpreads = new AreaSpread[2]
            {
                new AreaSpread(1, GlobalReferences.FlagCollision, 0.30f, 20, 250, true, true, 5, SpreadOption.Circle, LayerType.Height),
                new AreaSpread(2, GlobalReferences.FlagCollision, 0.125f, 20, 200, true, true, 5, SpreadOption.Circle, LayerType.Height)
            };

            mapManager = new TileMapManager(Settings, areaSpreads);
            mapManager.GridChanged += MapManager_GridChanged;
            mapManager.GridChangeRequested += MapManager_GridChangeRequested;
            mapManager.GridGenerationIsSlow += MapManager_GridGenerationIsSlow;
            mapManager.Changelevel(Settings, areaSpreads, x, y, false);
        }

        /// <summary>
        /// Loads required content for the scene
        /// </summary>
        public void LoadContent()
        { 
            Sprite blue = content.Load<Sprite, Texture>("1", "Content/testBlue.png");
            Sprite red = content.Load<Sprite, Texture>("-1", "Content/testRed.png");
            Sprite green = content.Load<Sprite, Texture>("0", "Content/testGreen.png");
            Sprite gray = content.Load<Sprite, Texture>("2", "Content/testGray.png");
            Font font = content.GetValue<Font>("font");
            dialogs = content.Load<DialogCollection>("Content/testDialogs.xml");

            HUD.DialogSystem dialog = new HUD.DialogSystem(Dialogs, new Vector2f(0, GlobalReferences.MainGame.WindowHeight - 200), new Vector2f(GlobalReferences.MainGame.WindowWidth, 200), font, Color.White, Color.Yellow);

            sprites = new Dictionary<int, Sprite>();
            sprites.Add(0, green);
            sprites.Add(1, blue);
            sprites.Add(2, gray);
            sprites.Add(-1, red);

            Player = new Player(y, x, new Vector2i(50, 50), red, dialog);
            entities = new EntityManager();
            entities.Entities.Add(Player);
        }

        /// <summary>
        /// Updates the current scene
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public void Update(float elapsedSeconds)
        {
            entities.Update(elapsedSeconds);
            mapManager.Update(Player.TileRow, Player.TileColumn);
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
                            
                            if (direction.X != 0 || direction.Y != 0)
                                Player.OnMove(this, new DebugConsole.ExecuteCommandArgs(direction));
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
            
            v.Zoom(zoomFactor);
            window.SetView(v);

            float zoomWidth = ((GlobalReferences.MainGame.WindowWidth * zoomFactor) - (GlobalReferences.MainGame.WindowWidth)) / 2f;
            float zoomHeight = ((GlobalReferences.MainGame.WindowHeight * zoomFactor) - (GlobalReferences.MainGame.WindowHeight)) / 2f;

            int rowCount = Convert.ToInt32((GlobalReferences.MainGame.WindowHeight * zoomFactor) / Settings.TileSize);
            int columnCount = Convert.ToInt32((GlobalReferences.MainGame.WindowWidth * zoomFactor) / Settings.TileSize);
            /*for (int row = 0; row < rowCount; row++)
            {
                for (int column = -0; column < columnCount; column++)
                {
                    int id = fieldsInView[(row * columnCount) + column].Id;
                    Sprite sprite = sprites[id];
                    
                    sprite.Position = new Vector2f(((player.TileColumn - (columnCount / 2)) + column) * settings.TileSize,
                        ((player.TileRow - (rowCount / 2)) + row) * settings.TileSize);
                    //sprite.Position = offset + new Vector2f((column * Settings.TileSize) - zoomWidth, (row * Settings.TileSize) - zoomHeight);
                    window.Draw(sprite);
                }
            }*/

            int fromRow = (player.TileRow - (rowCount / 2)) - 2;
            int toRow = (player.TileRow + (rowCount / 2)) + 2;
            int fromCol = (player.TileColumn - (columnCount / 2)) - 2;
            int toCol = (player.TileColumn + (columnCount / 2)) +2;
            for (int r = fromRow; r < toRow; r++)
            {
                for (int c = fromCol; c < toCol; c++)
                {
                    Tile t = mapManager.CurrentLevel.GetTileValue(r, c);
                    Sprite sprite = sprites[t.Id];
                    sprite.Position = new Vector2f(c * settings.TileSize, r * settings.TileSize);

                    window.Draw(sprite);
                }
            }

            entities.Draw(window);
        }      

        /// <summary>
        /// An eventhandler that is fired when a grid on screen isn't generated yet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridGenerationIsSlow(object sender, GridEventArgs e)
        {
            GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(String.Format("Grid generation is slow From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 0, 0);
        }

        /// <summary>
        /// Eventhandler will be fired when a new grid generation starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridChangeRequested(object sender, GridEventArgs e)
        {
            GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(String.Format("Grid change requested From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 255, 255);
        }

        /// <summary>
        /// Eventhandler will be fired when grid was changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridChanged(object sender, GridEventArgs e)
        {
            GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(String.Format("Grid changed From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 255, 255);
        }
    }
}
