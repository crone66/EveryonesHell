using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveryonesHellEditor
{
    public partial class EditorControl : UserControl
    {
        public delegate object LoadingHandler(Type type, string path);
        public delegate bool SavingHandler(object data, string path);

        public LoadingHandler LoadFile;
        public SavingHandler SaveFile;

        protected FileDescription file;
        protected TreeNode fileNode;
        protected int currentId;



        public EditorControl()
        {
            InitializeComponent();
        }

        public virtual void Init(FileDescription file, TreeNode fileNode)
        {
            this.file = file;
            this.fileNode = fileNode;
            currentId = -1;
        }

        public virtual void Init(FileDescription file, TreeNode fileNode, int id)
        {
            this.file = file;
            this.fileNode = fileNode;
            currentId = id;
        }

        public virtual void SetupNodes(TreeNode fileNode) { }

        public virtual object LoadCollection(FileDescription file) { return null; }
    }
}
