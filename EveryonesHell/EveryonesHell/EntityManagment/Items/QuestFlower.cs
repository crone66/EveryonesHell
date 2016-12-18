using SFML.System;

namespace EveryonesHell.EntityManagment.Items
{
    public class QuestFlower : Item
    {
        public QuestFlower(int tileRow, int tileColumn, Vector2i size, AnimationManager animations, int value)
            : base(tileRow, tileColumn, size, animations, 6, value, true)
        {

        }

        public override void Use(InteractiveObject user)
        {
        }
    }
}
