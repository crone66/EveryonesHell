using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuestEditor
{
    public partial class Form1 : UserControl
    {
        bool error;
        List<Quest> loadedQuests;
        List<int> idList;

        public Form1()
        {
            InitializeComponent();

            error = true;
            loadedQuests = new List<Quest>();
            idList = new List<int>();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tbDescription.Text.Length == 0)
            {
                error = true;
                lbError.Text = "Quest-ID fehlt";
            }

            if (tbBasedOn.Text.Length == 0)
            {
                error = true;
                lbError.Text = "Quest-ID fehlt";
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
                XmlSerializer xml = new XmlSerializer(typeof(Questcollection));
                StreamWriter sw = new StreamWriter("quests.xml");

                string name = Convert.ToString(tbName.Text);
                int id = Convert.ToInt32(tbID.Text);
                int questItem = Convert.ToInt32(tbQuestitem.Text);
                int basedOnDialogue = Convert.ToInt32(tbBasedOn.Text);
                int requiredItem = Convert.ToInt32(tbRequired.Text);
                string description = Convert.ToString(tbDescription.Text);

                Quest quest = new Quest(name, id, questItem, basedOnDialogue, requiredItem, description);
                loadedQuests.Add(quest);
                Questcollection questcollection = new Questcollection(loadedQuests.ToArray());

                xml.Serialize(sw, questcollection);
                sw.Close();

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

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Questcollection));
                    StreamReader r = new StreamReader(openFileDialog1.FileNames[0]);

                    Questcollection questCollection = (Questcollection)xml.Deserialize(r);

                    loadedQuests.Clear();
                    loadedQuests.AddRange(questCollection.Quests);
                    if (loadedQuests.Count > 0)
                    {
                        Quest quest = loadedQuests[0];

                        tbName.Text = quest.Name.ToString();
                        tbID.Text = quest.QuestID.ToString();
                        tbQuestitem.Text = quest.Questitem.ToString();
                        tbBasedOn.Text = quest.BasedOnDialogue.ToString();
                        tbRequired.Text = quest.RequiredItem.ToString();
                        tbDescription.Text = quest.Description.ToString();
                    }

                    for (int i = 0; i < loadedQuests.Count; i++)
                    {
                        Quest quest = loadedQuests[i];

                        idList.Add(quest.QuestID);
                    }

                    r.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei konnte nicht geladen werden!" + Environment.NewLine + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btLoad_Click(null, EventArgs.Empty);
        }
    }
}
