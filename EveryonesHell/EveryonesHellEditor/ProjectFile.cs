using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHellEditor
{
    public struct ProjectFile
    {
        public string Name;
        public string ProjectDir;
        public FileDescription[] Files;

        public ProjectFile(string name, string projectDir, FileDescription[] files)
        {
            Name = name;
            ProjectDir = projectDir;
            Files = files;
        }
    }

    public struct FileDescription
    {
        private string name;
        private string path;
        private string type;

        [ReadOnly(true)]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [ReadOnly(true)]
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        [ReadOnly(true)]
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public FileDescription(string name, string path, string type)
        {
            this.name = name;
            this.path = path;
            this.type = type;
        }
    }
}
