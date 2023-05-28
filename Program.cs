using System;
using EggCollector;

namespace EggCollector
{
    static class Program
    {
        static void Main()
        {
            using (var game = new EggCollector())
            {
                game.Run();
            }
        }
    }
}
