﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EveryonesHell.MenuManagment
{
    public class CreditsScreen : Menu
    {
        private float duration;
        private float elapsedDuration;

        Text text;
        Text[] lines;

        public CreditsScreen(Vector2f size, Font font, float duration)
            : base("CreditsScreen", new Vector2f(0, 0), size, false)
        {
            elapsedDuration = 0f;
            this.duration = duration;

            Text text = new Text("Credits", font, 50);
            text.Color = Color.White;
            text.Position = new Vector2f((size.X / 2) - (text.GetGlobalBounds().Width / 2), (size.Y / 2) - 100);

            Text text1 = new Text("Marcel Croonenbroeck", font, 30);
            text1.Color = Color.White;
            text1.Position = new Vector2f((size.X / 2) - (text.GetGlobalBounds().Width / 2), (size.Y / 2) - 30);

            Text text2 = new Text("Lukas", font, 30);
            text2.Color = Color.White;
            text2.Position = new Vector2f((size.X / 2) - (text.GetGlobalBounds().Width / 2), (size.Y / 2) + 15);

            Text text3 = new Text("Fabian", font, 30);
            text3.Color = Color.White;
            text3.Position = new Vector2f((size.X / 2) - (text.GetGlobalBounds().Width / 2), (size.Y / 2) + 60);
            

            lines = new Text[4]
            {
                text,
                text1,
                text2,
                text3
            };
        }

        public override void Draw(RenderWindow window)
        {
            foreach (Text item in lines)
            {
                window.Draw(item);
            }
        }

        public override void Input(float elapsedSeconds)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                elapsedDuration = duration;
        }

        public override void Update(float elapsedSeconds)
        {
            elapsedDuration += elapsedSeconds;
            if (elapsedDuration >= duration)
            {
                elapsedDuration = 0f;
                GlobalReferences.MainGame.MenuManager.Show("MainMenu");
            }
        }
    }
}
