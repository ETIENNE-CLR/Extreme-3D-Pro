using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.DirectInput;

namespace P_Manette
{
    public class Arrows
    {
        private DirectInput directInput;
        private Joystick joystick;
        private Controller controller;
        private int pov;

        public Arrows()
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

            pov = (state.PointOfViewControllers.Length > 0) ? state.PointOfViewControllers[0] : -1;
        }

        // Dessine les flèches directionnelles du D-pad
        public void Draw(SpriteBatch spriteBatch, Texture2D pixel, Rectangle rect)
        {
            float centerX = rect.X + rect.Width / 2f;
            float centerY = rect.Y + rect.Height / 2f;

            int arrowSize = 20;
            int thickness = 6;

            // Haut
            if (pov == 0 || pov == 4500 || pov == 31500)
                spriteBatch.Draw(pixel, new Rectangle((int)(centerX - thickness / 2), (int)(centerY - arrowSize), thickness, arrowSize), Color.Red);

            // Droite
            if (pov == 9000 || pov == 4500 || pov == 13500)
                spriteBatch.Draw(pixel, new Rectangle((int)centerX, (int)(centerY - thickness / 2), arrowSize, thickness), Color.Red);

            // Bas
            if (pov == 18000 || pov == 13500 || pov == 22500)
                spriteBatch.Draw(pixel, new Rectangle((int)(centerX - thickness / 2), (int)centerY, thickness, arrowSize), Color.Red);

            // Gauche
            if (pov == 27000 || pov == 22500 || pov == 31500)
                spriteBatch.Draw(pixel, new Rectangle((int)(centerX - arrowSize), (int)(centerY - thickness / 2), arrowSize, thickness), Color.Red);

            // Cadre
            int border = 2;
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, rect.Width, border), Color.Black); // top
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y + rect.Height - border, rect.Width, border), Color.Black); // bottom
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, border, rect.Height), Color.Black); // left
            spriteBatch.Draw(pixel, new Rectangle(rect.X + rect.Width - border, rect.Y, border, rect.Height), Color.Black); // right
        }
    }
}
