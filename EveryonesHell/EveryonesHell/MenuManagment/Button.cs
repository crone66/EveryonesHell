using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.MenuManagment
{
    public class Button
    {
        private Text text;
        private RectangleShape shape;
        private Color textColor;

        public Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
                text.Color = value;
            }
        }

        public Button(string buttonText, Font font, Vector2f position, Vector2f size)
        {
            text = new Text(buttonText, font, 18);
            text.Position = position + (new Vector2f(size.X / 2, size.Y / 2) - new Vector2f(text.GetGlobalBounds().Width / 2, 9));
            shape = new RectangleShape(size);
            shape.Position = position;
            shape.FillColor = new Color(192, 192, 192, 255);
            shape.OutlineColor = new Color(0, 0, 0, 255);
            shape.OutlineThickness = 2;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(shape);
            window.Draw(text);
        }
    }
}
