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

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Menu(string name, Vector2f position, Vector2f size, bool isVisable)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            IsVisable = isVisable;
        }

        public abstract void Update(float elapsedSeconds);

        public abstract void Draw(RenderWindow window);

        public abstract void Input(float elapsedSeconds);
    }
}
