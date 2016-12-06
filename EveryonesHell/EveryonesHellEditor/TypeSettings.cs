using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveryonesHellEditor
{
    public static class TypeSettings
    {
        public static Dictionary<string, KeyValuePair<Type, EditorControl>> Mapping = new Dictionary<string, KeyValuePair<Type, EditorControl>>();
    }
}
