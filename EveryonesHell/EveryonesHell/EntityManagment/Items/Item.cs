using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment.Items
{
    public abstract class Item : InteractiveObject
    {
        private int value;
        private bool canPickedUp;

        public bool CanPickedUp
        {
            get
            {
                return canPickedUp;
            }
        }

        public Item(int tileRow, int tileColumn, Vector2i size, AnimationManager animations, int groupId, int value, bool canPickedUp)
            :base(tileRow, tileColumn, size, animations, false,new Vector2f(1, 0), 0, 0, groupId, -1)
        {
            this.value = value;
            this.canPickedUp = canPickedUp;
        }

        public virtual bool PickUp(InteractiveObject picker)
        {
            if (picker != null && canPickedUp)
            {
                value = picker.Inventory.AddItem(GroupID, value, 10, true);
                if (value == 0)
                    CallOnDestroyEvent();

                return true;
            }

            return false;
        }

        public abstract void Use(InteractiveObject user);
    }
}
