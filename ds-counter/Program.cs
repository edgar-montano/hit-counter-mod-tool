using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;

namespace ds_counter
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
            if(args.Length==3)
            {
                gameName = args[0];
                filePath = args[1];
                BaseAddr = int.Parse(args[2],System.Globalization.NumberStyles.HexNumber);
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

            /* Load VAM.dll and hook into game process, load health value from static pointer */
            VAMemory vam = new VAMemory(gameName);
            int currHealth = vam.ReadInt32((IntPtr)BaseAddr);
            if (currHealth <= 0)
            {
                Console.WriteLine(@"Error has occured, health value is 0.\n
                    Please make sure Base Address is a proper static pointer to health address.");
                return; 
            }

            int prevHealth = currHealth;
            int totalDamageTaken = 0; 
            Console.WriteLine("Health address located current value:" + currHealth);
        
            while (true)
            {
                // read current health from static pointer
                currHealth = vam.ReadInt32((IntPtr)BaseAddr);
                totalDamageTaken = prevHealth - currHealth;
                // determine if a hit has been taken, if so write output to file
                // TODO: if (currHealth < prevHealth) && (prevHealth-currHealth>=damageOffset)
                if (currHealth < prevHealth)
                {
                    
                    Console.WriteLine("Damage taken: " + totalDamageTaken);
                    Console.WriteLine("Hit taken! HitCouter:"+ ++hitCounter);
                    using (StreamWriter outputFile = new StreamWriter(filePath))
                    {
                        outputFile.WriteLine(hitCounter);
                    }
                }
                else if (currHealth > prevHealth)
                {
                    Console.WriteLine("Health restored!");
                }
                prevHealth = currHealth;
                // reduce cpu usage by sleeping every 5s
                Thread.Sleep(5000);
            }
        }
    }
}
