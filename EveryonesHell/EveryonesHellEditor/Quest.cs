namespace QuestEditor
{
    public struct Quest
    {
        public string Name;
        public int QuestID;
        public int Questitem;
        public int BasedOnDialogue;
        public int RequiredItem;
        public string Description;

        public Quest(string name, int questID, int questItem, int basedOnDialogue, int requiredItem, string description)
        {
            Name = name;
            QuestID = questID;
            Questitem = questItem;
            BasedOnDialogue = basedOnDialogue;
            RequiredItem = requiredItem;
            Description = description;
        }
    }
}
