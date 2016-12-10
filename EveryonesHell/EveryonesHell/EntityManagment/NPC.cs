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
        public NPC(Vector2f position, Vector2i size, Sprite sprite)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 1, 1, 1, 1, 0), true, new SFML.System.Vector2f(1, 0), 1)
        {

        }

        public override void Update(float elapsedMilliseconds)
        {
        }

        public override void Draw(RenderWindow window)
        {        
        }
    }
}
