/* 
 * Purpose: Implements a Gaugebar
 * Author: Fabian Subat
 * Date: 10.12.2016
 */
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell
{
    class Gaugebar
    {
        Sprite gaugebarBorder;
        Sprite gaugebar;
        public Vector2f OverallGaugeBarSize;
        public int maxValue;
        public int currentValue;
        public float gaugePercent;
        private float elapsedTime;

        public Gaugebar(int currentvalue, int maxvalue, Vector2f gaugebarposition, Sprite gaugeBar, Sprite gaugeBarBorder, Vector2f overallsize, Color gaugecolor)
        {
            currentValue = currentvalue;
            maxValue = maxvalue;
            gaugebar = gaugeBar;
            gaugebarBorder = gaugeBarBorder;
            gaugebar.Position = gaugebarposition;
            gaugeBarBorder.Position = gaugebarposition;
            OverallGaugeBarSize = overallsize;
            gaugebar.Scale = overallsize;
            gaugeBarBorder.Scale = overallsize;
            gaugebar.Color = gaugecolor;
        }

        /// <summary>
        /// Calculates the percentage Value for the gaugebar
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public float calculateHealthPercent(float currentValue, float maxValue)
        {
            gaugePercent = currentValue / maxValue;
            return gaugePercent;
        }


        //public void Input(float elapsedSeconds)
        //{
        //    elapsedTime -= elapsedSeconds;
        //    if (elapsedTime <= 0f)
        //    {
        //        elapsedTime = 0.05f;
        //        if (Keyboard.IsKeyPressed(Keyboard.Key.Add) && currentValue < maxValue)
        //        {
        //            currentValue++;
        //            Console.WriteLine("currentHealth: {0}", currentValue);
        //        }

        //        if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract) && currentValue > 0)
        //        {
        //            currentValue--;
        //            Console.WriteLine("currentHealth: {0}", currentValue);
        //        }
        //    }
        //}

        /// <summary>
        /// Updates the healthbar
        /// </summary>
        public void Update(Vector2f position)
        {
            gaugebar.Position = position;
            gaugebarBorder.Position = position;
            calculateHealthPercent(currentValue, maxValue);
            if (OverallGaugeBarSize != null)
            {
                gaugebar.Scale = new Vector2f(OverallGaugeBarSize.X * gaugePercent, OverallGaugeBarSize.Y);
            } 
            else
            {
                gaugebar.Scale = new Vector2f(gaugePercent, 1.0f);
            }
            

        }

        /// <summary>
        /// Draws the Healthbar and the border
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            window.Draw(gaugebar);
            window.Draw(gaugebarBorder);
        }
    }
}
