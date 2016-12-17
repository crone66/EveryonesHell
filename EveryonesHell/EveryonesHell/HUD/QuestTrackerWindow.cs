﻿/* 
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
    public class QuestTrackerWindow : HudElement
    {
        private RectangleShape background;
        private RectangleShape nameBackground;
        private int width , height, nameWidth, lineSpacing;
        private Text text;
        private QuestManagment.QuestTracker questTracker;
        private Font font;
        private Vector2f position;
        private uint characterSizeNames, characterSizeInformation;
        
        /// <summary>
        /// initialze questtrackerwindow
        /// </summary>
        /// <param name="font">font used to draw the text</param>
        public QuestTrackerWindow(Vector2f position, Font font, QuestManagment.QuestTracker questTracker)
            :base(true, true)
        {
            this.questTracker = questTracker;
            this.position = position;

            width = 650;
            height = 400;
            nameWidth = 250;

            background = new RectangleShape(new Vector2f(width, height));
            background.FillColor = new SFML.Graphics.Color(0, 0, 0, 128);
            background.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 200);
            background.OutlineThickness = 2;
            background.Position = position;

            nameBackground = new RectangleShape(new Vector2f(nameWidth, height));
            nameBackground.FillColor = new SFML.Graphics.Color(0, 0, 0, 50);
            nameBackground.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 100);
            nameBackground.OutlineThickness = 2;
            nameBackground.Position = position;


            this.font = font;
            text = new Text("", font, 16);
            text.Color = new SFML.Graphics.Color(250, 250, 250);

            characterSizeNames = 16;
            characterSizeInformation = 13;
            lineSpacing = 12;
        }

        public override void Update(float elapsedSeconds)
        {
        }

        /// <summary>
        /// drawing all the information of the active quests
        /// </summary>
        /// <param name="window">window where everything is drawn</param>
        public override void Draw(RenderWindow window)
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
                text = new Text(questTracker.activeQuests[i].Name, font, characterSizeNames);

                text.Position = new Vector2f(textPosition.X, textPosition.Y + ((lineSpacing + characterSizeNames) * i));

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
                text = new Text(questTracker.activeQuests[i].Name, font, characterSizeNames);
                if (i == 0)
                {
                    text.Position += new Vector2f(0, 15);
                }
                else
                {
                    text.Position += new Vector2f(0, 15 + characterSizeInformation);
                }
                window.Draw(text);

                if (questTracker.activeQuests[i].Questitem != -1)
                {
                    text = new Text(questTracker.activeQuests[i].Questitem.ToString(), font, characterSizeInformation);
                    text.Position += new Vector2f(0, lineSpacing + characterSizeNames);
                    window.Draw(text);
                }

                if (questTracker.activeQuests[i].Enemy != -1)
                {
                    text = new Text(questTracker.activeQuests[i].Enemy.ToString(), font, characterSizeInformation);
                    text.Position += new Vector2f(0, lineSpacing + characterSizeNames);
                    window.Draw(text);

                    text = new Text(questTracker.activeQuests[i].EnemyCount.ToString(), font, characterSizeInformation);
                    text.Position += new Vector2f(0, lineSpacing + characterSizeInformation);
                    window.Draw(text);
                }

                text = new Text(questTracker.activeQuests[i].Description, font, characterSizeInformation);
                text.Position += new Vector2f(0, lineSpacing + characterSizeInformation);
                window.Draw(text);
            }
        }
    }
}
