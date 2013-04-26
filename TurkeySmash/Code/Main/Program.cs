using System;

namespace TurkeySmash
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TurkeySmashGame game = new TurkeySmashGame())
            {
                game.Run();
            }
        }
    }
#endif
}

