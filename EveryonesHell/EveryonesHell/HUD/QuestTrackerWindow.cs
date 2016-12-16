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
using DebugConsole;

namespace EveryonesHell.HUD
{
    class QuestTrackerWindow
    {
        private RectangleShape background;
        private RectangleShape nameBackground;
        private int width, height, nameWidth, lineSpacing;
        private Text text;
        private InventorySystem.Inventory inventory = new InventorySystem.Inventory();
        private QuestManagment.QuestTracker questTracker;
        private Font font;
        private Vector2f position;
        private uint characterSizeNames, characterSizeInformation;
        private bool isVisable;

        /// <summary>
        /// initialze questtrackerwindow
        /// </summary>
        /// <param name="font">font used to draw the text</param>
        public QuestTrackerWindow(Font font)
        {
            questTracker = new QuestManagment.QuestTracker(inventory, null);

            position = new Vector2f(-33000, -17000);

            background = new RectangleShape(new Vector2f(width, height));
            background.FillColor = new SFML.Graphics.Color(80, 80, 80, 128);
            background.OutlineColor = new SFML.Graphics.Color(80, 80, 80, 200);
            background.OutlineThickness = 2;
            background.Position = position;

            nameBackground = new RectangleShape(new Vector2f(nameWidth, height));
            nameBackground.FillColor = new SFML.Graphics.Color(80, 80, 80, 50);
            nameBackground.OutlineColor = new SFML.Graphics.Color(80, 80, 80, 100);
            nameBackground.OutlineThickness = 2;
            nameBackground.Position = position;

            width = 650;
            height = 400;
            nameWidth = 250;

            this.font = font;
            text = new Text("", font, 16);
            text.Color = new SFML.Graphics.Color(250, 250, 250);

            characterSizeNames = 16;
            characterSizeInformation = 13;
            lineSpacing = 12;

            isVisable = false;
        }

        public void UpdatePosition(EntityManagment.Player player)
        {
            position = player.Position - new Vector2f(300, 275);

            background.Position = position;
            nameBackground.Position = position;
        }

        /// <summary>
        /// drawing all the information of the active quests
        /// </summary>
        /// <param name="window">window where everything is drawn</param>
        public void Draw(RenderWindow window)
        {
            if (isVisable)
            {
                window.Draw(background);
                window.Draw(nameBackground);

                DrawQuestnames(window);
                DrawQuests(window);
            }
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

        public void OnQuestWindow(object sender, ExecuteCommandArgs e)
        {
            isVisable = !isVisable;
        }
    }
}
