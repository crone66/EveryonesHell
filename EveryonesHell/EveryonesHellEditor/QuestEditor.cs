/* 
 * Purpose: Windows forms application to create a XML-file including all quests
 * Author: Lukas Bosniak
 * Date: 12.11.2016
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using FileDescriptions;

namespace EveryonesHellEditor
{
    public partial class QuestEditor : EditorControl
    {
        bool error;
        List<Quest> loadedQuests;
        List<int> idList;

        public QuestEditor()
        {
            InitializeComponent();

            error = true;
            loadedQuests = new List<Quest>();
            idList = new List<int>();
        }

        public override void SetupNodes(TreeNode fileNode)
        {
            for (int i = 0; i < loadedQuests.Count; i++)
            {
                fileNode.Nodes.Add(new TreeNode(loadedQuests[i].QuestID.ToString()));
            }
        }

        public override object LoadCollection(FileDescription file)
        {
            return LoadFile(typeof(QuestCollection), file.Path);
        }

        public override void Init(FileDescription file, TreeNode fileNode)
        {
            base.Init(file, fileNode);
            if (File.Exists(file.Path))
            {
                QuestCollection questCollection = (QuestCollection)LoadCollection(file);
                loadedQuests.Clear();
                if (questCollection.Quests != null)
                    loadedQuests.AddRange(questCollection.Quests);
            }
        }

        public override void Init(FileDescription file, TreeNode fileNode, int id)
        {
            base.Init(file, fileNode, id);
            if (File.Exists(file.Path))
            {
                QuestCollection questCollection = (QuestCollection)LoadCollection(file);
                loadedQuests.Clear();
                if (questCollection.Quests != null)
                    loadedQuests.AddRange(questCollection.Quests);

                if (loadedQuests.Count > 0)
                {
                    int index = loadedQuests.FindIndex(q => q.QuestID == id);
                    if (index >= 0)
                    {
                        Quest quest = loadedQuests[index];

                        tbName.Text = quest.Name.ToString();
                        tbID.Text = quest.QuestID.ToString();
                        tbQuestitem.Text = quest.Questitem.ToString();
                        tbBasedOn.Text = quest.BasedOnDialogue.ToString();
                        tbRequired.Text = quest.RequiredItem.ToString();
                        tbEnemy.Text = quest.Enemy.ToString();
                        tbNumber.Text = quest.EnemyCount.ToString();
                        tbDescription.Text = quest.Description.ToString();
                    }
                }
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tbDescription.Text.Length == 0)
            {
                error = true;
                lbError.Text = "Quest-ID fehlt";
            }

            if (tbNumber.Text.Length == 0 && (tbEnemy.Text.Length > 0 || tbEnemy.Text == "-1"))
            {
                error = true;
                lbError.Text = "Number of enemies to kill fehlt";
            }

            if (tbBasedOn.Text.Length == 0)
            {
                error = true;
                lbError.Text = "Based on Dialogue fehlt";
            }

            if (tbID.Text.Length == 0)
            {
                error = true;
                lbError.Text = "Quest-ID fehlt";
            }

            if (tbName.Text.Length == 0)
            {
                error = true;
                lbError.Text = "Questname fehlt";
            }

            if (tbQuestitem.Text.Length == 0)
            {
                tbQuestitem.Text = "-1";
            }

            if (tbRequired.Text.Length == 0)
            {
                tbRequired.Text = "-1";
            }

            if (tbEnemy.Text.Length == 0)
            {
                tbEnemy.Text = "-1";
            }

            if (tbNumber.Text.Length == 0 && (tbEnemy.Text.Length == 0 || tbEnemy.Text == "-1"))
            {
                tbRequired.Text = "-1";
            }

            for (int i = 0; i < idList.Count; i++)
            {
                if (Convert.ToInt32(tbID.Text) == idList[i])
                {
                    error = true;
                    lbError.Text = "Es gibt bereits eine Quest mit dieser ID";
                }
            }

            if (!error)
            {
                string name = Convert.ToString(tbName.Text);
                int id = Convert.ToInt32(tbID.Text);
                if (loadedQuests.Exists(q => q.QuestID == id))
                {
                    if (MessageBox.Show("Achtung die ID existiert bereits, soll die ID überschrieben werden?", "Überschreiben?", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }

                int questItem = Convert.ToInt32(tbQuestitem.Text);
                int basedOnDialogue = Convert.ToInt32(tbBasedOn.Text);
                int requiredItem = Convert.ToInt32(tbRequired.Text);
                int enemy = Convert.ToInt32(tbEnemy.Text);
                int enemyCount = Convert.ToInt32(tbNumber.Text);
                string description = Convert.ToString(tbDescription.Text);

                Quest quest = new Quest(name, id, questItem, basedOnDialogue, requiredItem, enemy, enemyCount, description);
                loadedQuests.Add(quest);
                QuestCollection questcollection = new QuestCollection(loadedQuests.ToArray());

                SaveFile(questcollection, file.Path);
                fileNode.Nodes.Add(new TreeNode(id.ToString()));
                fileNode.ExpandAll();
                MessageBox.Show("Speichern erfolgreich");
            }
            else
            {
                MessageBox.Show("Behebe Fehler");
            }
        }

        private void tbID_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text.Length > 0)
            {
                int value;
                if (!int.TryParse(t.Text, out value))
                {
                    lbError.Text = "Eingabe hat das falsche Format";
                    error = true;
                }
                else
                {
                    lbError.Text = "";
                    error = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            New();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void New()
        {
            tbName.Text = "";
            tbID.Text = GetNextId().ToString();
            tbQuestitem.Text = "";
            tbBasedOn.Text = "";
            tbRequired.Text = "";
            tbEnemy.Text = "";
            tbNumber.Text = "";
            tbDescription.Text = "";
        }

        private int GetNextId()
        {
            int id = 0;
            do
            {
                if (!loadedQuests.Exists(q => q.QuestID == id))
                    return id;
                else
                    id++;
            } while (true);
        }
    }
}
