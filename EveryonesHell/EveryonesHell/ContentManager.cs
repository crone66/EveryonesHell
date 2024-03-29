﻿/* 
 * Purpose: Central storage for loaded content
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
namespace EveryonesHell
{
    /// <summary>
    /// The content manager can be used to store and load external assets
    /// </summary>
    public class ContentManager
    {
        private Dictionary<Type, Dictionary<string, object>> content;

        /// <summary>
        /// Initialize content manager
        /// </summary>
        public ContentManager()
        {
            content = new Dictionary<Type, Dictionary<string, object>>();
        }

        /// <summary>
        /// Returns the asset based on it's type and key.
        /// </summary>
        /// <typeparam name="T">asset type</typeparam>
        /// <param name="key">asset key</param>
        /// <returns>The value or reference of the specified asset</returns>
        public T GetValue<T>(string key)
        {
            Type type = typeof(T);

            if (Exists(type, key))
                return (T)content[type][key];

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Adds a new asset to the content manager
        /// </summary>
        /// <param name="key">Unique asset key</param>
        /// <param name="newContent">asset object</param>
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

        /// <summary>
        /// Loads a new asset from path 
        /// </summary>
        /// <typeparam name="T">asset Type</typeparam>
        /// <param name="path">asset path</param>
        /// <returns>returns a new instance of the asset type</returns>
        public T Load<T>(string path)
        {
            return Load<T>(path, path);
        }

        /// <summary>
        /// Loads a new asset from path 
        /// </summary>
        /// <typeparam name="T">asset Type</typeparam>
        /// <param name="path">asset path</param>
        /// <param name="key">asset key</param>
        /// <returns>returns a new instance of the asset type</returns>
        public T Load<T>(string path, string key)
        {
            if (Exists(typeof(T), key))
                throw new DuplicateKeyException("Key already exists!");

            Type type = typeof(T);
            if (type.GetConstructor(Type.EmptyTypes) != null || type.IsValueType)
            {
                XmlSerializer xml = new XmlSerializer(type);
                using (XmlReader reader = XmlReader.Create(path, new XmlReaderSettings()))
                {
                    if (xml.CanDeserialize(reader))
                    {
                        return (T)LoadXML<T>(reader);
                    }
                }
            }

            try
            {
                object obj = Activator.CreateInstance(typeof(T), path);
                Add(key, obj);
                return (T)obj;
            }
            catch (Exception e)
            {
                GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(e.Message, 255, 0, 0);
                if (Exists(typeof(T), "error"))
                    return (T)content[typeof(T)]["error"];
                else
                    throw e;
            }
        }

        /// <summary>
        /// Loads a new asset from path
        /// </summary>
        /// <typeparam name="T">asset type</typeparam>
        /// <param name="key">unique asset key</param>
        /// <param name="values">parameters for the constructor of the asset type</param>
        /// <returns>returns a new instance of the asset type, initialized with the given values</returns>
        public T Load<T>(string key, params object[] values)
        {
            if (Exists(typeof(T), key))
                throw new DuplicateKeyException("Key already exists!");

            try
            {
                object obj = Activator.CreateInstance(typeof(T), values);
                Add(key, obj);
                return (T)obj;
            }
            catch (Exception e)
            {
                GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(e.Message, 255, 0, 0);
                if (Exists(typeof(T), "error"))
                    return (T)content[typeof(T)]["error"];
                else
                    throw e;
            }
        }

        /// <summary>
        /// Loads a new asset from path
        /// </summary>
        /// <typeparam name="T">asset type</typeparam>
        /// <typeparam name="R">type that is required as parameter to initialize T</typeparam>
        /// <param name="key">unique asset key</param>
        /// <param name="path">path to initialize R</param>
        /// <returns>returns a new instance of the asset type (T), initialized with R</returns>
        public T Load<T, R>(string key, string path)
        {
            if (Exists(typeof(T), key) || Exists(typeof(R), path))
                throw new DuplicateKeyException("Key already exists!");

            try
            {
                object obj = Activator.CreateInstance(typeof(R), path);
                object result = Activator.CreateInstance(typeof(T), obj);
                Add(path, obj);
                Add(key, result);
                return (T)result;
            }
            catch(Exception e)
            {
                if(GlobalReferences.MainGame.ConsoleManager != null)
                    GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(e.Message, 255, 0, 0);

                if (Exists(typeof(T), "error"))
                    return (T)content[typeof(T)]["error"];
                else
                    throw e;
            }
        }

        /// <summary>
        /// Deserializes a xml file
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="path">XML file path</param>
        /// <returns>Returns an Deserialized object</returns>
        public T LoadXML<T>(string path)
        {
            //using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(path, new XmlReaderSettings()))
            {
                object result = LoadXML<T>(reader);
                Add(path, result);
                return (T)result;
            }
        }

        /// <summary>
        /// Deserializes a xml file
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="xmlReader">Stream to the xml file</param>
        /// <returns>Returns an Deserialized object</returns>
        private object LoadXML<T>(XmlReader xmlReader)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            return xml.Deserialize(xmlReader);
        }

        /// <summary>
        /// Checks if a asset type and asset key already exists
        /// </summary>
        /// <param name="t">asset type</param>
        /// <param name="key">unique asset key</param>
        /// <returns>returns true if asset type and asset key exists</returns>
        public bool Exists(Type t, string key)
        {
            return content.ContainsKey(t) && content[t].ContainsKey(key);
        }

    }
}
