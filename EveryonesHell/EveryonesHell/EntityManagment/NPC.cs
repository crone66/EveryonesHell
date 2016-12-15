using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public class NPC : Character
    {
        private HUD.DialogSystem dialog;

        public NPC(Vector2f position, Vector2i size, Sprite sprite, Sprite gaugeBar, Sprite gaugeBarBorder)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new SFML.System.Vector2f(1, 0), 1, gaugeBar, gaugeBarBorder)
        {

        }

        public NPC(int tileRow, int tileColumn, Vector2i size, Sprite sprite, HUD.DialogSystem dialog, Sprite gaugeBar, Sprite gaugeBarBorder)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new Vector2f(1, 0), 620, gaugeBar, gaugeBarBorder)
        {
            this.dialog = dialog;
        }

        /// <summary>
        /// Updates the NPC
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);
        }

        /// <summary>
        /// Draws the NPC
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);      
        }
    }
}
