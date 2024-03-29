﻿/* 
 * Purpose: window shown during the game, including all the information of the questtracker
 * Author: Lukas Bosniak
 * Date: 7.12.2016
 */

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EveryonesHell.HUD
{
    /// <summary>
    /// drawing the information of the currently active quests
    /// </summary>
    public class QuestTrackerWindow : HudElement
    {
        private RectangleShape background;
        private RectangleShape nameBackground;
        private int width, height, nameWidth, lineSpacing, displayedQuestCount;
        private Text text;
        private QuestManagment.QuestTracker questTracker;
        private Font font;
        private Vector2f position;
        private uint characterSizeNames, characterSizeInformation;
        private float elapsedTime;
        private const float keyDelay = 0.15f;

        /// <summary>
        /// initialze questtrackerwindow
        /// </summary>
        /// <param name="position">position where the window is drawn on the window</param>
        /// <param name="font">font used to draw the text</param>
        /// <param name="questTracker">questtracker included in the player</param>
        public QuestTrackerWindow(Vector2f position, Font font, QuestManagment.QuestTracker questTracker)
            :base(true, true)
        {
            this.questTracker = questTracker;
            this.position = position;

            width = 1060;
            height = 700;
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
            text = new Text("", font, 30);

            characterSizeNames = 30;
            characterSizeInformation = 26;
            lineSpacing = 24;
            displayedQuestCount = 0;
            elapsedTime = 0;
        }

        /// <summary>
        /// checking the input to choose between the active quests
        /// </summary>
        /// <param name="elapsedSeconds">elapsed time in the game</param>
        public override void Update(float elapsedSeconds)
        {
            elapsedTime += elapsedSeconds;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                IsOpen = false;
            }

            if (displayedQuestCount >= questTracker.activeQuests.Count)
            {
                displayedQuestCount = 0;
            }

            if (elapsedTime >= keyDelay)
            {
                elapsedTime = 0f;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && (displayedQuestCount + 1 < questTracker.activeQuests.Count))
                {
                    displayedQuestCount++;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && (displayedQuestCount > 0))
                {
                    displayedQuestCount--;
                }
            }
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

                if (i == displayedQuestCount)
                {
                    text.Color =  Color.Yellow;
                }

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
            if (questTracker.activeQuests.Count > 0)
            {
                Vector2f textPosition = new Vector2f(position.X + nameWidth + 15, position.Y + 15);

                text = new Text(questTracker.activeQuests[displayedQuestCount].Name, font, characterSizeNames);
                text.Position = new Vector2f(textPosition.X, textPosition.Y);
                window.Draw(text);
                textPosition.Y += characterSizeNames + lineSpacing;

                if (questTracker.activeQuests[displayedQuestCount].Questitem != -1)
                {
                    text = new Text("Questitem: " + questTracker.activeQuests[displayedQuestCount].Questitem.ToString(), font, characterSizeInformation);
                    text.Position = new Vector2f(textPosition.X, textPosition.Y);
                    window.Draw(text);
                    textPosition.Y += characterSizeInformation + lineSpacing;
                }

                if (questTracker.activeQuests[displayedQuestCount].Enemy != -1)
                {
                    text = new Text("Gegner-ID:   " + questTracker.activeQuests[displayedQuestCount].Enemy.ToString(), font, characterSizeInformation);
                    text.Position = new Vector2f(textPosition.X, textPosition.Y);
                    window.Draw(text);
                    textPosition.Y += characterSizeInformation + lineSpacing;

                    text = new Text("Anzahl an zu tötenden Gegnern:   " + questTracker.activeQuests[displayedQuestCount].EnemyCount.ToString(), font, characterSizeInformation);
                    text.Position = new Vector2f(textPosition.X, textPosition.Y);
                    window.Draw(text);
                    textPosition.Y += characterSizeInformation + lineSpacing;
                }

                text = new Text("Questbeschreibung:   " + questTracker.activeQuests[displayedQuestCount].Description, font, characterSizeInformation);
                text.Position = new Vector2f(textPosition.X, textPosition.Y);
                window.Draw(text);
            }
        }
    }
}
