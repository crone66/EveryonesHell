using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Text;

namespace EveryonesHell
{
    public static class UIHelper
    {
        /// <summary>
        /// Formats console output and creates new lines if the given line is to long
        /// </summary>
        /// <param name="baseLine">unformated line</param>
        /// <returns>returns an array of lines</returns>
        public static Text[] LineWrap(Text baseLine, int width)
        {
            string text = baseLine.DisplayedString;

            if (baseLine.GetGlobalBounds().Width <= width)
                return new Text[1] { baseLine };

            Text currentLine = new Text(baseLine);
            List<Text> lines = new List<Text>();

            StringBuilder sb = new StringBuilder();
            string currentText = text;
            while (currentLine.GetGlobalBounds().Width > width)
            {
                string lastWord = GetLastWord(currentText);
                if (lastWord == currentText)
                {
                    lastWord = currentText.Substring(currentText.Length - 10);
                    currentText = currentText.Substring(0, currentText.Length - 10);
                }
                else
                    currentText = RemoveLastWord(currentText, lastWord);

                if (lastWord.Length > 0)
                {
                    sb.Insert(0, " ");
                    sb.Insert(0, lastWord);
                }

                currentLine.DisplayedString = currentText;
                if (currentLine.GetGlobalBounds().Width <= width)
                {
                    lines.Add(currentLine);
                    if (sb.Length > 0)
                    {
                        currentLine = new Text(baseLine);
                        currentLine.DisplayedString = sb.ToString().Trim();
                        currentText = sb.ToString().Trim();
                        sb.Clear();
                        if (currentLine.GetGlobalBounds().Width <= width)
                        {
                            lines.Add(currentLine);
                            break;
                        }
                    }
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Removes the last word of a given line or if it's just one word, it removes the last 10 characters.
        /// </summary>
        /// <param name="text">text to remove from</param>
        /// <param name="word">word to remove</param>
        /// <returns>returns changed text</returns>
        private static string RemoveLastWord(string text, string word)
        {
            if (text.Length > word.Length)
                return text.Substring(0, text.Length - (word.Length + 1));
            else
                return text.Substring(0, text.Length - 10);
        }

        /// <summary>
        /// The Function searchs for the last word in a given string
        /// </summary>
        /// <param name="text">text to search in</param>
        /// <returns>returns last word</returns>
        private static string GetLastWord(string text)
        {
            string[] words = text.Split(' ');
            if (words != null && words.Length > 0)
            {
                if (words[words.Length - 1].Length > 0)
                    return words[words.Length - 1];
                else
                    return " ";
            }

            return null;
        }

        /// <summary>
        /// Calculates a vector scale from a source and destination size
        /// </summary>
        /// <param name="sourceSize">Source size</param>
        /// <param name="distinationSize">destination size</param>
        /// <returns>Returns a scale vector</returns>
        public static Vector2f GetScale(IntRect sourceSize, IntRect distinationSize)
        {
            return new Vector2f(distinationSize.Width / (float)sourceSize.Width, distinationSize.Height / (float)sourceSize.Height);
        }
    }
}
