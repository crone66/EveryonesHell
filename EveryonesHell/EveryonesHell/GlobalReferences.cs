/* 
 * Purpose: To access important global variables (eg. windowWidth, windowWidth,...)   
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using System;

namespace EveryonesHell
{
    /// <summary>
    /// Important references for the game
    /// </summary>
    public static class GlobalReferences
    {
        public static Random Randomizer;
        public static GameSettings Settings;
        public static GameState State;
    }
}
