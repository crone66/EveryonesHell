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
        public string[] DialogAnswers;
        public int[] DialogConditions;
        public string[] DialogValues;
        public int[] NextDialogAnswerIds;

        public Dialog(int dialogid, string dialogtext, int nextdialogid, string[] dialoganswers, int[] dialogconditions, string[] dialogvalues, int[] nextdialoganswerids)
        {
            DialogID = dialogid;
            DialogText = dialogtext;
            NextDialogId = nextdialogid;
            DialogAnswers = dialoganswers;
            DialogConditions = dialogconditions;
            DialogValues = dialogvalues;
            NextDialogAnswerIds = nextdialoganswerids;
        }

    }
}
