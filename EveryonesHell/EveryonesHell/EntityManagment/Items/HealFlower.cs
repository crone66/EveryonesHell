using System;
using SFML.System;

namespace EveryonesHell.EntityManagment.Items
{
    public class HealFlower : Item
    {
        private const int healPower = 20;
        public HealFlower(int tileRow, int tileColumn, Vector2i size, AnimationManager animations, int value)
            : base(tileRow, tileColumn, size, animations, 5, value, false)
        {

        }

        public override void Use(InteractiveObject user)
        {
            user.ChangeHealth(20, this);
            CallOnDestroyEvent();
        }
    }
}
