using System;

namespace EveryonesHell.HUD
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
