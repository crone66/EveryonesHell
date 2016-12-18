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
        public static Random Randomizer = new Random();
        public static GameSettings Settings;
        public static GameState State;
        public static Game MainGame;

        public static byte FlagCollision = 0x01;
        public static byte FlagObjectInTile = 0x02;
        public static byte FlagObjectCollisionCheck = 0x04;
    }
}
