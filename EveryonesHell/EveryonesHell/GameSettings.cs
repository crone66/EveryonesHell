/* 
 * Purpose: Holds all user-defined gamesettings
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */
namespace EveryonesHell
{
    /// <summary>
    /// Holds all current game settings
    /// </summary>
    public struct GameSettings
    {
        public int WindowWidth;
        public int WindowHeight;

        public bool FullScreen;
        public bool ShowCursor;

        /// <summary>
        /// Holds all current game related settings
        /// </summary>
        /// <param name="windowWidth">window width in pixel</param>
        /// <param name="windowHeight">window height in pixel</param>
        /// <param name="fullScreen">Value that indicates whether the window should be in full-screen mode.</param>
        /// <param name="showCursor">Value that indicates whether the mouse curor should be visable</param>
        public GameSettings(int windowWidth, int windowHeight, bool fullScreen, bool showCursor)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            FullScreen = fullScreen;
            ShowCursor = showCursor;
        }
    }
}
