using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
namespace EveryonesHellEditor
{
    public partial class NewProject : Form
    {
        private EveryonesHellEditorMain main;
        private bool showMain = true;
        public NewProject(EveryonesHellEditorMain main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(tbName.Text.Length > 0 && tbPath.Text.Length > 0)
            {
                if (!Directory.Exists(tbPath.Text))
                    Directory.CreateDirectory(tbPath.Text);

                string path = tbPath.Text + "/" +tbName.Text + ".xml";
                if (!File.Exists(path) || 
                    MessageBox.Show("Es existiert bereits eine Datei, soll die Datei überschrieben werden?", "Datei existiert bereits", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        ProjectFile pFile = new ProjectFile(tbName.Text, tbPath.Text, null);
                        XmlSerializer xml = new XmlSerializer(typeof(ProjectFile));
                        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            xml.Serialize(stream, pFile);
                        }

                        ProjectScreen ps = new ProjectScreen(main, path);
                        ps.Show();
                        showMain = false;
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Datei konnte nicht erstellt werden: " + ex.Message);
                    }
                }
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void NewProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (showMain)
            {
                main.Show();
            }
        }
    }
}
