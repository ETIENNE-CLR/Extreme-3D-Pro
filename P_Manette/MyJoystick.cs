using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class MyJoystick
    {
        private DirectInput directInput;
        private Joystick joystick;
        private Controller controller;

        // Stick principal
        public int AxesX { get; private set; }
        public int AxesY { get; private set; }

        // Stick secondaire
        public int RotationX { get; private set; }
        public int RotationY { get; private set; }

        // AxeZ
        public int RotationZ { get; private set; }

        public MyJoystick() 
        {
            controller = new Controller();
            directInput = new DirectInput();
            joystick = controller.joystick;

        }

        public void Update()
        {
            if (joystick == null) 
                return;

            joystick.Poll();
                
            var state = joystick.GetCurrentState();

            AxesX = state.X;
            AxesY = state.Y;

            RotationX = state.RotationX;
            RotationY = state.RotationY;

            RotationZ = state.RotationZ;
        }

        private void DrawStick(SpriteBatch spriteBatch, Texture2D pixel, Rectangle rect, float normX, float normY, Color color)
        {
            float centerX = rect.X + rect.Width / 2f;
            float centerY = rect.Y + rect.Height / 2f;

            float maxOffsetX = rect.Width / 2f - 5;
            float maxOffsetY = rect.Height / 2f - 5;

            float posX = centerX + normX * maxOffsetX;
            float posY = centerY + normY * maxOffsetY;

            int size = 10;
            Rectangle stickRect = new Rectangle((int)(posX - size / 2), (int)(posY - size / 2), size, size);
            spriteBatch.Draw(pixel, stickRect, color);

            // cadre
            int border = 2;
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, rect.Width, border), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y + rect.Height - border, rect.Width, border), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, border, rect.Height), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(rect.X + rect.Width - border, rect.Y, border, rect.Height), Color.Black);
        }

        // --- Barre pour RotationZ ---
        public void DrawBar(SpriteBatch spriteBatch, Texture2D pixel, Rectangle barRectangle)
        {
            spriteBatch.Draw(pixel, barRectangle, Color.Gray);
            float rot = MathHelper.Clamp(RotationZ / 65535f, 0f, 1f);

            Rectangle fill = new Rectangle(
                barRectangle.X,
                barRectangle.Y,
                (int)(barRectangle.Width * rot),
                barRectangle.Height
            );

            spriteBatch.Draw(pixel, fill, Color.Orange);

            int b = 2;
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y, barRectangle.Width, b), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y + barRectangle.Height - b, barRectangle.Width, b), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y, b, barRectangle.Height), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X + barRectangle.Width - b, barRectangle.Y, b, barRectangle.Height), Color.Black);
        }

        // --- HUD complet : stick gauche, stick droit, barre Z ---
        public void DrawJoystickHUD(SpriteBatch spriteBatch, Texture2D pixel, Rectangle leftStickRect, Rectangle rightStickRect, Rectangle barZRect)
        {
            float normLX = (AxesX - 32767f) / 32767f;
            float normLY = (AxesY - 32767f) / 32767f;
            DrawStick(spriteBatch, pixel, leftStickRect, normLX, normLY, Color.Red);

            float normRX = (RotationX - 32767f) / 32767f;
            float normRY = (RotationY - 32767f) / 32767f;
            DrawStick(spriteBatch, pixel, rightStickRect, normRX, normRY, Color.Blue);

            DrawBar(spriteBatch, pixel, barZRect);
        }
    }
}
