using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace counter
{
    class HitCounter
    {
        private  long healthAddr = 0;
        private  int hitCounter = 0;
        private  string filePath = "";
        private  string gameName = "";
        private  int damageOffset = 1;
        private bool verbose = false;
        private int sleepTimer = 1000; 

        public HitCounter(string game_name, string file_path, long addr) 
        {
            this.gameName = game_name;
            this.filePath = file_path;
            this.healthAddr = addr;
            Console.WriteLine("{0} was opened at address: {1} and file {2} will be created.", game_name, addr, file_path);
        }

        /*
         * Set the default timer for each cycle to sleep for, default is 1000ms (1s). 
         */
        public void setTimer(int timer)
        {
            this.sleepTimer = timer; 
        }

        /*
         * Set damageOffset value explicitly. This is used to determine the threshold
         * of damage taken before registering a hit event. 
         */
        public void setDamageOffset(int offset)
        {
            this.damageOffset = offset; 
        }

        /*
         * Set verbose flag on to print debug statements. 
         */
        public void setVerbose(bool mverbose)
        {
            this.verbose = mverbose;  
        }
        /*
         * Verbose mode is for debug purposes only.
         * It is not advise to use this for actual speedruns since it will incur extra 
         * overhead and wasted cpu cycles for the IO operations.
         */
        private void displayVerbose(long curr, long dmg)
        {
            Console.WriteLine("Health:{0} |\tDamage:{1} |\tHit Counter: {2} |", curr, dmg, this.hitCounter);
            Console.WriteLine("================================================");
        }

        public void hookGame()
        {
            //Load VAM.dll and hook into game process, load health value from static pointer
            VAMemory vam = new VAMemory(this.gameName);
            //initialize variables used in main event loop.
            long damageTaken, prevHealth, currHealth;
            
            //begin monitoring address value 
            currHealth = vam.ReadInteger((IntPtr)this.healthAddr);

            // main logic loop
            while (true)
            {
                // calculate damage taken for this cycle 
                prevHealth = currHealth;
                currHealth = vam.ReadInteger((IntPtr)this.healthAddr);

                //set damageTaken to 0 otherwise damageTaken will be a negative value when healed. 
                damageTaken = ((prevHealth - currHealth) > 0) ? (prevHealth - currHealth) : 0; 

                // register hit if value has decreased passed our threshold 
                if(damageTaken >= this.damageOffset)
                {
                    Console.WriteLine("Hit taken! Hit counter: {0} ", ++this.hitCounter);
                    using (StreamWriter outputFile = new StreamWriter(this.filePath))
                    {
                        outputFile.WriteLine(this.hitCounter);
                    }
                }

                // setVerbose() must be called to enable this verbose flag
                if (this.verbose)
                {
                    displayVerbose(currHealth, damageTaken);
                }
                
                Thread.Sleep(this.sleepTimer);

            }
        }

    }
}
