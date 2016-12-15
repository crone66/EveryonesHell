/* 
 * Purpose: Gives Attributes and Important Methods to Characters
 * Author: Fabian Subat
 * Date: 04.11.2016
 */

using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public class Character : InteractiveObject
    {
        public Character(Vector2f position, Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, Sprite gaugeBar, Sprite gaugeBarBorder)
            :base(position, size, inventory, animations, isMoveAble, viewDirection, speed, gaugeBar, gaugeBarBorder)
        {
        }

        public Character(int tileRow, int tileColumn, Vector2i size, InventorySystem.Inventory inventory, AnimationManager animations, bool isMoveAble, Vector2f viewDirection, float speed, Sprite gaugeBar, Sprite gaugeBarBorder)
            : base(tileRow, tileColumn, size, inventory, animations, isMoveAble, viewDirection, speed, gaugeBar, gaugeBarBorder)
        {
        }

        public override void Update(float elapsedMilliseconds)
        {
            base.Update(elapsedMilliseconds);
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }
    }
}
