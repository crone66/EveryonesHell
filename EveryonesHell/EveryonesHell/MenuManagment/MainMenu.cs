using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EveryonesHell.MenuManagment
{
    public class MainMenu : Menu
    {
        private Button[] lines;
        private int selectedIndex = 0;
        private bool prevUp = false;
        private bool prevDown = false;

        public MainMenu(Vector2f size, Font font)
            :base("MainMenu", new Vector2f(0,0), size, false)
        {
            lines = new Button[3]
            {
                new Button("Start",font, new Vector2f((size.X / 2) - 100, (size.Y / 2) - 60), new Vector2f(200, 30)),
                new Button("Credits",font, new Vector2f((size.X / 2) - 100, (size.Y / 2)), new Vector2f(200, 30)),
                new Button("Exit", font, new Vector2f((size.X / 2) - 100, (size.Y / 2) + 60), new Vector2f(200, 30))
            };

            lines[0].TextColor = Color.Green;
        }

        public override void Draw(RenderWindow window)
        {
            foreach (Button item in lines)
            {
                item.Draw(window);
            }
        }

        public override void Update(float elapsedSeconds)
        {
            
        }

        public override void Input(float elapsedSeconds)
        {
            int prevIndex = selectedIndex;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                if (!prevUp)
                {
                    selectedIndex--;
                    prevUp = true;
                }
            }
            else
            {
                prevUp = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                if (!prevDown)
                {
                    selectedIndex++;
                    prevDown = true;
                }
            }
            else
            {
                prevDown = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !prevUp && !prevDown)
            {
                if (selectedIndex == 0)
                    GlobalReferences.State = GameState.Play;
                else if (selectedIndex == 1)
                    GlobalReferences.MainGame.MenuManager.Show("CreditsScreen");
                else if (selectedIndex == 2)
                    GlobalReferences.State = GameState.Exit;
            }

            if (selectedIndex < 0)
                selectedIndex = lines.Length - 1;
            else if (selectedIndex >= lines.Length)
                selectedIndex = 0;

            if (prevIndex != selectedIndex)
            {
                lines[prevIndex].TextColor = Color.White;
                lines[selectedIndex].TextColor = Color.Green;
            }
        }
    }
}
