using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace P_Manette
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Controller contoller;
        private MyJoystick joystick;
        private Arrows fleches;
        private Boutton boutton; 
        private Curseur curseur; 
        private SpriteFont font;
        private Texture2D pixel;
        private Rectangle rotationZBar;
        Rectangle rightStickRect;
        Rectangle leftStickRect;
        Rectangle arrowsRect;
        Rectangle curseurRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            contoller = new Controller();
            joystick = new MyJoystick();
            fleches = new Arrows();
            curseur = new Curseur();
            boutton = new Boutton();
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            rotationZBar = new Rectangle(30, 30, 300, 20);
            leftStickRect = new Rectangle(100, 100, 200, 200);
            rightStickRect = new Rectangle(350, 100, 200, 200);
            arrowsRect = new Rectangle(600, 100, 100, 100);
            curseurRect = new Rectangle(50, 50, 20, 200);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            joystick.Update();
            boutton.Update();
            fleches.Update();
            curseur.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Dessiner tout le HUD du joystick
            joystick.DrawJoystickHUD(_spriteBatch, pixel, leftStickRect, rightStickRect, rotationZBar);

            // Afficher les boutons pressés
            boutton.DrawPressedButtons(_spriteBatch, pixel, font);

            fleches.Draw(_spriteBatch, pixel, arrowsRect);
            curseur.Draw(_spriteBatch, pixel, curseurRect);

            // Afficher l'état de la manette
            _spriteBatch.DrawString(
                font,
                (contoller.connectedController != null) ? "Manette detectee" : "Manette non detectee",
                new Vector2(100, 50),
                Color.White
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
