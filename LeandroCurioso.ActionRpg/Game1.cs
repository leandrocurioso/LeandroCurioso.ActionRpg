using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeandroCurioso.ActionRpg
{
    enum Dir {
        Up, Down, Left, Right
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D playerSprite;
        Texture2D walkDown;
        Texture2D walkUp;
        Texture2D walkLeft;
        Texture2D walkRight;
        Texture2D background;
        Texture2D skull;
        Texture2D ball;

        Player player = new Player();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = Content.Load<Texture2D>("player/player");
            walkDown = Content.Load<Texture2D>("player/walkDown");
            walkUp = Content.Load<Texture2D>("player/walkUp");
            walkLeft = Content.Load<Texture2D>("player/walkLeft");
            walkRight = Content.Load<Texture2D>("player/walkRight");
            background = Content.Load<Texture2D>("background");
            ball = Content.Load<Texture2D>("ball");
            skull = Content.Load<Texture2D>("skull");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);
            _spriteBatch.Draw(playerSprite, player.Position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
