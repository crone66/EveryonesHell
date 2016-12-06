using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
namespace EveryonesHellEditor
{
    public partial class ProjectScreen : Form
    {
        private string projectFilePath;
        private EveryonesHellEditorMain main;
        private ProjectFile projectFile;

        private Dictionary<string, List<string>> fileElements;
        private TreeNode rootNode;

        public ProjectScreen(EveryonesHellEditorMain main, string projectFilePath)
        {
            InitializeComponent();
            this.main = main;
            this.projectFilePath = projectFilePath;
            fileElements = new Dictionary<string, List<string>>();
        }

        private void ProjectScreen_Load(object sender, EventArgs e)
        {
            if(LoadProjectFile())
            {
                LoadCollections();
            }

            rootNode.ExpandAll();
            /*
             * 
-> Npc
 -> Quest
  -> Dialog -> Dialog -> Item/Recipe

-> Recipe = Item
 -> Item

-> Map
 -> NPC
  -> Item
             */
        }

        private bool LoadProjectFile()
        {
            try
            {
                projectFile = (ProjectFile)TryLoad(typeof(ProjectFile), projectFilePath);
                rootNode = new TreeNode(projectFile.Name);          
                tvFiles.Nodes.Add(rootNode);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Projektdatei konnte nicht geladen werden: " + ex.Message);
                Close();
                return false;
            }
        }

        private void LoadCollections()
        {
            if (projectFile.Files != null)
            {
                foreach (FileDescription fileDescription in projectFile.Files)
                {
                    TreeNode fileNode = new TreeNode(fileDescription.Name);
                    rootNode.Nodes.Add(fileNode);
                    Type type = Type.GetType(fileDescription.Type);

                    if (TypeSettings.Mapping.Any(m => m.Value.Key == type))
                    {
                        KeyValuePair<string, KeyValuePair<Type, EditorControl>> mapping = TypeSettings.Mapping.First(m => m.Value.Key == type);
                        mapping.Value.Value.LoadFile = TryLoad;
                        mapping.Value.Value.SaveFile = TrySave;
                        mapping.Value.Value.Init(fileDescription, fileNode);
                        mapping.Value.Value.SetupNodes(fileNode);
                    }

                    fileElements.Add(fileNode.Text, new List<string>());
                    foreach (TreeNode childNode in fileNode.Nodes)
                    {
                        fileElements[fileNode.Text].Add(childNode.Text);
                    }

                    
                    /*Type type = ConvertToType(item.Type);
                    object data = TryLoad(type, item.Path);
                    if(data != null)
                    {
                        files.Add(item.Name, data);
                        rootNode.Nodes.Add(new TreeNode(item.Name));
                    }*/
                }
            }
        }

        private void ProjectScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            splitContainer1.Panel1.Controls.Clear();
            main.Show();
        }

        private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (projectFile.Files.Any(f => f.Name == e.Node.Text))
                {
                    FileDescription fd = projectFile.Files.First(f => f.Name == e.Node.Text);
                    propertyGrid1.SelectedObject = fd;
                }
                else if(projectFile.Files.Any(f => f.Name == e.Node.Parent?.Text))
                {
                    OpenEditor(e.Node.Parent.Text, Convert.ToInt32(e.Node.Text));
                }
            }
            catch
            {

            }
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFileDialog create = new CreateNewFileDialog(projectFile.ProjectDir);
            create.FileCreated += Create_FileCreated;
            create.Show();
        }

        private void Create_FileCreated(object sender, FileCreatedArgs e)
        {
            FileDescription[] desc;
            if (projectFile.Files != null)
            {
                desc = new FileDescription[projectFile.Files.Length + 1];
                Array.Copy(projectFile.Files, desc, projectFile.Files.Length);
            }
            else
            {
                desc = new FileDescription[1];
            }
            desc[desc.Length - 1] = e.File;

            projectFile = new ProjectFile(projectFile.Name, projectFile.ProjectDir, desc);
            TrySave(projectFile, projectFilePath);

            rootNode.Nodes.Add(new TreeNode(e.File.Name));
            rootNode.ExpandAll();
            fileElements.Add(e.File.Name, new List<string>());
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = tvFiles.SelectedNode.Text;
            if(fileElements.ContainsKey(text))
            {
                OpenEditor(text);
            }
        }

        private void OpenEditor(string fileName, int id = -1)
        {
            try
            {
                splitContainer1.Panel1.Controls.Clear();
                FileDescription file = projectFile.Files.First(f => f.Name == fileName);
                Type type = Type.GetType(file.Type);

                KeyValuePair<string, KeyValuePair<Type, EditorControl>> res = TypeSettings.Mapping.First(s => s.Value.Key == type);

                splitContainer1.Panel1.Controls.Add(res.Value.Value);
                res.Value.Value.Dock = DockStyle.Fill;
                res.Value.Value.Show();
                res.Value.Value.SaveFile = TrySave;
                res.Value.Value.LoadFile = TryLoad;
                if(id > -1)
                    res.Value.Value.Init(file, tvFiles.SelectedNode, id);
                else
                    res.Value.Value.Init(file, tvFiles.SelectedNode);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Dialog konnte nicht erstellt werden: " + Environment.NewLine + ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private object TryLoad(Type type, string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    XmlSerializer xml = new XmlSerializer(type);
                    return xml.Deserialize(stream);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Datei " + path + " konnte nicht geladen werden!" + Environment.NewLine + ex.Message);
                return null;
            }
        }

        private bool TrySave(object data, string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    XmlSerializer xml = new XmlSerializer(data.GetType());
                    xml.Serialize(stream, data);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Datei " + path + " konnte nicht gespeichert werden!" + Environment.NewLine + ex.Message);
                return false;
            }
        }

        private void tvFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvFiles.SelectedNode = e.Node;
            contextMenuStrip1.Items[1].Enabled = fileElements.ContainsKey(e.Node.Text) && e.Node != rootNode;
            contextMenuStrip1.Items[0].Enabled = e.Node == rootNode;
        }


    }
}
