using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();
            Button AddTimeButton = new Button();
            Button subtractTimeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, AddTimeButton, subtractTimeButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

         

            do
            {
                string input;
                 System.Console.WriteLine("Press a to add 10 sec");
                System.Console.WriteLine("Press s to subtract 10 sec");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'a':
                        AddTimeButton.Press();
                        break;

                    case 'A':
                        AddTimeButton.Press();
                        break;
                    case 's':
                        subtractTimeButton.Press();
                        break;
                    case 'S':
                        subtractTimeButton.Press();
                        break;
                    case 'E':
                        break;

                }
            } while (true);

            // The simple sequence should now run

            //System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input
        
        }
    }
}
