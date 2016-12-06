/* 
 * Purpose: struct collecting all the quests
 * Author: Lukas Bosniak
 * Date: 13.11.2016
 */

namespace FileDescriptions
{
    public struct Questcollection
    {
        public Quest[] Quests;

        public Questcollection(Quest[] quests)
        {
            Quests = quests;
        }
    }
}
