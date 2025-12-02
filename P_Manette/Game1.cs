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
        private Boutton boutton; 
        private SpriteFont font;
        private Texture2D pixel;
        private Rectangle rotationZBar;
        private Rectangle axeXYrectangle;

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
            boutton = new Boutton();
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            rotationZBar = new Rectangle(30, 30, 300, 20);
            axeXYrectangle = new Rectangle(100, 100, 200, 200);
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            joystick.DrawBar(_spriteBatch, pixel, rotationZBar);
            joystick.DrawAxes(_spriteBatch, pixel, axeXYrectangle);
            boutton.DrawPressedButtons(_spriteBatch, pixel, font);
            _spriteBatch.DrawString(font, (contoller.connectedController != null) ? "Manette detectee" : "Manette non detectee", new Vector2(100, 100), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
