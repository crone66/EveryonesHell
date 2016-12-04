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
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// Constructor of Game. Initzilizes RenderWindow and calls LoadContent
        /// </summary>
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

        /// <summary>
        /// Loads content after window initzialisation
        /// </summary>
        private void LoadContent()
        {
            Font font = new Font(@"C:\Windows\Fonts\arial.ttf");
            ConsoleManager = new DebugConsoleManager(this, font);
            window.TextEntered += ConsoleManager.TextEntered;
            
            currentScene = new Scene(zoomFactor);
            currentScene.LoadContent();
            GlobalReferences.State = GameState.Play;
        }

        /// <summary>
        /// Input Handling called before Update
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        private void Input(float elapsedSeconds)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.BackSlash))
                Game.ConsoleManager.DebugConsole.Open();

            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    currentScene.Input(elapsedSeconds);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Exit:
                    break;
            }
        }

        /// <summary>
        /// Update loop, update objects depending on current gameState
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        private void Update(float elapsedSeconds)
        {
            Game.ConsoleManager.Update(elapsedSeconds);
            //Update something
            switch(GlobalReferences.State)
            {
                
                case GameState.Play:
                    currentScene.Update(elapsedSeconds);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Exit:
                    break;
            }
        }

        /// <summary>
        /// Draw loop, draws content depending on current gameState
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        private void Draw(float elapsedSeconds)
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

        /// <summary>
        /// Used to dispose managed and unmanaged memory
        /// </summary>
        public void Dispose()
        {
        }
    }
}
