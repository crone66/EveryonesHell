using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.HUD
{
    public class HudManager
    {
        private List<HudElement> hudElements;
        private List<HudElement> fixedHudElements;

        public List<HudElement> HudElements
        {
            get
            {
                return hudElements;
            }
        }

        public List<HudElement> FixedHudElements
        {
            get
            {
                return fixedHudElements;
            }
        }

        public HudManager()
        {
            fixedHudElements = new List<HudElement>();
            hudElements = new List<HudElement>();
        }

        public void RegistHud(HudElement hud, bool fixedHud)
        {
            if (fixedHud)
                fixedHudElements.Add(hud);
            else
                hudElements.Add(hud);
        }

        public void Update(float elapsedSeconds)
        {
            foreach (HudElement item in HudElements)
            {
                item.Update(elapsedSeconds);
            }

            foreach (HudElement item in FixedHudElements)
            {
                item.Update(elapsedSeconds);
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (HudElement item in HudElements)
            {
                item.Draw(window);
            }
        }

        public void DrawFixed(RenderWindow window, float zoom)
        {
            View view = window.GetView();

            View newView = new View(new Vector2f(view.Size.X / 2f, view.Size.Y / 2f), view.Size);
            newView.Zoom(zoom);
            window.SetView(newView);

            foreach (HudElement item in FixedHudElements)
            {
                item.Draw(window);
            }
        }
    }
}
