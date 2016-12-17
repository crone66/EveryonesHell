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
        private int countX, countY, width, height, currentFrame, columnFrame;
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

        public IntRect SpriteRect
        {
            get
            {
                if (spriteList.Count > 0)
                    return spriteList[currentFrame];
                else
                    return sprite.TextureRect;
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
        /// <param name="elapsedSeconds">elapsed time in the game</param>
        public void Update(float elapsedSeconds, Vector2f velocity)
        {
            if (velocity != new Vector2f(0,0))
            {
                currentanimationTime += elapsedSeconds;

                if (currentanimationTime >= frameTime)
                {
                    currentanimationTime = 0.0f;
                    columnFrame++;

                    if (columnFrame >= countX)
                    {
                        columnFrame = 0;
                    }
                }
            }

            if (velocity.X > 0)
            {
                currentFrame = (countX * 2) + columnFrame;
            }
            if (velocity.X < 0)
            {
                currentFrame = countX + columnFrame;
            }
            if (velocity.Y > 0)
            {
                currentFrame = columnFrame;
            }
            if (velocity.Y < 0)
            {
                currentFrame = (countX * 3) + columnFrame;
            }
        }

        public AnimationManager Clone()
        {
            return new AnimationManager(sprite, countX, countY, width, height, frameTime);
        }
    }
}
