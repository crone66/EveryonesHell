using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDescriptions
{
    public struct Item
    {
        public int ItemID;
        public string ItemName;
        public int SpriteID;
        public int Rarity;
        public int Value;
        public bool Stackable;
        public bool Tradeable;
        public string Description;

        public Item(int itemid, string itemname, int spriteid, int rarity,int value, bool stackable, bool tradeable, string description)
        {
            ItemID = itemid;
            ItemName = itemname;
            SpriteID = spriteid;
            Rarity = rarity;
            Value = value;
            Stackable = stackable;
            Tradeable = tradeable;
            Description = description;
        }
    }
}
