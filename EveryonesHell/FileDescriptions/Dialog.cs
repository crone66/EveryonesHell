using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDescriptions
{
    public struct Dialog
    {
        public int DialogID;
        public string DialogText;
        public int NextDialogId;
        public int[] DialogAnswerIds;

        public UnknownEntity[] Rewards;
        public UnknownEntity[] Conditions;

        public Dialog(int dialogId, string dialogText, int nextDialogId, int[] dialogAnswerIds, UnknownEntity[] rewards, UnknownEntity[] conditions)
        {
            DialogID = dialogId;
            DialogText = dialogText;
            NextDialogId = nextDialogId;
            DialogAnswerIds = dialogAnswerIds;
            Rewards = rewards;
            Conditions = conditions;
        }

    }

    public struct UnknownEntity
    {
        public string Type;
        public int Id;
        public int Count;

        public UnknownEntity(string type, int id, int count)
        {
            Type = type;
            Id = id;
            Count = count;
        }

        public UnknownEntity(string type, int id)
        {
            Type = type;
            Id = id;
            Count = 1;
        }
    }
}
