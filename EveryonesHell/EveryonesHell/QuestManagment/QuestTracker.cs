/* 
 * Purpose: struct including all the characteristics of a quest
 * Author: Lukas Bosniak
 * Date: 3.12.2016
 */

using System.Collections.Generic;
using FileDescriptions;
using System;

namespace EveryonesHell.QuestManagment
{
    /// <summary>
    /// checking the active quests
    /// </summary>
    public class QuestTracker
    {
        public InventorySystem.Inventory inventory;
        private QuestCollection loadedQuests = new QuestCollection();
        public List<Quest> activeQuests = new List<Quest>();
        public event EventHandler OnQuestFinished;

        /// <summary>
        /// initialize the questtracker
        /// </summary>
        /// <param name="inventory">inventorysystem to check wether the player gained a required item or not</param>
        /// <param name="loadedQuests">questcollection including every quest in the game</param>
        public QuestTracker(InventorySystem.Inventory inventory, QuestCollection loadedQuests)
        {
            this.inventory = inventory;
            this.loadedQuests = loadedQuests;
        }

        /// <summary>
        /// adding the new gained quest to the according list 
        /// </summary>
        /// <param name="questID">ID of the quest you want to activate</param>
        public bool ActivateQuest(int questID)
        {
            if (!activeQuests.Exists(q => q.QuestID == questID))
            {
                activeQuests.Add(loadedQuests.Quests[questID]);
                return true;
            }

            return false;
        }

        /// <summary>
        /// checking quests where the player has to require an item
        /// </summary>
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
                            inventory.InventorySlots.RemoveAt(j);
                            OnQuestFinished?.Invoke(this, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// checking quests where the player has to kill NPCs
        /// </summary>
        /// <param name="groupID">groupID of the entity that was just killed</param>
        private void EnemyQuest(int groupID)
        {
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].Enemy == groupID)
                {
                    if (activeQuests[i].EnemyCount - 1 <= 0)
                    {
                        activeQuests.RemoveAt(i);
                        OnQuestFinished?.Invoke(this, null);
                    }
                    else
                        activeQuests[i] = new Quest(activeQuests[i].Name, activeQuests[i].QuestID, activeQuests[i].Questitem, activeQuests[i].BasedOnDialogue, activeQuests[i].RequiredItem, activeQuests[i].Enemy, activeQuests[i].EnemyCount - 1, activeQuests[i].Description);

                    break;
                }
            }
        }

        /// <summary>
        /// checking whether a killed npc is part of a quest or not
        /// </summary>
        /// <param name="sender">eventsender</param>
        /// <param name="e">eventargs</param>
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
