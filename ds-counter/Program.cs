using System;
using System.Threading;
using System.IO;
using counter; 

namespace program
{
    class Program
    {
        public static int BaseAddr = 0;
        public static int currHealth = 0;
        public static int hitCounter = 0;
        public static string filePath = "";
        public static string gameName = "";
        public static int damageOffset = 1; 

        static void Main(string[] args)
        {
           /* Set parameters, first game executable, base address and path for hitcounter file*/
            if(args.Length>=3)
            {
                gameName = args[0];
                filePath = args[1];
                BaseAddr = int.Parse(args[2],System.Globalization.NumberStyles.HexNumber);
                HitCounter hc = new HitCounter(gameName, filePath, BaseAddr);
                hc.setVerbose(true);
                hc.hookGame(); 
                Console.Read();
                Console.WriteLine("Command line arguments specified: \n"+ 
                    "gameName:" + gameName + "\nBaseAddr:" + BaseAddr + "\nfilePath:" + filePath);
            }
            /* Set default parameters if no command line arguments specified */
            else
            {
                Console.WriteLine(@"No arguments specified.
                    Usage:   ./counter.exe <game-name> <file-path> <base-addr>
                    Example: ./counter.exe DarkSoulsRemastered ./hitCounter.txt 0dcbd0f");
                return;
            }
        }
    }
}
