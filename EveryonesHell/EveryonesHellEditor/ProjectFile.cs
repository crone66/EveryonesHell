using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHellEditor
{
    public struct ProjectFile
    {
        public string Name;
        public FileDescription[] Files;
    }

    public struct FileDescription
    {
        public string Name;
        public string Path;
        public Type Type;
    }
}
