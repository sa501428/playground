using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuroraGame
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (AuroraGame game = new AuroraGame())
                game.Run();
        }
    }
#endif
}
