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

        private View view;
        private float zoomFactor = 1f;

        private Scene scene;

        public Game()
        {
            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "test");
            view = window.GetView();
            view.Zoom(zoomFactor);
            window.SetView(view);
            LoadContent();
        }

        /// <summary>
        /// Starts the gameloop and measures the elapsed time between each cycle
        /// </summary>
        public void Run()
        {
            Clock clock = new Clock();
            while (window.IsOpen && GlobalReferences.State != GameState.Exit)
            {
                window.DispatchEvents();
                Time time = clock.Restart();

                Input(time.AsMilliseconds());
                Update(time.AsMilliseconds());
                Draw(time.AsMilliseconds());
            }
        }

        private void LoadContent()
        {
            scene = new Scene(zoomFactor);
            scene.LoadContent(window);
            GlobalReferences.State = GameState.Play;
        }

       
        private void Input(float elapsedMilliseconds)
        {
            switch(GlobalReferences.State)
            {
                case GameState.Play:
                    scene.Input(elapsedMilliseconds);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Exit:
                    break;
            }
        }

        private void Update(float elapsedMilliseconds)
        {
            //Update something
            switch(GlobalReferences.State)
            {
                case GameState.Play:
                    scene.Update(elapsedMilliseconds);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Exit:
                    break;
            }
        }

        private void Draw(float elapsedMilliseconds)
        {

            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    {
                        window.Clear(Color.Blue);
                        scene.Draw(window);
                        window.Display();
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Exit:
                    break;
            }
            
        }

        public void Dispose()
        {
        }
    }
}
