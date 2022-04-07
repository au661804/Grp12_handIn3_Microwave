using System;
using Microwave.Classes.Interfaces;
using System.Timers;

namespace Microwave.Classes.Boundary
{
    public class Buzzer: IBuzzer
    {
        private IOutput myOutput;
        private bool isOn = false;
        

        public Buzzer(IOutput output)
        {
            myOutput = output;
        }

        
        public void StartBuz(int numOfbuz)
        {
            TimeSpan k = TimeSpan.FromMilliseconds(1000); 
            if (!isOn)
            {
                for (int i = 1; i < numOfbuz;)
                { 
                    
                    myOutput.OutputLine("Buz");
                    k.Milliseconds = 1000;
                    isOn = true;
                    
                    
                }
                
            }
        }

        
    }
}