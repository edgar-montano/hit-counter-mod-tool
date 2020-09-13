using System;
using counter; 

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /* Set parameters, first game executable, base address */
            if (args.Length>=2)
            {
                string gameName = args[0];
                long addr = long.Parse(args[1],System.Globalization.NumberStyles.HexNumber);
                HitCounter hc = new HitCounter(gameName, addr); 
                if(args.Length==3 && args[2]=="-v") hc.setVerbose(true);
                hc.hookGame(); 
            }
            /* Set default parameters if no command line arguments specified */
            else
            {
                Console.WriteLine(@"No arguments specified.
                    Usage:   ./ds-counter.exe <game-name> <health-address>
                    Example: ./ds-counter.exe DarkSoulsRemastered  0dcbd0f");
                return;
            }
        }
    }
}
