using System;

namespace EveryonesHell
{
    public class DialogChangedArgs : EventArgs
    {
        public int NewDialogId;
        public int PrevDialogId;

        public DialogChangedArgs(int newDialogId, int prevDialogId)
        {
            NewDialogId = newDialogId;
            PrevDialogId = prevDialogId;
        }
    }
}
