using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class Controller
    {
        private GamePadState currentState;
        private GamePadState previousState;

        public bool buttonPressed;

        public Controller()
        {

        }

        public void Update()
        {
            previousState = currentState;

            currentState = GamePad.GetState(PlayerIndex.One);
            if (!currentState.IsConnected)
            {
                return;
            }

            if (currentState.IsButtonDown(Buttons.A))
            {
                buttonPressed = true;
            }
        }
    }
}
