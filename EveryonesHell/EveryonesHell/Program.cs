/* 
 * Purpose: Entry point
 * Author: Marcel Croonenbroeck
 * Date: 04.11.2016
 */
namespace EveryonesHell
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}
