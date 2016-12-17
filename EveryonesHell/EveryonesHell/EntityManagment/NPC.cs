﻿using SFML.Graphics;
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
        
        public NPC(Vector2f position, Vector2i size, Sprite sprite, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds)
            :base(position, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 3, 4, 50, 50, 0.16f), true, new Vector2f(1, 0), 1, healthBar, groupID,factionId, dialogIds)
        {

        }

        public NPC(int tileRow, int tileColumn, Vector2i size, Sprite sprite, Gaugebar healthBar, int groupID, int factionId, int[] dialogIds)
            : base(tileRow, tileColumn, size, new InventorySystem.Inventory(32), new AnimationManager(sprite, 3, 4, 50, 50, 0.16f), true, new Vector2f(1, 0), 620, healthBar, groupID, factionId, dialogIds)
        {
            
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
