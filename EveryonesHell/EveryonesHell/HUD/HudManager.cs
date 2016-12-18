using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace EveryonesHell.HUD
{
    public class HudManager
    {
        private List<HudElement> hudElements;
        private List<HudElement> fixedHudElements;
        private bool hasOpenWindow;
        
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

        public bool HasOpenWindow
        {
            get
            {
                return hasOpenWindow;
            }
        }

        public HudManager()
        {
            fixedHudElements = new List<HudElement>();
            hudElements = new List<HudElement>();
        }

        public void RegistHud(HudElement hud, bool fixedHud)
        {
            hud.OnClose += Hud_StateChanged;
            hud.OnOpen += Hud_StateChanged;
            if (fixedHud)
                fixedHudElements.Add(hud);
            else
                hudElements.Add(hud);
        }

        private void Hud_StateChanged(object sender, HudStateChangedArgs e)
        {
            HudElement hud = sender as HudElement;
            if (hud.IsWindow && hud.IsOpen != e.PreviouseState)
            {
                hasOpenWindow = hud.IsOpen;
            }
        }

        public void Update(float elapsedSeconds)
        {
            foreach (HudElement item in HudElements)
            {
                if(item.IsOpen)
                    item.Update(elapsedSeconds);
            }

            foreach (HudElement item in FixedHudElements)
            {
                if(item.IsOpen)
                    item.Update(elapsedSeconds);
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (HudElement item in HudElements)
            {
                if(item.IsOpen)
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
                if(item.IsOpen)
                    item.Draw(window);
            }
        }
    }
}
