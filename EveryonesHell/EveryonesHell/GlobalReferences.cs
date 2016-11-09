﻿/* 
 * Purpose: To access important global variables (eg. windowWidth, windowWidth,...)   
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using System;

namespace EveryonesHell
{
    public static class GlobalReferences
    {
        public static Random Randomizer;
        public static GameSettings Settings;
        public static bool Exit = false;
    }
}
