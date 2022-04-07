using System;
using Microwave.Classes.Interfaces;

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
            if (!isOn)
            {
                for (int i = 1; i < numOfbuz;)
                { 
                    myOutput.OutputLine("Buz");
                    isOn = true;
                    TimeSpan.FromMilliseconds(1000);

                }
                
            }
        }

        
    }
}