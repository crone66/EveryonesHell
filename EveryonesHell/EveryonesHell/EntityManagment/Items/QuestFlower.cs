using SFML.System;

namespace EveryonesHell.EntityManagment.Items
{
    public class QuestFlower : Item
    {
        public QuestFlower(int tileRow, int tileColumn, Vector2i size, AnimationManager animations, int value, bool isPrototyp)
            : base(tileRow, tileColumn, size, animations, 6, value, true, isPrototyp)
        {

        }

        public QuestFlower(Vector2i size, AnimationManager animations, int value, bool isPrototyp)
            : base(size, animations, 6, value, true, isPrototyp)
        {

        }

        public override void Use(InteractiveObject user)
        {
        }

        public override Entity Clone()
        {
            return new QuestFlower(0, 0, Size, animations, value, false);
        }
    }
}
