/* 
 * Purpose: Holds all user-defined gamesettings
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */
namespace EveryonesHell
{
    public struct GameSettings
    {
        public int WindowWidth;
        public int WindowHeight;

        public bool FullScreen;
        public bool ShowCursor;

        public GameSettings(int windowWidth, int windowHeight, bool fullScreen, bool showCursor)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            FullScreen = fullScreen;
            ShowCursor = showCursor;
        }
    }
}
