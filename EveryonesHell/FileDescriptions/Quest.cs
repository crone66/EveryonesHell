/* 
 * Purpose: struct including all the characteristics of a quest
 * Author: Lukas Bosniak
 * Date: 12.11.2016
 */

namespace FileDescriptions
{
    public struct Quest
    {
        public string Name;
        public int QuestID;
        public int Questitem;
        public int BasedOnDialogue;
        public int RequiredItem;
        public int Enemy;
        public int EnemyCount;
        public string Description;

        public Quest(string name, int questID, int questItem, int basedOnDialogue, int requiredItem, int enemy, int enemyCount, string description)
        {
            Name = name;
            QuestID = questID;
            Questitem = questItem;
            BasedOnDialogue = basedOnDialogue;
            RequiredItem = requiredItem;
            Enemy = enemy;
            EnemyCount = enemyCount;
            Description = description;
        }
    }
}
