using SFML.Graphics;
using SFML.System;

namespace EveryonesHell.HUD
{
    public abstract class HudElement
    {
        public HudElement()
        {

        }

        public abstract void Update(float elapsedSeconds);

        public abstract void Draw(RenderWindow window);
    }
}
