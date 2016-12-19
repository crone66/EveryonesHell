
using SFML.Graphics;
using SFML.System;
using EveryonesHell.EntityManagment;

namespace EveryonesHell.HUD
{
    public class GameOver : HudElement
    {
        private RectangleShape background;
        private Text text;
        private float duration;
        private float elapsedDuration;
        private Scene scene;

        public GameOver(int width, int height, Font font, float duration, Scene scene)
            :base(true, true, false)
        {
            this.scene = scene;
            this.duration = duration;
            background = new RectangleShape(new Vector2f(width - 20, height - 20));
            background.Position = new Vector2f(10, 10);

            background.FillColor = new Color(0, 0, 0, 128);
            background.OutlineColor = new Color(0, 0, 0, 200);
            background.OutlineThickness = 2;

            text = new Text("Game Over", font, 50);
            text.Color = Color.Red;
            text.Position = new Vector2f(((width / 2) + 10) - (text.GetGlobalBounds().Width / 2) , ((height / 2) + 10) - 25);
        }

        public override void Draw(RenderWindow window)
        {
            window.Draw(background);
            window.Draw(text);
        }

        public override void Update(float elapsedSeconds)
        {
            elapsedDuration += elapsedSeconds;
            if(elapsedDuration > duration)
            {
                elapsedDuration = 0;
                IsOpen = false;
                scene.Respawn();
            }
        }
    }
}
