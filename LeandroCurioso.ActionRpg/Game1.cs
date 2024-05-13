using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeandroCurioso.ActionRpg
{
    enum Dir {
        Down, Up, Left, Right
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Camera camera;

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
            this.camera = new Camera(_graphics.GraphicsDevice);

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

            player.animations[(int)Dir.Down] = new SpriteAnimation(walkDown, 4, 8);
            player.animations[(int)Dir.Up] = new SpriteAnimation(walkUp, 4, 8);
            player.animations[(int)Dir.Left] = new SpriteAnimation(walkLeft, 4, 8);
            player.animations[(int)Dir.Right] = new SpriteAnimation(walkRight, 4, 8);

            player.anim = player.animations[0];

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            if (!player.Dead)
            {
                Controller.Update(gameTime, skull);
            }

            camera.Position = player.Position;
            camera.Update(gameTime);

            foreach(var projectile in Projectile.projectiles) {
                projectile.Update(gameTime);
            }

            foreach(var e in Enemy.enemies) {
                e.Update(gameTime, player.Position, player.Dead);
                int sum = 32 + e.radius;
                if (Vector2.Distance(player.Position, e.Position) < sum)
                {
                    player.Dead = true;
                }
            }

            foreach (var projectile in Projectile.projectiles)
            {
                foreach (var e in Enemy.enemies)
                {
                    int sum = projectile.radius + e.radius;
                    if (Vector2.Distance(projectile.Position, e.Position) < sum)
                    {
                        projectile.Collided = true;
                        e.Dead = true;
                    }
                }
            }

            Projectile.projectiles.RemoveAll(p => p.Collided);
            Enemy.enemies.RemoveAll(p => p.Dead);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(camera);
            _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);
            foreach(var e in Enemy.enemies) {
                e.anim.Draw(_spriteBatch);
            }
           foreach(var projectile in Projectile.projectiles) {
                _spriteBatch.Draw(ball, new Vector2(projectile.Position.X - 48, projectile.Position.Y - 48), Color.White);
            }
           if (!player.Dead)
            {
                player.anim.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
