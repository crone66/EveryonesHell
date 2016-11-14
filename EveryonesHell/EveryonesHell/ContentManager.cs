/* 
 * Purpose: Central storage for loaded content
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace EveryonesHell
{
    public class ContentManager
    {
        private Dictionary<Type, Dictionary<string, object>> content;

        public ContentManager()
        {
            content = new Dictionary<Type, Dictionary<string, object>>();
        }

        public Sprite CreateSprite(string path)
        {
            Texture texture = GetValue<Texture>(path);
            return new Sprite(texture);
        }

        public Sound CreateSound(string path)
        {
            SoundBuffer sound = GetValue<SoundBuffer>(path);
            return new Sound(sound);
        }

        private T GetValue<T>(string path) where T : class
        {
            Type type = typeof(T);
            
            if (content.ContainsKey(type))
            {
                if (content[type].ContainsKey(path))
                {
                    return content[type][path] as T;
                }
            }
            throw new KeyNotFoundException();
        }

        public void Add(string key, object newContent)
        {
            Type type = newContent.GetType();
            if (!content.ContainsKey(type))
            {
                content.Add(type, new Dictionary<string, object>());
            }

            if (!content[type].ContainsKey(key))
            {
                content[type].Add(key, newContent);
            }
            else
            {
                throw new DuplicateKeyException("Key already exists!");
            }
        }
    }
}
