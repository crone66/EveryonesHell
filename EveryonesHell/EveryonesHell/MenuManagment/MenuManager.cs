using SFML.Graphics;
using System.Collections.Generic;

namespace EveryonesHell.MenuManagment
{
    public class MenuManager
    {
        private List<Menu> menus;

        public MenuManager(List<Menu> menus)
        {
            this.menus = menus;
        }

        public void Update(float elapsedSeconds)
        {
            foreach (Menu item in menus)
            {
                if(item.IsVisable)
                    item.Update(elapsedSeconds);
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (Menu item in menus)
            {
                if(item.IsVisable)
                    item.Draw(window);
            }
        }

        public void Show(string menuName)
        {
            foreach (Menu item in menus)
            {
                if (item.Name == menuName)
                    item.IsVisable = true;
                else
                    item.IsVisable = false;

            }
        }

        public bool AddMenu(Menu menu)
        {
            if (!menus.Contains(menu))
            {
                menus.Add(menu);
                return true;
            }
            return false;
        }

        public bool RemoveMenu(Menu menu)
        {
            return menus.Remove(menu);
        }

        public bool RemoveMenu(string menuName)
        {
            return menus.RemoveAll(m => m.Name == menuName) > 0;
        }
    }
}
