using SFML.Graphics;
using System;

namespace EveryonesHell.HUD
{
    public abstract class HudElement
    {
        private bool isWindow;
        private bool isOpen;
        public event EventHandler<HudStateChangedArgs> OnClose;
        public event EventHandler<HudStateChangedArgs> OnOpen;

        public bool IsWindow
        {
            get
            {
                return isWindow;
            }
        }

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }

            set
            {
                bool prev = isOpen;
                isOpen = value;
                if (value)
                    OnOpen?.Invoke(this, new HudStateChangedArgs(prev));
                else
                    OnClose?.Invoke(this, new HudStateChangedArgs(prev));
            }
        }

        public HudElement(bool isWindow, bool isFixed, bool isOpen = false)
        {
            this.isOpen = isOpen;
            this.isWindow = isWindow;
            GlobalReferences.MainGame.CurrentScene.HudManager.RegistHud(this, isFixed);
        }

        public abstract void Update(float elapsedSeconds);

        public abstract void Draw(RenderWindow window);
    }
}
