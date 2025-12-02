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
        public int RotationZ { get; private set; }
        public int AxesX { get; private set; }
        public int AxesY { get; private set; }


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

            RotationZ = state.RotationZ;
            AxesX = state.X;
            AxesY = state.Y;
        }

        public void DrawAxes(SpriteBatch spriteBatch, Texture2D pixel, Rectangle barRectangle)
        {

            float normX = (AxesX - 32767f) / 32767f;
            float normY = (AxesY - 32767f) / 32767f;

            // Centre de la zone
            float centerX = barRectangle.X + barRectangle.Width / 2f;
            float centerY = barRectangle.Y + barRectangle.Height / 2f;

            // Déplacement en fonction des axes
            float maxOffsetX = barRectangle.Width / 2f - 5;
            float maxOffsetY = barRectangle.Height / 2f - 5;

            float posX = centerX + normX * maxOffsetX;
            float posY = centerY + normY * maxOffsetY;

            // carré représentant le stick
            int size = 10;
            Rectangle stickRect = new Rectangle((int)(posX - size / 2), (int)(posY - size / 2), size, size);
            spriteBatch.Draw(pixel, stickRect, Color.Red);

            // cadre autour de la zone
            int border = 2;
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y, barRectangle.Width, border), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y + barRectangle.Height - border, barRectangle.Width, border), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y, border, barRectangle.Height), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X + barRectangle.Width - border, barRectangle.Y, border, barRectangle.Height), Color.Black);
        }

        public void DrawBar(SpriteBatch spriteBatch, Texture2D pixel, Rectangle barRectangle)
        {
            // Fond gris
            spriteBatch.Draw(pixel, barRectangle, Color.Gray);

            // Normalisation
            float rot = MathHelper.Clamp(RotationZ / 65535f, 0f, 1f);

            // Portion remplie
            Rectangle fill = new Rectangle(
                barRectangle.X,
                barRectangle.Y,
                (int)(barRectangle.Width * rot),
                barRectangle.Height
            );

            spriteBatch.Draw(pixel, fill, Color.Orange);

            // Cadre
            int b = 2;
            // top
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y, barRectangle.Width, b), Color.Black);
            // bottom
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y + barRectangle.Height - b, barRectangle.Width, b), Color.Black);
            // left
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X, barRectangle.Y, b, barRectangle.Height), Color.Black);
            // right
            spriteBatch.Draw(pixel, new Rectangle(barRectangle.X + barRectangle.Width - b, barRectangle.Y, b, barRectangle.Height), Color.Black);
        }
    }
}
