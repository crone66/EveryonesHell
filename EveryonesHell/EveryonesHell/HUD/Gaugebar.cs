/* 
 * Purpose: Provides a Gaugebar
 * Author: Fabian Subat
 * Date: 10.12.2016
 */
using SFML.Graphics;
using SFML.System;

namespace EveryonesHell.HUD
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

        /// <summary>
        /// Can be called to create a gaugebar with the basic parameters
        /// </summary>
        /// <param name="currentvalue"></param> Sets the startvalue of the gaugebar. Must be lower or equal maxvalue
        /// <param name="maxvalue"></param> Sets the maximum value for the bar
        /// <param name="gaugebarPosition"></param>  Defines the position of bar and border
        /// <param name="gaugeBar"></param> Passes the sprite for the bar itself
        /// <param name="gaugeBarBorder"></param> Passes the sprite for the border
        /// <param name="overallSize"></param> Defines the scaling for bar and border
        /// <param name="gaugeColor"></param> Controls the color
        /// <param name="isFixed"></param> Controls if the created bar is added to the Fixed HUD Elements
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
        }

        /// <summary>
        /// Initializes the Gaugebar with a new Position and Scale
        /// </summary>
        /// <param name="currentvalue"></param> Sets the startvalue of the gaugebar. Must be lower or equal maxvalue
        /// <param name="maxvalue"></param> Sets the maximum value for the bar
        /// <param name="newPosition"></param> Provides the repositioning of bar and border
        /// <param name="overallSize"></param> Defines the scaling for bar and border
        public void Init(int currentValue, int maxValue, Vector2f newPosition, Vector2f overallSize)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            position = newPosition;
            OverallGaugeBarSize = overallSize;
            scale = OverallGaugeBarSize;
        }

        /// <summary>
        /// Initializes the gaugebar
        /// </summary>
        /// <param name="currentvalue"></param> Sets the startvalue of the gaugebar. Must be lower or equal maxvalue
        /// <param name="maxvalue"></param> Sets the maximum value for the bar
        public void Init(int currentValue, int maxValue)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            scale = OverallGaugeBarSize;
        }

        /// <summary>
        /// Calculates the percentage Value for the gaugebar
        /// </summary>
        /// <param name="currentvalue"></param> Sets the startvalue of the gaugebar. Must be lower or equal maxvalue
        /// <param name="maxvalue"></param> Sets the maximum value for the bar
        /// <returns></returns>
        private float calculateHealthPercent(float currentValue, float maxValue)
        {
            gaugePercent = currentValue / maxValue;
            return gaugePercent;
        }

        /// <summary>
        /// Updating the gaugebar
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public override void Update(float elapsedSeconds)
        {
            
        }

        /// <summary>
        /// Updates the gaugebar including the position
        /// </summary>
        /// <param name="position"></param> Updates the position of bar and border
        /// <param name="currentvalue"></param> Sets the startvalue of the gaugebar. Must be lower or equal maxvalue
        /// <param name="maxvalue"></param> Sets the maximum value for the bar
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
        /// Updates the gaugebars Values
        /// </summary>
        /// <param name="currentvalue"></param> Sets the startvalue of the gaugebar. Must be lower or equal maxvalue
        /// <param name="maxvalue"></param> Sets the maximum value for the bar
        public void Update(int currentValue, int maxValue)
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
        /// Draws the Healthbar and the border to the window
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            gaugebar.Position = position;
            gaugebarBorder.Position = position;
            gaugebarBorder.Scale = OverallGaugeBarSize;
            gaugebar.Scale = scale;
            gaugebar.Color = color;

            window.Draw(gaugebar);
            window.Draw(gaugebarBorder);
        }

        /// <summary>
        /// Clones the gaugebar to the fixed HUD Elements
        /// </summary>
        /// <param name="isFixed"></param> Add to fixed Elements or not
        /// <returns>Returns current gaugebar</returns>
        public Gaugebar Clone(bool isFixed)
        {
            return new Gaugebar(currentValue, maxValue, position, gaugebar, gaugebarBorder, OverallGaugeBarSize, color, isFixed);
        }
    }
}
