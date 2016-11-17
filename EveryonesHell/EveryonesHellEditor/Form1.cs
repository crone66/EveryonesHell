using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        bool error = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(tbSpriteID.Text.Length > 0)
            {
                textBoxCheck(tbSpriteID, e);
            }
            else
            {
                DisplayLabel.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (tbSpriteID.Text.Length > 0)
            {
                textBoxCheck(tbValue, e);
            }
            else
            {
                DisplayLabel.Text = "";
            }
        }

        private void cbItemID_TextUpdate(object sender, EventArgs e)
        {
            if (cbItemID.Text.Length > 0)
            {
                comboBoxCheck(cbItemID, e);
            }
            else
            {
                DisplayLabel.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!error)
            {
                int ItemID = 0;
                if(cbItemID.Text.Length > 0)
                {
                    ItemID = Convert.ToInt32(cbItemID.Text);
                }

                string ItemName = tbItemName.Text;

                int SpriteID = 0;
                if(tbSpriteID.Text.Length > 0)
                {
                    SpriteID = Convert.ToInt32(tbSpriteID.Text);
                }

                int Rarity = cbRarity.SelectedIndex;

                int Value = 0;
				if (tbValue.Text.Length > 0)
				{
					Value = Convert.ToInt32(tbValue.Text);
				}

                string Description = tbDescription.Text;
                bool Stackable = chBStackable.Checked;
                bool Tradeable = chBTradeable.Checked;

                Item item = new Item(ItemID, ItemName, SpriteID, Rarity, Value, Stackable, Tradeable, Description);

                List<Item> ItemList;

                XmlSerializer xml = new XmlSerializer(typeof(Item));
                StreamWriter sw = new StreamWriter("item" + ItemID.ToString() + ".xml");
                xml.Serialize(sw, item);
                sw.Close();

                DisplayLabel.Text = "Save successfull!";
            }
            else
            {
                MessageBox.Show("Please fill all fields correctly.");
            }
        }

        private void textBoxCheck(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text.Length > 0)
            {
                int value;
                if (!int.TryParse(t.Text, out value))
                {
                    DisplayLabel.Text = "Value and Sprite ID need to be Numbers.";
                    error = true;
                }
                else
                {
                    DisplayLabel.Text = "";
                    error = false;
                }
            }
        }

        private void comboBoxCheck(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Text.Length > 0)
            {
                int value;
                if (!int.TryParse(cb.Text, out value))
                {
                    DisplayLabel.Text = "Item ID has to be a Numeric Value.";
                    error = true;
                }
                else
                {
                    DisplayLabel.Text = "";
                    error = false;
                }
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }
                else if (c is ComboBox)
                {
                    (c as ComboBox).SelectedIndex = -1;
                    (c as ComboBox).Text = "";
                }
                else if (c is CheckBox)
                {
                    (c as CheckBox).Checked = false;
                }
            }
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Item));
                    StreamReader r = new StreamReader(openFileDialog1.FileNames[0]);

                    Item item = (Item)xml.Deserialize(r);

                    cbItemID.Text = item.ItemID.ToString();
                    tbItemName.Text = item.ItemName;
                    tbSpriteID.Text = item.SpriteID.ToString();
                    cbRarity.SelectedIndex = item.Rarity;
                    tbValue.Text = item.Value.ToString();
                    chBStackable.Checked = item.Stackable;
                    chBTradeable.Checked = item.Tradeable;
                    tbDescription.Text = item.Description;

                    r.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei konnte nicht geladen werden!" + Environment.NewLine + ex.Message);
                }
            }
        }
    }
}
