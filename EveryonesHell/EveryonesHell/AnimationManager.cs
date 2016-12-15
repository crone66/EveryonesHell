/* 
 * Purpose: to run an animation of a spritesheet
 * Author: Lukas Bosniak
 * Date: 9.11.2016
 */

using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace EveryonesHell
{
    public class AnimationManager
    {
        private Sprite sprite;
        private int countX, countY, width, height, currentFrame;
        private float frameTime, currentanimationTime;
        private List<IntRect> spriteList;

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        /// <summary>
        /// initialize animationmanager
        /// </summary>
        /// <param name="sprite">spritesheet</param>
        /// <param name="spriteCountX">amount of frames x</param>
        /// <param name="spriteCountY">amount of frames y</param>
        /// <param name="frameWidth">width of the frames</param>
        /// <param name="frameHeight">height of the frames</param>
        /// <param name="animationFrameTime">time between two frames</param>
        public AnimationManager(Sprite sprite, int spriteCountX, int spriteCountY, int frameWidth, int frameHeight, float animationFrameTime)
        {
            this.Sprite = sprite;
            countX = spriteCountX;
            countY = spriteCountY;
            width = frameWidth;
            height = frameHeight;
            frameTime = animationFrameTime;
            currentFrame = 0;
            currentanimationTime = 0.0f;
            spriteList = new List<IntRect>();

            LoadList();
        }

        /// <summary>
        /// adding the different rectangles the manager has to render to a list
        /// </summary>
        private void LoadList()
        {
            int positionX = 0;
            int positionY = 0;
            IntRect rect = new IntRect();

            for (int i = 0; i < countY; i++)
            {
                positionY = height * i;

                for (int j = 0; j < countX; j++)
                {
                    positionX = width * j;

                    rect = new IntRect(positionX, positionY, width, height);

                    spriteList.Add(rect);
                }
            }
        }

        /// <summary>
        /// changing between the rectangles in the list
        /// </summary>
        /// <param name="gameTime">elapsed time in the game</param>
        public void Update(Time gameTime)
        {
            currentanimationTime += gameTime.AsMilliseconds();

            if (currentanimationTime >= frameTime)
            {
                currentanimationTime = 0.0f;
                currentFrame++;

                if (currentFrame >= spriteList.Count)
                {
                    currentFrame = 0;
                }
            }

            Sprite.TextureRect = spriteList[currentFrame];
        }

        /// <summary>
        /// drawing the current frame
        /// </summary>
        /// <param name="window">where you draw the frame</param>
        public void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }

        public AnimationManager Clone()
        {
            return new AnimationManager(sprite, countX, countY, width, height, frameTime);
        }
    }
}
