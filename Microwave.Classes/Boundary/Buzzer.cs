using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer: IBuzzer
    {
        public void StartBuz()
        {
            Console.WriteLine("*Buzzing!*");
        }

        public void StopBuz()
        {
            throw new System.NotImplementedException();
        }
    }
}