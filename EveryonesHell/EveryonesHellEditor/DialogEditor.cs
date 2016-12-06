using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileDescriptions;
using System.Xml.Serialization;
using System.IO;

namespace EveryonesHellEditor
{
    public partial class DialogEditor : EditorControl
    {
        private List<Dialog> dialogs;
        private Nullable<Dialog> currentDialog;

        public DialogEditor()
        {
            InitializeComponent();
            dialogs = new List<Dialog>();
        }

        private void DialogEditor_Load(object sender, EventArgs e)
        {
            New();
        }

        public override void Init(FileDescription file, TreeNode fileNode)
        {
            base.Init(file, fileNode);
            if (File.Exists(file.Path))
            {
                dialogs.Clear();
                dialogs.AddRange(((DialogCollection)LoadCollection(file)).Dialogs);
            }
        }

        public override void Init(FileDescription file, TreeNode fileNode, int id)
        {
            base.Init(file, fileNode, id);
            if (File.Exists(file.Path))
            {
                dialogs.Clear();
                dialogs.AddRange(((DialogCollection)LoadCollection(file)).Dialogs);
            }
            New(id);
        }

        public override void SetupNodes(TreeNode fileNode)
        {
            for (int i = 0; i < dialogs.Count; i++)
            {
                fileNode.Nodes.Add(dialogs[i].DialogID.ToString());
            }
        }

        public override object LoadCollection(FileDescription file)
        {
            return LoadFile(typeof(DialogCollection), file.Path);
        }

        private void UpdatePreview(int dialogId)
        {
            try
            {
                Dialog diag = dialogs.First(d => d.DialogID == dialogId);
                lblPreviewNextId.Text = diag.NextDialogId.ToString();
                lblPreviewText.Text = diag.DialogText;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Preview kann nicht angezeigt werden: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int diagId = Convert.ToInt32(cbId.Text);
                int nextId = cbNextId.Text.Length > 0 ? Convert.ToInt32(cbNextId.Text) : -1;
                string text = tbText.Text;
                int[] answerIds = new int[lbAnswers.Items.Count];
                for (int i = 0; i < lbAnswers.Items.Count; i++)
                {
                    answerIds[i] = Convert.ToInt32(lbAnswers.Items[i]);
                }

                if(dialogs.Exists(d => d.DialogID == diagId))
                {
                    dialogs.Remove(dialogs.Find(d => d.DialogID == diagId));
                }

                Dialog diag = new Dialog(diagId, text, nextId, answerIds, null, null);
                dialogs.Add(diag);
                fileNode.Nodes.Add(new TreeNode(diagId.ToString()));
                fileNode.ExpandAll();
                if (SaveFile(new DialogCollection(dialogs.ToArray()), file.Path))
                {
                    MessageBox.Show("Daten wurden gespeichert!");
                    New();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Eingabefelder prüfen: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Soll der Dialog wirklich gelöscht werden?", "Löschen", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (currentDialog.HasValue)
                {
                    for (int i = 0; i < fileNode.Nodes.Count; i++)
                    {
                        if (fileNode.Nodes[i].Text == currentDialog.Value.DialogID.ToString())
                            fileNode.Nodes.RemoveAt(i);
                    }
                    dialogs.Remove(currentDialog.Value);
                    SaveFile(new DialogCollection(dialogs.ToArray()), file.Path);
                }

                New();
            }
        }

        private void Clear()
        {
            cbAnswerId.Text = "";
            cbAnswerId.Items.Clear();
            cbId.Text = "";
            cbId.Items.Clear();
            cbNextId.Text = "";
            cbNextId.Items.Clear();
            tbText.Clear();
            lbAnswers.Items.Clear();
            lblPreviewNextId.Text = "";
            lblPreviewText.Text = "";
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if(cbNextId.Text.Length > 0)
            {
                UpdatePreview(Convert.ToInt32(cbNextId.Text));
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if(cbAnswerId.Text.Length > 0)
                lbAnswers.Items.Add(cbAnswerId.Text);
        }

        private int GetNextId()
        {
            int id = 0;
            do
            {
                if (!dialogs.Exists(d => d.DialogID == id))
                    return id;
                else
                    id++;
            } while (true);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Alle nicht gespeicherten Änderungen gehen verloren, trotzdem fortfahren?", "Neu", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                New();
            }
        }

        private void New()
        {
            Clear();
            for (int i = 0; i < dialogs.Count; i++)
            {
                cbId.Items.Add(dialogs[i].DialogID.ToString());
                cbAnswerId.Items.Add(dialogs[i].DialogID.ToString());
                cbNextId.Items.Add(dialogs[i].DialogID.ToString());
            }

            cbId.Text = GetNextId().ToString();
        }

        private void New(int id)
        {
            New();
            int index = dialogs.FindIndex(d => d.DialogID == id);
            if(index >= 0)
            {
                currentDialog = dialogs[index];
                ShowCurrentDialog();
            }
        }

        private void lbAnswers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAnswers.SelectedIndex > -1)
            {
                UpdatePreview(Convert.ToInt32(lbAnswers.Items[lbAnswers.SelectedIndex]));
            }
        }

        private void cbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentDialog = dialogs.First(d => d.DialogID == Convert.ToInt32(cbId.Text));
                ShowCurrentDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Dialog konnte nicht angezeigt werden: " + ex.Message);
            }
        }

        private void ShowCurrentDialog()
        {
            Clear();
            tbText.Text = currentDialog.Value.DialogText;
            cbId.Text = currentDialog.Value.DialogID.ToString();
            for (int i = 0; i < currentDialog.Value.DialogAnswerIds.Length; i++)
            {
                lbAnswers.Items.Add(currentDialog.Value.DialogAnswerIds[i].ToString());
            }
            cbNextId.Text = currentDialog.Value.NextDialogId.ToString();         
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if(lbAnswers.SelectedIndex >= 0)
                lbAnswers.Items.RemoveAt(lbAnswers.SelectedIndex);
        }
    }
}
