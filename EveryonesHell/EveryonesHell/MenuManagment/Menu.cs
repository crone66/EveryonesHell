using SFML.Graphics;
using SFML.System;

namespace EveryonesHell.MenuManagment
{
    public abstract class Menu
    {
        private string name;
        private Vector2f position;
        private Vector2f size;
        public bool IsVisable;

        /// <summary>
        /// Name of menu
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Initzializes a menu
        /// </summary>
        /// <param name="name">Menu name</param>
        /// <param name="position">Position of menu</param>
        /// <param name="size">Size of menu</param>
        /// <param name="isVisable">Indicates whenther the menu is visable or not</param>
        public Menu(string name, Vector2f position, Vector2f size, bool isVisable)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            IsVisable = isVisable;
        }

        /// <summary>
        /// Updates menu
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public abstract void Update(float elapsedSeconds);

        /// <summary>
        /// Draws menu
        /// </summary>
        /// <param name="window">Window to render</param>
        public abstract void Draw(RenderWindow window);

        /// <summary>
        /// Handles menu input
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last input</param>
        public abstract void Input(float elapsedSeconds);
    }
}
