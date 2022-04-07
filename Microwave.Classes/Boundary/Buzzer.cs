using System;
using System.Linq;
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

        
        public void StartAlarmBuz()
        {
            if (!isOn)
            { 
                myOutput.OutputLine("Buz Buz Buz");

                isOn = true;
                    
                
            }
        }

        
    }
}