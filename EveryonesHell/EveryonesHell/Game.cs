﻿/* 
 * Purpose: Manages game loop and window settings
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using EveryonesHell.HUD;
using EveryonesHell.MenuManagment;
using System.Collections.Generic;
using SFML.Audio;

namespace EveryonesHell
{
    /// <summary>
    /// 
    /// </summary>
    public class Game : IDisposable
    {
        private RenderWindow window;
        private ContentManager content;

        private uint windowWidth = 1024;
        private uint windowHeight = 768;

        private View view;
        private float zoomFactor = 1.3f;

        private Scene currentScene;
        private DebugConsoleManager consoleManager;
        private GameState prevState;

        private MenuManager menuManager;

        public Scene CurrentScene
        {
            get
            {
                return currentScene;
            }
        }
        
        public DebugConsoleManager ConsoleManager
        {
            get
            {
                return consoleManager;
            }
        }

        public uint WindowWidth
        {
            get
            {
                return windowWidth;
            }

            set
            {
                windowWidth = value;
            }
        }

        public uint WindowHeight
        {
            get
            {
                return windowHeight;
            }

            set
            {
                windowHeight = value;
            }
        }

        public MenuManager MenuManager
        {
            get
            {
                return menuManager;
            }
           
        }

        /// <summary>
        /// Constructor of Game. Initzilizes RenderWindow and calls LoadContent
        /// </summary>
        public Game()
        {
            GlobalReferences.MainGame = this;
            window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Everyones Hell");
            window.Closed += Window_Closed;
            window.LostFocus += Window_LostFocus;
            window.GainedFocus += Window_GainedFocus;
            
            view = window.GetView();
            view.Zoom(zoomFactor);
            window.SetView(view);
            LoadContent();
        }

        private void Window_GainedFocus(object sender, EventArgs e)
        {
            GlobalReferences.State = prevState;
        }

        private void Window_LostFocus(object sender, EventArgs e)
        {
            prevState = GlobalReferences.State;
            GlobalReferences.State = GameState.Pause;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
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
            content = new ContentManager();
            Font font = content.Load<Font>(@"C:\Windows\Fonts\arial.ttf", "font");

            Sound sound = content.Load<Sound, SoundBuffer>("menu", "Content/Sounds/menu.wav");
            
            consoleManager = new DebugConsoleManager(this, font);
            window.TextEntered += consoleManager.TextEntered;
            
            
            currentScene = new Scene(content, zoomFactor);
            currentScene.LoadContent();
            List<Menu> menus = new List<Menu>();
            menus.Add(new MainMenu(new Vector2f(windowWidth * zoomFactor, windowHeight * zoomFactor), font, sound));
            menus.Add(new SplashScreen(new Vector2f(windowWidth * zoomFactor, windowHeight * zoomFactor), font, 5f));
            menus.Add(new CreditsScreen(new Vector2f(windowWidth * zoomFactor, windowHeight * zoomFactor), font, 5f));
            menuManager = new MenuManager(menus);
            menuManager.Show("SplashScreen");

            currentScene.Draw(window);
            GlobalReferences.State = GameState.Menu;
        }

        /// <summary>
        /// Input Handling called before Update
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        private void Input(float elapsedSeconds)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.BackSlash))
                consoleManager.DebugConsole.Open();

            switch (GlobalReferences.State)
            {
                case GameState.Play:
                    currentScene.Input(elapsedSeconds);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    menuManager.Input(elapsedSeconds);
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
            consoleManager.Update(elapsedSeconds);
            //Update something
            switch(GlobalReferences.State)
            {
                
                case GameState.Play:
                    currentScene.Update(elapsedSeconds);
                    break;
                case GameState.Pause:
                    break;
                case GameState.Menu:
                    menuManager.Update(elapsedSeconds);
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
            windowWidth = Convert.ToUInt32(window.DefaultView.Size.X);
            windowHeight = Convert.ToUInt32(window.DefaultView.Size.Y);

            window.Clear(Color.Black);
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
                    menuManager.Draw(window);
                    break;
                case GameState.Exit:
                    break;
            }
            consoleManager.Draw(window);
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
