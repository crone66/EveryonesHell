using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveryonesHellEditor
{
    public partial class EveryonesHellEditorMain : Form
    {
        public EveryonesHellEditorMain()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (tbPath.Text.Length > 0 && System.IO.File.Exists(tbPath.Text))
            {
                ProjectScreen ps = new ProjectScreen(this, tbPath.Text);
                ps.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Der angegebene Pfad existiert nicht!");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewProject p = new NewProject(this);
            p.Show();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(openFileDialog1.FileNames.Length > 0)
                    tbPath.Text = openFileDialog1.FileNames[0];
            }
        }

        private void EveryonesHellEditorMain_Load(object sender, EventArgs e)
        {
            TypeSettings.Mapping.Add("Quest collection", new KeyValuePair<Type, EditorControl>(typeof(FileDescriptions.QuestCollection), new QuestEditor()));
            TypeSettings.Mapping.Add("Item collection", new KeyValuePair<Type, EditorControl>(typeof(FileDescriptions.ItemCollection), new ItemEditor()));
            TypeSettings.Mapping.Add("Dialog collection", new KeyValuePair<Type, EditorControl>(typeof(FileDescriptions.DialogCollection), new DialogEditor()));
        }
    }
}
