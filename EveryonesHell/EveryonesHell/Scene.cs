using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileMapSystem;

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

        private byte[] fieldsInView;
        private float elapsedTime;
        private int x = -657;
        private int y = -340; 
        private float zoomFactor;
        private uint windowWidth = 800;
        private uint windowHeight = 600;

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        /// <summary>
        /// A Scene can be used as represantation of a level or menu
        /// </summary>
        /// <param name="zoomFactor">indicates the zoom factor to zoom in or out (default value 1)</param>
        public Scene(float zoomFactor)
        {
            this.zoomFactor = zoomFactor;
            settings = new GeneratorSettings(1, 50, 1.5f, 1000000, 10000000, true, 1000f);
            areaSpreads = new AreaSpread[2]
            {
                new AreaSpread(1, 0.30f, 20, 250, true, true, 5, SpreadOption.Circle, LayerType.Height),
                new AreaSpread(2, 0.125f, 20, 200, true, true, 5, SpreadOption.Circle, LayerType.Height)
            };

            mapManager = new TileMapManager(settings, areaSpreads);
            mapManager.GridChanged += MapManager_GridChanged;
            mapManager.GridChangeRequested += MapManager_GridChangeRequested;
            mapManager.GridGenerationIsSlow += MapManager_GridGenerationIsSlow;
            mapManager.Changelevel(settings, areaSpreads, x, y, false);
            Game.ConsoleManager.DebugConsole.WriteLine("X: " + X.ToString() + " Y:" + Y.ToString(), 255, 255, 255);
        }

        /// <summary>
        /// An eventhandler that is fired when a grid on screen isn't generated yet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridGenerationIsSlow(object sender, GridEventArgs e)
        {
            Game.ConsoleManager.DebugConsole.WriteLine(String.Format("Grid generation is slow From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 0, 0);
        }

        /// <summary>
        /// Eventhandler will be fired when a new grid generation starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridChangeRequested(object sender, GridEventArgs e)
        {
            Game.ConsoleManager.DebugConsole.WriteLine(String.Format("Grid change requested From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 255, 255);
        }

        /// <summary>
        /// Eventhandler will be fired when grid was changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapManager_GridChanged(object sender, GridEventArgs e)
        {
            Game.ConsoleManager.DebugConsole.WriteLine(String.Format("Grid changed From (Row: {0}, Column: {1}); To (Row: {2}, Column: {3}), Recycled: {4}", e.OldGridRow, e.OldGridColumn, e.NewGridRow, e.NewGridColumn, e.IsRecycledMap), 255, 255, 255);
        }

        /// <summary>
        /// Loads required content for the scene
        /// </summary>
        public void LoadContent()
        {
            //Load your content
            
            content = new ContentManager();
            Sprite blue = content.Load<Sprite, Texture>("1", "Content/testBlue.png");
            Sprite red = content.Load<Sprite, Texture>("-1", "Content/testRed.png");
            Sprite green = content.Load<Sprite, Texture>("0", "Content/testGreen.png");
            Sprite gray = content.Load<Sprite, Texture>("2", "Content/testGray.png");
            sprites = new Dictionary<int, Sprite>();
            sprites.Add(0, green);
            sprites.Add(1, blue);
            sprites.Add(2, gray);
            sprites.Add(-1, red);
        }

        /// <summary>
        /// Updates the current scene
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public void Update(float elapsedSeconds)
        {
            mapManager.Update(Y, X);
            fieldsInView = mapManager.CurrentLevel.GetTileMapInScreen(Convert.ToInt32(windowWidth * zoomFactor), Convert.ToInt32(windowHeight * zoomFactor));
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
                        if (!Game.ConsoleManager.DebugConsole.IsOpen)
                        {
                            elapsedTime -= elapsedSeconds;                    
                            if (elapsedTime <= 0f)
                            {
                                elapsedTime = 0.05f;
                                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                                {
                                    TryMove(0, -1);
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                                {
                                    TryMove(0, 1);
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                                {
                                    TryMove(-1, 0);
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                                {
                                    TryMove(1, 0);
                                }
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
            windowWidth = Convert.ToUInt32(window.DefaultView.Size.X);
            windowHeight = Convert.ToUInt32(window.DefaultView.Size.Y);

            float zoomWidth = ((windowWidth * zoomFactor) - (windowWidth)) / 2f;
            float zoomHeight = ((windowHeight * zoomFactor) - (windowHeight)) / 2f;

            int rowCount = Convert.ToInt32((windowHeight * zoomFactor) / settings.TileSize);
            int columnCount = Convert.ToInt32((windowWidth * zoomFactor) / settings.TileSize);
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int id = fieldsInView[(row * columnCount) + column];
                    Sprite sprite = sprites[id];
                    sprite.Position = new Vector2f((column * 50) - zoomWidth, (row * 50) - zoomHeight);
                    window.Draw(sprite);
                }
            }

            sprites[-1].Position = new Vector2f(((columnCount / 2) * 50) - zoomWidth, ((rowCount / 2) * 50) - zoomHeight);
            window.Draw(sprites[-1]);
        }

        /// <summary>
        /// (Testing) Collision detection and player movement
        /// </summary>
        /// <param name="xTranslation"></param>
        /// <param name="yTranslation"></param>
        private void TryMove(int xTranslation, int yTranslation)
        {
            int tileId = mapManager.CurrentLevel.GetTile(Y + yTranslation, X + xTranslation);
            if (tileId == 0)
            {
                X += xTranslation;
                Y += yTranslation;
            }
        }
    }
}
