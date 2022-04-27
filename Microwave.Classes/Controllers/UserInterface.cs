﻿using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class UserInterface : IUserInterface
    {
        private enum States
        {
            READY, 
            SETPOWER, 
            SETTIME, 
            COOKING, 
            DOOROPEN
        }
        public int MaxPower { get; set; }

        private States myState = States.READY;

        private ICookController myCooker;
        private ILight myLight;
        private IDisplay myDisplay;

        private IBuzzer mybuzzer;

        private int powerLevel = 50;

        private int time = 1;

        public UserInterface(
            IButton powerButton,
            IButton timeButton,
            IButton startCancelButton,
            IButton addTimeButton,
            IButton subtractTimeButton,
            IDoor door,
            IDisplay display,
            ILight light,
            ICookController cooker, IBuzzer buzzer)
        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            timeButton.Pressed += new EventHandler(OnTimePressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);
            addTimeButton.Pressed += new EventHandler(OnAddTimePressed);
            subtractTimeButton.Pressed += new EventHandler(OnsubtractTimePressed);
            
            //måske en event, hvis vi laver en -10 sekunder.

            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
            mybuzzer = buzzer;
        }

        private void ResetValues()
        {
            MaxPower = 50;
            time = 1;
        }

        public void OnPowerPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myDisplay.ShowPower(MaxPower);
                    myState = States.SETPOWER;
                    break;
                case States.SETPOWER:
                   MaxPower = MaxPower >= 1100 ? 50 : MaxPower+50; // bare ændret 700 om til myCooker.MaxPower
                    myDisplay.ShowPower(MaxPower);
                    break;
            }
        }

        public void OnTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(time, 0);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    time += 1;
                    myDisplay.ShowTime(time, 0);
                    break;

                //case States.COOKING:
                //    myCooker.OffsetTime(10);
                //    break; // der tilføjes 10 sek, hvis den allerede er i gang. 

            }
        }
        public void OnAddTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.COOKING:
                    myCooker.PlusTimer(10);
                    break; // der tilføjes 10 sek, hvis den allerede er i gang. 
            }
        }
        public void OnsubtractTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.COOKING:
                    myCooker.MinusTimer(10);
                    break; 
            }
        }


        public void OnStartCancelPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    ResetValues();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
                case States.SETTIME:
                    myLight.TurnOn();
                    myCooker.StartCooking(MaxPower, time*60);
                    myState = States.COOKING;
                    break;
                case States.COOKING:
                    ResetValues();
                    myCooker.Stop();
                    myLight.TurnOff();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
            }
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myLight.TurnOn();
                    myState = States.DOOROPEN;
                    break;
                case States.SETPOWER:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.SETTIME:
                    ResetValues();
                    myLight.TurnOn();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.COOKING:
                    myCooker.Stop();
                    myDisplay.Clear();
                    ResetValues();
                    myState = States.DOOROPEN;
                    break;
            }
        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.DOOROPEN:
                    myLight.TurnOff();
                    myState = States.READY;
                    break;
            }
        }

        public void CookingIsDone()
        {
            switch (myState)
            {
                case States.COOKING:
                    ResetValues();
                    myDisplay.Clear();
                    myLight.TurnOff();
                    mybuzzer.StartAlarmBuz();
                    
                    myState = States.READY;
                    break;
            }
        }
    }
}