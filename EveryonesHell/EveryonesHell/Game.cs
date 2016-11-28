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

        private Scene currentScene;
        public Scene CurrentScene
        {
            get
            {
                return currentScene;
            }
        }
        public static DebugConsoleManager ConsoleManager;

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
                Input(time.AsSeconds());
                Update(time.AsSeconds());
                Draw(time.AsSeconds());
            }
        }

        private void LoadContent()
        {
            Font font = new Font(@"C:\Windows\Fonts\arial.ttf");
            ConsoleManager = new DebugConsoleManager(this, font);
            window.TextEntered += ConsoleManager.TextEntered;

            currentScene = new Scene(zoomFactor);
            currentScene.LoadContent();
            GlobalReferences.State = GameState.Play;
        }

       
        private void Input(float elapsedMilliseconds)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.BackSlash))
                Game.ConsoleManager.DebugConsole.Open();

            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    currentScene.Input(elapsedMilliseconds);
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
            Game.ConsoleManager.Update(elapsedMilliseconds);
            //Update something
            switch(GlobalReferences.State)
            {
                
                case GameState.Play:
                    currentScene.Update(elapsedMilliseconds);
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
            window.Clear(Color.Blue);
            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    {
                        currentScene.Draw(window);
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Exit:
                    break;
            }
            Game.ConsoleManager.Draw(window);
            window.Display();

        }

        public void Dispose()
        {
        }
    }
}
