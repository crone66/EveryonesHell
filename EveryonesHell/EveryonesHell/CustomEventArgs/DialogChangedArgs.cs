using System;

namespace EveryonesHell.HUD
{
    public class DialogChangedArgs : EventArgs
    {
        public int NewDialogId;
        public int PrevDialogId;

        /// <summary>
        /// Dialog changed event args
        /// </summary>
        /// <param name="newDialogId">New dialog id</param>
        /// <param name="prevDialogId">Previouse dialog id</param>
        public DialogChangedArgs(int newDialogId, int prevDialogId)
        {
            NewDialogId = newDialogId;
            PrevDialogId = prevDialogId;
        }
    }
}
