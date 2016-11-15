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
    public partial class ProjectScreen : Form
    {
        public ProjectScreen()
        {
            InitializeComponent();
        }

        private void ProjectScreen_Load(object sender, EventArgs e)
        {
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
    }
}
