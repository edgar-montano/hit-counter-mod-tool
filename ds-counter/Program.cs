using System;
using System.Threading;
using System.IO;
using counter; 

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /* Set parameters, first game executable, base address and path for hitcounter file*/
            if (args.Length>=3)
            {
                string gameName = args[0];
                string filePath = args[1]; //TODO: Rework file path, no longer needed. dump all info to different files. 
                long BaseAddr = long.Parse(args[2],System.Globalization.NumberStyles.HexNumber);
                Console.WriteLine("max:{0} base:{1}",new IntPtr(Int64.MaxValue),BaseAddr);
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
