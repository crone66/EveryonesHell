using SFML.Graphics;
using System.Collections.Generic;

namespace EveryonesHell.MenuManagment
{
    public class MenuManager
    {
        private List<Menu> menus;

        /// <summary>
        /// Initzializes a menu manager
        /// </summary>
        /// <param name="menus">Collection of menus</param>
        public MenuManager(List<Menu> menus)
        {
            this.menus = menus;
        }

        /// <summary>
        /// Updates all visable menus
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last update</param>
        public void Update(float elapsedSeconds)
        {
            foreach (Menu item in menus)
            {
                if(item.IsVisable)
                    item.Update(elapsedSeconds);
            }
        }

        /// <summary>
        /// Draws all visable menus
        /// </summary>
        /// <param name="window">Window to render</param>
        public void Draw(RenderWindow window)
        {
            foreach (Menu item in menus)
            {
                if(item.IsVisable)
                    item.Draw(window);
            }
        }

        /// <summary>
        /// Calls the input method of all visable menus
        /// </summary>
        /// <param name="elapsedSeconds">Elapsed seconds since last input</param>
        public void Input(float elapsedSeconds)
        {
            foreach (Menu item in menus)
            {
                if (item.IsVisable)
                    item.Input(elapsedSeconds);
            }
        }

        /// <summary>
        /// Shows a window by its name and hides all other windows
        /// </summary>
        /// <param name="menuName">Menu name</param>
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

        /// <summary>
        /// Adds a new menu to the menu manager
        /// </summary>
        /// <param name="menu">Menu to add</param>
        /// <returns>Returns true on success</returns>
        public bool AddMenu(Menu menu)
        {
            if (!menus.Contains(menu))
            {
                menus.Add(menu);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the given menu
        /// </summary>
        /// <param name="menu">Menu to remove</param>
        /// <returns>Returns true on success</returns>
        public bool RemoveMenu(Menu menu)
        {
            return menus.Remove(menu);
        }

        /// <summary>
        /// Remove menu by its name
        /// </summary>
        /// <param name="menuName">Menu name</param>
        /// <returns>Returns true on success</returns>
        public bool RemoveMenu(string menuName)
        {
            return menus.RemoveAll(m => m.Name == menuName) > 0;
        }
    }
}
