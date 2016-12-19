using SFML.Graphics;
using SFML.System;

namespace EveryonesHell.MenuManagment
{
    public class Button
    {
        private Text text;
        private RectangleShape shape;
        private Color textColor;

        /// <summary>
        /// Color of button text
        /// </summary>
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

        /// <summary>
        /// Initzializes a new button
        /// </summary>
        /// <param name="buttonText">Button text</param>
        /// <param name="font">Text font</param>
        /// <param name="position">Button position</param>
        /// <param name="size">Button size</param>
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

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="window">Window to render</param>
        public void Draw(RenderWindow window)
        {
            window.Draw(shape);
            window.Draw(text);
        }
    }
}
