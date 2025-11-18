using System;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class Controller
    {
        private Joystick joystick;
        private DirectInput directInput;

        public bool buttonPressed;
        private bool connectedController;

        public Controller()
        {
            directInput = new DirectInput();
            DetectedController();
        }

        public void Update()
        {
            
        }

        private bool DetectedController()
        {
            var joystickGuid = Guid.Empty;

            foreach(var device in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly))
            {
                if (device.InstanceName.Contains("Extreme 3D"))
                {
                    joystickGuid = device.InstanceGuid;
                    break;
                }
            }
        }

        public bool PressedButton()
        {

        }
    }
}
