using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
namespace EveryonesHellEditor
{
    public partial class CreateNewFileDialog : Form
    {
        private string projectDir;
        public event EventHandler<FileCreatedArgs> FileCreated;
        public CreateNewFileDialog(string projectDir)
        {
            InitializeComponent();
            this.projectDir = projectDir;
        }

        private void CreateNewFileDialog_Load(object sender, EventArgs e)
        {
            cbFileType.Items.Clear();
            foreach (KeyValuePair<string, KeyValuePair<Type, EditorControl>> item in TypeSettings.Mapping)
            {
                cbFileType.Items.Add(item.Key);
            }
            cbFileType.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string path = projectDir +"/" + tbName.Text + ".xml";
            if (!File.Exists(path))
            {
                Type type = GetTypeFromString();
                if(type == null)
                {
                    MessageBox.Show("Es muss ein Dateityp ausgewählt werden!");
                    return;
                }

                FileDescription file = new FileDescription(tbName.Text, path, type.AssemblyQualifiedName);
                FileCreated?.Invoke(this, new FileCreatedArgs(file));
                this.Close();
            }
            else
            {
                MessageBox.Show("Es existiert bereits eine Datei mit diesen Namen!");
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Type GetTypeFromString()
        {
            if (cbFileType.SelectedIndex >= 0)
            {
                if (TypeSettings.Mapping.ContainsKey(cbFileType.Text))
                    return TypeSettings.Mapping[cbFileType.Text].Key;
            }
            return null;
        }
    }


    public class FileCreatedArgs:EventArgs
    {
        public FileDescription File;

        public FileCreatedArgs(FileDescription file)
        {
            File = file;
        }
    }
}
