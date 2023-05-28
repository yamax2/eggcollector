using System;
using EggCollector;

namespace EggCollector
{
    static class Program
    {
        static void Main()
        {
            using (var game = new EggCollectorGame())
            {
                game.Run();
            }
        }
    }
}
