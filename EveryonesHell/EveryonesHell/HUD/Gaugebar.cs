/* 
 * Purpose: Implements a Gaugebar for flexible use
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
        Sprite gaugeBarBorder = new Sprite(new Texture(@"../../Resources/healthbarborder.png"));
        Sprite healthbarimage = new Sprite(new Texture(@"../../Resources/healthbar.png"));
        Sprite gaugebar = new Sprite(new Texture(@"../../Resources/gaugebar.png"));
        public Vector2f OverallGaugeBarSize;
        public int maxValue;
        public int currentValue;
        public float gaugePercent;
        private float elapsedTime;

        public Gaugebar(int currentvalue, int maxvalue, Vector2f gaugebarposition, Vector2f overallsize, Color gaugecolor)
        {
            currentValue = currentvalue;
            maxValue = maxvalue;
            gaugebar.Position = gaugebarposition;
            gaugeBarBorder.Position = gaugebarposition;
            OverallGaugeBarSize = overallsize;
            gaugebar.Scale = overallsize;
            gaugeBarBorder.Scale = overallsize;
            gaugebar.Color = gaugecolor;
        }

        /// <summary>
        ///Calculates the health% to scale the lifebar. Should be called in Dmg/Heal Func. 
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="maxHealth"></param>
        /// <returns></returns>
        public float calculateHealthPercent(float currentHealth, float maxHealth)
        {
            gaugePercent = currentHealth / maxHealth;
            return gaugePercent;
        }


        public void Input(float elapsedSeconds)
        {
            elapsedTime -= elapsedSeconds;
            if (elapsedTime <= 0f)
            {
                elapsedTime = 0.05f;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Add) && currentValue < maxValue)
                {
                    currentValue++;
                    Console.WriteLine("currentHealth: {0}", currentValue);
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract) && currentValue > 0)
                {
                    currentValue--;
                    Console.WriteLine("currentHealth: {0}", currentValue);
                }
            }
        }

        /// <summary>
        /// Updates the healthbar
        /// </summary>
        /// <param name="window"></param>
        public void Update(RenderWindow window)
        {
            calculateHealthPercent(currentValue, maxValue);
            if (OverallGaugeBarSize != null)
            {
                //healthbarimage.Scale = new Vector2f(OverallHealthBarSize.X * healthPercent, OverallHealthBarSize.Y);
                gaugebar.Scale = new Vector2f(OverallGaugeBarSize.X * gaugePercent, OverallGaugeBarSize.Y);
            } 
            else
            {
                //healthbarimage.Scale = new Vector2f(healthPercent, 1.0f);
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
            window.Draw(gaugeBarBorder);
        }
    }
}
