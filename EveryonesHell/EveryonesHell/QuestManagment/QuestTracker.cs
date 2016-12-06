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
    class QuestTracker
    {
        XmlSerializer xml = new XmlSerializer(typeof(QuestCollection));
        StreamReader r;

        InventorySystem.Inventory inventory;
        List<Quest> loadedQuests = new List<Quest>();
        List<Quest> activeQuests = new List<Quest>();

        /// <summary>
        /// initialize the questtracker
        /// </summary>
        /// <param name="inventory">invetorysystem to check wether the player gained a required item or not</param>
        /// <param name="path">path of the xml-file containing all the quests</param>
        public QuestTracker(InventorySystem.Inventory inventory, string path)
        {
            this.inventory = inventory;
            r = new StreamReader(path);

            QuestCollection questCollection = (QuestCollection)xml.Deserialize(r);
            loadedQuests.AddRange(questCollection.Quests);
        }

        /// <summary>
        /// adding the new gained quest to the according list 
        /// </summary>
        /// <param name="questID">ID of the quest you want to activate</param>
        public void ActivateQuest(int questID)
        {
            activeQuests.Add(loadedQuests[questID]);
        }

        /// <summary>
        /// checking the conditions of the active quests
        /// </summary>
        public void CheckConditions()
        {
            int questType = -1;

            for (int i = activeQuests.Count - 1; i >= 0; i--)
            {
                if (activeQuests[i].Questitem != -1)
                {
                    questType = 0;
                }

                if (activeQuests[i].Enemy != -1)
                {
                    questType = 1;
                }

                switch (questType)
                {
                    case 0:
                        ItemQuest(i);
                        break;
                    case 1:
                        EnemyQuest(i);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// checking quests where the player has to require an item
        /// </summary>
        /// <param name="quest">ID of the quest</param>
        private void ItemQuest(int quest)
        {
            for (int i = 0; i < inventory.InventorySlots.Count; i++)
            {
                if (inventory.InventorySlots[i].ItemId == activeQuests[quest].RequiredItem)
                {
                    activeQuests.RemoveAt(quest);
                }
            }
        }


        /// <summary>
        /// checking quests where the player has to kill NPCs
        /// </summary>
        /// <param name="quest">ID of the quest</param>
        private void EnemyQuest(int quest)
        {

        }
    }
}
