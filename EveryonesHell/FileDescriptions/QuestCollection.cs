/* 
 * Purpose: struct collecting all the quests
 * Author: Lukas Bosniak
 * Date: 13.11.2016
 */

namespace FileDescriptions
{
    public struct QuestCollection
    {
        public Quest[] Quests;

        public QuestCollection(Quest[] quests)
        {
            Quests = quests;
        }
    }
}
