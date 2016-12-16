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
    public class Gaugebar : HUD.HudElement
    {
        private Sprite gaugebarBorder;
        private Sprite gaugebar;
        private Vector2f OverallGaugeBarSize;
        private Vector2f position;
        private Color color;
        private int maxValue;
        private int currentValue;
        private float gaugePercent;
        private Vector2f scale;
        private bool isFixed;

        public bool IsFixed
        {
            get
            {
                return isFixed;
            }
        }

        public Gaugebar(int currentvalue, int maxvalue, Vector2f gaugebarPosition, Sprite gaugeBar, Sprite gaugeBarBorder, Vector2f overallSize, Color gaugeColor, bool isFixed)
            :base(false, isFixed, true)
        {
            currentValue = currentvalue;
            maxValue = maxvalue;
            gaugebar = gaugeBar;
            gaugebarBorder = gaugeBarBorder;
            position = gaugebarPosition;
            color = gaugeColor;
            OverallGaugeBarSize = overallSize;
            scale = OverallGaugeBarSize;
            this.isFixed = isFixed;
            GlobalReferences.MainGame.CurrentScene.HudManager.RegistHud(this, isFixed);
        }

        public void Init(int currentValue, int maxValue, Vector2f newPosition, Vector2f overallSize)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            position = newPosition;
            OverallGaugeBarSize = overallSize;
            scale = OverallGaugeBarSize;
        }

        public void Init(int currentValue, int maxValue)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            scale = OverallGaugeBarSize;
        }

        /// <summary>
        /// Calculates the percentage Value for the gaugebar
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private float calculateHealthPercent(float currentValue, float maxValue)
        {
            gaugePercent = currentValue / maxValue;
            return gaugePercent;
        }

        public override void Update(float elapsedSeconds)
        {
            
        }
        /// <summary>
        /// Updates the healthbar
        /// </summary>
        public void Update(Vector2f position, int currentValue, int maxValue)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            calculateHealthPercent(currentValue, maxValue);
            if (OverallGaugeBarSize != null)
            {
                scale = new Vector2f(OverallGaugeBarSize.X * gaugePercent, OverallGaugeBarSize.Y);
            } 
            else
            {
                scale = new Vector2f(gaugePercent, 1.0f);
            }           
        }

        /// <summary>
        /// Draws the Healthbar and the border
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            gaugebar.Position = position;
            gaugebarBorder.Position = position;
            gaugebar.Scale = scale;
            gaugebar.Color = color;

            window.Draw(gaugebar);
            window.Draw(gaugebarBorder);

        }

        public Gaugebar Clone(bool isFixed)
        {
            return new Gaugebar(currentValue, maxValue, position, gaugebar, gaugebarBorder, OverallGaugeBarSize, color, isFixed);
        }
    }
}
