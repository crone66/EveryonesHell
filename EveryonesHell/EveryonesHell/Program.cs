/* 
 * Purpose: Entry point
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */
using System;

namespace EveryonesHell
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            using (Game game = new Game())
            {
                game.Run();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
            {
                ExceptionHandler eHandler = new ExceptionHandler("ErrorMessage");
                if (e.ExceptionObject is Exception)
                    eHandler.HandleException(e.ExceptionObject as Exception);
            }
        }
    }


}
