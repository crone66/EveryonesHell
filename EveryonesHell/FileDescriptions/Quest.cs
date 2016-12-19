/* 
 * Purpose: struct including all the characteristics of a quest
 * Author: Lukas Bosniak
 * Date: 12.11.2016
 */

namespace FileDescriptions
{
    /// <summary>
    /// struct including all the parameters of a quest
    /// </summary>
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

        /// <summary>
        /// initialze the struct quest
        /// </summary>
        /// <param name="name">name of the quest</param>
        /// <param name="questID">ID of the quest</param>
        /// <param name="questItem">item the player has to find</param>
        /// <param name="basedOnDialogue">dialogue the player must have to get the quest</param>
        /// <param name="requiredItem">item the play must have in his inventory to get the quest</param>
        /// <param name="enemy">ID of the enemy the player has to kill</param>
        /// <param name="enemyCount">amount of enemies the player has to kill</param>
        /// <param name="description">description of the quest</param>
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
