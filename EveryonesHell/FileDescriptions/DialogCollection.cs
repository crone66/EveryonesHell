using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDescriptions
{
    public struct DialogCollection
    {
        public Dialog[] Dialogs;

        public DialogCollection(Dialog[] dialogs)
        {
            Dialogs = dialogs;
        }
    }
}
