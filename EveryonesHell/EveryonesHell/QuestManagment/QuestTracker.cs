/* 
 * Purpose: struct including all the characteristics of a quest
 * Author: Lukas Bosniak
 * Date: 3.12.2016
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FileDescriptions;

namespace EveryonesHell.QuestManagment
{
    public class QuestTracker
    {
        public InventorySystem.Inventory inventory;
        private QuestCollection loadedQuests = new QuestCollection();
        public List<Quest> activeQuests = new List<Quest>();

        /// <summary>
        /// initialize the questtracker
        /// </summary>
        /// <param name="inventory">invetorysystem to check wether the player gained a required item or not</param>
        /// <param name="path">path of the xml-file containing all the quests</param>
        public QuestTracker(InventorySystem.Inventory inventory, QuestCollection loadedQuests)
        {
            this.inventory = inventory;
            this.loadedQuests = loadedQuests;
        }

        /// <summary>
        /// adding the new gained quest to the according list 
        /// </summary>
        /// <param name="questID">ID of the quest you want to activate</param>
        public void ActivateQuest(int questID)
        {
            activeQuests.Add(loadedQuests.Quests[questID]);
        }

        /// <summary>
        /// checking quests where the player has to require an item
        /// </summary>
        /// <param name="quest">ID of the quest</param>
        public void ItemQuest()
        {
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].Questitem != -1)
                {
                    for (int j = 0; i < inventory.InventorySlots.Count; i++)
                    {
                        if (inventory.InventorySlots[j].ItemId == activeQuests[i].RequiredItem)
                        {
                            activeQuests.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// checking quests where the player has to kill NPCs
        /// </summary>
        /// <param name="quest">ID of the quest</param>
        private void EnemyQuest(int groupID)
        {
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].Enemy == groupID)
                {
                    activeQuests[i] = new Quest(activeQuests[i].Name, activeQuests[i].QuestID, activeQuests[i].Questitem, activeQuests[i].BasedOnDialogue, activeQuests[i].RequiredItem, activeQuests[i].Enemy, activeQuests[i].EnemyCount - 1, activeQuests[i].Description);
                }
            }
        }

        /// <summary>
        /// checking whether a killed npc is part of a quest or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Projectile_OnDoDamage(object sender, EntityManagment.VictimArgs e)
        {
            if (e.Victim is EntityManagment.InteractiveObject)
            {
                EntityManagment.InteractiveObject interativeObject = e.Victim as EntityManagment.InteractiveObject;

                if (e.Killed)
                {
                    EnemyQuest(interativeObject.GroupID);
                }
            }
        }
    }
}
