/* 
 * Purpose: Custom exception for dictionaries (added key already exists)
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */

using System;

namespace EveryonesHell
{
    public class DuplicateKeyException : Exception
    {
        /// <summary>
        /// Duplicate key exception
        /// </summary>
        /// <param name="message">Error message</param>
        public DuplicateKeyException(string message)
            :base(message)
        {

        }
    }
}
