/* 
 * Purpose: Manages game loop and window settings
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using TileMapSystem;
using System.Collections.Generic;

namespace EveryonesHell
{
    public class Game : IDisposable
    {
        private RenderWindow window;

        private uint windowWidth = 800;
        private uint windowHeight = 600;

        private ContentManager content;
        private StreamedTileMap tileMap;
        private Dictionary<int, Sprite> sprites;
        private int x = 10;
        private int y = 10;


        public Game()
        {
            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "test");

            LoadContent();
        }

        /// <summary>
        /// Starts the gameloop and measures the elapsed time between each cycle
        /// </summary>
        public void Run()
        {
            Clock clock = new Clock();
            while (window.IsOpen && !GlobalReferences.Exit)
            {
                Time time = clock.Restart();

                Input(time.AsSeconds());
                Update(time.AsSeconds());
                Draw(time.AsSeconds());
            }
        }

        private void LoadContent()
        {
            

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
            TileMapGenerator generator = new TileMapGenerator();
            tileMap = generator.GenerateMap(new GeneratorSettings(1, 50, 1.5f, 1000000, 10000000, true, 1000f, LayerDepth.One),
                new AreaSpread[2] { new AreaSpread(1, 0.30f, 0, 20, 250, true, SpreadOption.Circle), new AreaSpread(2, 0.125f, 0, 20, 200, true, SpreadOption.Circle) }, 1, 1);

        }

        private float elapsedTime;
        private void Input(float elapsedSeconds)
        {
            elapsedTime -= elapsedSeconds;
            if (elapsedTime <= 0f)
            {
                elapsedTime = 0.25f;
                //Handle input
                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                {
                    y -= 1 ;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    y += 1;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                {
                    x -= 1;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    x += 1;
                }
            }
        }

        private void Update(float elapsedSeconds)
        {
            //Update something
            tileMap.Update(y, x);
        }

        private void Draw(float elapsedSeconds)
        {
            window.Clear(Color.Blue);

            int[,] fieldInView = tileMap.GetTileMapInScreen(800, 600);
            int rowCount = fieldInView.GetUpperBound(0);
            int columnCount = fieldInView.GetUpperBound(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int id = fieldInView[row, column];
                    Sprite sprite = sprites[id];
                    sprite.Position = new Vector2f(column * 50, row * 50);
                    window.Draw(sprite);
                }
            }

            sprites[-1].Position = new Vector2f((columnCount / 2) * 50, (rowCount / 2) * 50);
            window.Draw(sprites[-1]);
            //Draw something

            window.Display();
        }

        public void Dispose()
        {
        }
    }
}
