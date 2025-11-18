using System;
using System.Linq;
using Microsoft.Xna.Framework;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class Controller
    {
        private DirectInput directInput;
        private Joystick joystick;
        private JoystickState joyState;
        public bool buttonPressed;
        public string connectedController;

        public Controller()
        {
            directInput = new DirectInput();
            DetectedController();
        }

        public void Update()
        {
            var state = joystick.GetCurrentState();
            int nb = 11;
            if (PressedButton(nb))
            {
                throw new Exception($"Button {nb} appuié");
            }
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

        public bool PressedButton(int numBtn)
        {
            var state = joystick.GetCurrentState();
            this.buttonPressed = state.Buttons.Length > 0 && state.Buttons[numBtn - 1];
            return buttonPressed;
        }
    }
}
