using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class Curseur
    {
        private DirectInput directInput;
        private Joystick joystick;
        private Controller controller;
        private int throttle;

        public Curseur() 
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

            throttle = state.Z;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel, Rectangle rect)
        {
            if (joystick == null) return;

            // Normalisation entre 0 et 1
            float norm = throttle / 65535f;

            // Barre de fond
            spriteBatch.Draw(pixel, rect, Color.Gray);

            // Portion remplie (remonte du bas vers le haut)
            Rectangle fill = new Rectangle(
                rect.X,
                rect.Y + (int)((1 - norm) * rect.Height), // inversé pour que bas = 0
                rect.Width,
                (int)(norm * rect.Height)
            );
            spriteBatch.Draw(pixel, fill, Color.Orange);

            // Cadre
            int border = 2;
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, rect.Width, border), Color.Black); // top
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y + rect.Height - border, rect.Width, border), Color.Black); // bottom
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, border, rect.Height), Color.Black); // left
            spriteBatch.Draw(pixel, new Rectangle(rect.X + rect.Width - border, rect.Y, border, rect.Height), Color.Black); // right
        }
    }
}
