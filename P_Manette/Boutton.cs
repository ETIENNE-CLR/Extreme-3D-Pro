using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;

namespace P_Manette
{
    public class Boutton
    {
        private DirectInput directInput;
        private Joystick joystick;
        private Controller controller;
        public List<int> buttonsPressed;

        public Boutton()
        {
            controller = new Controller();
            directInput = new DirectInput();
            joystick = controller.joystick;
            buttonsPressed = new List<int>();
        }

        public void Update()
        {
            if (joystick == null)
                return;

            joystick.Poll();
            var state = joystick.GetCurrentState();

            buttonsPressed.Clear();
            bool[] buttons = state.Buttons;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i])
                    buttonsPressed.Add(i + 1);
            }
        }

        public void DrawPressedButtons(SpriteBatch spriteBatch, Texture2D pixel, SpriteFont font)
        {
            if (buttonsPressed.Count == 0) return;

            int startX = 50;
            int startY = 50;
            int width = 40;
            int height = 40;
            int spacing = 10;

            for (int i = 0; i < buttonsPressed.Count; i++)
            {
                int x = startX + i * (width + spacing);
                int y = startY;

                spriteBatch.Draw(pixel, new Microsoft.Xna.Framework.Rectangle(x, y, width, height), Microsoft.Xna.Framework.Color.OrangeRed);

                string text = buttonsPressed[i].ToString();
                var textSize = font.MeasureString(text);
                float textX = x + (width - textSize.X) / 2;
                float textY = y + (height - textSize.Y) / 2;

                spriteBatch.DrawString(font, text, new Microsoft.Xna.Framework.Vector2(textX, textY), Microsoft.Xna.Framework.Color.White);
            }
        }


    }
}
