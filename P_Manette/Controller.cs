using System;
using System.Linq;
using Microsoft.Xna.Framework;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class Controller
    {
        private DirectInput directInput;
        public Joystick joystick;
        private JoystickState joyState;
        public string connectedController;

        public Controller()
        {
            directInput = new DirectInput();
            DetectedController();
        }

        public void DetectedController()
        {
            var joystickId = Guid.Empty;

            var devices = directInput
                .GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly)
                .Concat(directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
                .ToList();

            foreach (var device in devices)
            {
                if (device.InstanceName.Contains("3D"))
                {
                    joystickId = device.InstanceGuid;
                    connectedController = device.InstanceName;
                    break;
                }
            }

            if (joystickId != Guid.Empty)
            {
                joystick = new Joystick(directInput, joystickId);
                joystick.Properties.BufferSize = 1024;
                joystick.Acquire();
            }
        }
    }
}