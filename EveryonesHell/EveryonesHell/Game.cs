/* 
 * Purpose: Manages game loop and window settings
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace EveryonesHell
{
    public class Game : IDisposable
    {
        private RenderWindow window;

        private uint windowWidth = 800;
        private uint windowHeight = 600;

        private ContentManager content;

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
            while (window.IsOpen)
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
            content.Add("ball.png", new Texture("ball.png"));
            Sprite s = content.CreateSprite("ball.png");
        }

        private void Input(float elapsedSeconds)
        {
            //Handle input
        }

        private void Update(float elapsedSeconds)
        {
            //Update something
        }

        private void Draw(float elapsedSeconds)
        {
            window.Clear(Color.Blue);

            //Draw something

            window.Display();
        }

        public void Dispose()
        {
        }
    }
}
