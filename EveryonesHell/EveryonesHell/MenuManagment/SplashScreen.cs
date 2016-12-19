using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EveryonesHell.MenuManagment
{
    public class SplashScreen : Menu
    {
        private float duration;
        private float elapsedDuration;

        private Text text;

        /// <summary>
        /// Initzializes a new splash screen
        /// </summary>
        /// <param name="size"></param>
        /// <param name="font"></param>
        /// <param name="duration"></param>
        public SplashScreen(Vector2f size, Font font, float duration)
            :base("SplashScreen", new Vector2f(0,0), size, false)
        {
            elapsedDuration = 0f;
            this.duration = duration;

            text = new Text("BLUB Productions", font, 50);
            text.Color = Color.White;
            text.Position = new Vector2f((size.X / 2) - (text.GetGlobalBounds().Width / 2), (size.Y / 2) - 25);  
        }

        /// <summary>
        /// Draws splash screen
        /// </summary>
        /// <param name="window">Window to render</param>
        public override void Draw(RenderWindow window)
        {
            window.Draw(text);
        }

        /// <summary>
        /// Handles splash screen input
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed second since last input</param>
        public override void Input(float elapsedSeconds)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                elapsedDuration = duration;
        }

        /// <summary>
        /// Updates splash screen
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public override void Update(float elapsedSeconds)
        {
            text.Color = new Color(255, 255, 255, (byte)Convert.ToInt32((1-(elapsedDuration / duration)) * 255f));
            
            elapsedDuration += elapsedSeconds;
            if (elapsedDuration >= duration)
            {
                text.Color = Color.White;
                elapsedDuration = 0f;
                GlobalReferences.MainGame.MenuManager.Show("MainMenu");
            }
        }
    }
}
