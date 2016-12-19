/* 
 * Purpose: struct collecting all the quests
 * Author: Lukas Bosniak
 * Date: 13.11.2016
 */

namespace FileDescriptions
{
    /// <summary>
    /// struct including all the quests
    /// </summary>
    public struct QuestCollection
    {
        public Quest[] Quests;

        /// <summary>
        /// initialze the struct questcollection
        /// </summary>
        /// <param name="quests">quest you want to add to the struct</param>
        public QuestCollection(Quest[] quests)
        {
            Quests = quests;
        }
    }
}
