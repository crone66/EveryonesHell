using System;
using SFML.System;

namespace EveryonesHell.EntityManagment.Items
{
    public class HealFlower : Item
    {
        private const int healPower = 20;
        public HealFlower(int tileRow, int tileColumn, Vector2i size, AnimationManager animations, int value, bool isPrototyp)
            : base(tileRow, tileColumn, size, animations, 5, value, false, isPrototyp)
        {

        }

        public HealFlower(Vector2i size, AnimationManager animations, int value, bool isPrototyp)
            : base(size, animations, 5, value, false, isPrototyp)
        {

        }

        public override void Use(InteractiveObject user)
        {
            user.ChangeHealth(20, this);
            CallOnDestroyEvent();
        }

        public override Entity Clone()
        {
            return new HealFlower(0, 0, Size, Animations, value, false);
        }
    }
}
