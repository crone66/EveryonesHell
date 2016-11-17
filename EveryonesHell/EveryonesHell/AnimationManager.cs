using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

/* 
 * Purpose: Abspielen von Animationen per Spritesheet
 * Author: Lukas Bosniak
 * Date: 9.11.2016
 */

namespace EveryonesHell
{
    public class AnimationManager
    {
        private Sprite sprite;
        private int countX, countY, width, height, currentFrame;
        private float frameTime, currentanimationTime;
        private List<IntRect> spriteList;

        public AnimationManager(Sprite sprite, int spriteCountX, int spriteCountY, int frameWidth, int frameHeight, float animationFrameTime)
        {
            this.sprite = sprite;
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

            sprite.TextureRect = spriteList[currentFrame];
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(sprite);
        }
    }
}
