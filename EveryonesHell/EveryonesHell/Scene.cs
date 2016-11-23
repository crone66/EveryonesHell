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
    public class Scene
    {
        private TileMapManager mapManager;
        private GeneratorSettings settings;
        private AreaSpread[] areaSpreads;

        private ContentManager content;
        private Dictionary<int, Sprite> sprites;

        private DebugConsoleManager consoleManager;

        private byte[] fieldsInView;
        private float elapsedTime;
        private int x = 10;
        private int y = -210; 
        private float zoomFactor;
        private uint windowWidth = 800;
        private uint windowHeight = 600;


        public Scene(float zoomFactor)
        {
            this.zoomFactor = zoomFactor;
            settings = new GeneratorSettings(1, 50, 1.5f, 1000000, 10000000, true, 1000f);
            areaSpreads = new AreaSpread[2]
            {
                new AreaSpread(1, 0.30f, 0, 20, 250, true, true, 5, SpreadOption.Circle, LayerType.Height),
                new AreaSpread(2, 0.125f, 0, 20, 200, true, true, 5, SpreadOption.Circle, LayerType.Height)
            };

            mapManager = new TileMapManager(settings, areaSpreads, x, y);
        }

        public void LoadContent(RenderWindow window)
        {
            Font font = new Font(@"C:\Windows\Fonts\arial.ttf");
            consoleManager = new DebugConsoleManager(this, font);
            window.TextEntered += consoleManager.TextEntered;

            //Load your content
            content = new ContentManager();
            content.Add("Content/testBlue.png", new Texture("Content/testBlue.png"));
            content.Add("Content/testRed.png", new Texture("Content/testRed.png"));
            content.Add("Content/testGreen.png", new Texture("Content/testGreen.png"));
            content.Add("Content/testGray.png", new Texture("Content/testGray.png"));
            Sprite blue = content.CreateSprite("Content/testBlue.png");
            Sprite red = content.CreateSprite("Content/testRed.png");
            Sprite green = content.CreateSprite("Content/testGreen.png");
            Sprite gray = content.CreateSprite("Content/testGray.png");

            sprites = new Dictionary<int, Sprite>();
            sprites.Add(0, green);
            sprites.Add(1, blue);
            sprites.Add(2, gray);
            sprites.Add(-1, red);
        }

        public void Update(float elapsedMilliseconds)
        {
            mapManager.Update(y, x);
            fieldsInView = mapManager.CurrentLevel.GetTileMapInScreen(Convert.ToInt32(windowWidth * zoomFactor), Convert.ToInt32(windowHeight * zoomFactor));
            consoleManager.Update(elapsedMilliseconds);
        }

        public void Input(float elapsedMilliseconds)
        {
            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    {
                        if (!consoleManager.DebugConsole.IsOpen)
                        {
                            elapsedTime -= elapsedMilliseconds;
                            if (elapsedTime <= 0f)
                            {
                                //Handle input
                                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                                {
                                    TryMove(0, -1);
                                    elapsedTime = 15f;
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                                {
                                    TryMove(0, 1);
                                    elapsedTime = 15f;
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                                {
                                    TryMove(-1, 0);
                                    elapsedTime = 15f;
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                                {
                                    TryMove(1, 0);
                                    elapsedTime = 15f;
                                }
                                else if (Keyboard.IsKeyPressed(Keyboard.Key.BackSlash))
                                    consoleManager.DebugConsole.Open();
                            }
                        }
                    }
                    break;
                case GameState.Pause:
                    break;
            }
        }

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

            consoleManager.Draw(window);
        }

        private void TryMove(int xTranslation, int yTranslation)
        {
            int tileId = mapManager.CurrentLevel.GetTile(y + yTranslation, x + xTranslation);
            if (tileId == 0)
            {
                x += xTranslation;
                y += yTranslation;
            }
        }
    }
}
