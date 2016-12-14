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
        private uint characterSizeNames, characterSizeInformation;
        private int lineSpacing;
        
        /// <summary>
        /// initialze questtrackerwindow
        /// </summary>
        /// <param name="font">font used to draw the text</param>
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
            text.Color = new Color(250, 250, 250);

            characterSizeNames = 16;
            characterSizeInformation = 13;
            lineSpacing = 12;
        }

        /// <summary>
        /// drawing all the information of the active quests
        /// </summary>
        /// <param name="window">window where everything is drawn</param>
        public void Draw(RenderWindow window)
        {
            window.Draw(background);
            window.Draw(nameBackground);

            DrawQuestnames(window);
            DrawQuests(window);
        }

        /// <summary>
        /// drawing all the questnames
        /// </summary>
        /// <param name="window">window where everything is drawn</param>
        private void DrawQuestnames(RenderWindow window)
        {
            Vector2f textPosition = new Vector2f(position.X + 15, position.Y + 15);

            for (int i = 0; i < questTracker.activeQuests.Count; i++)
            {
                text.Position += new Vector2f(0, (lineSpacing + characterSizeNames) * i);

                text = new Text(questTracker.activeQuests[i].Name, font, characterSizeNames);

                window.Draw(text);
            }
        }

        /// <summary>
        /// drawing questname, questitems, enemies, how many enemies you have to kill and the description
        /// </summary>
        /// <param name="window">window where everything is drawn</param>
        private void DrawQuests(RenderWindow window)
        {
            Vector2f textPosition = new Vector2f(position.X + 265, position.Y);

            for (int i = 0; i < questTracker.activeQuests.Count; i++)
            {
                if (i == 0)
                {
                    text.Position += new Vector2f(0, 15);
                }
                else
                {
                    text.Position += new Vector2f(0, 15 + characterSizeInformation);
                }

                text = new Text(questTracker.activeQuests[i].Name, font, characterSizeNames);
                window.Draw(text);

                if (questTracker.activeQuests[i].Questitem != -1)
                {
                    text.Position += new Vector2f(0, lineSpacing + characterSizeNames);
                    text = new Text(questTracker.activeQuests[i].Questitem.ToString(), font, characterSizeInformation);
                    window.Draw(text);
                }

                if (questTracker.activeQuests[i].Enemy != -1)
                {
                    text.Position += new Vector2f(0, lineSpacing + characterSizeNames);
                    text = new Text(questTracker.activeQuests[i].Enemy.ToString(), font, characterSizeInformation);
                    window.Draw(text);

                    text.Position += new Vector2f(0, lineSpacing + characterSizeInformation);
                    text = new Text(questTracker.activeQuests[i].EnemyCount.ToString(), font, characterSizeInformation);
                    window.Draw(text);
                }

                text.Position += new Vector2f(0, lineSpacing + characterSizeInformation);
                text = new Text(questTracker.activeQuests[i].Description, font, characterSizeInformation);
                window.Draw(text);
            }
        }
    }
}
