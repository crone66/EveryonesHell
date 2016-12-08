/* 
 * Purpose: window shown during the game, including all the information of the questtracker
 * Author: Lukas Bosniak
 * Date: 7.12.2016
 */

using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace EveryonesHell.HUD
{
    class QuestTrackerWindow
    {
        private RectangleShape background;
        private int width = 650;
        private int height = 400;
        private RectangleShape nameBackground;
        private int nameWidth = 250;
        private Text text;
        private InventorySystem.Inventory inventory = new InventorySystem.Inventory();
        private QuestManagment.QuestTracker questTracker;
        private Font font;
        private Vector2f position = new Vector2f(75, 200);
        
        public QuestTrackerWindow(Font font)
        {
            questTracker = new QuestManagment.QuestTracker(inventory, null);

            background = new RectangleShape(new Vector2f(width, height));
            background.FillColor = new Color(0, 0, 0, 128);
            background.OutlineColor = new Color(0, 0, 0, 200);
            background.Position = position;

            nameBackground = new RectangleShape(new Vector2f(nameWidth, height));
            nameBackground.FillColor = new Color(0, 0, 0, 50);
            nameBackground.OutlineColor = new Color(0, 0, 0, 100);

            this.font = font;
            text = new Text("", font, 16);
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(background);
            window.Draw(nameBackground);

            DrawQuestnames(window);
        }

        public void DrawQuestnames(RenderWindow window)
        {
            Vector2f textPosition = new Vector2f(position.X + 15, position.Y + 15);

            for (int i = 0; i < questTracker.activeQuests.Count; i++)
            {
                text.Position = new Vector2f(textPosition.X, textPosition.Y + 12 * i);

                text = new Text(questTracker.activeQuests[i].Name, font, 16);

                window.Draw(text);
            }
        }
    }
}
